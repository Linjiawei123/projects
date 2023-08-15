<template>
    <div class="box">
        <div class="search">
            <el-input class="search-input" v-model="this.searchParams.account" placeholder="请输入账号" />
            <el-input class="search-input" v-model="this.searchParams.name" placeholder="请输入昵称" />
            <el-date-picker class="search-input" v-model="this.searchParams.beginTime" type="datetime"
                value-format="YYYY-MM-DD HH:mm:ss" placeholder="请选择开始时间" />
            <el-date-picker class="search-input" v-model="this.searchParams.endTime" type="datetime"
                value-format="YYYY-MM-DD HH:mm:ss" placeholder="请选择结束时间" />
            <el-button class="search-but" type="primary" @click="clickSearchHandle">查询</el-button>
        </div>
        <div class="operate"></div>
        <div class="tableBox">
            <el-table class="table" :data="page.pageData" row-key="id"
                :header-cell-style="{ background: '#eef1f6', color: '#606266' }" stripe>
                <el-table-column label="用户账号" style="width: 20%;">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.account }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="用户昵称" style="width: 20%;">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.name }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="登录时间" style="width: 20%;">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ getTimeStr(scope.row.loginTime) }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="登录IP" style="width: 20%;">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.loginIP }}</span>
                        </div>
                    </template>
                </el-table-column>
            </el-table>
        </div>
        <div class="pagination">
            <el-pagination v-model:current-page="this.searchParams.pageIndex" v-model:page-size="this.searchParams.pageSize"
                :page-sizes="[10, 20, 30, 40]" :small="paginationSmall" layout="total, sizes, prev, pager, next, jumper"
                :total="Number(page.recordCount)" @size-change="handleSizeChange" @current-change="handleCurrentChange" />
        </div>
    </div>
</template>
<script>
import path from '../../../api/path'
import axios from '../../../api/instance'

export default {
    name: 'loginlog',
    data() {
        return {
            page: {},
            searchParams: {
                pageSize: 10,
                pageIndex: 1,
                account: '',
                name: '',
                beginTime: '',
                endTime: ''
            },
            paginationSmall: true
        }
    },
    mounted() {
        this.getList();
    },
    components: {
    },
    methods: {
        getList() {
            let formData = new window.FormData();
            formData.append("account", this.searchParams.account);
            formData.append("name", this.searchParams.name);
            if (this.searchParams.beginTime != null && this.searchParams.beginTime != '')
                formData.append("beginTime", this.searchParams.beginTime);
            if (this.searchParams.endTime != null && this.searchParams.endTime != '')
                formData.append("endTime", this.searchParams.endTime);
            axios.post(path.baseUrl + path.logs + '/LoginLog/' + this.searchParams.pageSize + '/' + this.searchParams.pageIndex, formData)
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
        getTimeStr(data) {
            var date = new Date(data);
            var year = date.getFullYear();
            var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
            var day = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
            var hours = date.getHours() < 10 ? "0" + date.getHours() : date.getHours();
            var minutes = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
            var seconds = date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds();
            return year + "-" + month + "-" + day + " " + hours + ":" + minutes + ":" + seconds;
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
    margin-right: 5px;
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