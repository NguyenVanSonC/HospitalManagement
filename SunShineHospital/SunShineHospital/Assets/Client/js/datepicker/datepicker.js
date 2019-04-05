$(document).ready(function() {
    datepicker.init();
    datepicker.registerEvents();
    //var doctorId = parseInt($("#calendar").data('id'));
    //datepicker.getCalendarDoctor(doctorId);
});

function sameDay(d1, d2) {
    return d1.getFullYear() === d2.getFullYear() &&
        d1.getMonth() === d2.getMonth() &&
        d1.getDate() === d2.getDate();
}

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
            dateFormat: "yy-mm-dd",
            onSelect: function (selected) {
                var doctorId = parseInt($("#calendar").data('id'));
                datepicker.getCalendarDoctor(doctorId, selected);
                var doctorId = parseInt($(this).data('id'));
                datepicker.bookingdoctor_day(selected, doctorId);
            }
        });
    },

    
    registerEvents: function () {
        $('input:radio[name=radio_time]').change(function () {
            var selected = $(this).val();
            var parent = $(this).parent();
            var doctorId = parseInt($(this).data('id'));
            datepicker.bookingdoctor_time(selected, doctorId);
            parent.find('label').css("background-color", "black");
            parent.find('label').css("color", "#fff");
            $('input:radio[name=radio_time]').each(function(i) {
                if (selected != $(this).val()) {
                    var parent = $(this).parent();
                    if ($(this).is(':enabled')) {
                        parent.find('label').css("background-color", "#f8f8f8");
                        parent.find('label').css("color", "black");
                    }
                }
            });
        });

        $('#btnCreateBooking').off('click').on('click', function (e) {
            e.preventDefault();
            datepicker.checkTimeBooking();
        });
    },

    getCalendarDoctor: function (doctorId, selected) {
        $.ajax({
            url: '/Booking/GetCalendarDoctor',
            data: {
                doctorId: doctorId
            },
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var dateDoctor = response.data;
                    if (selected != null) {
                        var currentdate = new Date();
                        var daySelected = new Date(selected);
                        var currentTime = currentdate.getHours() * 60 + currentdate.getMinutes();
                        $('input:radio[name=radio_time]').each(function (i) {
                            var parent = $(this).parent();
                            if (sameDay(currentdate, daySelected)) {
                                var timeofInput = $(this).val();
                                var array = timeofInput.split(":");
                                var hour = parseInt(array[0]);
                                var minute = parseInt(array[1]);
                                var totalTime = (hour * 60) + minute;
                                if (totalTime < currentTime) {
                                    parent.find('label').css("background-color", "#74d1c6");
                                    parent.find('label').css("color", "#fff");
                                    $(this).attr("disabled", true);
                                    console.log("thoa man dk");
                                    parent.find('label').on('mouseover mouseenter mouseleave mouseup mousedown',
                                        function () {
                                            parent.find('label').css("background-color", "#74d1c6");
                                            parent.find('label').css("color", "#fff");
                                        });
                                    console.log("Same Day");
                                }
                            } else {
                                parent.find('label').css("background-color", "#f8f8f8");
                                parent.find('label').css("color", "black");
                                $(this).attr("disabled", false);
                                parent.find('label').on('mouseover', function () {
                                    $(this).css("background-color", "#e74e84");
                                    $(this).css("color", "#fff");
                                });
                                parent.find('label').on('mouseout', function () {
                                    $(this).css("background-color", "#f8f8f8");
                                    $(this).css("color", "black");
                                });
                                console.log("Different Day");
                            }
                        });
                        
                        $.each(dateDoctor, function (key, value) {
                            var date = new Date(value.Day);
                            if (sameDay(date, daySelected)) {
                                $('input:radio[name=radio_time]').each(function (i) {
                                    var parent = $(this).parent();
                                    if ($(this).val() == value.Time) {
                                        parent.find('label').css("background-color", "#74d1c6");
                                        parent.find('label').css("color", "#fff");
                                        $(this).attr("disabled", true);
                                        parent.find('label').on('mouseover mouseenter mouseleave mouseup mousedown',
                                            function () {
                                                parent.find('label').css("background-color", "#74d1c6");
                                                parent.find('label').css("color", "#fff");
                                            });
                                    }
                                });
                            } 
                        });
                    }
                }
            }
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
                if (res.status) {
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
                    if (res.error == "day") {
                        notification.displayError("Vui Lòng chọn ngày khám");
                    } else {
                        notification.displayError("Vui Lòng chọn giờ khám");
                    }
                    
                }
            }
        });
    }
}