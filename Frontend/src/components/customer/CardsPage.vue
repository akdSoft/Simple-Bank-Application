<script setup>
import Card from "../Card.vue";
import {onMounted, ref} from "vue";
import api from '../../api/axiosInstance.js'
import CreateDebitCardModal from "./ModalsAndComponents/CreateDebitCardModal.vue";
import CreateVirtualCardModal from "./ModalsAndComponents/CreateVirtualCardModal.vue";
import VirtualCardMoneyTransferModal from "./ModalsAndComponents/VirtualCardMoneyTransferModal.vue";

const debitCards = ref([])
const virtualCards = ref([])

const showCreateDebitCardModal = ref(false)
const showCreateVirtualCardModal = ref(false)
const showVirtualCardMoneyTransferModal = ref(false)

onMounted(async () => {
  await  loadCards();
})

async function loadCards(){
  try{
    const response = await api.get('/Card/debit-cards/user')
    debitCards.value = response.data

    const response2 = await api.get('/Card/virtual-cards/user')
    virtualCards.value = response2.data

  } catch (err) {
    alert(err.message)
  }
}
</script>

<template>
    <div class="dashboard-wrapper">
      <div class="dashboard-card">
        <h2 class="dashboard-title" style="text-transform: none">CARDS (only TRY for now)</h2>

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

        <button class="dashboard-button" @click="showCreateDebitCardModal = true">Create Debit Card</button>
        <CreateDebitCardModal v-if="showCreateDebitCardModal" @close="loadCards(); showCreateDebitCardModal = false"></CreateDebitCardModal>

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

        <button class="dashboard-button" @click="showCreateVirtualCardModal = true">Create Virtual Card</button>
        <CreateVirtualCardModal v-if="showCreateVirtualCardModal" @close="loadCards(); showCreateVirtualCardModal = false"></CreateVirtualCardModal>

        <button class="dashboard-button" @click="showVirtualCardMoneyTransferModal = true">Virtual Card - Money Transfer</button>
        <VirtualCardMoneyTransferModal v-if="showVirtualCardMoneyTransferModal" @close="loadCards(); showVirtualCardMoneyTransferModal = false"></VirtualCardMoneyTransferModal>

        <router-link to="/customer/dashboard">
          <button class="dashboard-button">Return</button>
        </router-link>
      </div>
    </div>

</template>

<style scoped>

</style>