<template>
    <div class="box">
        <div class="search">
            <el-input class="search-input" v-model="this.searchParams.InvenCode" placeholder="存货编码" clearable />
            <el-input class="search-input" v-model="this.searchParams.InvenName" placeholder="存货名称" clearable />
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
                <el-table-column fixed label="存货编码" style="width: 30%;">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.invenCode }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="存货名称" style="width: 20%;">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.invenName }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="存货分类" style="width: 20%;">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.invenTypeName }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="规格型号" style="width: 20%;">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.specsModel }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="计量单位" style="width: 20%;">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.meaUnit }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="参考售价" style="width: 20%;">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.selPrice }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="参考进价" style="width: 20%;">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.purPrice }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="最低库存" style="width: 20%;">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.smallStockNum }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="最高库存" style="width: 20%;">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.bigStockNum }}</span>
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
                        <el-popconfirm title="确定要删除该存货？" icon="WarningFilled" icon-color="#ef4136"
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
                <h4 class="title">{{ operate === 1 ? '新增存货' : '修改存货' }}</h4>
                <div class="separator"></div>
            </div>
            <el-form ref="form" :model="form" :rules="rules" :label-width="100">
                <el-row>
                    <el-col :span="10">
                        <el-form-item label="存货编码：" prop="invenCode">
                            <el-input v-model="form.invenCode" />
                        </el-form-item>
                    </el-col>
                    <el-col :span="10">
                        <el-form-item label="存货名称：" prop="invenName">
                            <el-input v-model="form.invenName" />
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="10">
                        <el-form-item label="存货分类：" prop="invenTypeCode">
                            <el-select v-model="form.invenTypeCode" class="m-2" placeholder="选择分类" :style="{ width: '100%' }">
                                <el-option v-for="item in types" :key="item.invenTypeCode" :label="item.invenTypeName"
                                    :value="item.invenTypeCode" />
                            </el-select>
                        </el-form-item>
                    </el-col>
                    <el-col :span="10">
                        <el-form-item label="规格型号：" prop="specsModel">
                            <el-input v-model="form.specsModel" />
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="10">
                        <el-form-item label="计量单位：" prop="meaUnit">
                            <el-input v-model="form.meaUnit" />
                        </el-form-item>
                    </el-col>
                    <el-col :span="10">
                        <el-form-item label="参考售价：" prop="selPrice">
                            <el-input v-model="form.selPrice" />
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="10">
                        <el-form-item label="参考售价：" prop="selPrice">
                            <el-input v-model="form.selPrice" />
                        </el-form-item>
                    </el-col>
                    <el-col :span="10">
                        <el-form-item label="参考进价：" prop="purPrice">
                            <el-input v-model="form.purPrice" />
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="10">
                        <el-form-item label="最低库存：" prop="smallStockNum">
                            <el-input v-model="form.smallStockNum" />
                        </el-form-item>
                    </el-col>
                    <el-col :span="10">
                        <el-form-item label="最高库存：" prop="bigStockNum">
                            <el-input v-model="form.bigStockNum" />
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
import path from '../../../api/path'
import axios from '../../../api/instance'
import { Plus, View, Delete, Edit, Operation } from '@element-plus/icons-vue'

export default {
    name: 'user',
    data() {
        return {
            page: {},
            types: [],
            searchParams: {
                pageSize: 10,
                pageIndex: 1,
                InvenCode: '',
                InvenName: ''
            },
            paginationSmall: true,
            form: {},
            selectInvenTypeCode: '',
            drawerVisible: false,
            operate: 0,
            rules: {
                invenCode: [{ required: true, message: '请输入存货编号', trigger: 'blur' }],
                invenName: [{ required: true, message: '请输入存货名称', trigger: 'blur' }],
                invenTypeCode: [{ required: true, message: '请选择存货类别', trigger: 'blur' }],
                purPrice: [{
                    validator: (rule, value, callback) => {
                        if (value !== null) {
                            const regex = /^[0-9]+(\.[0-9]{1,2})?$/;
                            if (!regex.test(value)) {
                                callback(new Error('请输入有效的数字，最多保留两位小数'));
                            }
                        }
                        callback();
                    },
                    trigger: 'blur'
                }],
                selPrice: [{
                    validator: (rule, value, callback) => {
                        if (value !== null) {
                            const regex = /^[0-9]+(\.[0-9]{1,2})?$/;
                            if (!regex.test(value)) {
                                callback(new Error('请输入有效的数字，最多保留两位小数'));
                            } else if (value !== '' && this.form.value1 !== '') {
                                const floatValue1 = parseFloat(this.form.value1);
                                const floatValue2 = parseFloat(value);
                                if (floatValue2 > floatValue1) {
                                    callback(new Error('参考售价不能低于参考进价，请重新输入！'));
                                }
                            }
                        }
                        callback();
                    },
                    trigger: 'blur'
                }],
                bigStockNum: [{
                    validator: (rule, value, callback) => {
                        if (value !== null) {
                            const regex = /^[0-9]+$/;
                            if (!regex.test(value)) {
                                callback(new Error('请输入整数'));
                            }
                        }
                        callback();
                    },
                    trigger: 'blur'
                }],
                smallStockNum: [{
                    validator: (rule, value, callback) => {
                        if (value !== null) {
                            const regex = /^[0-9]+$/;
                            if (!regex.test(value)) {
                                callback(new Error('请输入整数'));
                            } else if (value !== '' && this.form.value1 !== '') {
                                const floatValue1 = parseFloat(this.form.value1);
                                const floatValue2 = parseFloat(value);
                                if (floatValue2 > floatValue1) {
                                    callback(new Error('最低库存不许大于最高库存！'));
                                }
                            }
                        }
                        callback();
                    },
                    trigger: 'blur'
                }]
            }
        }
    },
    mounted() {
        this.getList();
        this.getTypes();
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
            if (this.searchParams.InvenCode != '') formData.append("InvenCode", this.searchParams.InvenCode);
            if (this.searchParams.InvenName != '') formData.append("InvenName", this.searchParams.InvenName);
            axios.post(path.baseUrl + path.inven + '/' + this.searchParams.pageSize + '/' + this.searchParams.pageIndex, formData)
                .then(res => {
                    this.page = res.data.data;
                }).catch(err => {
                    console.log(err);
                })
        },
        getTypes() {
            axios.get(path.baseUrl + path.inven + '/gettype')
                .then(res => {
                    this.types = res.data.data;
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
                    axios.post(path.baseUrl + path.inven, this.form).then(res => {
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
                    axios.put(path.baseUrl + path.inven, this.form).then(res => {
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
            axios.delete(path.baseUrl + path.inven + '/' + Id)
                .then(res => {
                    this.getList();
                }).catch(err => {
                    console.log(err);
                })
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