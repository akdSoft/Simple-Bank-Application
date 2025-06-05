<script setup>
import {ref} from "vue";
import api from '../../api/axiosInstance.js'

const users = ref([])

async function showUserList(){
  try {
    const response = await api.get('/User')
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

    <table class="custom-table" v-if="users.length > 0">
      <tbody>
      <tr>
        <th class="custom-table-h">Id</th>
        <th class="custom-table-h">Name</th>
        <th class="custom-table-h">Surname</th>
        <th class="custom-table-h">Username</th>
        <th class="custom-table-h">Email</th>
      </tr>

      <tr v-for="user in users" :key="user.id">
        <td class="custom-table-h">{{user.id}}</td>
        <td class="custom-table-h">{{user.name}}</td>
        <td class="custom-table-h">{{user.surname}}</td>
        <td class="custom-table-h">{{user.username}}</td>
        <td class="custom-table-h">{{user.email}}</td>
      </tr>
      </tbody>
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