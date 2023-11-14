<template>
    <div class="draggable-component" ref="draggable" :style="positionStyle">
        <div class="drag-handle" @mousedown.prevent="handleMousedown">
            <i class="fa fa-arrows" aria-hidden="true"></i>
        </div>
        <slot></slot>
    </div>
</template>
  
<script>
export default {
    data() {
        return {
            isDragging: false,
            x: 0,
            y: 0
        }
    },
    computed: {
        positionStyle() {
            return {
                transform: `translate(${this.x}px, ${this.y}px)`,
                transition: this.isDragging ? 'none' : 'transform 0.3s'
            }
        }
    },
    methods: {
        handleMousedown(event) {
            this.isDragging = true;
            const { left, top } = this.$refs.draggable.getBoundingClientRect();
            this.x = event.clientX - left;
            this.y = event.clientY - top;
            document.addEventListener('mousemove', this.handleMousemove);
            document.addEventListener('mouseup', this.handleMouseup);
        },
        handleMousemove(event) {
            this.x = event.clientX - this.x;
            this.y = event.clientY - this.y;
        },
        handleMouseup() {
            this.isDragging = false;
            document.removeEventListener('mousemove', this.handleMousemove);
            document.removeEventListener('mouseup', this.handleMouseup);
        }
    }
}
</script>
  
<style scoped>
.draggable-component {
    position: fixed;
    z-index: 9999;
}

.drag-handle {
    cursor: move;
    display: inline-block;
    padding: 8px;
    background-color: #448aff;
    color: #fff;
    border-radius: 50%;
}

.fa-arrows {
    font-size: 16px;
}
</style>