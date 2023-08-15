<template>
    <div class="popup-mask">
        <div class="popup-wrapper">
            <div class="popup-header">
                <span class="popup-title">用户中心</span>
                <el-button class="popup-close" size="small" link @click="close"><el-icon>
                        <CircleClose />
                    </el-icon></el-button>
            </div>
            <div class="popup-content">
                <div class="user">
                    <el-form label-width="50px" :model="user" style="max-width: 280px">
                        <el-upload class="avatar-uploader" action="" :http-request="uploadFile" :show-file-list="false"
                            :file-list="fileList" :disabled="!edit" accept=".jpg, .png, jpeg">
                            <el-avatar :src="user.url" :size="100" style="margin-bottom: 10px;" />
                        </el-upload>
                        <el-form-item label="账号：">
                            <el-input v-model="user.account" disabled />
                        </el-form-item>
                        <el-form-item label="昵称：">
                            <el-input v-model="user.userName" :disabled="!edit" />
                        </el-form-item>
                        <el-form-item label="密码：">
                            <el-input v-model="user.password" :disabled="!edit" />
                        </el-form-item>
                    </el-form>
                    <span class="dialog-footer">
                        <el-button type="warning" v-if="!edit" @click="edit = true" link><el-icon>
                                <Edit />
                            </el-icon></el-button>
                        <el-button type="primary" v-if="edit" @click="clickEditHandle(user)">保存</el-button>
                        <el-button type="info" v-if="edit" @click="edit = false, init()">取消</el-button>
                    </span>
                </div>
            </div>
        </div>
    </div>
</template>
  
<script>
import { CircleClose, Edit } from '@element-plus/icons-vue'
import { mapState } from "vuex";
import axios from '../../api/instance'
import path from '../../api/path'
import md5 from '../../js/md5'

export default {
    name: 'UserInfo',
    data() {
        return {
            user: {},
            edit: false,
            fileList: []
        }
    },
    props: {
        form: [Number, Object],
    },
    components: {
        CircleClose, Edit
    },
    computed: {
        ...mapState(["userInfo"]),
    },
    created() {
        this.init();
    },

    methods: {
        init() {
            this.user = { ...this.userInfo };
        },
        close() {
            this.$emit("change", false);
        },
        clickEditHandle(data) {
            axios.put(path.baseUrl + path.loginUser, {
                name: data.userName,
                password: data.password == null ? null : md5.hexMD5(data.password),
                url: data.url
            }).then(res => {
                this.$emit("change", false);
            }).catch(err => {
                console.log(err);
            })
        },
        uploadFile(file) {
            const formData = new FormData();
            formData.append("file", file.file);
            axios.post(path.baseUrl + path.UploadPic, formData, { headers: { 'Content-Type': 'multipart/form-data' } }).then(res => {
                this.user.url = res.data.data
            })
        }
    }
}
</script>
  
<style scoped>
.popup-mask {
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    z-index: 999;
    background-color: rgba(0, 0, 0, 0.5);
}

.popup-wrapper {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    background-color: #fff;
    width: 80%;
    max-width: 300px;
    border-radius: 4px;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
}

.popup-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 10px;
    border-bottom: 1px solid #eee;
}

.popup-title {
    font-size: 16px;
    font-weight: bold;
}

.popup-close {
    font-size: 20px;
    cursor: pointer;
}

.popup-content {
    padding: 10px;
}

.el-avatar {
    border: 3px solid #aed4fe;
}
</style>