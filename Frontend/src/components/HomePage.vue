<script setup>
</script>

<template>
  <div class="wrapper">
    <div class="homePage-card">
      <div class="homePage-title">
        <a class="title">Simple Bank Application</a>
        <a>(to be changed)</a>
      </div>
      <div class="homepage-button">
        <a href="/login" style="width: 40%">
          <div class="card" style="height: 80px; display: flex; flex-direction: row; align-items: center; justify-content: center; box-shadow: 0 0 10px rgba(0, 0, 0, 0.3);" title="Register">
            <p style="font-size: 24px">LOGIN</p>
          </div>
        </a>

        <a href="/register" style="width: 40%">
          <div class="card" style="height: 80px; display: flex; flex-direction: row; align-items: center; justify-content: center; box-shadow: 0 0 10px rgba(0, 0, 0, 0.3);" title="Register">
            <p style="font-size: 24px">REGISTER</p>
          </div>
        </a>
      </div>
    </div>
  </div>
</template>

<style scoped>
.wrapper {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100vh;
}

.homePage-card {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: space-between;
  padding-top: 90px;
  padding-bottom: 90px;
  background-color: transparent;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  width: 450px;
  height: 550px;
  border-radius: 20px;
}

.homePage-title {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.homepage-button {
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: center;
  gap: 60px;
}

.card {
  background-color: #ffffff;
  padding: 20px;
  border-radius: 23px;
  min-height: 40px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  overflow: hidden;
}

.title {
  color: black;
  font-size: 28px;
  font-weight: bold;
}

</style>