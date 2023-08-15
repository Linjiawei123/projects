<template>
    <div class="chatIcon">
        <el-popover placement="top-start" width="400" trigger="hover">
            <div class="emotionList">
                <a href="javascript:void(0);" @click="getEmo(index)" v-for="(item, index) in faceList" :key="index"
                    class="emotionItem">{{ item }}</a>
            </div>
            <img height="20" slot="reference" src="../../assets/表情.svg" />
        </el-popover>
    </div>
</template>
<script>
import emojiData from "../../assets/emoji.json";
export default {
    mounted() {
        for (let i in emojiData) {
            this.faceList.push(emojiData[i].char);
        }
    },
    data() {
        return {
            faceList: [],
        };
    },
    methods: {
        getEmo(index) {
            this.$emit("getEmo", this.faceList[index]);
        },
    },
};
</script>
<style scoped>
.chatIcon {
    font-size: 25px;
}

.emotionList {
    display: flex;
    flex-wrap: wrap;
    padding: 5px;
}

.emotionItem {
    width: 10%;
    font-size: 20px;
    text-align: center;
}

.emotionItem {
    text-decoration: none;
}

.emotionItem:link {
    text-decoration: none;
}

.emotionItem:visited {
    text-decoration: none;
}

.emotionItem:hover {
    text-decoration: none;
}

.emotionItem:active {
    text-decoration: none;
}
</style>
  