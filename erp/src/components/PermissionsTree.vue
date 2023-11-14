<template>
    <div class="treeBox">
        <el-tree v-cloak :data="treeData" :props="defaultProps" node-key="id" default-expand-all highlight-current
            :expand-on-click-node="false">
            <template #default="{ node, data }">
                <div class="custom-tree-node">
                    <span class="menu">{{ node.label }}</span>
                    <span class="checkbox">
                        <el-checkbox v-if="data.rights.length > 0" v-for="item in data.rights" :key="item.id"
                            :label="item.name" :disabled="this.operate === 1" v-model="item.isSelect"
                            @change="chooseItem($event, item.id)" />
                    </span>
                </div>
            </template>
        </el-tree>
    </div>
</template>
<script>
import path from '../api/path'
import axios from '../api/instance'

export default {
    name: 'PermissionsTree',
    data() {
        return {
            operate: 0,
            loading: false,
            treeData: [],
            checkedArr: Array(),
            defaultProps: {
                children: 'child',
                label: 'name',
                rights: 'rights'
            }
        }
    },
    props: {
        tree: [Number, Object],
    },
    created() {
        this.init();
    },
    methods: {
        init(param) {
            let treeDefault = { ...this.tree };
            this.operate = treeDefault.operate;
            if (treeDefault.rights)
                this.checkedArr = this.checkedArr.concat(treeDefault.rights);
            this.getMenuRights();
            
        },
        getMenuRights() {
            axios.post(path.baseUrl + path.menuRights, {
                ids: this.checkedArr
            }).then(res => {
                this.treeData = res.data.data;
                this.$emit("changeRights", this.checkedArr);
            }).catch(err => {
                console.log(err);
            })
        },

        // 获取权限id
        chooseItem(event, id) {
            // 如果是选中
            if (event) {
                // 把选中的id存入数组
                this.checkedArr.push(id);
                this.$emit("changeRights", this.checkedArr);
            } else {
                //如果是取消选中则从数组中删除该id
                this.checkedArr.splice(this.checkedArr.indexOf(id), 1);
                this.$emit("changeRights", this.checkedArr);
            }

        },
    }
}
</script>
<style>
.treeBox {
    width: calc(100% - 10px);
    max-width: 900px;
    min-height: 300px;
    max-height: 500px;
    margin-top: 10px;
    margin-bottom: 10px;
    border-radius: 3px;
    padding: 5px;
    border: 1px solid #bbbbbb;
    overflow: auto
}

:deep(.el-tree) {
    width: 100%;
}

:deep(.el-tree-node__contente) {
    width: 100%;
}

/* 移入树形 */
.el-tree-node__content:hover {
    background: rgba(238, 238, 238, 0.3) !important;
}

/* 选中当前node的样式 */
.el-tree--highlight-current .el-tree-node.is-current>.el-tree-node__content {
    background: rgba(246, 246, 246, 0.5) !important;
    color: rgb(91, 90, 90) !important;
}

.custom-tree-node {
    width: 100%;
    position: relative;
}

.menu {
    float: left;
    position: absolute;
    top: 50%;
    left: 0;
    transform: translateY(-50%);
}

.checkbox {
    float: right;
    margin-left: 60px;
}

.el-checkbox{
    margin-right: 6px;
}

[v-cloak] {
    display: none;
}
</style>