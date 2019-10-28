(function (deviceService) {

    var getDevices = function () {
        console.log('RUNNING GET DEVICES FROM INSIDE VUE WITH THIS: ', this);

        window.setTimeout(function () {
            $.ajax({
                type: 'GET',
                url: '/Device/GetDevices',
                data: {},
                cache: false,
                complete: function (jqxhr, status) {
                    console.log('ajax completed with status : ', status);
                },
                success: function (data, status, jqxhr) {
                    console.log('ajax success data: ', data, '\nstatus: ', status);
                    this.devices = data;
                }
            });
            getDevices();
        },
        500);
    };

    var app = new Vue({
        el: "#deviceDetailsVue",
        data: {
            devices: {}
        },
        mounted: function () {
            getDevices.call(this);
        }
    });

})(window.SERVICE.Devices);