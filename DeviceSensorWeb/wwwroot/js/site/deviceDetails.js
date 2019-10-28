(function (deviceService) {

    var elExists = document.getElementById("deviceDetailsVue");

    if (!elExists) {
        return;
    }

    var deviceId = window.deviceDetailsId || {};
    var celciusChartCtx;
    var humidityChartCtx;
    var carbonMonoxideChartCtx;

    var app = new Vue({
        el: "#deviceDetailsVue",
        data: {
            device: {},
            time: 'hour',
            carbonMonoxideReadings: {},
            humidityReadings: {},
            temperatureReadings: {},
            dateTimeReadings: {}
        },
        methods: {
            pollDevices: function () {
                window.setTimeout(function () {
                    this.fetchDetails();
                    this.pollDevices();
                }.bind(this),
                5000);
            },
            fetchDetails: function () {
                $.ajax({
                    type: 'GET',
                    url: '/Device/GetDevice',
                    data: { Id: deviceId },
                    cache: false,
                    success: function (data, status, jqxhr) {
                        this.setChartData(data);
                    }.bind(this)
                });
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


                this.device = data;
                this.carbonMonoxideReadings = cmr;
                this.humidityReadings = hp;
                this.temperatureReadings = tc;
                this.dateTimeReadings = rdt;

                //console.log('DATA: ', this);

                if (!celciusChartCtx) celciusChartCtx = this.$refs["celciusChart"].getContext('2d');
                if (!humidityChartCtx) humidityChartCtx = this.$refs["humidityChart"].getContext('2d');
                if (!carbonMonoxideChartCtx) carbonMonoxideChartCtx = this.$refs["carbonMonoxideChart"].getContext('2d');

                //console.log('celciusChartTx:', celciusChartCtx);

                var celciusChart = new Chart(celciusChartCtx, {
                    type: 'line',
                    data: {
                        datasets: [{
                            label: 'Temperature in Celcius',
                            data: this.temperatureReadings,
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
                            data: this.humidityReadings,
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
                            data: this.carbonMonoxideReadings,
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
            },
            formattedDate: function (date) {
                return moment(date).format('MM/DD/YYYY hh:MM:SS A');
            }
        },
        mounted: function () {
            if (deviceId) {
                this.fetchDetails();
                this.pollDevices();
            }
        }
    });

})(window.SERVICE.Devices);
