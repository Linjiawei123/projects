<template>
    <div class="box">
        <div class="search">
            <el-input class="search-input" v-model="keyword" placeholder="请输入关键词" />
            <el-button class="search-but" type="primary" @click="clickSearchHandle">查询</el-button>
        </div>
        <div class="operate">
            <el-button class="operate-add" type="info" link @click="clickAddTopHandle"
                v-if="this.$isHasRgihts('Role_Add')"><el-icon>
                    <Plus />
                </el-icon><span>添加</span></el-button>
        </div>
        <div class="tableBox">
            <el-table class="table" :data="list" row-key="id"
                :header-cell-style="{ background: '#eef1f6', color: '#606266' }" stripe>
                <el-table-column label="分类编号" style="width: 30%;">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.invenTypeCode }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="分类名称" style="width: 30%;">
                    <template #default="scope">
                        <div style="display: flex; align-items: center">
                            <span>{{ scope.row.invenTypeName }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column fixed="right" label="操作" style="width: 40%;">
                    <template #default="scope">
                        <el-button link type="warning" size="small" @click="clickEditHandle(scope.row)"
                            v-if="this.$isHasRgihts('Role_Update') && scope.row.isHasInven === false"><el-icon>
                                <Edit />
                            </el-icon></el-button>
                        <el-popconfirm title="确定要删除该分类？" icon="WarningFilled" icon-color="#ef4136"
                            @confirm="clickDeleteHandle(scope.row.id)"
                            v-if="this.$isHasRgihts('Role_Delete') && scope.row.isHasInven === false">
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
        <el-drawer v-model="drawerVisible" :with-header="false" :show-close="false" size="30%">
            <div class="title-box">
                <h4 class="title">{{ operate === 1 ? '新增分类' : '修改分类' }}</h4>
                <div class="separator"></div>
            </div>
            <el-form ref="form" :model="form" :rules="rules" :label-width="100">
                <el-form-item label="分类编号：" prop="invenTypeCode">
                    <el-input v-model="form.invenTypeCode" />
                </el-form-item>
                <el-form-item label="分类名称：" prop="invenTypeName">
                    <el-input v-model="form.invenTypeName" />
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
import path from '../../../api/path'
import axios from '../../../api/instance'
import { Plus, View, Delete, Edit } from '@element-plus/icons-vue'

export default {
    name: 'role',
    data() {
        return {
            list: [],
            form: {},
            keyword: '',
            drawerVisible: false,
            operate: 0,
            rules: {
                invenTypeCode: [{ required: true, message: '请输入分类编号', trigger: 'blur' }],
                invenTypeName: [{ required: true, message: '请输入分类名称', trigger: 'blur' }]
            }
        }
    },
    mounted() {
        this.getList();
    },
    components: {
        Plus, View, Delete, Edit
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
            axios.get(path.baseUrl + path.inventoryType)
                .then(res => {
                    this.list = res.data.data;
                }).catch(err => {
                    console.log(err);
                })
        },
        clickSearchHandle() {
            this.getList();
        },
        clickAddTopHandle() {
            this.operate = 1;
            this.drawerVisible = true;
        },
        confirmAdd() {
            this.$refs['form'].validate(valid => {
                if (valid) {
                    axios.post(path.baseUrl + path.inventoryType, this.form).then(res => {
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
        clickEditHandle(data) {
            this.operate = 2;
            this.form = { ...data };
            this.drawerVisible = true;
        },
        confirmEdit() {
            this.$refs['form'].validate(valid => {
                if (valid) {
                    axios.put(path.baseUrl + path.inventoryType, this.form).then(res => {
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
            axios.delete(path.baseUrl + path.inventoryType + '/' + Id)
                .then(res => {
                    this.getList();
                }).catch(err => {
                    console.log(err);
                })
        },
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
    box-shadow: 2px 2px 2px #cfcfcf;
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