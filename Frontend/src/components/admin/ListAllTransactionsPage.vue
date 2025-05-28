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
    <div class="dashboard-card">
      <h2 class="dashboard-title">List All Transactions</h2>

      <table v-if="transactions.length > 0">
        <tbody>
        <tr>
          <th>Id</th>
          <th>Account Id</th>
          <th>User Id</th>
          <th>Amount</th>
          <th>Type</th>
          <th>Related Account ID</th>
        </tr>

        <tr v-for="transaction in transactions" :key="transaction.id">
          <td>{{transaction.id}}</td>
          <td>{{transaction.accountId}}</td>
          <td>{{transaction.userId}}</td>
          <td>{{transaction.amount}}</td>
          <td>{{transaction.type}}</td>
          <td>{{transaction.relatedAccountId}}</td>
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