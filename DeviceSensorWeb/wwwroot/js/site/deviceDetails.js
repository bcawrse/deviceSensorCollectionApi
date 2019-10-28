(function (deviceService) {

    var app = new Vue({
        el: "#deviceDetailsVue",
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
                2000);
            }
        },
        mounted: function () {
            console.log('deviceDetailsVue VUE mounted');
            this.pollDevices();
        }
    });

})(window.SERVICE.Devices);