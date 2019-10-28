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
            time: 'day',
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
                15000);
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

                this.device = data;
                this.carbonMonoxideReadings = data.sensorReadings.map(function (val) {
                    //return val.carbonMonoxideLevel;
                    return { x: val.readingDateTime, y: val.carbonMonoxideLevel };
                });

                this.humidityReadings = data.sensorReadings.map(function (val) {
                    return { x: val.readingDateTime, y: val.humidityPercentage };
                });

                //this.dateTimeReadings = data.sensorReadings.map(function (val) {
                //    return val.readingDateTime;
                //});

                this.temperatureReadings = data.sensorReadings.map(function (val) {
                    //return val.temperatureCelcius;
                    return { x: val.readingDateTime, y: val.temperatureCelcius };
                });

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
                            xAxes: [{
                                type: "time",
                                time: {
                                    tooltipFormat: 'MM/DD/YYYY hh:MM:SS A',
                                    unit: this.time
                                },
                                scaleLabel: {
                                    display: true,
                                    labelString: 'Date & Time'
                                }
                            }],
                            yAxes: [{
                                ticks: {
                                    beginAtZero: true,
                                    LabelString: 'Temperature'
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
                            xAxes: [{
                                type: "time",
                                time: {
                                    tooltipFormat: 'MM/DD/YYYY hh:MM:SS A',
                                    unit: this.time
                                },
                                scaleLabel: {
                                    display: true,
                                    labelString: 'Date & Time'
                                }
                            }],
                            yAxes: [{
                                scaleLabel: {
                                    display: true,
                                    LabelString: "Humidity %"
                                }
                            }]
                        }
                    }
                });

                var carbonMonoxideChart = new Chart(carbonMonoxideChartCtx, {
                    type: 'line',
                    data: {
                        datasets: [{
                            label: 'Carbon Monoxide Readings in PPM',
                            data: this.carbonMonoxideReadings,
                            borderWidth: 1
                        }]
                    },
                    options: {
                        responsive: true,
                        maintainAspectRatio: false,
                        scales: {
                            xAxes: [{
                                type: "time",
                                time: {
                                    tooltipFormat: 'MM/DD/YYYY hh:MM:SS A',
                                    unit: this.time
                                },
                                scaleLabel: {
                                    display: true,
                                    labelString: 'Date & Time'
                                }
                            }],
                            yAxes: [{
                                ticks: {
                                    beginAtZero: true,
                                    LabelString: 'Carbon Monoxide Level'
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
