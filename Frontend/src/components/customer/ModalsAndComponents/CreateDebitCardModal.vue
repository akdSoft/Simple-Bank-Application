<script setup>
import {onMounted, ref} from "vue";
import axios from "axios";

const onlineShoppingEnabled = ref(false)
const cardPassword = ref('')
const accounts = ref([])
const selectedAccountId = ref('')

onMounted(async () => {
  await  loadAccounts();
})

async function loadAccounts(){
  try{
    const response = await axios.get('http://localhost:5280/api/BankAccount/user', {withCredentials: true},)
    accounts.value = response.data
  } catch (err) {
    alert(err.message)
  }
}

async function createCard(){
  const payload = {
    onlineShopping: onlineShoppingEnabled.value,
    password: cardPassword.value,
    linkedAccountId: selectedAccountId.value
  }
  try{
    const response = await axios.post('http://localhost:5280/api/Card/debit-card', payload, {withCredentials: true},)

    cardPassword.value = '';
    selectedAccountId.value = '';

    alert('debit card created')
  } catch (err) {
    alert(err.message)
  }
}
</script>

<template>
  <div class="modal-overlay" @click.self="$emit('close')">
    <div class="modal-content">
      <h2 class="dashboard-title">Create Debit Card</h2>
      <label>Choose an account to add a card</label>

      <select class="select"  id="account" v-model="selectedAccountId">
        <option class="select-items" value="">--Choose an Account--</option>
        <option class="select-items" v-for="account in accounts" :key="account.id" :value="account.id">
          {{account.id}}
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

</style>