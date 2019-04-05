$(document).ready(function () {
    booking.registerEvents();
});


var booking = {
    registerEvents: function () {
        $('#frmBooking').validate({
            rules: {
                name_booking: "required",
                address_booking: "required",
                email_booking: {
                    required: true,
                    email: true
                },
                telephone_booking: {
                    required: true,
                    number: true
                },
                birth_booking: {
                    required: true,
                    date: true
                }
            },
            messages: {
                name_booking: "Yêu cầu nhập tên",
                address_booking: "Yêu cầu nhập địa chỉ",
                email_booking: {
                    required: "Bạn cần nhập email",
                    email: "Định dạng email chưa đúng"
                },
                telephone_booking: {
                    required: "Yêu cầu nhập số điện thoại",
                    number: "Số điện thoại phải là số."
                },
                birth_booking: "Yêu cầu điền ngày tháng năm sinh"
            }
        });

        $('#btnBooked').off('click').on('click', function (e) {
            e.preventDefault();
            var isValid = $('#frmBooking').valid();
            if (isValid) {
                booking.createBooking();
            } else {
                console.log("not ok");
            }
        });

        $('#healthinsurance').change(function () {
            if (this.checked) {
                $(".treatments").find('strong').text('140VNĐ -(30%)');
                $('#chargeTotal').text('140VNĐ');
            } else {
                $(".treatments").find('strong').text('200VNĐ');
            }
        });

        $('#chkUserLoginInfo').off('click').on('click', function () {
            if ($(this).prop('checked'))
                booking.getLoginUser();
            else {
                $('#firstname_booking').val(''),
                    $('#address_booking').val(''),
                    $('#telephone_booking').val(''),
                    $('#email_booking').val('')
            }
        });
    },

    getLoginUser: function () {
        $.ajax({
            url: '/Booking/GetUser',
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var user = response.data;
                    var gender = user.Gender;
                    console.log(user.BirthDay);
                    if (gender) {
                        $('#gender').val('true');
                    } else {
                        $('#gender').val('false');
                    }
                    $('#firstname_booking').val(user.FullName),
                        $('#address_booking').val(user.Address),
                        // $('#birth_booking').val(),
                        $('#telephone_booking').val(user.PhoneNumber),
                        $('#email_booking').val(user.Email)

                }
            }
        });
    },

    cancelAppoinment: function(appoinmentId) {
        $.ajax({
            url: '/Booking/CancelAppoinment',
            type: 'GET',
            dataType: 'json',
            data: {
                appoinmentID: appoinmentId
            },
            success: function(response) {
                if (response.status) {
                    booking.createBooking();
                }
            }
        });
    },

    createBooking: function () {
        var healthinsurance = false;
        if ($('#healthinsurance').is(':checked')) {
            healthinsurance = true;
        }

        var appoinment = {
            DoctorID: $('#doctor_id').val(),
            FullName: $('#firstname_booking').val(),
            Address: $('#address_booking').val(),
            BirthDay: $('#birth_booking').val(),
            PhoneNumber: $('#telephone_booking').val(),
            Email: $('#email_booking').val(),
            Gender: $('#gender').find('option:selected').val(),
            Healthinsurance: healthinsurance,
            Status : false
        }
        $.ajax({
            url: '/Booking/BookedDoctor',
            type: 'POST',
            dataType: 'json',
            data: {
                appoinmentViewModel: JSON.stringify(appoinment)
            },
            success: function(response) {
                if (response.status) {
                    window.location.href = "/thank-booking.html";
                } else {
                    if (response.error == "dulicate-appoinment") {
                        bootbox.confirm({
                            message: "Bạn muốn hủy lịch khám cũ ?",
                            buttons: {
                                cancel: {
                                    label: 'Không',
                                    className: 'btn-danger'
                                },
                                confirm: {
                                    label: 'Có',
                                    className: 'btn-success'
                                },
                            },
                            callback: function (result) {
                                if (result == true) {
                                    var appoinmentId = parseInt(response.appoinmentID);
                                    booking.cancelAppoinment(appoinmentId);
                                } else {
                                    window.location.href = "/";
                                }
                            }
                        });
                    }
                }
            }
        });
    }

}