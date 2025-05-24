<script setup>

import {ref} from "vue";
import axios from "axios";

const users = ref([])

async function showUserList(){
  try {
    const response = await axios.get('http://localhost:5280/api/User', {withCredentials: true})
    users.value = response.data
  } catch (err) {
    alert(err.message)
  }

}
</script>

<template>
<div class="dashboard-wrapper">
  <div class="dashboard-card">
    <h2 class="dashboard-title">List All Users</h2>

    <table v-if="users.length > 0">
      <tr>
        <th>Id</th>
        <th>Name</th>
        <th>Surname</th>
        <th>Username</th>
        <th>Email</th>
      </tr>

      <tr v-for="user in users" :key="user.id">
        <td>{{user.id}}</td>
        <td>{{user.name}}</td>
        <td>{{user.surname}}</td>
        <td>{{user.username}}</td>
        <td>{{user.email}}</td>
      </tr>
    </table>

    <button class="dashboard-button" @click="showUserList">Show User List</button>

    <router-link to="/admin/dashboard">
      <button class="dashboard-button">Return</button>
    </router-link>
  </div>
</div>
</template>

<style scoped>

</style>