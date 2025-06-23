<script setup>
import {onMounted, ref} from "vue";
import api from '../../../api/axiosInstance.js';

const onlineShoppingEnabled = ref(false)
const cardPassword = ref('')
const accounts = ref([])
const selectedAccountId = ref('')

onMounted(async () => {
  await loadAccounts();
})

async function loadAccounts(){
  try{
    const response = await api.get('/BankAccount/user')
    accounts.value = response.data.filter(account => account.currencyType === 'TRY')
  } catch (err) {
    alert(err.message)
  }
}

async function createCard(){
  if(!selectedAccountId.value || cardPassword.value.length !== 4){
    alert("All fields must be selected and completed correctly")
    return
  }

  const payload = {
    onlineShopping: onlineShoppingEnabled.value,
    password: cardPassword.value,
    linkedAccountId: selectedAccountId.value
  }
  try{
    const response = await api.post('/Card/debit-card', payload)

    cardPassword.value = '';
    selectedAccountId.value = '';

    alert('Debit card has been created')
  } catch (err) {
    if(err.status === 400){
      alert("All fields must be selected and completed correctly")
    }
    else{
      alert(err.message)
    }
  }
}
</script>

<template>
  <div class="modal-overlay" @click.self="$emit('close')" style="z-index: 2">
    <div class="modal-content">
      <h2 class="dashboard-title">Create Debit Card</h2>
      <label>Choose a TRY account to add a card</label>

      <select class="select"  id="account" v-model="selectedAccountId">
        <option class="select-items" value="">--Choose an Account--</option>
        <option class="select-items" v-for="account in accounts" :key="account.id" :value="account.id">
          {{account.id}} - {{account.currencyType}} Account
        </option>
      </select>

      <div style="display: flex; flex-direction: row; justify-content: center; align-items: center; gap: 100px">
        <label>Enter a 4 digit password:</label>
        <input class="input" type="text" maxlength="4" v-model="cardPassword">
      </div>

      <div style="display: flex; flex-direction: row; justify-content: center; align-items: center; gap: 20px">
        <label>Online Shopping</label>
        <button v-if="onlineShoppingEnabled" @click="onlineShoppingEnabled = false">Enabled</button>
        <button v-if="!onlineShoppingEnabled" @click="onlineShoppingEnabled = true">Disabled</button>
      </div>

      <div style="margin: 10px">
        <button @click="createCard">Create Card</button>
      </div>

      <button @click="$emit('close')">Close</button>
    </div>
  </div>
</template>

<style scoped>
.dashboard-wrapper{
  display: flex;
  justify-content: center;
  align-items: center;
  /*width: 100%;*/
  /*height: 100vh;*/
}

.dashboard-card {
  background-color: #1e1e1e;
  color: white;
  padding: 2rem;
  border-radius: 1rem;
  width: 100%;
  max-width: 500px;
  text-align: center;
  box-shadow: 0 0 20px rgba(0, 0, 0, 0.2);
  z-index: 1;
}

.dashboard-title {
  font-weight: bold;
  margin-bottom: 1rem;
  text-transform: uppercase;
  font-size: 24px;
}

.dashboard-button {
  background: transparent;
  color: white;
  border: 1px solid white;
  font-size: 1rem;
  padding: 0.75rem 1.5rem;
  margin: 0.5rem 0;
  width: 100%;
  cursor: pointer;
  transition: 0.3s ease-in-out;
  border-radius: 0.5rem;
}

.dashboard-button:hover {
  background-color: white;
  color: #1e1e1e;
}

.input {
  border: 1px solid white;
  padding: 10px;
  margin: 0.2rem 0;
  border-radius: 10px;
  background-color: #3b3b3b;
  font-size: small;
  font-weight: bold;
  text-align: center;
}

.select {
  background: transparent;
  border: 1px solid white;
  font-size: 1rem;
  padding: 0.75rem 1.5rem;
  margin: 0.5rem 0;
  width: 100%;
  cursor: pointer;
  transition: 0.3s ease-in-out;
  border-radius: 0.5rem;
}

.select-items {
  background-color: #212121;
  top: 100%;
  left: 0;
  right: 0;
  z-index: 99;
}

.debit-or-virtual-card {
  background-color: blue;
  color: white;
  padding: 2rem;
  border-radius: 1rem;
  max-height: 120px;
  min-width: 250px;
  text-align: center;
  box-shadow: 0 0 20px rgba(0, 0, 0, 0.2);
  margin: 0 auto;
}
.debit-or-virtual-card-number {
  top:100px;
  left:20px;
  font-size: 20px;
  font-family: 'Courier New', Courier, monospace;
  word-spacing: 1px;
}
.debit-or-virtual-card-name {
  font-size: 20px;
  font-family: 'Courier New', Courier, monospace;
  left: 20px;
  top:130px
}
.debit-or-virtual-card-expdate {
  font-family: 'Courier New', Courier, monospace;
  top:130px;
  left:160px
}
.debit-or-virtual-card-cvv {
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  top:135px;
  left:120px;
  font-size: 17px;
  color:white;
}
.debit-or-virtual-card-type {
  top:100px;
  left:20px;
  font-size: 20px;
  font-family: 'Courier New', Courier, monospace;
  word-spacing: 1px;
}

.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  background-color: rgba(0,0,0,0.6);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
}

.modal-content {
  background-color: #6c6868;
  padding: 2rem;
  border-radius: 1rem;
  max-width: 500px;
  width: 90%;
}

.custom-table {
  width: 100%;
  border-collapse: collapse;
  table-layout: fixed;
  text-align: center;
  font-size: 14px;
}

.custom-table-h {
  border: 1px solid #ccc;
  padding: 8px;
}
</style>