<template>
    <div class="page">
        <div class="search">
            <el-input class="search-input" v-model="input" placeholder="请输入关键词" />
            <el-button class="search-but" type="primary" @click="clickSearchHandle">查询</el-button>
        </div>
        <div class="operate">
            <el-button class="operate-add" type="info" link @click="clickAddTopHandle" v-if="this.$isHasRgihts('Department_Add')"><el-icon>
                    <Plus />
                </el-icon><span>添加</span></el-button>
        </div>
        <div class="tableBox">
            <el-table class="table" :data="list" row-key="id"
                :header-cell-style="{ background: '#eef1f6', color: '#606266' }" stripe :tree-props="{ children: 'child' }">
                <el-table-column fixed label="部门名称" style="width: 30%;">
                    <template #default="scope">
                        <div style="display: inline; align-items: center">
                            <span style="margin-left: 10px">{{ scope.row.name }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="部门编号" style="width: 20%;">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.code }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column fixed="right" label="操作" style="width: 20%;">
                    <template #default="scope">
                        <el-button link type="primary" size="small" @click="clickAddHandle(scope.row)" v-if="this.$isHasRgihts('Department_Add')"><el-icon>
                                <CirclePlus />
                            </el-icon></el-button>
                        <el-button link type="info" size="small" @click="clickInfoHandle(scope.row)" v-if="this.$isHasRgihts('Department_View')"><el-icon>
                                <View />
                            </el-icon></el-button>
                        <el-button link type="warning" size="small" @click="clickEditHandle(scope.row)" v-if="this.$isHasRgihts('Department_Update')"><el-icon>
                                <Edit />
                            </el-icon></el-button>
                        <el-popconfirm title="确定要删除该部门？" icon="WarningFilled" icon-color="#ef4136"
                            @confirm="clickDeleteHandle(scope.row.id)" v-if="this.$isHasRgihts('Department_Delete')">
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
        <Form v-if="showPopup" :propform="form" @change="closePopup"></Form>
    </div>
</template>
<script>

import Form from './Form.vue'
import path from '../../../api/path'
import axios from '../../../api/instance'
import { Plus, CirclePlus, View, Delete, Edit, Select, Operation } from '@element-plus/icons-vue'

export default {
    name: 'Department',
    data() {
        return {
            input: '',
            list: [],
            form: {},
            showPopup: false
        }
    },
    components: {
        Plus,
        View,
        Delete,
        Edit,
        Select,
        CirclePlus,
        Operation,
        Form
    },
    mounted() {
        this.getAllData();
    },
    methods: {
        getAllData() {
            axios.get(path.baseUrl + path.department + '?Name=' + this.input)
                .then(res => {
                    this.list = res.data.data
                }).catch(err => {
                    console.log(err);
                })
        },
        clickSearchHandle() {
            this.getAllData();
        },
        clickAddTopHandle() {
            this.openPopup({ operate: 4 });
        },
        clickAddHandle(data) {
            this.openPopup({ operate: 0, ...data });
        },
        clickInfoHandle(data) {
            this.openPopup({ operate: 1, ...data });
        },
        clickEditHandle(data) {
            this.openPopup({ operate: 2, ...data });
        },
        clickDeleteHandle(Id) {
            axios.delete(path.baseUrl + path.department + '/' + Id)
                .then(res => {
                    this.getAllData();
                }).catch(err => {
                    console.log(err);
                })
        },
        openPopup(param) {
            this.showPopup = true;
            this.form = param;
        },
        closePopup(param) {
            this.showPopup = param;
            this.getAllData();
        }
    }
}
</script>
<style scoped>
.page {
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
    height: 40px;
    background-color: #fff;
    border-radius: 3px;
    margin-top: 10px;
    display: flex;
    align-items: center;
    box-shadow: 2px 2px 2px #bbbbbb;
    position: relative;
}
</style>