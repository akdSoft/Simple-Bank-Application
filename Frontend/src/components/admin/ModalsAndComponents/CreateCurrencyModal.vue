<script setup>
import {computed, onMounted, ref} from "vue";
import axios from "axios";

const name = ref('')
const tryIndexedValue = ref('')


async function createCurrency(){
  const payload = {
    name: name.value,
    tryIndexedValue: tryIndexedValue.value
  }

  try{
    const response = await axios.post('http://localhost:5280/api/Currency', payload, {withCredentials: true})

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

      <div style="margin: 10px">
        <button @click="createCurrency">Create</button>
      </div>

      <button @click="$emit('close')">Close</button>
    </div>
  </div>
</template>

<style scoped>

</style>