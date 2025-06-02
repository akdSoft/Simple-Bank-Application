<script setup>
const props = defineProps({
  cardNumber: String,
  expirationDate: [String, Date],
  cvv: String,
  cardholderName: String,
  cardType: String,
  availableLimit: Number
})
</script>

<template>
  <div class="debit-or-virtual-card">
    <div class="debit-or-virtual-card-number">{{ cardNumber }}</div>

    <div style="display: flex; flex-direction: row; justify-content: center; gap: 50px">
      <div class="debit-or-virtual-card-expdate">{{ expirationDate }}</div>
      <div class="debit-or-virtual-card-cvv">{{ cvv }}</div>
    </div>

    <div class="debit-or-virtual-card-type">{{ cardType }}</div>
    <div class="debit-or-virtual-card-name">{{ cardholderName }}</div>
    <div v-if="cardType == 'Virtual Card'">Available limit: {{availableLimit}}</div>
  </div>
</template>

<style scoped>

</style>