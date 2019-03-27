var notification = {
    init: function () {
        toastr.options = {
            "debug": false,
            "positionClass": "toast-top-center",
            "onclick": null,
            "fadeIn": 300,
            "fadeOut": 1000,
            "timeOut": 3000,
            "extendedTimeOut": 1000
        };
    },

    displaySuccess: function (message) {
        toastr.success(message);
    },

    displayError: function (error) {
        if (Array.isArray(error)) {
            error.each(function (err) {
                toastr.error(err);
            });
        }
        else {
            toastr.error(error);
        }
    },

    displayWarning: function (message) {
        toastr.warning(message);
    },

    displayInfo: function (message) {
        toastr.info(message);
    }
}