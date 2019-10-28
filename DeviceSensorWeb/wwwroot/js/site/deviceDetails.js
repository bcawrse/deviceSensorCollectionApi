(function (deviceService) {

    var deviceId = window.deviceDetailsId;

    var app = new Vue({
        el: "#deviceDetailsVue",
        data: {
            device: {},
            chartData: {},
            time: 'hour',
            carbonMonoxideReadings: {},
            humidityReadings: {},
            temperatureReadings: {},
            dateTimeReadings: {}
        },
        methods: {
            pollDevices: function () {
            //    window.setTimeout(function () {
                    $.ajax({
                        type: 'GET',
                        url: '/Device/GetDevice',
                        data: { Id: deviceId},
                        cache: false,
                        success: function (data, status, jqxhr) {
                            //this.device = data;
                            this.setChartData(data);
                        }.bind(this)
                    });

                //    this.pollDevices();

                //}.bind(this),
                //2000);
            },
            setChartData: function(data) {
                if (!data) {
                    return {};
                }

                var cmr = data.sensorReadings.map(function (val) {
                    return val.carbonMonoxideLevel;
                });

                var hp = data.sensorReadings.map(function (val) {
                    return val.humidityPercentage;
                });

                var rdt = data.sensorReadings.map(function (val) {
                    return val.readingDateTime;
                });

                var tc = data.sensorReadings.map(function (val) {
                    return val.temperatureCelcius;
                });

                var carbonMonoxideReadings = cmr;
                var humidityReadings = hp;
                var temperatureReadings = tc;
                var dateTimeReadings = rdt;

                var time = this.time;

                //carbonMonoxideLevel: 0
                //deviceHealth: "good"
                //humidityPercentage: 86
                //readingDateTime: "2019-10-27T01:34:35.235Z"
                //temperatureCelcius: 24

                //console.log('DATA: ', this);

                var celciusChartCtx = this.$refs["celciusChart"].getContext('2d');
                var humidityChartCtx = this.$refs["humidityChart"].getContext('2d');
                var carbonMonoxideChartCtx = this.$refs["carbonMonoxideChart"].getContext('2d');

                //console.log('celciusChartTx:', celciusChartCtx);

                var celciusChart = new Chart(celciusChartCtx, {
                    type: 'line',
                    data: {
                        datasets: [{
                            label: 'Temperature in Celcius',
                            data: temperatureReadings,
                            borderWidth: 1
                        }]
                    },
                    options: {
                        responsive: true,
                        maintainAspectRatio: false,
                        scales: {
                            yAxes: [{
                                ticks: {
                                    beginAtZero: true
                                }
                            }]
                        }
                    }
                });

                var humidityChart = new Chart(humidityChartCtx, {
                    type: 'line',
                    data: {
                        datasets: [{
                            label: 'Humidity Readings',
                            data: humidityReadings,
                            borderWidth: 1
                        }]
                    },
                    options: {
                        responsive: true,
                        maintainAspectRatio: false,
                        scales: {
                            yAxes: [{
                                ticks: {
                                    beginAtZero: true
                                }
                            }]
                        }
                    }
                });

                var carbonMonoxideChart = new Chart(carbonMonoxideChartCtx, {
                    type: 'line',
                    data: {
                        datasets: [{
                            label: 'Carbon Monoxide Readings',
                            data: carbonMonoxideReadings,
                            borderWidth: 1
                        }]
                    },
                    options: {
                        responsive: true,
                        maintainAspectRatio: false,
                        scales: {
                            yAxes: [{
                                ticks: {
                                    beginAtZero: true
                                }
                            }]
                        }
                    }
                });
            }
        },
        mounted: function () {
            if (deviceId) {
                this.pollDevices();

                
            }
        }
    });

})(window.SERVICE.Devices);
