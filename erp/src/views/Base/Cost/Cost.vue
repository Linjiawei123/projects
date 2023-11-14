<template>
    <div class="box">
        <div class="search">
            <el-input class="search-input" v-model="this.searchParams.CostCode" placeholder="费用编号" clearable />
            <el-input class="search-input" v-model="this.searchParams.CostName" placeholder="费用名称" clearable />
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
                <el-table-column fixed label="费用编号">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.costCode }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="费用名称">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.costName }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="费用分类">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.costTypeName }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="备注">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.remark }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column fixed="right" label="操作">
                    <template #default="scope">
                        <!-- <el-button link type="info" size="small" @click="clickInfoHandle(scope.row)" v-if="this.$isHasRgihts('User_View')"><el-icon>
                                <View />
                            </el-icon></el-button> -->
                        <el-button link type="warning" size="small" @click="clickEditHandle(scope.row)"
                            v-if="this.$isHasRgihts('User_Update')"><el-icon>
                                <Edit />
                            </el-icon></el-button>
                        <el-popconfirm title="确定要删除该费用？" icon="WarningFilled" icon-color="#ef4136"
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
                <h4 class="title">{{ operate === 1 ? '新增费用' : '修改费用' }}</h4>
                <div class="separator"></div>
            </div>
            <el-form ref="form" :model="form" :rules="rules" :label-width="100">
                <el-row>
                    <el-col :span="10">
                        <el-form-item label="费用编号：" prop="costCode">
                            <el-input v-model="form.costCode" />
                        </el-form-item>
                    </el-col>
                    <el-col :span="10">
                        <el-form-item label="费用名称：" prop="costName">
                            <el-input v-model="form.costName" />
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="10">
                        <el-form-item label="费用分类：" prop="costTypeCode">
                            <el-select v-model="form.costTypeCode" class="m-2" placeholder="选择分类" :style="{ width: '100%' }">
                                <el-option v-for="item in types" :key="item.costTypeCode" :label="item.costTypeName"
                                    :value="item.costTypeCode" />
                            </el-select>
                        </el-form-item>
                    </el-col>
                    <el-col :span="10">
                        <el-form-item label="备注：" prop="remark">
                            <el-input v-model="form.remark" />
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
                CostCode: '',
                CostName: ''
            },
            paginationSmall: true,
            form: {},
            selectcostTypeCode: '',
            drawerVisible: false,
            operate: 0,
            rules: {
                costCode: [{ required: true, message: '请输入费用编号', trigger: 'blur' }],
                costName: [{ required: true, message: '请输入费用名称', trigger: 'blur' }],
                costTypeCode: [{ required: true, message: '请选择费用类别', trigger: 'blur' }]
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
            if (this.searchParams.CostCode != '') formData.append("CostCode", this.searchParams.CostCode);
            if (this.searchParams.CostName != '') formData.append("CostName", this.searchParams.CostName);
            axios.post(path.baseUrl + path.cost + '/' + this.searchParams.pageSize + '/' + this.searchParams.pageIndex, formData)
                .then(res => {
                    this.page = res.data.data;
                }).catch(err => {
                    console.log(err);
                })
        },
        getTypes() {
            axios.get(path.baseUrl + path.cost + '/gettype')
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
                    axios.post(path.baseUrl + path.cost, this.form).then(res => {
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
                    axios.put(path.baseUrl + path.cost, this.form).then(res => {
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
            axios.delete(path.baseUrl + path.cost + '/' + Id)
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