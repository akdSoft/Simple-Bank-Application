<script setup>
import {onMounted, ref} from "vue";
import api from '../../../api/axiosInstance.js';

const onlineShoppingEnabled = ref(false)

async function createCard(){
  const payload = {
    onlineShopping: onlineShoppingEnabled.value,
  }
  try{
    const response = await api.post('/Card/virtual-card', payload)
    alert('virtual card created')
  } catch (err) {
    alert(err.message)
  }
}
</script>

<template>
  <div class="modal-overlay" @click.self="$emit('close')">
    <div class="modal-content">
      <h2 class="dashboard-title">Create Virtual Card</h2>

      <div style="display: flex; flex-direction: row; justify-content: center; align-items: center; gap: 20px">
        <label>Online Shopping</label>
        <button v-if="onlineShoppingEnabled" @click="onlineShoppingEnabled = false">Enabled</button>
        <button v-if="!onlineShoppingEnabled" @click="onlineShoppingEnabled = true">Disabled</button>
      </div>

      <div style="margin: 10px">
        <button @click="createCard">Create Card</button>
      </div>

      <button @click="$emit('close')">Close</button>
    </div>
  </div>
</template>

<style scoped>

</style>