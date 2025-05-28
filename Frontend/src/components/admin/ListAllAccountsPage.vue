<script setup>

import {ref} from "vue";
import axios from "axios";

const accounts = ref([])

async function showAccountList(){
  try {
    const response = await axios.get('http://localhost:5280/api/BankAccount', {withCredentials: true})
    accounts.value = response.data
  } catch (err) {
    alert(err.message)
  }

}
</script>

<template>
  <div class="dashboard-wrapper">
    <div class="dashboard-card">
      <h2 class="dashboard-title">List All Accounts</h2>

      <table v-if="accounts.length > 0">
        <tbody>
        <tr>
          <th>Id</th>
          <th>Balance</th>
          <th>User Id</th>
          <th>User Name</th>
        </tr>

        <tr v-for="account in accounts" :key="account.id">
          <td>{{account.id}}</td>
          <td>{{account.balance}}</td>
          <td>{{account.userId}}</td>
          <td>{{account.userName + ' ' + account.userSurname}}</td>
        </tr>
        </tbody>

      </table>

      <button class="dashboard-button" @click="showAccountList">Show Account List</button>

      <router-link to="/admin/dashboard">
        <button class="dashboard-button">Return</button>
      </router-link>
    </div>
  </div>
</template>

<style scoped>

</style>