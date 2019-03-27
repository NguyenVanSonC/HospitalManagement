$(document).ready(function() {
    datepicker.init();
    datepicker.registerEvents();
});

var datepicker = {
    init: function () {
        $.datepicker.regional["vi-VN"] =
            {
                closeText: "Đóng",
                prevText: "Trước",
                nextText: "Sau",
                currentText: "Hôm nay",
                monthNames: ["Tháng một", "Tháng hai", "Tháng ba", "Tháng tư", "Tháng năm", "Tháng sáu", "Tháng bảy", "Tháng tám", "Tháng chín", "Tháng mười", "Tháng mười một", "Tháng mười hai"],
                monthNamesShort: ["Một", "Hai", "Ba", "Bốn", "Năm", "Sáu", "Bảy", "Tám", "Chín", "Mười", "Mười một", "Mười hai"],
                dayNames: ["Chủ nhật", "Thứ hai", "Thứ ba", "Thứ tư", "Thứ năm", "Thứ sáu", "Thứ bảy"],
                dayNamesShort: ["CN", "Hai", "Ba", "Tư", "Năm", "Sáu", "Bảy"],
                dayNamesMin: ["CN", "T2", "T3", "T4", "T5", "T6", "T7"],
                weekHeader: "Tuần",
                dateFormat: "dd/mm/yy",
                firstDay: 1,
                isRTL: false,
                showMonthAfterYear: false,
                yearSuffix: ""
            };
        $.datepicker.setDefaults($.datepicker.regional["vi-VN"]);

        var dateToday = new Date();
        $("#calendar").datepicker({
            minDate: dateToday,
            beforeShowDay: $.datepicker.noWeekends,
            dateFormat: "yy-mm-dd"
        });
    },

    registerEvents: function () {
        $("#calendar").on("change", function () {
            var selected = $(this).val();
            var doctorId = parseInt($(this).data('id'));
            datepicker.bookingdoctor_day(selected, doctorId);
        });

        $('input:radio[name=radio_time]').change(function () {
            var selected = $(this).val();
            var doctorId = parseInt($(this).data('id'));
            datepicker.bookingdoctor_time(selected, doctorId);
        });

        $('#btnCreateBooking').off('click').on('click', function (e) {
            e.preventDefault();
            datepicker.checkTimeBooking();
        });
    },

    bookingdoctor_day: function (day, doctorId) {
        $.ajax({
            url: '/Booking/BookingDoctor',
            data: {
                doctorId: doctorId,
                day: day
            },
            type: 'POST',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    datepicker.registerEvents();
                    console.log("day ok");
                } else {
                    alert("Bạn đã đặt lịch 1 bác sĩ rồi!");
                }
            }
        });
    },

    bookingdoctor_time: function (time, doctorId) {
        $.ajax({
            url: '/Booking/BookingDoctor',
            data: {
                doctorId: doctorId,
                time: time
            },
            type: 'POST',
            dataType: 'json',
            success: function (res) {
                if (re.status) {
                    datepicker.registerEvents();
                    console.log("time ok");
                } else {
                    alert("Bạn đã đặt lịch 1 bác sĩ rồi!");
                }
            }
        });
    },

    checkTimeBooking: function () {
        $.ajax({
            url: '/Booking/CheckTimeBooking',
            type: 'POST',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    window.location.href = "/dat-lich.html";
                } else {
                    notification.init();
                    notification.displayError("Vui Lòng chọn lịch khám");
                }
            }
        });
    }
}