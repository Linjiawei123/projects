<template>
    <div>
        <div class="button-container">
            <el-button type="primary">新增</el-button>
            <el-button type="danger">删除</el-button>
            <el-button type="warning">修改</el-button>
            <el-button type="info">审计</el-button>
            <el-button type="primary">查询</el-button>
        </div>
        <div class="query-container">
            <!-- 在这里放置多个查询框 -->
        </div>
        <el-table :data="tableData" border stripe>
            <!-- 在这里定义表格的列 -->
        </el-table>
        <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange" :page-sizes="[10, 20, 50, 100]"
            layout="total, sizes, prev, pager, next" :total="total"></el-pagination>
    </div>
</template>
  
<script>
import { ref } from 'vue';

export default {
    data() {
        return {
            tableData: [],
            total: 0,
        };
    },
    methods: {
        // 处理每页显示数量的变更
        handleSizeChange(size) {
            console.log(size);
            // 更新数据
        },
        // 处理当前页码的变更
        handleCurrentChange(page) {
            console.log(page);
            // 更新数据
        },
    },
};
</script>
  
<style>
.button-container {
    margin-bottom: 20px;
}

.query-container {
    margin-bottom: 20px;
}
</style>