<script setup>
import '../../assets/dashboard.css'
import {useRouter} from "vue-router";

const router = useRouter()

async function logOut(){
  localStorage.removeItem('token')
  await router.push('/')
}
</script>

<template>
  <div class="dashboard-wrapper">
    <div class="dashboard-card">
      <h2 class="dashboard-title">admin page</h2>

      <router-link to="/admin/list-all-users">
        <button class="dashboard-button">List All Users</button>
      </router-link>

      <router-link to="/admin/delete-user">
        <button class="dashboard-button">Delete User</button>
      </router-link>

      <router-link to="/admin/list-all-accounts">
        <button class="dashboard-button">List All Accounts</button>
      </router-link>

      <router-link to="/admin/delete-account">
        <button class="dashboard-button">Delete Account</button>
      </router-link>

      <router-link to="/admin/currency-dashboard">
        <button class="dashboard-button">Currency Dashboard</button>
      </router-link>

      <router-link to="/admin/list-all-cards">
        <button class="dashboard-button">List All Cards</button>
      </router-link>

      <router-link to="/admin/list-all-transactions">
        <button class="dashboard-button">List All Transactions</button>
      </router-link>


      <button class="dashboard-button" @click="logOut">Log Out</button>
    </div>
  </div>
</template>

<style scoped>

</style>