<template>
    <div class="box">
        <div class="search">
            <el-input class="search-input" v-model="this.searchParams.SupplierName" placeholder="供应商编码" clearable />
            <el-input class="search-input" v-model="this.searchParams.SupplierName" placeholder="供应商名称" clearable />
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
                <el-table-column fixed label="供应商编码" style="width: 30%;">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.supplierCode }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column fixed label="供应商名称" style="width: 20%;">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.supplierName }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="联系人" style="width: 20%;">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.linkman }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="联系电话" style="width: 20%;">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.telephoneCode }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="邮箱" style="width: 20%;">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.email }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="邮政编码" style="width: 20%;">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.postCode }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="网址" style="width: 20%;">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.url }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="地址" style="width: 20%;">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.address }}</span>
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
                        <el-popconfirm title="确定要删除该供应商？" icon="WarningFilled" icon-color="#ef4136"
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
                <h4 class="title">{{ operate === 1 ? '新增供应商' : '修改供应商' }}</h4>
                <div class="separator"></div>
            </div>
            <el-form ref="form" :model="form" :rules="rules" :label-width="100">
                <el-form-item label="供应商编码：" prop="supplierCode">
                    <el-input v-model="form.supplierCode" />
                </el-form-item>
                <el-form-item label="供应商名称：" prop="supplierName">
                    <el-input v-model="form.supplierName" />
                </el-form-item>
                <el-form-item label="联系人：" prop="linkman">
                    <el-input v-model="form.linkman" />
                </el-form-item>
                <el-form-item label="联系电话：" prop="telephoneCode">
                    <el-input v-model="form.telephoneCode" />
                </el-form-item>
                <el-form-item label="邮箱：" prop="email">
                    <el-input v-model="form.email" />
                </el-form-item>
                <el-form-item label="邮政编码：" prop="postCode">
                    <el-input v-model="form.postCode" />
                </el-form-item>
                <el-form-item label="网址：" prop="url">
                    <el-input v-model="form.url" />
                </el-form-item>
                <el-form-item label="地址：" prop="adress">
                    <el-input v-model="form.address" />
                </el-form-item>
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
            searchParams: {
                pageSize: 10,
                pageIndex: 1,
                SupplierCode: '',
                SupplierName: ''
            },
            paginationSmall: true,
            form: {},
            selectInvenTypeCode: '',
            drawerVisible: false,
            operate: 0,
            rules: {
                supplierCode: [{ required: true, message: '请输入供应商编号', trigger: 'blur' }],
                supplierName: [{ required: true, message: '请输入供应商名称', trigger: 'blur' }],
                linkman: [{ required: true, message: '请输入联系人', trigger: 'blur' }],
                telephoneCode: [{ required: true, message: '请输入联系电话', trigger: 'blur' }, { validator: this.validateTelephone, trigger: 'blur' }],
                email: [{ type: 'email', message: '请输入有效的邮箱地址', trigger: 'blur' }]
            }
        }
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
            if (this.searchParams.SupplierCode != '') formData.append("SupplierCode", this.searchParams.SupplierCode);
            if (this.searchParams.SupplierName != '') formData.append("SupplierName", this.searchParams.SupplierName);
            axios.post(path.baseUrl + path.supplier + '/' + this.searchParams.pageSize + '/' + this.searchParams.pageIndex, formData)
                .then(res => {
                    this.page = res.data.data;
                }).catch(err => {
                    console.log(err);
                })
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
                    axios.post(path.baseUrl + path.supplier, this.form).then(res => {
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
                    axios.put(path.baseUrl + path.supplier, this.form).then(res => {
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
            axios.delete(path.baseUrl + path.supplier + '/' + Id)
                .then(res => {
                    this.getList();
                }).catch(err => {
                    console.log(err);
                })
        },
        validateTelephone(rule, value, callback) {
            const reg = /^1[3456789]\d{9}$/; // 更具实际需求调整正则表达式
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