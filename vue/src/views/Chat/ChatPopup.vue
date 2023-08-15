<template>
    <div class="floating-window" ref="windowRef">
        <div class="chat-app">
            <div class="chat-sidebar">
                <div class="sidebar-header">
                    <h1>Chat</h1>
                </div>
                <div class="sidebar-search">
                    <input type="text" v-model="searchQuery" placeholder="搜索" />
                    <i class="fa fa-search"></i>
                </div>
                <div class="sidebar-contacts">
                    <div v-for="(contact, index) in filteredContacts" :key="index" class="contact"
                        :class="{ active: activeContactIndex === index }" @click="changeActiveContact(index)">
                        <div class="contact-avatar">
                            <img :src="imageSrc(contact.avatar)" />
                        </div>
                        <div class="contact-content">
                            <div class="contact-name">{{ contact.name }}<span v-if="onLine(contact.id)"
                                    style="color: lime; font-size: 0.5em;">在线</span></div>
                            <div class="contact-message">{{ lastMessage(contact.messages) }}</div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="chat-main">
                <div class="mini-but">
                    <button class="minimize-button" @click="minimizeFloatingWindow">×</button>
                </div>
                <div class="chat-header">
                    <div class="header-name">
                        {{ activeContact.name }}
                    </div>
                </div>
                <div class="chat-messages">
                    <div ref="scrollContainer" class="scrollContainer">
                        <div v-for="(message, index) in activeContact.messages" :key="index" class="message">
                            <div :class="[message.senderId === this.userInfo.id ? 'sent' : 'received']">
                                <div v-if="message.senderId != this.userInfo.id" class="header-avatar">
                                    <img :src="imageSrc(activeContact.avatar)" />
                                </div>
                                <div class="message-data">
                                    <div class="message-date">{{ message.sendTime }}</div>
                                    <div class="message-content">{{ isEmoji(message) }}</div>
                                </div>
                                <div v-if="message.senderId === this.userInfo.id" class="header-avatar">
                                    <img :src="imageSrc(activeContact.avatar)" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="chat-input">
                    <input v-model="sendContent" type="text" placeholder="" />
                    <img v-if="sendContent === ''" src="../../assets/表情.svg" alt="Smile" style="width: 40px;height: 40px;"
                        class="svg-button" @click="toggleEmojiPicker">
                    <button v-if="sendContent != ''" @click="sendToUser">发送</button>
                </div>
                <transition name="fade">
                    <div v-if="showEmojiPicker" class="emoji-picker-modal" @click="closeEmojiPicker">
                        <div class="emoji-picker-container">
                            <div class="emoji-list">
                                <div v-for="emoji in emojis" :key="emoji.unicode" class="emoji-item"
                                    @click="sendEmoji(emoji)">
                                    {{ emoji.emoji }}
                                </div>
                            </div>
                        </div>
                    </div>
                </transition>
            </div>
        </div>
    </div>
</template>

<script>
import axios from '../../api/instance'
import path from '../../api/path'
import * as signalR from '@microsoft/signalr'
import emojisData from "../../assets/emoji.json"

export default {
    data() {
        return {
            contacts: [],
            activeContactIndex: 0,
            searchQuery: "",
            connection: null,
            sendUserid: '',
            sendContent: '',
            connectedUsers: [],
            userInfo: {},
            defaultAvatar: require('../../assets/avater.png'),
            showEmojiPicker: false,
            emojis: emojisData,
            selectedEmoji: ""
        };
    },
    components: {
    },
    computed: {
        activeContact() {
            return this.contacts[this.activeContactIndex];
        },
        filteredContacts() {
            if (this.searchQuery) {
                return this.contacts.filter(contact =>
                    contact.name.toLowerCase().includes(this.searchQuery.toLowerCase())
                );
            } else {
                return this.contacts;
            }
        }
    },
    created() {
        this.init();
    },
    mounted() {
        this.setupDraggableWindow();
        this.$watch('activeContactIndex', newIndex => {
            var data = this.contacts[newIndex];
            this.sendUserid = data.id;
        }, { immediate: true });
        this.connectToChatHub();
        this.$nextTick(() => {
            this.scrollToBottom();
        });
    },
    methods: {
        init() {
            this.userInfo = this.$store.state.userInfo;
            this.getAllData();
        },
        getAllData() {
            axios.get(path.baseUrl + "SignalR/GetUsers?UserId=" + this.userInfo.id)
                .then(res => {
                    this.contacts = res.data
                }).catch(err => {
                    console.log(err);
                })
        },
        minimizeFloatingWindow() {
            this.$emit('minimize');
            this.disconnectSignalR();
        },
        setupDraggableWindow() {
            const windowRef = this.$refs.windowRef;
            let isDragging = false;
            let mouseOffsetX = 0;
            let mouseOffsetY = 0;

            const handleMouseDown = (e) => {
                isDragging = true;
                mouseOffsetX = e.clientX - windowRef.offsetLeft;
                mouseOffsetY = e.clientY - windowRef.offsetTop;
            };

            const handleMouseMove = (e) => {
                if (isDragging) {
                    const newLeft = e.clientX - mouseOffsetX;
                    const newTop = e.clientY - mouseOffsetY;
                    windowRef.style.left = `${newLeft}px`;
                    windowRef.style.top = `${newTop}px`;
                }
            };

            const handleMouseUp = () => {
                isDragging = false;
            };

            windowRef.addEventListener('mousedown', handleMouseDown);
            window.addEventListener('mousemove', handleMouseMove);
            window.addEventListener('mouseup', handleMouseUp);

            const beforeUnmountHandler = () => {
                windowRef.removeEventListener('mousedown', handleMouseDown);
                window.removeEventListener('mousemove', handleMouseMove);
                window.removeEventListener('mouseup', handleMouseUp);
            };
            this.$options.beforeUnmount = beforeUnmountHandler;
        },

        async scrollToBottom() {
            await this.$nextTick();
            const container = this.$refs.scrollContainer;
            container?.lastElementChild?.scrollIntoView({ behavior: 'smooth' });
        },

        changeActiveContact(index) {
            this.activeContactIndex = index;
        },

        imageSrc(data) {
            return data || this.defaultAvatar;
        },

        lastMessage(data) {
            if (data === null || data.length === 0) {
                return " ";
            } else
                return this.isEmoji(data[data.length - 1]);
        },

        isEmoji(data) {
            if (data.type === 2) {
                return this.emojis.find(w => w.unicode === data.content).emoji;
            } else
                return data.content;
        },

        toggleEmojiPicker() {
            this.showEmojiPicker = !this.showEmojiPicker;
        },

        closeEmojiPicker() {
            this.showEmojiPicker = false;
        },

        onLine(id) {
            return this.connectedUsers.includes(id);
        },
        connectToChatHub() {
            const userId = this.userInfo.id;
            this.connection = new signalR.HubConnectionBuilder()
                .withUrl(path.chatUrl + "chatHub?userId=" + userId)
                .build();

            this.connection.start().then(() => {
                console.log("Connected to chat hub");
            });

            this.connection.on('ReceiveMessage', msg => {
                this.contacts.find(w => w.id === msg.senderId).messages.push(msg);
                this.scrollToBottom();
            })

            this.connection.on('ConnectedUsers', users => {
                this.connectedUsers = users;
            })
        },
        disconnectSignalR() {
            if (this.connection)
                this.connection.stop();
        },
        sendToUser() {
            this.connection.invoke("SendMessageToUser", this.sendUserid, this.sendContent)
                .then(res => {
                    if (res != null) {
                        this.sendContent = '';
                        this.contacts.find(w => w.id === res.receiverId).messages.push(res);
                        this.scrollToBottom();
                    }
                }).catch((error) => {
                    console.error(error);
                });
        },
        sendEmoji(emoji) {
            this.selectedEmoji = emoji.emoji;
            this.showEmojiPicker = false;
            this.connection.invoke('SendEmoji', this.sendUserid, emoji.unicode)
                .then(res => {
                    if (res != null) {
                        this.sendContent = '';
                        this.contacts.find(w => w.id === res.receiverId).messages.push(res);
                        this.scrollToBottom();
                    }
                })
                .catch(error => {
                    console.error(error);
                });
        }

    }
}
</script>

<style>
* {
    box-sizing: border-box;
}

body {
    font-family: Arial, sans-serif;
    margin: 0;
}

.floating-window {
    position: fixed;
    top: 50px;
    left: 50px;
    width: 60%;
    height: 60%;
    background-color: white;
    border: 1px solid black;
    z-index: 9999;
}

.minimize-button {
    position: absolute;
    top: 5px;
    right: 5px;
    width: 20px;
    height: 20px;
    font-size: 14px;
    font-weight: bold;
    border: none;
    background-color: transparent;
    cursor: pointer;
}

.chat-app {
    display: flex;
    width: 100%;
    height: 100%;
    background-color: #f5f5f5;
}

.chat-sidebar {
    flex: 1;
    padding: 20px;
    background-color: #fff;
    border-right: 1px solid #ccc;
}

.sidebar-header {
    text-align: center;
    margin-bottom: 20px;
}

.sidebar-search {
    display: flex;
    align-items: center;
    margin-bottom: 20px;
}

.sidebar-search input {
    flex: 1;
    padding: 8px;
    border: 1px solid #ccc;
    border-radius: 3px;
}

.sidebar-contacts {
    overflow-y: auto;
}

.contact {
    display: flex;
    align-items: center;
    padding: 10px;
    cursor: pointer;
}

.contact.active {
    background-color: #f5f5f5;
}

.contact-avatar {
    margin-right: 10px;
}

.contact-avatar img {
    width: 40px;
    height: 40px;
    border-radius: 50%;
}

.contact-content {
    flex: 1;
}

.contact-name {
    font-weight: bold;
}

.contact-message {
    color: #999;
}

.chat-main {
    flex: 3;
    padding: 10px;
    background-color: #f3f3f3;
    overflow-y: auto;
    position: relative;
}

.mini-but {
    height: 10px;
}

.chat-header {
    display: flex;
    align-items: center;
    height: 30px;
    /* margin-bottom: 20px; */
    position: relative;
    background-color: #e4e4e4;
}

.header-avatar {
    display: inline-block;
    padding: 5px;
}

.header-avatar img {
    width: 40px;
    height: 40px;
    border-radius: 50%;
}

.header-name {
    font-weight: bold;
    top: 0;
    right: 0;
    left: 0;
    bottom: 0;
    margin: auto;
}

.chat-messages {
    height: calc(100% - 85px);
    padding: 5px;
    overflow-y: scroll;
    overflow: auto;
    background-color: #fffdfd;
}

.chat-messages::-webkit-scrollbar {
    width: 0.3em;
}

.chat-messages::-webkit-scrollbar-thumb {
    background-color: #dedede;
}

.scrollContainer {
    overflow-y: auto;
}

.message {
    margin-bottom: 10px;
}

.sent {
    text-align: right;
}

.received {
    text-align: left;
}

.message-data {
    display: inline-block;
    background-color: #fffdfd;
}

.message-content {
    display: inline-block;
    padding: 5px 10px;
    background-color: #f5f5f5;
    border-radius: 10px;
}

.message-date {
    color: #9a9a9a;
}

.chat-input {
    width: calc(100% - 20px);
    display: flex;
    align-items: center;
    padding: 5px;
    position: absolute;
    bottom: 5px;
    background-color: #e4e4e4;
}

.chat-input input {
    flex: 1;
    padding: 8px;
    border: 1px solid #ccc;
    border-radius: 5px;
    height: 40px;
}

.chat-input button {
    font-size: 18px;
    padding: 8px;
    margin-left: 10px;
    background-color: #f5f5f5;
    border: none;
    cursor: pointer;
    color: #959595;
    border-radius: 5px;
}

.fa {
    font-family: FontAwesome;
}

.fade-enter-active,
.fade-leave-active {
    transition: opacity 0.2s;
}

.fade-enter,
.fade-leave-to {
    opacity: 0;
}

.emoji-picker-modal {
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background-color: rgba(0, 0, 0, 0.5);
    display: flex;
    justify-content: center;
    align-items: center;
    z-index: 999;
}

.emoji-picker-container {
    background-color: white;
    padding: 20px;
    border-radius: 5px;
    width: 300px;
    max-height: 300px;
    overflow-y: auto;
}

.emoji-list {
    display: flex;
    flex-wrap: wrap;
}

.emoji-item {
    cursor: pointer;
    padding: 5px;
}

.selected-emoji {
    margin-top: 20px;
}
</style>