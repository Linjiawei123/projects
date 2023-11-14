<template>
    <div class="box">
        <div class="search">
            <el-cascader class="search-input" v-model="this.searchParams.departmentId" :options="departments"
                :props="defaultParams" clearable placeholder="请选择" autocomplete="off" @change="getParent" empty
                ref="cascaderAddr" />
            <el-input class="search-input" v-model="this.searchParams.name" placeholder="请输入关键词" />
            <el-button class="search-but" type="primary" @click="clickSearchHandle">查询</el-button>
        </div>
        <div class="operate">
            <el-button class="operate-add" type="info" link @click="clickAddTopHandle" v-if="this.$isHasRgihts('Employee_Add')"><el-icon>
                    <Plus />
                </el-icon><span>添加</span></el-button>
        </div>
        <div class="tableBox">
            <el-table class="table" :data="page.pageData" row-key="id"
                :header-cell-style="{ background: '#eef1f6', color: '#606266' }" stripe>
                <el-table-column label="所属部门" min-width="10%">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.department }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="名称" min-width="10%">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.name }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="编号" min-width="10%">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.code }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="性别" min-width="5%">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.sex }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="生日" min-width="10%">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.birthday }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="身份证" min-width="15%">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.identityCard }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="入职日期" min-width="10%">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.hireDate }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="职位" min-width="10%">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.job }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="用户账号" min-width="10%">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.account }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column fixed="right" label="操作" min-width="10%">
                    <template #default="scope">
                        <el-button link type="info" size="small" @click="clickInfoHandle(scope.row)" v-if="this.$isHasRgihts('Employee_View')"><el-icon>
                                <View />
                            </el-icon></el-button>
                        <el-button link type="warning" size="small" @click="clickEditHandle(scope.row)" v-if="this.$isHasRgihts('Employee_Update')"><el-icon>
                                <Edit />
                            </el-icon></el-button>
                        <el-popconfirm title="确定要删除该用户？" icon="WarningFilled" icon-color="#ef4136"
                            @confirm="clickDeleteHandle(scope.row.id)" v-if="this.$isHasRgihts('Employee_Delete')">
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
        <userForm v-if="showForm" :form="form" @closeForm="closeForm"></userForm>
    </div>
</template>
<script>
import { toRaw } from 'vue'
import path from '../../../api/path'
import axios from '../../../api/instance'
import userForm from './Form.vue'
import { Plus, View, Delete, Edit, Operation } from '@element-plus/icons-vue'

export default {
    name: 'employee',
    data() {
        return {
            page: {},
            departments: [],
            searchParams: {
                pageSize: 10,
                pageIndex: 1,
                departmentId: '',
                name: ''
            },
            paginationSmall: true,
            showForm: false,
            select: '',
            defaultParams: {
                label: 'name',
                value: 'id',
                children: 'child',
                checkStrictly: true
            }
        }
    },
    mounted() {
        this.getList();
        this.getDepartments();
    },
    components: {
        Plus, View, Delete, Edit, Operation, userForm
    },
    methods: {
        getList() {
            let formData = new window.FormData();
            formData.append("departmentId", this.select);
            formData.append("name", this.searchParams.name);
            axios.post(path.baseUrl + path.employee + '/' + this.searchParams.pageSize + '/' + this.searchParams.pageIndex, formData)
                .then(res => {
                    this.page = res.data.data;
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
        getParent(data) {
            if (data != null) {
                const arr = toRaw(data);
                this.select = arr[arr.length - 1];
            } else
                this.select = '';
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
            this.openForm({ operate: 0 });
        },
        clickInfoHandle(data) {
            this.openForm({ operate: 1, ...data });
        },
        clickEditHandle(data) {
            this.openForm({ operate: 2, ...data });
        },
        clickDeleteHandle(Id) {
            axios.delete(path.baseUrl + path.employee + '/' + Id)
                .then(res => {
                    this.getList();
                }).catch(err => {
                    console.log(err);
                })
        },
        openForm(params) {
            this.showForm = true;
            this.form = params;
        },
        closeForm(param) {
            this.showForm = param;
            this.getList();
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
    width: 200px;
    display: inline;
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
</style>