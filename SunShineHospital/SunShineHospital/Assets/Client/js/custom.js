$(document).ready(function () {
    common.registerEvents();

});
var common = {
    registerEvents: function () {
        $("#softby").change(function () {
            var softselected = $(this).children("option:selected").val();
            var url = window.location.href;
            var indexofurl = url.indexOf("?");
            url = url.substr(0, indexofurl);
            location.replace(url + softselected);
        });

        $("#softbydepartment").change(function () {
            var softselected = $(this).children("option:selected").val();
            var url = window.location.href;
            var indexofurl = url.indexOf("?");
            url = url.substr(0, indexofurl);
            location.replace(url + softselected);
        });
        $("#btnLogout").off('click').on('click', function (e) {
            e.preventDefault();
            common.Logout();
        });
    },

    Logout: function() {
        $.ajax({
            url: '/Account/LogOut',
            type: 'POST',
            dataType: 'json',
            success: function(response) {
                if (response.status) {
                    var child = document.getElementById("top_access").children;
                    $(child[3]).show();
                    $(child[4]).show();
                    $(child[2]).remove();
                    $(child[1]).remove();
                }
            }
        });
    }
}