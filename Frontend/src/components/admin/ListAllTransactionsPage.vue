<script setup>

import {ref} from "vue";
import axios from "axios";

const transactions = ref([])

async function showTransactionList(){
  try {
    const response = await axios.get('http://localhost:5280/api/Transaction', {withCredentials: true})
    transactions.value = response.data
  } catch (err) {
    alert(err.message)
  }

}
</script>

<template>
  <div class="dashboard-wrapper">
    <div class="dashboard-card" style="max-width: max-content">
      <h2 class="dashboard-title">List All Transactions</h2>

      <table class="custom-table" v-if="transactions.length > 0">
        <tbody>
        <tr>
          <th class="custom-table-h">Id</th>
          <th class="custom-table-h">Source Type</th>
          <th class="custom-table-h">Source Id</th>
          <th class="custom-table-h">Target Type</th>
          <th class="custom-table-h">Target Id</th>
          <th class="custom-table-h">Amount</th>
          <th class="custom-table-h">User Id</th>
          <th class="custom-table-h">Type</th>
          <th class="custom-table-h">Timestamp</th>
        </tr>

        <tr v-for="transaction in transactions" :key="transaction.id">
          <td class="custom-table-h">{{transaction.id}}</td>
          <td class="custom-table-h">{{transaction.sourceType}}</td>
          <td class="custom-table-h">{{transaction.sourceId}}</td>
          <td class="custom-table-h">{{transaction.targetType}}</td>
          <td class="custom-table-h">{{transaction.targetId}}</td>
          <td class="custom-table-h">{{transaction.amount}}</td>
          <td class="custom-table-h">{{transaction.userId}}</td>
          <td class="custom-table-h">{{transaction.type}}</td>
          <td class="custom-table-h">{{transaction.timestamp}}</td>
        </tr>
        </tbody>
      </table>

      <button class="dashboard-button" @click="showTransactionList">Show Transaction List</button>

      <router-link to="/admin/dashboard">
        <button class="dashboard-button">Return</button>
      </router-link>
    </div>
  </div>
</template>

<style scoped>

</style>