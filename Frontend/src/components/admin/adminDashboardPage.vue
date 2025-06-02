<script>
import axios from 'axios';
import '../../assets/dashboard.css'
export default {
  methods: {
    async logOut(){
      try{
        const response = await axios.post('http://localhost:5280/api/Auth/logout', {},{withCredentials: true})
        if(response.data === 'logged out'){
          this.$router.push('/')
        }
        else{
          alert('unexpected situation')
        }
      } catch (err){
        alert(err.message);
      }
    }
  }
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

      <router-link to="/admin/list-all-cards">
        <button class="dashboard-button">List All Cards</button>
      </router-link>

      <router-link to="/admin/delete-account">
        <button class="dashboard-button">Delete Account</button>
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