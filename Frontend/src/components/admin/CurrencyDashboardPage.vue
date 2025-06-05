<script setup>
import {ref} from "vue";
import api from '../../api/axiosInstance.js'
import CreateCurrencyModal from "./ModalsAndComponents/CreateCurrencyModal.vue";

const currencies = ref([])
const showCreateCurrencyModal = ref(false)

async function showCurrencyList(){
  try {
    const response = await api.get('/Currency')
    currencies.value = response.data
  } catch (err) {
    alert(err.message)
  }
}

async function createCurrency(){
  const payload = {


  }
  try {
    const response = await api.post('/Currency')
    currencies.value = response.data
  } catch (err) {
    alert(err.message)
  }
}
</script>

<template>
  <div class="dashboard-wrapper">
    <div class="dashboard-card">
      <h2 class="dashboard-title">List All Accounts</h2>

      <table class="custom-table" v-if="currencies.length > 0">
        <tbody>
        <tr>
          <th class="custom-table-h">Id</th>
          <th class="custom-table-h">Currency Name</th>
          <th class="custom-table-h">TRY Indexed Value</th>
        </tr>

        <tr v-for="currency in currencies" :key="currency.id">
          <td class="custom-table-h">{{currency.id}}</td>
          <td class="custom-table-h">{{currency.name}}</td>
          <td class="custom-table-h">{{currency.tryIndexedValue}}</td>
        </tr>
        </tbody>

      </table>

      <button class="dashboard-button" @click="showCurrencyList">Show Currency List</button>

      <button class="dashboard-button" @click="showCreateCurrencyModal = true">Create Currency</button>
      <CreateCurrencyModal v-if="showCreateCurrencyModal" @close="showCreateCurrencyModal = false"></CreateCurrencyModal>

      <router-link to="/admin/dashboard">
        <button class="dashboard-button">Return</button>
      </router-link>
    </div>
  </div>
</template>

<style scoped>

</style>