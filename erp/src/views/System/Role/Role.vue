<template>
    <div class="box">
        <div class="search">
            <el-input class="search-input" v-model="this.searchParams.keyword" placeholder="请输入关键词" />
            <el-button class="search-but" type="primary" @click="clickSearchHandle">查询</el-button>
        </div>
        <div class="operate">
            <el-button class="operate-add" type="info" link @click="clickAddTopHandle" v-if="this.$isHasRgihts('Role_Add')"><el-icon>
                    <Plus />
                </el-icon><span>添加</span></el-button>
        </div>
        <div class="tableBox">
            <el-table class="table" :data="page.pageData" row-key="id"
                :header-cell-style="{ background: '#eef1f6', color: '#606266' }" stripe>
                <el-table-column label="角色编号" style="width: 20%;">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.code }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="角色名称" style="width: 20%;">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.name }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="角色状态" style="width: 20%;">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <el-icon v-if="scope.row.status" style="color: green;"><Select /></el-icon>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column fixed="right" label="操作" style="width: 20%;">
                    <template #default="scope">
                        <el-button link type="info" size="small" @click="clickInfoHandle(scope.row)" v-if="this.$isHasRgihts('Role_View')"><el-icon>
                                <View />
                            </el-icon></el-button>
                        <el-button link type="warning" size="small" @click="clickEditHandle(scope.row)" v-if="this.$isHasRgihts('Role_Update')"><el-icon>
                                <Edit />
                            </el-icon></el-button>
                        <el-button link type="success" size="small" @click="clickRoleUserHandle(scope.row)" v-if="this.$isHasRgihts('Role_Update')"><el-icon>
                                <User />
                            </el-icon></el-button>
                        <el-popconfirm title="确定要删除该角色组？" icon="WarningFilled" icon-color="#ef4136"
                            @confirm="clickDeleteHandle(scope.row.id)" v-if="this.$isHasRgihts('Role_Delete')">
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
        <roleForm v-if="showForm" :form="form" @closeForm="closeForm"></roleForm>
        <roleUser v-if="showRoleUser" :roledata="role" @closeRoleUser="closeRoleUser"></roleUser>
    </div>
</template>
<script>
import path from '../../../api/path'
import axios from '../../../api/instance'
import roleForm from './Form.vue'
import roleUser from './User.vue'
import { Plus, View, Delete, Edit, User } from '@element-plus/icons-vue'

export default {
    name: 'role',
    data() {
        return {
            page: {},
            searchParams: {
                pageSize: 10,
                pageIndex: 1,
                keyword: ''
            },
            paginationSmall: true,
            showForm: false,
            showRoleUser: false
        }
    },
    mounted() {
        this.getList();
    },
    components: {
        Plus, View, Delete, Edit, User, roleForm, roleUser
    },
    methods: {
        getList() {
            let formData = new window.FormData();
            formData.append("Keyword", this.searchParams.keyword);
            axios.post(path.baseUrl + path.role + '/' + this.searchParams.pageSize + '/' + this.searchParams.pageIndex, formData)
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
            this.openForm({ operate: 0 });
        },
        clickInfoHandle(data) {
            this.openForm({ operate: 1, ...data });
        },
        clickEditHandle(data) {
            this.openForm({ operate: 2, ...data });
        },
        clickRoleUserHandle(data){
            this.showRoleUser = true;
            this.role = {...data};
        },
        clickDeleteHandle(Id) {
            axios.delete(path.baseUrl + path.role + '/' + Id)
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
        },
        closeRoleUser(){
            this.showRoleUser = false;
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