<template>
    <div class="popup-mask">
        <div class="popup-wrapper">
            <div class="popup-header">
                <span class="popup-title">标题</span>
                <el-button class="popup-close" size="small" link @click="close"><el-icon>
                        <CircleClose />
                    </el-icon></el-button>
            </div>
            <div class="popup-content">
            </div>
        </div>
    </div>
</template>
  
<script>
import axios from '../../../api/instance'
import path from '../../../api/path'
import { CircleClose } from '@element-plus/icons-vue'

export default {
    name: 'Popup',
    data() {
        return {
        }
    },
    props: {
        form: [Number, Object],
    },
    components: {
        CircleClose
    },
    created() {
    },

    methods: {
        close() {
            this.$emit("change", false);
        }
    }
};
</script>
  
<style scoped>
.popup-mask {
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    z-index: 999;
    background-color: rgba(0, 0, 0, 0.5);
}

.popup-wrapper {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    background-color: #fff;
    width: 80%;
    max-width: 400px;
    border-radius: 4px;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
}

.popup-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 10px;
    border-bottom: 1px solid #eee;
}

.popup-title {
    font-size: 16px;
    font-weight: bold;
}

.popup-close {
    font-size: 20px;
    cursor: pointer;
}

.popup-content {
    padding: 10px;
}

</style>