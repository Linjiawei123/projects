<template>
    <div class="popup-mask">
        <div class="popup-wrapper">
            <div class="popup-header">
                <span class="popup-title">{{ operate === 4 ? '新增菜单' : (operate === 0 ? '新增子菜单' : (operate === 1 ? '菜单详情' :
                    '编辑菜单')) }}</span>
                <el-button class="popup-close" size="small" link @click="close"><el-icon>
                        <CircleClose />
                    </el-icon></el-button>
            </div>
            <div class="popup-content">
                <el-form :model="formMenu">
                    <el-form-item label="上级菜单：" label-width="80px">
                        <el-cascader class="from-cascader" v-model="formMenu.parentId" :options="menus"
                            :props="defaultParams" placeholder="上级菜单" autocomplete="off" :disabled="operate != 2"
                            @change="getParentMenu" ref="cascaderAddr" />
                    </el-form-item>
                    <el-form-item label="菜单名称：" label-width="80px">
                        <el-input v-model="formMenu.name" autocomplete="off" :disabled="operate === 1" />
                    </el-form-item>
                    <el-form-item label="菜单代码：" label-width="80px">
                        <el-input v-model="formMenu.code" autocomplete="off" :disabled="operate === 1" />
                    </el-form-item>
                    <el-form-item label="菜单地址：" label-width="80px">
                        <el-input v-model="formMenu.url" autocomplete="off" :disabled="operate === 1" />
                    </el-form-item>
                    <el-form-item label="菜单图标：" label-width="80px">
                        <el-input v-model="formMenu.icon" autocomplete="off" :disabled="operate === 1" />
                    </el-form-item>
                    <el-form-item label="菜单排序：" label-width="80px">
                        <el-input v-model="formMenu.sort" autocomplete="off" :disabled="operate === 1" />
                    </el-form-item>
                    <el-form-item label="默认菜单：" label-width="80px">
                        <el-switch v-model="formMenu.isDefault" class="ml-2"
                            style="--el-switch-on-color: #13ce66; --el-switch-off-color: #ff4949" autocomplete="off"
                            :disabled="operate === 1" />
                    </el-form-item>
                </el-form>
                <span class="dialog-footer">
                    <el-button type="primary" v-if="operate != 1"
                        @click="operate === 2 ? clickEditHandle(formMenu) : clickAddHandle(formMenu)">保存</el-button>
                </span>
            </div>
        </div>
    </div>
</template>
  
<script>
import { ref, toRaw } from 'vue'
import axios from '../../../api/instance'
import path from '../../../api/path'
import { CircleClose } from '@element-plus/icons-vue'

export default {
    name: 'Popup',
    data() {
        return {
            operate: 0,
            selectMenuId: '',
            formMenu: {},
            menus: [],
            defaultParams: {
                label: 'name',
                value: 'id',
                children: 'child',
                checkStrictly: true
            }
        }
    },
    props: {
        form: [Number, Object],
    },
    components: {
        CircleClose
    },
    created() {
        this.init();
    },

    methods: {
        init(params) {
            this.operate = this.form.operate;
            this.getList();
            if (this.operate != 0) this.formMenu = { ...this.form };
            else {
                this.formMenu.parentId = this.form.id;
                this.selectMenuId = this.formMenu.parentId;
            }
        },
        getList() {
            axios.get(path.baseUrl + path.menu + '?Name=')
                .then(res => {
                    this.menus = res.data.data
                }).catch(err => {
                    console.log(err);
                })
        },
        close() {
            this.$emit("change", false, ref([]));
        },
        getParentMenu(data) {
            const arr = toRaw(data);
            this.selectMenuId = arr[arr.length - 1];
        },
        clickAddHandle(menu) {
            this.selectMenuId = this.selectMenuId === '' ? this.formMenu.parentId : this.selectMenuId;
            axios.post(path.baseUrl + path.addmenu, {
                parentId: this.selectMenuId,
                name: menu.name,
                code: menu.code,
                sort: menu.sort,
                url: menu.url,
                icon: menu.icon,
                remark: '',
                isDefault: menu.isDefault,
                isBlank: true
            }).then(res => {
                this.$emit("change", false, ref([]));
            }).catch(err => {
                console.log(err);
            });
        },
        clickEditHandle(menu) {
            this.selectMenuId = this.selectMenuId === '' ? this.formMenu.parentId : this.selectMenuId;
            axios.put(path.baseUrl + path.editmenu, {
                id: menu.id,
                parentId: this.selectMenuId,
                name: menu.name,
                code: menu.code,
                sort: menu.sort,
                url: menu.url,
                icon: menu.icon,
                remark: '',
                isDefault: menu.isDefault,
                isBlank: true
            }).then(res => {
                this.$emit("change", false, ref([]));
                console.log(res.data);
            }).catch(err => {
                console.log(err);
            });
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
    max-width: 400px;
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

:deep(.from-cascader) {
    width: 100%;
}
</style>