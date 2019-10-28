(function (deviceService, moment) {

    var elExists = document.getElementById("deviceVue");
    if (!elExists) {
        return;
    }

    var app = new Vue({
        el: "#deviceVue",
        data: {
            devices: {}
        },
        methods: {
            pollDevices: function () {
                window.setTimeout(function () {
                    $.ajax({
                        type: 'GET',
                        url: '/Device/GetDevices',
                        data: {},
                        cache: false,
                        success: function (data, status, jqxhr) {
                            this.devices = data;
                        }.bind(this)
                    });

                    this.pollDevices();

                }.bind(this),
                5000);
            },
            deviceIdUrl: function (device) {
                return "/device/details?deviceId=" + device.id;
            },
            formatedDate: function (date) {
                return moment(date).format('MM/DD/YYYY hh:MM:SS A');
            }
        },
        mounted: function () {
            this.pollDevices();
        }
    });

})(window.SERVICE.Devices, moment);
