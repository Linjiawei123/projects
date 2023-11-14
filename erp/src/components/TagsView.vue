<template>
    <div class="tagsbox">
        <div @contextmenu.prevent="openMenu(item, $event)" :class="isActive(item.url) ? 'active' : ''" class="tagsview"
            v-for="(item, index) in tags" :key="index" @click="tagsmenu(item)">
            {{ item.name }}
            <el-button v-if="item.id" class="tag-close" size="small" link @click.stop="handleClose(item, index)">
                <el-icon>
                    <CircleClose />
                </el-icon>
            </el-button>
            <span class="el-icon-close tagsicon" @click.stop="handleClose(item, index)"></span>
            <ul v-show="visible" class="contextmenu" :style="{ left: left + 'px', top: top + 'px' }">
                <li @click.stop="cleartags($route.path)">关闭所有</li>
            </ul>
        </div>
    </div>
</template>
  
<script>
import { mapState, mapMutations } from "vuex";
import { CircleClose } from '@element-plus/icons-vue'
export default {
    data() {
        return {
            visible: false,
            top: 0,
            left: 0
        }
    },
    computed: {
        ...mapState(["tags"]),
    },
    watch: {
        visible(value) {
            if (value) {
                document.body.addEventListener('click', this.closeMenu)
            } else {
                document.body.removeEventListener('click', this.closeMenu)
            }
        }
    },
    components: {
        CircleClose
    },
    methods: {
        ...mapMutations(["closeTab", "cleartagsview"]),
        handleClose(item, index) {
            let length = this.tags.length - 1;
            this.closeTab(item);
            if (item.url !== this.$route.path) {
                return;
            }
            if (index === length) {
                if (length === 0) {
                    if (this.$route.path !== "/home") {
                        this.$router.push({ path: "/home" });
                    }
                } else {
                    this.$router.push({ path: this.tags[index - 1].url });
                    this.$store.commit("menuIndex", this.tags[index-1].id)
                }
            } else {
                this.$router.push({ path: this.tags[index].url });
                this.$store.commit("menuIndex", this.tags[index].id)
            }
        },
        tagsmenu(item) {
            if (this.$route.path !== item.url) {
                this.$router.push({ path: item.url });
                if (item.id)
                    this.$store.commit("menuIndex", item.id)
            }
        },
        isActive(route) {
            return route === this.$route.path
        },
        openMenu(tag, e) {
            if (tag.id) {
                this.visible = true
                this.selectedTag = tag
                const offsetLeft = this.$el.getBoundingClientRect().left
                this.left = e.clientX - offsetLeft + 210
                this.top = e.clientY + 10
            }
        },
        closeMenu() {
            this.visible = false
        },
        cleartags(val) {
            this.cleartagsview(val)
            this.$store.commit("menuIndex", '')
            this.visible = false
        }
    },
};
</script>
  
<style lang="scss" scoped>
.tagsview {
    cursor: pointer;
    margin-left: 1px;
    height: 26px;
    line-height: 26px;
    padding: 0 8px;
    border: 1px solid #dddede;
    color: #6c6c6c;
    font-size: 12px;
    float: left;
}

.tagsicon:hover {
    color: #faaeae;

}

.active {
    background-color: #65bfff;
    color: #fff;
    .tag-close{
        color: #fff;
    }
}

.contextmenu {
    margin: 0;
    background: #fff;
    z-index: 100;
    position: absolute;
    list-style-type: none;
    padding: 2px 0;
    border-radius: 4px;
    font-size: 12px;
    font-weight: 400;
    color: #333;
    box-shadow: 2px 2px 3px 0 rgba(0, 0, 0, .3);

    li {
        margin: 0;
        padding: 7px 16px;
        cursor: pointer;

        &:hover {
            background: #eee;
        }
    }
}
</style>
  