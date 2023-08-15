<template>
    <div class="popup-mask">
        <div class="popup-wrapper">
            <div class="popup-header">
                <span class="popup-title">{{ operate === 4 ? '新增部门' : (operate === 0 ? '新增下级部门' : (operate === 1 ? '部门详情' :
                    '编辑部门')) }}</span>
                <el-button class="popup-close" size="small" link @click="close"><el-icon>
                        <CircleClose />
                    </el-icon></el-button>
            </div>
            <div class="popup-content">
                <el-form :model="form">
                    <el-form-item label="上级部门：" label-width="80px">
                        <el-cascader class="from-cascader" v-model="form.parentId" :options="list"
                            :props="defaultParams" placeholder="上级部门" autocomplete="off" :disabled="operate != 2"
                            @change="getParent" ref="cascaderAddr" />
                    </el-form-item>
                    <el-form-item label="部门名称：" label-width="80px">
                        <el-input v-model="form.name" autocomplete="off" :disabled="operate === 1" />
                    </el-form-item>
                    <el-form-item label="部门编号：" label-width="80px">
                        <el-input v-model="form.code" autocomplete="off" :disabled="operate === 1" />
                    </el-form-item>
                </el-form>
                <span class="dialog-footer">
                    <el-button type="primary" v-if="operate != 1"
                        @click="operate === 2 ? clickEditHandle(form) : clickAddHandle(form)">保存</el-button>
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
    name: 'Form',
    data() {
        return {
            operate: 0,
            selectId: '',
            form: {},
            list: [],
            defaultParams: {
                label: 'name',
                value: 'id',
                children: 'child',
                checkStrictly: true
            }
        }
    },
    props: {
        propform: [Number, Object],
    },
    components: {
        CircleClose
    },
    created() {
        this.init();
    },

    methods: {
        init(params) {
            this.operate = this.propform.operate;
            this.getList();
            if (this.operate != 0) this.form = { ...this.propform };
            else {
                this.form.parentId = this.propform.id;
                this.selectId = this.form.parentId;
            }
        },
        getList() {
            axios.get(path.baseUrl + path.department + '?Name=')
                .then(res => {
                    this.list = res.data.data
                }).catch(err => {
                    console.log(err);
                })
        },
        close() {
            this.$emit("change", false, ref([]));
        },
        getParent(data) {
            const arr = toRaw(data);
            this.selectId = arr[arr.length - 1];
        },
        clickAddHandle(data) {
            this.selectId = this.selectId === '' ? this.form.parentId : this.selectId;
            axios.post(path.baseUrl + path.department, {
                parentId: this.selectId,
                name: data.name,
                code: data.code,
            }).then(res => {
                this.$emit("change", false, ref([]));
            }).catch(err => {
                console.log(err);
            });
        },
        clickEditHandle(data) {
            this.selectId = this.selectId === '' ? this.form.parentId : this.selectId;
            axios.put(path.baseUrl + path.department, {
                id: data.id,
                parentId: this.selectId,
                name: data.name,
                code: data.code,
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