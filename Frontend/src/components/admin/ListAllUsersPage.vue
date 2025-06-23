<script setup>
import {ref, onMounted, computed} from "vue";
import {useRouter} from "vue-router";
import api from '../../api/axiosInstance.js'

const users = ref([])

async function loadUsers(){
  try{
    const response = await api.get('/user')
    users.value = response.data
  } catch (err) {
    if(err.status === 401){
      alert("Unauthorized")
    } else {
      alert(err.message)
    }
  }
}

const router = useRouter()

onMounted(async () => {
  await loadUsers()
})

async function logOut(){
  localStorage.removeItem('token')
  await router.push('/')
}
</script>

<template>
  <div class="topbar">
    <ul>
      <li>
        <a>
          <button @click="logOut">
            <img style="width: 2.5vw; height: auto" src="../../assets/img/logout.png">
          </button>
        </a>
      </li>
    </ul>
  </div>

  <div class="wrapper">
    <div class="container">
      <div class="column flex-1">
        <div class="card ads" style="height: 140px" title="Project Title">Project Title (To be added)</div>
        <div class="card ads" style="height: 530px" title="Project Info">Project Info (To be added)</div>
        <div class="card blank" style="height: 100px">Blank</div>
      </div>

      <div class="column flex-2">
        <div class="card users-wrapper" title="users">
          <div class="users-container">
            <div class="users-header">
              <a class="title" style="margin-top: 10px">Users</a>
            </div>

            <ul>
              <li v-for="user in users" :key="user.id">
                <div style="display: flex; flex-direction: row; align-items: center; gap: 15px">
                  <img style="width: 70px; height: 70px; " src="../../assets/img/user.png">

                  <div style="display: flex; flex-direction: column">
                    <a >{{ user.name + ' ' +  user.surname}}</a>
                    <a style="color: gray">{{ user.username}}</a>
                  </div>
                </div>
                <div style="display: flex; flex-direction: column; align-items: flex-end">
                  <a >Total Balance: ₺{{ user.totalBalanceInTRY }}</a>
                  <a style="color: gray">Bank Accounts: {{ user.bankAccounts.length }}</a>
                  <a style="color: gray">User ID: {{ user.id}}</a>
                </div>
              </li>
            </ul>
          </div>
        </div>
      </div>

      <div class="column flex-3">
        <div class="card return" title="Return">
          <a class="title">Return to the homepage</a>
          <a href="/admin/dashboard">
            <img class="image" src="../../assets/img/return.png">
          </a>
        </div>
      </div>

    </div>
  </div>
</template>

<style scoped>
.wrapper {
  margin-top: 8vh;
  width: 100%;
  height: 90vh;
  display: flex;
  justify-content: center;
  box-sizing: border-box;
}

.container {
  display: flex;
  width: 100%;
  height: 100%;
  box-sizing: border-box;
}

@media (max-width: 1120px) {
  .container {
    flex-wrap: wrap;
  }

  .column {
    flex: 1 1 250px;
    margin-bottom: 20px;
  }

  .flex-1 { flex: 100 1 250px; }
  .flex-2 { flex: 310 1 250px; }
  .flex-3 { flex: 100 1 250px; }
}

@media (min-width: 1121px) {
  .container {
    flex-wrap: nowrap;
  }
  .flex-1 { flex: 100; }
  .flex-2 { flex: 310; }
  .flex-3 { flex: 100; }
}

.column {
  margin-left: 10px;
  margin-right: 10px;
  display: flex;
  flex-direction: column;
  gap: 20px;
  min-width: 0;
  min-height: unset;
  flex-shrink: 1;
}

.card {
  background-color: #ffffff;
  padding: 20px;
  border-radius: 23px;
  min-height: 40px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  overflow: hidden;
}

.ads {
  background: #daaeee;
  background: radial-gradient(circle, rgba(218, 174, 238, 1) 0%, rgba(148, 187, 233, 1) 100%);
}

.blank {
  background-color: dimgray;
  visibility: hidden;
  height: 60px;
}

.users-wrapper {
  height: 860px;
  background-color: transparent;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  overflow-y: auto
}

.users-container {
  z-index: 1;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  width: 100%;
}

.users-header {
  display: flex;
  flex-direction: row;
  width: 100%;
  justify-content: space-between;
  padding-left: 50px;
  padding-right: 50px
}

.users-container ul {
  display: flex;
  flex-direction: column-reverse;
  list-style: none;
  padding: 0;
  margin: 0;
  width: 85%;
}

.users-container li {
  display: flex;
  flex-direction: row;
  padding-left: 10px;
  padding-right: 10px;
  align-items: center;
  justify-content: space-between;
  height: 120px;
  border-radius: 15px;
  margin-top: 20px;
  transition: background-color 0.2s ease;
  box-shadow: 0 4px 6px rgba(0,0,0,0.08);
  background-color: white;
  font-weight: bold;
}

.return {
  height: 80px;
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: space-between;
  .image {
    height: 50px;
    width: auto;
    margin-left: 20px;
    object-fit: contain;
    aspect-ratio: 1/1;
  }
}

.title {
  color: black;
  font-size: 20px;
}

.topbar{
  position: fixed;
  display: flex;
  flex-direction: row-reverse;
  top: 0;
  left:0;
  width: 100vw;
  height: 6vh;
  background: white;
  z-index: 0;
  overflow: hidden;
}

.topbar ul {
  display: flex;
  flex-direction: row-reverse;
  list-style: none;
  padding-right: 30px;
  align-items: center;
}

.topbar li {
  margin-left: 30px;
  max-width: 100%;
  overflow: hidden;
}

.topbar a {
  display: flex;
  font-size: 1.2vw;
}
</style>