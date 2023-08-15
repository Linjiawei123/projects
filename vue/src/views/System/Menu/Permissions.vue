<template>
    <div class="popup-mask">
        <div class="popup-wrapper">
            <div class="popup-header">
                <span class="popup-title">{{ content === 0 ? '权限列表' : (content === 1 ? '新增权限' : '编辑权限') }}</span>
                <div class="popup-but">
                    <el-button size="small" link @click="clickShowFormHandle"><el-icon>
                            <CirclePlus v-if="content === 0 && this.$isHasRgihts('Permission_View')" />
                            <House v-if="content === 1 || content === 2" />
                        </el-icon></el-button>
                    <el-button size="small" link @click="close"><el-icon>
                            <CircleClose />
                        </el-icon></el-button>
                </div>
            </div>
            <div class="popup-content">
                <div class="tableBox" v-if="content === 0">
                    <el-table class="table" :data="list" row-key="id"
                        :header-cell-style="{ background: '#eef1f6', color: '#606266' }" stripe
                        :tree-props="{ children: 'child' }">
                        <el-table-column fixed label="权限名称" style="width: 20%;">
                            <template #default="scope">
                                <div style="display: inline; align-items: center">
                                    <span style="margin-left: 10px">{{ scope.row.name }}</span>
                                </div>
                            </template>
                        </el-table-column>
                        <el-table-column label="权限代码" style="width: 40%;">
                            <template #default="scope">
                                <div style="display: flex; align-items: center">
                                    <span>{{ scope.row.code }}</span>
                                </div>
                            </template>
                        </el-table-column>
                        <el-table-column label="备注" style="width: 30%;">
                            <template #default="scope">
                                <div style="display: flex; align-items: center">
                                    <span>{{ scope.row.remark }}</span>
                                </div>
                            </template>
                        </el-table-column>
                        <el-table-column fixed="right" label="操作" style="width: 10%;">
                            <template #default="scope">
                                <el-button link type="warning" size="small" v-if="this.$isHasRgihts('Permission_Update')"
                                    @click="clickEditShowHandle(scope.row)"><el-icon>
                                        <Edit />
                                    </el-icon></el-button>
                                <el-button link type="danger" size="small" v-if="this.$isHasRgihts('Permission_Delete')"
                                    @click="clickDeleteHandle(scope.row.id)"><el-icon>
                                        <Delete />
                                    </el-icon></el-button>
                            </template>
                        </el-table-column>
                    </el-table>
                </div>
                <div class="from" v-if="content === 1 || content === 2">
                    <el-form :model="form">
                        <el-form-item label="权限名称：" label-width="80px">
                            <el-input v-model="form.name" autocomplete="off" />
                        </el-form-item>
                        <el-form-item label="权限代码：" label-width="80px">
                            <el-input v-model="form.code" autocomplete="off" />
                        </el-form-item>
                        <el-form-item label="权限备注：" label-width="80px">
                            <el-input v-model="form.remark" autocomplete="off" />
                        </el-form-item>
                    </el-form>
                    <span class="dialog-footer">
                        <el-button type="primary"
                            @click="content === 2 ? clickEditHandle(form) : clickAddHandle(form)">保存</el-button>
                    </span>
                </div>
            </div>
        </div>
    </div>
    <DelTip v-if="showTip" :tipData="tipData" @change="closeDelTip"></DelTip>
</template>
  
<script>
import axios from '../../../api/instance'
import path from '../../../api/path'
import DelTip from './PermissionsDelTip.vue'
import { Delete, Edit, CirclePlus, CircleClose, House } from '@element-plus/icons-vue'

export default {
    name: 'Permissions',
    emits:['closePermissions'],
    data() {
        return {
            MenuId: '',
            list: [],
            content: 0,
            form: {},
            showTip: false,
            tipData: {}
        }
    },
    props: {
        menu: [Object]
    },
    components: {
        Delete,
        Edit,
        CircleClose,
        CirclePlus,
        House,
        DelTip
    },
    created() {
        this.init();
    },

    mounted() {
        this.getList();
    },

    methods: {
        init(params) {
            this.MenuId = this.menu.id;
        },
        close() {
            this.$emit("closePermissions", false);
        },
        getList() {
            axios.get(path.baseUrl + path.permissionsList + this.MenuId)
                .then(res => {
                    this.list = res.data.data
                }).catch(err => {
                    console.log(err);
                })
        },
        clickShowFormHandle() {
            if (this.content != 0) {
                this.content = 0;
                this.form = {};
            }
            else
                this.content = 1;
        },
        clickEditShowHandle(data) {
            this.content = 2;
            this.form = data;
        },
        clickAddHandle(data) {
            axios.post(path.baseUrl + path.permissionsAdd, {
                menuId: this.MenuId,
                name: data.name,
                code: data.code,
                remark: data.remark
            }).then(res => {
                this.content = 0;
                this.getList();
                this.form = {};
            }).catch(err => {
                console.log(err);
            })
        },
        clickEditHandle(data) {
            axios.put(path.baseUrl + path.permissionsEdit, {
                id: data.id,
                menuId: this.MenuId,
                name: data.name,
                code: data.code,
                remark: data.remark
            }).then(res => {
                this.content = 0;
                this.getList();
                this.form = {};
            }).catch(err => {
                console.log(err);
            })
        },
        clickDeleteHandle(id) {
            this.showTip = true;
            this.tipData = { id: id, tip: '您确定删除该权限？' };
        },
        closeDelTip(param) {
            this.showTip = false;
            if (param)
                this.getList();
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
    max-width: 800px;
    /* height: 50%; */
    max-height: 600px;
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

.popup-but {
    font-size: 40px;
    cursor: pointer;
}

.popup-content {
    padding: 10px;
}

.table {
    width: 100%;
    height: 100%;
    border-radius: 3px;
    box-shadow: 2px 2px 2px #bbbbbb;
}

.from {
    width: 100%;
    height: 100%;
}
</style>