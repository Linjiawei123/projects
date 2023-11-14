<template>
    <div class="popup-mask">
        <div class="popup-wrapper">
            <div class="popup-header">
                <span class="popup-title">提示：{{ tip }}</span>
                <div class="popup-but">
                    <el-button size="small" link @click="close"><el-icon>
                            <CircleClose />
                        </el-icon></el-button>
                </div>
            </div>
            <span class="popup-content">
                <el-button type="danger" @click="clickDeleteHandle">确认</el-button>
                <el-button type="info" @click="close">取消</el-button>
            </span>
        </div>
    </div>
</template>
  
<script>
import axios from '../../../api/instance'
import path from '../../../api/path'
import { CircleClose } from '@element-plus/icons-vue'

export default {
    name: 'PermissionsDelTips',
    data() {
        return {
            id: '',
            tip: ''
        }
    },
    props: {
        tipData: [String, String]
    },
    components: {
        CircleClose
    },

    created() {
        this.init();
    },

    methods: {
        init(param) {
            this.id = this.tipData.id;
            this.tip = this.tipData.tip;
        },
        clickDeleteHandle() {
            axios.delete(path.baseUrl + path.permissionsDel + '?Id=' + this.id).then(res => {
                this.$emit("change", true);
            }).catch(err => {
                console.log(err);
            })
        },
        close() {
            this.$emit("change", false);
        },
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
    height: 120px;
    border-radius: 4px;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
}

.popup-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 10px;
    border-bottom: 1px solid #eee;
    margin-bottom: 10px;
}

.popup-title {
    font-size: 16px;
    font-weight: bold;
}

.popup-but {
    font-size: 40px;
    cursor: pointer;
}

.popup-content {
    padding: 10px;
}
</style>