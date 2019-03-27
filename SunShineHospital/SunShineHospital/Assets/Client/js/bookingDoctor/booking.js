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

        $('#healthinsurance').change(function() {
            if (this.checked) {
                $(".treatments").find('strong').text('140VNĐ -(30%)');
                $('#chargeTotal').text('140VNĐ');
            } else {
                $(".treatments").find('strong').text('200VNĐ');
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
                    console.alert("Thank you so much");
                }
            }
        });
    }

}