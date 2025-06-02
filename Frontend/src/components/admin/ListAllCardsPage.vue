<script setup>
import Card from "../Card.vue";
import {onMounted, ref} from "vue";
import axios from "axios";

const debitCards = ref([])
const virtualCards = ref([])


onMounted(async () => {
  await  loadCards();
})

async function loadCards(){
  try{
    const response = await axios.get('http://localhost:5280/api/Card/debit-cards', {withCredentials: true},)
    debitCards.value = response.data

    const response2 = await axios.get('http://localhost:5280/api/Card/virtual-cards', {withCredentials: true},)
    virtualCards.value = response2.data

  } catch (err) {
    alert(err.message)
  }
}
</script>

<template>
  <div class="dashboard-wrapper">
    <div class="dashboard-card">
      <h2 class="dashboard-title">Cards</h2>

      <h3 v-if="debitCards.length > 0">Debit Cards</h3>
      <div style="display: flex; overflow-x: auto; gap: 1rem;">
        <Card v-for="debitcard in debitCards" :key="debitcard.id"
              :card-number="debitcard.cardNumber"
              :expiration-date="debitcard.expirationDate.split('T')[0]"
              :cvv="debitcard.cvv"
              :card-type="debitcard.type"
              :cardholder-name="debitcard.cardholderNameAndSurname">
        </Card>
      </div>


      <h3 v-if="virtualCards.length > 0">Virtual Cards</h3>

      <div style="display: flex; overflow-x: auto; gap: 1rem;">
        <Card @click="alert('hey')" v-for="virtualcard in virtualCards" :key="virtualcard.id"
              :card-number="virtualcard.cardNumber"
              :expiration-date="virtualcard.expirationDate.split('T')[0]"
              :cvv="virtualcard.cvv"
              :card-type="virtualcard.type"
              :cardholder-name="virtualcard.cardholderNameAndSurname"
              :available-limit="virtualcard.availableLimit">
        </Card>
      </div>


      <router-link to="/admin/dashboard">
        <button class="dashboard-button">Return</button>
      </router-link>
    </div>
  </div>

</template>

<style scoped>

</style>