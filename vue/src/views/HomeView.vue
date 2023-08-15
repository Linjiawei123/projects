<template>
    <div class="home">
        <el-container class="menu-container">
            <el-header class="header-menu">
                <div class="header-left">
                    <div class="header-title">后台管理系统</div>
                    <div class="toggle-button" @click="toggleCollapse">
                        <el-icon v-if="toggle" size="20">
                            <CaretLeft />
                        </el-icon>
                        <el-icon v-else size="20">
                            <CaretRight />
                        </el-icon>
                    </div>
                </div>
                <el-menu class="header-el-menu" mode="horizontal" background-color="#334FDE" text-color="#fff"
                    active-text-color="#ffd04b" style="border-bottom:0!important;" @select="selectHeaderMenu">
                    <el-menu-item v-for="item in menus" :key="item.id" :index="item.id">
                        <el-icon>
                            <component :is="item.icon??'Menu'"></component>
                        </el-icon>
                        <span>{{ item.name }}</span>
                    </el-menu-item>
                </el-menu>
                <div class="h-6" />
                <div class="chat">
                    <el-button type="success" circle @click="toggleChat"><el-icon>
                            <ChatDotRound />
                        </el-icon>
                        <teleport to="body">
                            <chatPopup v-if="showChat" @minimize="toggleChat" />
                        </teleport>
                    </el-button>
                </div>
                <el-dropdown class="user">
                    <div class="userinfo">
                        <el-avatar class="user-avatar"
                            :src="this.$store.state.userInfo.url ?? require('../assets/avater.png')" />
                        <div class="username">{{ this.$store.state.userInfo.userName }}</div>
                    </div>
                    <template #dropdown>
                        <el-dropdown-menu>
                            <el-dropdown-item @click="clickUserInfo()">个人中心</el-dropdown-item>
                            <el-dropdown-item @click="clickLoginOut()">退出登录</el-dropdown-item>
                        </el-dropdown-menu>
                    </template>
                </el-dropdown>
            </el-header>
            <el-container class="main-container">
                <el-aside class="el-aside" :width="leftmenu.length > 0 ? (isCollapse ? '64px' : '200px') : '0px'">
                    <el-scrollbar>
                        <el-menu :default-active="this.$store.state.menuActiveIndex" background-color="#0f6eb3"
                            text-color="#fff" active-text-color="#ffd04b" style="border-right:0!important;"
                            :collapse="isCollapse" :collapse-transition="false" v-for="item in leftmenu" :key="item.id"
                            :index="item.id" @select="handleSelect(item)">

                            <el-sub-menu v-if="item.child.length > 0" :index="item.id">
                                <template #title>
                                    <el-icon>
                                        <component :is="item.icon??'Menu'"></component>
                                    </el-icon>
                                    <span>{{ item.name }}</span></template>
                                <el-menu-item-group v-for="subItem in item.child" :key="subItem.id">
                                    <el-menu-item :index="subItem.id" @click="handleSelect(subItem)">
                                        <el-icon>
                                            <component :is="subItem.icon?subItem.icon:'Menu'"></component>
                                        </el-icon>
                                        <span>{{ subItem.name }}</span>
                                    </el-menu-item>

                                </el-menu-item-group>
                            </el-sub-menu>
                            <el-menu-item v-else :index="item.id">
                                <el-icon>
                                    <component :is="item.icon??'Menu'"></component>
                                </el-icon>
                                <span>{{ item.name }}</span>
                            </el-menu-item>
                        </el-menu>
                    </el-scrollbar>
                </el-aside>
                <el-main class="el-main">
                    <div class="breadcrumb">
                        <tagsView></tagsView>
                    </div>
                    <div class="mainview">
                        <router-view></router-view>
                    </div>
                </el-main>
            </el-container>
        </el-container>
        <userInfo v-if="showUserInfo" @change="closePopup"></userInfo>
    </div>
</template>
<script>

import {
    Menu as IconMenu, ChatDotRound
} from '@element-plus/icons-vue'
import axios from '../api/instance'
import path from '../api/path'
import { mapState } from 'vuex'
import tagsView from '../components/TagsView.vue'
import userInfo from './System/UserInfo.vue'
import chatPopup from './Chat/ChatPopup.vue'

export default {
    name: "HomeView",
    data() {
        return {
            menus: [],
            leftmenu: [
            ],
            isCollapse: true,
            toggle: false,
            headerActiveIndex: '',
            activeIndex: '',
            showUserInfo: false,
            showChat: false
        }
    },
    mounted() {
        axios.get(path.baseUrl + path.loginUser)
            .then(res => {
                this.menus = res.data.data.menus;
                this.$store.commit('setUserInfo', res.data.data);
            }).catch(err => {
                console.log(err);
            })
    },
    computed: {
        ...mapState(["isTagsShow"]),
        ...mapState(["menuActiveIndex"])
    },
    watch: {
        isTagsShow() {
            if (this.isTagsShow) {
                this.subwidth = '64px'
            } else {
                this.subwidth = '200px'
            }
        }
    },
    methods: {
        selectHeaderMenu(id) {
            this.headerActiveIndex = id;
            let menu = this.menus.find(w => w.id == id);
            if (menu != null) {
                this.isCollapse = false;
                this.toggle = true;
                this.leftmenu = menu.child != null ? menu.child : [];

            } else {
                this.leftmenu = [];
                this.toggle = false;
            }
        },
        handleSelect(menu) {
            if (menu.url != null && menu.url != '') {
                this.$router.push(menu.url);
                this.$store.commit("pushtags", menu);
            }
            else
                this.$router.push('/home/main');
        },
        toggleCollapse() {
            if (this.leftmenu.length > 0) {
                this.isCollapse = !this.isCollapse;
                this.toggle = this.toggle == false ? true : false;
            }
        },
        clickUserInfo() {
            this.showUserInfo = true;
        },
        closePopup(param) {
            this.showUserInfo = param;
        },
        clickLoginOut() {
            this.$store.commit('delToken');
            this.$router.push('/');
        },
        toggleChat() {
            this.showChat = !this.showChat;
        }
    },
    components: {
        IconMenu,
        tagsView,
        userInfo,
        ChatDotRound,
        chatPopup
    }
}
</script>
<style scoped>
.home {
    width: 100%;
    height: 100%;
    background-color: bisque;
    position: relative;
}

.menu-container {
    width: 100%;
    height: 100%;
    position: absolute;
    position: relative;
    background-color: aquamarine;
}

.header-menu {
    display: flex;
    padding: 0;
    background-color: #334FDE;
    user-select: none;
    -ms-user-select: none;
    -moz-user-select: none;
    -khtml-user-select: none;
    -webkit-user-select: none;
}

.header-left {
    width: 200px;
    display: flex;
}

.header-title {
    width: 150px;
    height: 100%;
    color: #fff;
    font-size: 20px;
    font-family: "楷体";
    font-weight: bolder;
    text-align: center;
    align-items: center;
    line-height: 60px;
    display: inline;
    user-select: none;
    -ms-user-select: none;
    -moz-user-select: none;
    -khtml-user-select: none;
    -webkit-user-select: none;
}

.toggle-button {
    width: 50px;
    height: 100%;
    background-color: #334FDE;
    color: #fff;
    display: inline;
    position: relative;
}

.toggle-button .el-icon {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    user-select: none;
    -ms-user-select: none;
    -moz-user-select: none;
    -khtml-user-select: none;
    -webkit-user-select: none;
}

.header-el-menu {
    width: 85%;
    height: 100%;
    user-select: none;
    -ms-user-select: none;
    -moz-user-select: none;
    -khtml-user-select: none;
    -webkit-user-select: none;
}

.toggle-button h3 {
    user-select: none;
    -ms-user-select: none;
    -moz-user-select: none;
    -khtml-user-select: none;
    -webkit-user-select: none;
}

:deep(:focus) {
    outline: 0;
}

.chat {
    float: right;
    display: flex;
    justify-content: center;
    align-items: center;
}

.user {
    width: 15%;
    height: 100%;
    float: right;
}

.userinfo {
    width: 100%;
    height: 100%;
    color: #fff;
    display: flex;
    align-items: center;
    justify-content: center;
    user-select: none;
    -ms-user-select: none;
    -moz-user-select: none;
    -khtml-user-select: none;
    -webkit-user-select: none;
}

.user-avatar {
    width: 40px;
    height: 40px;
    display: inline;
    float: left;
}

.username {
    color: #fff;
    display: inline;
    float: left;
    margin-left: 10px;
}

.el-aside {
    background-color: #0f6eb3;
}

.el-sub-menu :deep(.el-menu-item-group__title) {
    padding: 0;
}

.el-main {
    width: 100%;
    height: 100%;
    background-color: #eff1f1;
    margin: 0;
    padding: 0;
}

.breadcrumb {
    width: calc(100% - 10px);
    height: 30px;
    margin: 5px 5px 0px 5px;
    padding: 2px;
    background: #fff;
    border-radius: 5px;
    box-shadow: 2px 2px 2px #bbbbbb;
}

.mainview {
    width: 100%;
    height: calc(100% - 45px);
    padding: 5px;
}
</style>