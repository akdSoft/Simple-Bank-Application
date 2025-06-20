<script setup>
import { ref, onMounted } from 'vue';
import api from '../../../api/axiosInstance.js'
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend
} from 'chart.js'
import { Line } from 'vue-chartjs'

ChartJS.register(
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip,
    Legend
)

const chartData = ref({
  labels: [],
  datasets: [{
    label: 'Balance (TRY)',
    data: [],
    borderColor: '#22c55e',
    backgroundColor: 'rgba(34, 197, 94, 0.1)',
    borderWidth: 3,
    tension: 0.3,
  }]
})

const chartOptions = ref({
  responsive: true,
  maintainAspectRatio: false,
  plugins: {
    title: {
      display: true,
      text: 'Balance History',
      font: { size: 16 }
    }
  },
  scales: {
    y: {
      beginAtZero: false
    }
  }
})

const groupByDay = (transactions) => {
  const dailyData = [];

  const addedDates = [];

  transactions.sort((a, b) => new Date(a.timestamp) - new Date(b.timestamp));

  for (let i = transactions.length - 1; i >= 0; i--) {
    const t = transactions[i];
    const date = new Date(t.timestamp);
    const dateKey = date.toISOString().split('T')[0]; // YYYY-MM-DD

    if (!addedDates.includes(dateKey)) {
      addedDates.push(dateKey);
      dailyData.unshift({
        label: date.toLocaleString('tr-TR', {
               day: '2-digit',
               month: '2-digit',
               hour: '2-digit',
               minute: '2-digit'
             }),
        balance: t.currentBalance
      });
    }
  }

  return dailyData;
};

const loadData = async () => {
  try {
    const response = await api.get(`/Transaction/account/${0}`)
    const data = response.data;

    const dailyData = groupByDay(data);

    chartData.value.labels = dailyData.map(t => t.label);
    chartData.value.datasets[0].data = dailyData.map(t => t.balance);
  } catch (err) {
    alert(err.message)
  }
}

onMounted(() => {
  loadData();
});
</script>

<template>
  <div class="balance-chart">
    <Line
        :data="chartData"
        :options="chartOptions"
        :key="chartData.labels.length"
    />
  </div>
</template>

<style scoped>
.balance-chart {
  width: 100%;
  height: 100%;
  display: block;
  margin: 0 auto;
}
</style>