<template>
    <div class="popup-mask">
        <div class="popup-wrapper">
            <div class="popup-header">
                <span class="popup-title">成员管理</span>
                <el-button class="popup-close" size="small" link @click="close"><el-icon>
                        <CircleClose />
                    </el-icon></el-button>
            </div>
            <div class="popup-content">
                <el-transfer v-cloak v-model="roleUser" :props="{
                        key: 'id',
                        label: 'name',
                    }" filterable :titles="['用户', '角色成员']" filter-placeholder="用户昵称" :data="users" />
                <span class="dialog-footer">
                    <el-button type="primary" style="width: 100px;" @click="clickSaveHandle">保存</el-button>
                </span>
            </div>

        </div>
    </div>
</template>
  
<script>
import axios from '../../../api/instance'
import path from '../../../api/path'
import { CircleClose } from '@element-plus/icons-vue'

export default {
    name: 'roleuser',
    data() {
        return {
            operate: 0,
            role: {},
            users: [],
            roleUser: []
        }
    },
    props: {
        roledata: [Object],
    },
    components: {
        CircleClose
    },
    created() {
        this.init();
    },

    methods: {
        init(params) {
            this.role = { ...this.roledata };
            this.getUserData();
            this.getRoleUser();
        },
        getUserData() {
            axios.post(path.baseUrl + path.roleUserList)
                .then(res => {
                    this.users = res.data.data;
                }).catch(err => {
                    console.log(err);
                })
        },
        getRoleUser() {
            axios.post(path.baseUrl + path.roleUser + '/' + this.role.id)
                .then(res => {
                    this.roleUser = res.data.data;
                }).catch(err => {
                    console.log(err);
                })
        },
        clickSaveHandle() {
            axios.post(path.baseUrl + path.roleUser, {
                roleId: this.role.id,
                userId: this.roleUser
            }).then(res => {
                this.$emit("closeRoleUser");
            }).catch(err => {
                console.log(err);
            })
        },
        close() {
            this.$emit("closeRoleUser");
        }
    }
};
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
    max-width: 600px;
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

[v-cloak] {
    display: none;
}
</style>