<template>
  <div>
    <video-player :options="videoOptions" />
    <FloatingButton>
      <div class="content">
        <p>这是一个可拖动的悬浮窗</p>
        <p>拖动我试试看！</p>
      </div>
    </FloatingButton>
  </div>
</template>
  
<script>
import VideoPlayer from '../../components/VideoPlayer.vue';
import FloatingButton from '@/components/FloatingButton.vue';

export default {
  name: 'Video',
  components: {
    VideoPlayer,
    FloatingButton
  },
  data() {
    return {
      videoOptions: {
        autoplay: true,
        controls: true,
        sources: [
          {
            src:
              'http://192.168.0.160:8002/m3u8/20230419/b6607a44-8b4c-496b-a4f9-1c6e9f0df8e6/index.m3u8',
            type: 'application/x-mpegURL'
          }
        ]
      }
    };
  }
};
</script>
<style>
.container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100%;
}

.content {
  padding: 16px;
}
</style>