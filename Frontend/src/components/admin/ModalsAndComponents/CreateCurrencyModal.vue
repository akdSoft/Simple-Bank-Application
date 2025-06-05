<script setup>
import {computed, onMounted, ref} from "vue";
import api from '../../../api/axiosInstance.js';

const name = ref('')
const tryIndexedValue = ref('')
const symbol = ref('')


async function createCurrency(){
  const payload = {
    name: name.value,
    tryIndexedValue: tryIndexedValue.value,
    symbol: symbol.value
  }

  try{
    const response = await api.post('/Currency', payload)

    if (response.status === 200){
      alert('currency created')
    } else {
      alert('unexpected response: ' + response.status)
    }
  } catch (err) {
    alert(err.message)
  }
}
</script>

<template>
  <div class="modal-overlay" @click.self="$emit('close')">
    <div class="modal-content">
      <h2 class="dashboard-title">Create Currency</h2>

      <label>Currency Name</label>

      <div style="display: flex; flex-direction: row; justify-content: center; align-items: center; gap: 100px">
        <label>Enter name:</label>
        <input class="input" type="text" v-model="name">
      </div>


      <label>TRY Indexed Value</label>

      <div style="display: flex; flex-direction: row; justify-content: center; align-items: center; gap: 100px">
        <label>Enter value:</label>
        <input class="input" type="number" v-model="tryIndexedValue">
      </div>


      <label>Symbol</label>

      <div style="display: flex; flex-direction: row; justify-content: center; align-items: center; gap: 100px">
        <label>Enter symbol:</label>
        <input class="input" type="text" v-model="symbol">
      </div>


      <div style="margin: 10px">
        <button @click="createCurrency">Create</button>
      </div>

      <button @click="$emit('close')">Close</button>
    </div>
  </div>
</template>

<style scoped>

</style>