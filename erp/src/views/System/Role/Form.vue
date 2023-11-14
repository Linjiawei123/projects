<template>
    <div class="popup-mask">
        <div class="popup-wrapper">
            <div class="popup-header">
                <span class="popup-title">{{ operate === 0 ? '新增' : (operate === 1 ? '详情' :
                    '编辑') }}</span>
                <el-button class="popup-close" size="small" link @click="close"><el-icon>
                        <CircleClose />
                    </el-icon></el-button>
            </div>
            <div class="popup-content">
                <el-form :model="role">
                    <el-row>
                        <el-col :span="6">
                            <el-form-item label="角色编号：" label-width="80px">
                                <el-input v-model="role.code" autocomplete="off" :disabled="operate === 1" />
                            </el-form-item>
                        </el-col>
                        <el-col :span="6"><el-form-item label="角色名称：" label-width="80px">
                                <el-input v-model="role.name" autocomplete="off" :disabled="operate === 1" />
                            </el-form-item></el-col>
                        <el-col :span="6"><el-form-item label="角色状态：" label-width="80px">
                                <el-switch v-model="role.status" class="ml-2"
                                    style="--el-switch-on-color: #13ce66; --el-switch-off-color: #ff4949" autocomplete="off"
                                    :disabled="operate === 1" />
                            </el-form-item></el-col>
                    </el-row>
                </el-form>
                <rightsTree :tree="tree" @changeRights="chooseRights"></rightsTree>
                <span class="dialog-footer">
                    <el-button type="primary" v-if="operate != 1"
                        @click="operate === 2 ? clickEditHandle(role) : clickAddHandle(role)">保存</el-button>
                </span>
            </div>
        </div>
    </div>
</template>
  
<script>
import axios from '../../../api/instance'
import path from '../../../api/path'
import rightsTree from '../../../components/PermissionsTree.vue'
import { CircleClose } from '@element-plus/icons-vue'

export default {
    name: 'roleform',
    data() {
        return {
            operate: 0,
            role: {},
            tree: {},
            rights: []
        }
    },
    props: {
        form: [Number, Object],
    },
    components: {
        CircleClose, rightsTree
    },
    created() {
        this.init();
    },

    methods: {
        init(params) {
            this.operate = this.form.operate;
            if (this.operate != 0) {
                this.role = { ...this.form };
                this.tree = { operate: this.operate, ...this.form };
            } else
                this.tree = { operate: this.operate, ...this.form };
        },
        close() {
            this.$emit("closeForm", false);
        },
        clickAddHandle(data) {
            axios.post(path.baseUrl + path.role, {
                code: data.code,
                name: data.name,
                status: data.status,
                RightsId: this.rights
            }).then(res => {
                console.log(res);
                this.$emit("closeForm", false);
            }).catch(err => {
                console.log(err);
            });
        },
        clickEditHandle(data) {
            axios.put(path.baseUrl + path.role, {
                id: data.id,
                code: data.code,
                name: data.name,
                status: data.status,
                RightsId: this.rights
            }).then(res => {
                console.log(res);
                this.$emit("closeForm", false);
            }).catch(err => {
                console.log(err);
            });
        },
        chooseRights(param) {
            this.rights = param;
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
    max-width: 910px;
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