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
    fill: true
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

const loadData = async () => {
  try {
    const response = await api.get(`/Transaction/account/${0}`)

    const data = response.data;

    chartData.value.labels = data.map(t => {
      const date = new Date(t.timestamp);
      return date.toLocaleString('tr-TR', {
        day: '2-digit',
        month: '2-digit',
        year: 'numeric',
        hour: '2-digit',
        minute: '2-digit'
      });
    });
    chartData.value.datasets[0].data = data.map(t => t.currentBalance);

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
  height: 400px;
}
</style>