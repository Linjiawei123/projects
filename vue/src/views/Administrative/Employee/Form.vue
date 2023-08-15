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
                <el-form :model="employee">
                    <div class="formData">
                        <el-row>
                            <el-col :span="11">
                                <el-form-item label="名称：" label-width="80px">
                                    <el-input v-model="employee.name" autocomplete="off" :disabled="operate === 1" />
                                </el-form-item>
                            </el-col>
                            <el-col :span="11">
                                <el-form-item label="部门：" label-width="80px">
                                    <el-cascader class="from-cascader" v-model="employee.departmentId"
                                        :options="departments" :props="defaultParams" placeholder="请选择" autocomplete="off"
                                        :disabled="operate === 1" @change="getParent" ref="cascaderAddr" />
                                </el-form-item>
                            </el-col>
                            <el-col :span="11">
                                <el-form-item label="用户账号：" label-width="80px">
                                    <el-select v-model="employee.userId" class="m-2" placeholder="请选择"
                                        :disabled="operate === 1">
                                        <el-option v-for="item in users" :key="item.id" :label="item.name"
                                            :value="item.id" />
                                    </el-select>
                                </el-form-item>
                            </el-col>
                            <el-col :span="11">
                                <el-form-item label="编号：" label-width="80px">
                                    <el-input v-model="employee.code" autocomplete="off" :disabled="operate === 1" />
                                </el-form-item>
                            </el-col>
                            <el-col :span="11">
                                <el-form-item label="性别：" label-width="80px">
                                    <el-select v-model="employee.sex" class="m-2" placeholder="请选择"
                                        :disabled="operate === 1">
                                        <el-option v-for="item in sex" :key="item.value" :label="item.label"
                                            :value="item.value" />
                                    </el-select>
                                </el-form-item>
                            </el-col>
                            <el-col :span="11">
                                <el-form-item label="出生日期：" label-width="80px">
                                    <el-date-picker class="search-input" v-model="employee.birthday"
                                        :disabled="operate === 1" type="date" value-format="YYYY-MM-DD"
                                        placeholder="请选择出生日期" />
                                </el-form-item>
                            </el-col>
                            <el-col :span="11">
                                <el-form-item label="身份证：" label-width="80px">
                                    <el-input v-model="employee.identityCard" autocomplete="off"
                                        :disabled="operate === 1" />
                                </el-form-item>
                            </el-col>
                            <el-col :span="11">
                                <el-form-item label="籍贯：" label-width="80px">
                                    <el-input v-model="employee.nativePlace" autocomplete="off" :disabled="operate === 1" />
                                </el-form-item>
                            </el-col>
                            <el-col :span="22">
                                <el-form-item label="住址：" label-width="80px">
                                    <el-input v-model="employee.address" autocomplete="off" :disabled="operate === 1" />
                                </el-form-item>
                            </el-col>
                            <el-col :span="11">
                                <el-form-item label="手机号码：" label-width="80px">
                                    <el-input v-model="employee.phone" autocomplete="off" :disabled="operate === 1" />
                                </el-form-item>
                            </el-col>
                            <el-col :span="11">
                                <el-form-item label="邮箱：" label-width="80px">
                                    <el-input v-model="employee.email" autocomplete="off" :disabled="operate === 1" />
                                </el-form-item>
                            </el-col>
                            <el-col :span="11">
                                <el-form-item label="入职日期：" label-width="80px">
                                    <el-date-picker class="search-input" v-model="employee.hireDate"
                                        :disabled="operate === 1" type="date" value-format="YYYY-MM-DD"
                                        placeholder="请选择入职日期" />
                                </el-form-item>
                            </el-col>
                            <el-col :span="11">
                                <el-form-item label="职位：" label-width="80px">
                                    <el-input v-model="employee.job" autocomplete="off" :disabled="operate === 1" />
                                </el-form-item>
                            </el-col>
                            <el-col :span="22">
                                <el-form-item label="备注：" label-width="80px">
                                    <el-input v-model="employee.remark" autocomplete="off"
                                        :autosize="{ minRows: 3, maxRows: 5 }" type="textarea" maxlength="150"
                                        show-word-limit :disabled="operate === 1" />
                                </el-form-item>
                            </el-col>
                        </el-row>
                    </div>
                    <div class="photo">
                        <el-upload class="avatar-uploader" action="" :http-request="uploadFile" :show-file-list="false"
                            :file-list="fileList" :disabled="operate === 1" accept=".jpg, .png, jpeg">
                            <el-image class="photo-image" :src="employee.photo"><template #error>
                                    <div class="image-slot">
                                        <el-icon><icon-picture /></el-icon>
                                    </div>
                                </template></el-image>
                        </el-upload>
                    </div>
                </el-form>
                <span class="dialog-footer">
                    <el-button type="primary" v-if="operate != 1"
                        @click="operate === 2 ? clickEditHandle(employee) : clickAddHandle(employee)">保存</el-button>
                </span>
            </div>
        </div>
    </div>
</template>
  
<script>
import { toRaw } from 'vue'
import axios from '../../../api/instance'
import path from '../../../api/path'
import { CircleClose, Picture as IconPicture } from '@element-plus/icons-vue'

export default {
    name: 'Form',
    data() {
        return {
            operate: 0,
            employee: {},
            fileList: [],
            sex: [
                {
                    value: true,
                    label: '男',
                },
                {
                    value: false,
                    label: '女',
                }
            ],
            select: '',
            departments: [],
            users: [],
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
        CircleClose, IconPicture
    },
    created() {
        this.init();
        this.getDepartments();
        this.getUsers(this.form.id);
    },

    methods: {
        init(params) {
            this.operate = this.form.operate;
            if (this.operate != 0) {
                this.get(this.form.id);
            }
        },
        get(id) {
            axios.get(path.baseUrl + path.employee + '/' + id)
                .then(res => {
                    this.employee = res.data.data
                }).catch(err => {
                    console.log(err);
                })
        },
        getDepartments() {
            axios.get(path.baseUrl + path.employee + '/Department')
                .then(res => {
                    this.departments = res.data.data
                }).catch(err => {
                    console.log(err);
                })
        },
        getUsers(id) {
            id = id??'';
            axios.get(path.baseUrl + path.employee + '/NotBindUser?Id='+ id)
                .then(res => {
                    this.users = res.data.data
                }).catch(err => {
                    console.log(err);
                })
        },
        getParent(data) {
            const arr = toRaw(data);
            this.select = arr[arr.length - 1];
        },
        close() {
            this.$emit("closeForm", false);
        },
        clickAddHandle(data) {
            axios.post(path.baseUrl + path.employee, {
                name: data.name,
                code: data.code,
                sex: data.sex,
                birthday: data.birthday,
                identityCard: data.identityCard,
                nativePlace: data.nativePlace,
                address: data.address,
                photo: data.photo,
                email: data.email,
                phone: data.phone,
                hireDate: data.hireDate,
                job: data.job,
                remark: data.remark,
                departmentId: this.select,
                userId: data.userId
            }).then(res => {
                this.$emit("closeForm", false);
                console.log(res.data);
            }).catch(err => {
                console.log(err);
            });
        },
        clickEditHandle(data) {
            axios.put(path.baseUrl + path.employee, {
                id: data.id,
                name: data.name,
                code: data.code,
                sex: data.sex,
                birthday: data.birthday,
                identityCard: data.identityCard,
                nativePlace: data.nativePlace,
                address: data.address,
                photo: data.photo,
                email: data.email,
                phone: data.phone,
                hireDate: data.hireDate,
                job: data.job,
                remark: data.remark,
                departmentId: this.select === '' ? this.employee.departmentId : this.select,
                userId: data.userId
            }).then(res => {
                this.$emit("closeForm", false);
                console.log(res.data);
            }).catch(err => {
                console.log(err);
            });
        },
        uploadFile(file) {
            const formData = new FormData();
            formData.append("file", file.file);
            axios.post(path.fileUrl, formData, { headers: { 'Content-Type': 'multipart/form-data' } }).then(res => {
                this.employee.photo = res.data.data
            })
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

.formData {
    width: 70%;
    display: inline;
    float: left;
}

.photo {
    width: 30%;
    height: 300px;
    display: inline;
    float: left;
}

.photo-image {
    width: 210px;
    height: 280px;
    background-color: #eee;
    text-align: center;
    line-height: 280px;
    border: 1px;
}
</style>