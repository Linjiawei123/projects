<template>
 <el-config-provider :locale="zhCn">
      <router-view />
    </el-config-provider>
</template>
<script setup>
import { ElConfigProvider } from "element-plus"
import zhCn from "element-plus/lib/locale/lang/zh-cn"

  // zhCn.el.pagination = {
  //   pagesize: '条每页',
  //   total: '共 {total}条',
  //   goto: '跳到',
  //   pageClassifier: ''
  // }
</script>
<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

.el-table th.gutter{ 
    
    display: table-cell!important; 
}

body .el-table colgroup.gutter{
    display: table-cell!important;
}

.el-form-item__label{
  padding: 0px 5px 0px 0px;
}

.el-col{
  margin-left: 5px;
}
</style>
