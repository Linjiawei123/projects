<template>
    <div class="box">
        <div class="search">
            <el-input class="search-input" v-model="this.searchParams.CustomerName" placeholder="客户编码" clearable />
            <el-input class="search-input" v-model="this.searchParams.CustomerName" placeholder="客户名称" clearable />
            <el-input class="search-input" v-model="this.searchParams.Linkman" placeholder="联系人" clearable />
            <el-input class="search-input" v-model="this.searchParams.TelephoneCode" placeholder="联系电话" clearable />
            <el-input class="search-input" v-model="this.searchParams.Email" placeholder="邮箱" clearable />
            <el-input class="search-input" v-model="this.searchParams.PostCode" placeholder="邮政编码" clearable />
            <el-input class="search-input" v-model="this.searchParams.Url" placeholder="网站" clearable />
            <el-input class="search-input" v-model="this.searchParams.Address" placeholder="地址" clearable />
            <el-button class="search-but" type="primary" @click="clickSearchHandle">查询</el-button>
        </div>
        <div class="operate">
            <el-button class="operate-add" type="info" link @click="clickAddTopHandle"
                v-if="this.$isHasRgihts('User_Add')"><el-icon>
                    <Plus />
                </el-icon><span>添加</span></el-button>
        </div>
        <div class="tableBox">
            <el-table class="table" :data="page.pageData" row-key="id"
                :header-cell-style="{ background: '#eef1f6', color: '#606266' }" stripe>
                <el-table-column fixed label="客户编码">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.customerCode }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column fixed label="客户名称">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.customerName }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="业务员">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.employee }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="法人">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.atrMan }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="联系人">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.linkman }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="联系电话">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.telephoneCode }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="传真号码">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.faxCode }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="邮箱">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.email }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="邮政编码">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.postCode }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="网址" style="width: 300px;">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.url }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="地址" style="width: 300px;">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.address }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="客户等级">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ getTypeStr(1, scope.row.gradeCode) }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="客户状态">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ getTypeStr(2, scope.row.stateCode) }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="信用等级">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ getTypeStr(3, scope.row.creditCode) }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="行业分类">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ getTypeStr(4, scope.row.tradeCode) }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column fixed="right" label="操作" style="width: 20%;">
                    <template #default="scope">
                        <!-- <el-button link type="info" size="small" @click="clickInfoHandle(scope.row)" v-if="this.$isHasRgihts('User_View')"><el-icon>
                                <View />
                            </el-icon></el-button> -->
                        <el-button link type="warning" size="small" @click="clickEditHandle(scope.row)"
                            v-if="this.$isHasRgihts('User_Update')"><el-icon>
                                <Edit />
                            </el-icon></el-button>
                        <el-popconfirm title="确定要删除该客户？" icon="WarningFilled" icon-color="#ef4136"
                            @confirm="clickDeleteHandle(scope.row.id)" v-if="this.$isHasRgihts('User_Delete')">
                            <template #reference>
                                <el-button link type="danger" size="small"><el-icon>
                                        <Delete />
                                    </el-icon></el-button>
                            </template>
                        </el-popconfirm>
                    </template>
                </el-table-column>
            </el-table>
        </div>
        <div class="pagination">
            <el-pagination v-model:current-page="this.searchParams.pageIndex" v-model:page-size="this.searchParams.pageSize"
                :page-sizes="[10, 20, 30, 40]" :small="paginationSmall" layout="total, sizes, prev, pager, next, jumper"
                :total="Number(page.recordCount)" @size-change="handleSizeChange" @current-change="handleCurrentChange" />
        </div>
        <el-drawer v-model="drawerVisible" :with-header="false" :show-close="false" size="40%">
            <div class="title-box">
                <h4 class="title">{{ operate === 1 ? '新增客户' : '修改客户' }}</h4>
                <div class="separator"></div>
            </div>
            <el-form ref="form" :model="form" :rules="rules">
                <el-row>
                    <el-col :span="10">
                        <el-form-item label="客户编码：" prop="customerCode">
                            <el-input v-model="form.customerCode" />
                        </el-form-item>
                    </el-col>
                    <el-col :span="10">
                        <el-form-item label="客户名称：" prop="customerName">
                            <el-input v-model="form.customerName" />
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="10">
                        <el-form-item label="业务员：" prop="employeeCode">
                            <el-input v-model="form.employeeCode" />
                        </el-form-item>
                    </el-col>
                    <el-col :span="10">
                        <el-form-item label="法人：" prop="atrMan">
                            <el-input v-model="form.atrMan" />
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="10">
                        <el-form-item label="联系人：" prop="linkman">
                            <el-input v-model="form.linkman" />
                        </el-form-item>
                    </el-col>
                    <el-col :span="10">
                        <el-form-item label="联系电话：" prop="telephoneCode">
                            <el-input v-model="form.telephoneCode" />
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="10">
                        <el-form-item label="传真号码：" prop="faxCode">
                            <el-input v-model="form.faxCode" />
                        </el-form-item>
                    </el-col>
                    <el-col :span="10">
                        <el-form-item label="邮箱：" prop="email">
                            <el-input v-model="form.email" />
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="10">
                        <el-form-item label="邮政编码：" prop="postCode">
                            <el-input v-model="form.postCode" />
                        </el-form-item>
                    </el-col>
                    <el-col :span="10">
                        <el-form-item label="网址：" prop="url">
                            <el-input v-model="form.url" />
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-form-item label="地址：" prop="adress">
                    <el-input v-model="form.address" />
                </el-form-item>
                <el-row>
                    <el-col :span="10">
                        <el-form-item label="客户等级：" prop="gradeCode">
                            <el-select v-model="form.gradeCode" placeholder="选择等级">
                                <el-option v-for="item in types.grade" :key="item.gradeCode" :label="item.gradeName"
                                    :value="item.gradeCode" />
                            </el-select>
                        </el-form-item>
                    </el-col>
                    <el-col :span="10">
                        <el-form-item label="客户状态：" prop="stateCode">
                            <el-select v-model="form.stateCode" placeholder="选择等级">
                                <el-option v-for="item in types.state" :key="item.stateCode" :label="item.stateName"
                                    :value="item.stateCode" />
                            </el-select>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="10">
                        <el-form-item label="信用等级：" prop="creditCode">
                            <el-select v-model="form.creditCode" placeholder="选择等级">
                                <el-option v-for="item in types.credit" :key="item.creditCode" :label="item.creditName"
                                    :value="item.creditCode" />
                            </el-select>
                        </el-form-item>
                    </el-col>
                    <el-col :span="10">
                        <el-form-item label="行业分类：" prop="tradeCode">
                            <el-select v-model="form.tradeCode" placeholder="选择等级">
                                <el-option v-for="item in types.trade" :key="item.tradeCode" :label="item.tradeName"
                                    :value="item.tradeCode" />
                            </el-select>
                        </el-form-item>
                    </el-col>
                </el-row>

            </el-form>
            <div style="text-align: center; margin: 20px 0;">
                <el-button type="primary" @click="operate === 1 ? confirmAdd() : confirmEdit()">确认</el-button>
                <el-button @click="drawerVisible = false">取消</el-button>
            </div>
        </el-drawer>
    </div>
</template>
<script>
import path from '../../api/path'
import axios from '../../api/instance'
import { Plus, View, Delete, Edit, Operation } from '@element-plus/icons-vue'

export default {
    name: 'user',
    data() {
        return {
            page: {},
            types: {},
            searchParams: {
                pageSize: 10,
                pageIndex: 1,
                CustomerCode: '',
                CustomerName: '',
                Linkman: '',
                TelephoneCode: '',
                Email: '',
                PostCode: '',
                Url: '',
                Address: ''
            },
            paginationSmall: true,
            form: {},
            selectInvenTypeCode: '',
            drawerVisible: false,
            operate: 0,
            rules: {
                customerCode: [{ required: true, message: '请输入客户编号', trigger: 'blur' }],
                customerName: [{ required: true, message: '请输入客户名称', trigger: 'blur' }],
                employeeCode: [{ required: true, message: '请选择业务员', trigger: 'blur' }],
                linkman: [{ required: true, message: '请输入联系人', trigger: 'blur' }],
                telephoneCode: [{ required: true, message: '请输入联系电话', trigger: 'blur' }, { validator: this.validateTelephone, trigger: 'blur' }],
                email: [{ type: 'email', message: '请输入有效的邮箱地址', trigger: 'blur' }]
            }
        }
    },
    created() {
        this.getTypes();
    },
    mounted() {
        this.getList();
    },
    components: {
        Plus, View, Delete, Edit, Operation
    },
    watch: {
        drawerVisible(newValue) {
            if (!newValue) {
                this.form = {};
            }
        }
    },
    methods: {
        getList() {
            let formData = new window.FormData();
            if (this.searchParams.CustomerCode != '') formData.append("CustomerCode", this.searchParams.CustomerCode);
            if (this.searchParams.CustomerName != '') formData.append("CustomerName", this.searchParams.CustomerName);
            if (this.searchParams.Linkman != '') formData.append("Linkman", this.searchParams.Linkman);
            if (this.searchParams.TelephoneCode != '') formData.append("TelephoneCode", this.searchParams.TelephoneCode);
            if (this.searchParams.Email != '') formData.append("Email", this.searchParams.Email);
            if (this.searchParams.PostCode != '') formData.append("PostCode", this.searchParams.PostCode);
            if (this.searchParams.Url != '') formData.append("Url", this.searchParams.Url);
            if (this.searchParams.Address != '') formData.append("Address", this.searchParams.Address);
            axios.post(path.baseUrl + path.customer + '/' + this.searchParams.pageSize + '/' + this.searchParams.pageIndex, formData)
                .then(res => {
                    this.page = res.data.data;
                }).catch(err => {
                    console.log(err);
                })
        },
        getTypes() {
            axios.get(path.baseUrl + path.customer + '/gettype')
                .then(res => {
                    console.log(res.data.data);
                    this.types = res.data.data;
                }).catch(err => {
                    console.log(err);
                })
        },
        getTypeStr(type, code) {
            if (code && this.types) {
                switch (type) {
                    case 1:
                        return this.types.grade?.find(w => w.gradeCode === code).gradeName;
                    case 2:
                        return this.types.state?.find(w => w.stateCode === code).stateName;
                    case 3:
                        return this.types.credit?.find(w => w.creditCode === code).creditName;
                    case 4:
                        return this.types.trade?.find(w => w.tradeCode === code).tradeName;
                    default:
                        return '';
                }
            } else
                return '';

        },
        clickSearchHandle() {
            this.getList();
        },
        handleSizeChange() {
            this.getList();
        },
        handleCurrentChange() {
            this.getList();
        },
        clickAddTopHandle() {
            this.operate = 1;
            this.drawerVisible = true;
        },
        confirmAdd() {
            this.$refs['form'].validate(valid => {
                if (valid) {
                    axios.post(path.baseUrl + path.customer, this.form).then(res => {
                        if (res.data.status) {
                            this.drawerVisible = false;
                            this.getList();
                        }
                    }).catch(err => {
                        console.log(err);
                    });
                }
            });

        },
        clickInfoHandle(data) {
            this.operate = 2;
            this.form = { ...data };
            this.drawerVisible = true;
        },
        clickEditHandle(data) {
            this.operate = 2;
            this.form = { ...data };
            this.drawerVisible = true;
        },
        confirmEdit() {
            this.$refs['form'].validate(valid => {
                if (valid) {
                    axios.put(path.baseUrl + path.customer, this.form).then(res => {
                        if (res.data.status) {
                            this.drawerVisible = false;
                            this.getList();
                        }
                    }).catch(err => {
                        console.log(err);
                    });
                }
            });
        },
        clickDeleteHandle(Id) {
            axios.delete(path.baseUrl + path.customer + '/' + Id)
                .then(res => {
                    this.getList();
                }).catch(err => {
                    console.log(err);
                })
        },
        validateTelephone(rule, value, callback) {
            const reg = /^1[3456789]\d{9}$/;
            if (!reg.test(value)) {
                callback(new Error('请输入有效的联系电话'));
            } else {
                callback();
            }
        }
    }
}
</script>
<style>
.box {
    width: 100%;
    height: calc(100% - 10px);
    background-color: #fff;
    border-radius: 3px;
    padding: 5px;
    box-shadow: 2px 2px 2px #bbbbbb;
}

.operate {
    width: 100%;
    height: 25px;
    background-color: #fcfcfc;
    border-radius: 3px;
    margin-top: 10px;
    margin-bottom: 10px;
    display: flex;
    align-items: center;
    box-shadow: 2px 2px 2px #bbbbbb;
}

.operate-add {
    display: inline;
    margin-left: 10px;
}

.search {
    width: 100%;
    height: 30px;
    display: flex;
}

.search-input {
    width: 150px;
    display: inline;
    margin-right: 5px;
}

.search-but {
    display: inline;
    margin-left: 10px;
}

.table {
    width: 100%;
    height: calc(100% - 110px);
    border-radius: 3px;
    box-shadow: 2px 2px 2px #bbbbbb;
}

.pagination {
    width: 100%;
    height: 30px;
    background-color: #fcfcfc;
    border-radius: 3px;
    margin-top: 10px;
    display: flex;
    align-items: center;
    box-shadow: 2px 2px 2px #bbbbbb;
    position: relative;
}

.pagination .el-pagination {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
}

.el-drawer__body {
    padding: 10px;
}

.title-box {
    display: flex;
    align-items: center;
    justify-content: flex-start;
    border-bottom: 2px solid #cecfd1;
    margin-bottom: 20px;
}

.title {
    margin: 10px 0;
    font-size: 20px;
    font-weight: bold;
}

.separator {
    margin: 10px 0;
    border-top: 2px solid #ccc;
}
</style>