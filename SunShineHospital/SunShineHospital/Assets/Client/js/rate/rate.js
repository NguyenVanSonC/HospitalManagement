$(document).ready(function () {
    $('#stars li').on('mouseover', function () {
        var onStar = parseInt($(this).data('value'), 10); 
        $(this).parent().children('li.star').each(function (e) {
            if (e < onStar) {
                $(this).addClass('hover');
            }
            else {
                $(this).removeClass('hover');
            }
        });

    }).on('mouseout', function () {
        $(this).parent().children('li.star').each(function (e) {
            $(this).removeClass('hover');
        });
    });

    $('#stars li').on('click', function () {
        var onStar = parseInt($(this).data('value'), 10);
        var stars = $(this).parent().children('li.star');

        for (i = 0; i < stars.length; i++) {
            $(stars[i]).removeClass('selected');
        }

        for (i = 0; i < onStar; i++) {
            $(stars[i]).addClass('selected');
        }

        var ratingValue = parseInt($('#stars li.selected').last().data('value'), 10);
        var doctorId = $('#header-rate').data('id');
        rating.createRating(ratingValue, doctorId);
    });
});

var rating = {
    registerEvents: function() {

    },

    createRating: function (countRate, doctorId) {
        var rateModel = {
            DoctorID: doctorId,
            CountRate: countRate
        }
        $.ajax({
            url: '/Rate/CreateRate',
            type: 'POST',
            dataType: 'json',
            data: {
                rateViewModel: JSON.stringify(rateModel)
            },
            success: function (res) {
                if (res.status) {
                    console.log("OK");
                    var doctorId = $('#rate-doctor-id').data('id');
                    rating.getRateData(doctorId);
                    notification.init();
                    notification.displaySuccess("Bạn đã đánh giá " + countRate + " sao");
                }
            }
        });
    },

    getRateData: function(doctorId) {
        $.ajax({
            url: '/Rate/GetRateDetail',
            type: 'GET',
            dataType: 'json',
            data: {
                doctorId: doctorId
            },
            success: function (res) {
                if (res.status) {
                    var data = res.data;
                    $('#average-rate').html(data.AverageRate);
                    console.log(data.ArrayPercent);
                    var elementChild = $('#rate-list').find('.progress-bar');
                    $.each(elementChild, function(i, element) {
                        $(this).css("width", data.ArrayPercent[i]+'%');
                    });
                    console.log(elementChild);
                }
            }
        });
    }
}
