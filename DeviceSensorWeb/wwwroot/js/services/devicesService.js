﻿var SERVICE = window.SERVICE || (window.SERVICE = {});
SERVICE.Devices =
(function ($) {

    var state = {

        init: false
    };

    $(function () {
        init();
    });

    var init = function () {
        state.init = true;
    };

    var getDevices = function () {
        if (!state.init) {
            return {};
        }

        $.ajax({
            type: 'GET',
            url: '/Device/GetDevices',
            data: {},
            cache: false,
            complete: function () {

            }
        });

    };

    return {
        getDevices: function () { return getDevices(); }
    };

})(jQuery);
