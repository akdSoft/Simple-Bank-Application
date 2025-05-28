<script setup>

</script>

<template>
  <div class="modal-overlay" @click.self="$emit('close')">
    <div class="modal-content">
      <button @click="$emit('close')">Close</button>
    </div>
  </div>
</template>

<style scoped>

</style>