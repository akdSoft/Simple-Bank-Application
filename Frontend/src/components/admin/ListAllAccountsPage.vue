<script setup>
import {ref} from "vue";
import api from '../../api/axiosInstance.js'

const accounts = ref([])

async function showAccountList(){
  try {
    const response = await api.get('/BankAccount')
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

      <table class="custom-table" v-if="accounts.length > 0">
        <tbody>
        <tr>
          <th class="custom-table-h">Id</th>
          <th class="custom-table-h">Balance</th>
          <th class="custom-table-h">Account Type</th>
          <th class="custom-table-h">User Id</th>
          <th class="custom-table-h">User Name</th>
        </tr>

        <tr v-for="account in accounts" :key="account.id">
          <td class="custom-table-h">{{account.id}}</td>
          <td class="custom-table-h">{{account.balance}}</td>
          <td class="custom-table-h">{{account.accountType}}</td>
          <td class="custom-table-h">{{account.userId}}</td>
          <td class="custom-table-h">{{account.userName + ' ' + account.userSurname}}</td>
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