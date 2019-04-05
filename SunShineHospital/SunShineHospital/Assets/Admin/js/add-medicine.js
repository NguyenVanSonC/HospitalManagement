$(document).ready(function () {
    medicine.registerEvents();
    medicine.loadData();
});

var medicine = {
    registerEvents: function () {
        $('#add-medicine').off('click').on('click', function (e) {
            e.preventDefault();
            $('#medicine-name').attr('disabled', true);
            $("#myModal").modal();
            medicine.showFormPrescription();
        });

        $('#category-medicine').off('click').on('click', function (e) {
            e.preventDefault();
            var medicineCategotyId = $(this).val();
            medicine.getMedicineName(medicineCategotyId);
        });

        $('#frmMedicine').validate({
            rules: {
                calendar_medicine: "required",
                number_medicine: {
                    required: true,
                    number: true
                },
                number_day: {
                    required: true,
                    number: true
                }
            },
            messages: {
                calendar_medicine: "Yêu chọn toa thuốc",
                number_day: {
                    required: "Yêu cầu nhập số ngày uống",
                    number: "Số ngày phải là số."
                },
                number_medicine: {
                    required: "Yêu cầu nhập số lượng thuốc",
                    number: "Số lượng phải là số"
                }
            }
        });

        $('#btn-frmMedicine').off('click').on('click', function (e) {
            e.preventDefault();
            var isValid = $('#frmMedicine').valid();
            if (isValid) {
                $("#myModal").modal('hide');
                var status = $('#frmMedicine').data('status');
                if (status == 'update') {
                    var medicineId = $('#frmMedicine').data('medicineid');
                    var appointmentId = $('#frmMedicine').data('id');
                    medicine.updateItem(medicineId, appointmentId);
                } else {
                    medicine.createPrescription();
                }
            }
        });

        $('.btnDeleteAll').off('click').on('click', function (e) {
            e.preventDefault();
            var appointmentId = $(this).data('id');
            medicine.deleteAll(appointmentId);
        });

        $('.btnDeleteItem').off('click').on('click', function (e) {
            e.preventDefault();
            var appointmentId = $('#frmMedicine').data('id');
            var medicineId = $(this).data('id');
            medicine.deleteItem(medicineId, appointmentId);
        });

        $('.btnEditItem').off('click').on('click', function (e) {
            e.preventDefault();
            console.log("aaaaaaaaaaa");
            var medicineId = $(this).data('id');
            var medicineName = $(this).data('name');
            $("#myModal").modal();
            $(':input', '#frmMedicine')
                .not(':button, :submit, :reset, :hidden')
                .val('');
            var option = $('#medicine-name');
            option.append($('<option>', {
                value: 0,
                text: medicineName
            }));
            option.attr("disabled", true);
            $('#category-medicine').attr("disabled", true);
            $('#frmMedicine').attr("data-status", "update");
            $('#frmMedicine').attr("data-medicineid", medicineId.toString());
        });
        
    },

    updateItem: function (medicineId, appointmentId) {
        var before_meal = false;
        if ($('#before-meal').is(':checked')) {
            before_meal = true;
        }
        var medicineModel = {
            MedicineId: medicineId,
            Quantity: $('#number_medicine').val(),
            TotalDay: $('#number_day').val(),
            Calendar: $('#calendar_medicine').val(),
            BeforeMeal: before_meal,
            AppointmentId: appointmentId
        }
        $.ajax({
            url: '/Admin/Prescription/Update',
            type: 'GET',
            dataType: 'json',
            data: {
                medicineViewModel: JSON.stringify(medicineModel)
            },
            success: function (respond) {
                if (respond.status) {
                    $('#frmMedicine').attr("data-status", "");
                    medicine.loadData();
                }
            }
        });
    },

    deleteItem: function(medicineId, appointmentId) {
        $.ajax({
            url: '/Admin/Prescription/Delete',
            type: 'GET',
            dataType: 'json',
            data: {
                medicineId: medicineId,
                appointmentId: appointmentId
            },
            success: function (respond) {
                if (respond.status) {
                    medicine.loadData();
                }
            }
        });
    },

    deleteAll: function(appointmentId) {
        $.ajax({
            url: '/Admin/Prescription/DeleteAll',
            type: 'GET',
            dataType: 'json',
            data: {
                appointmentId: appointmentId
            },
            success: function(respond) {
                if (respond.status) {
                    console.log("Vao");
                    medicine.loadData();
                }
            }
        });
    },

    
    getTotalOrder: function () {
        var listStatus = $('.txtBeforeMeal');
        $.each(listStatus, function (i, item) {
            var status = $(item).data('status');
            if (status) {
                $(item).show();
            }
        });
        var listTextBox = $('.txtQuantity');
        var total = 0;
        $.each(listTextBox, function (i, item) {
            total += parseInt($(item).text()) * parseFloat($(item).data('price'));
        });
        return total;
    },

    showFormPrescription: function () {
        var uri = 'http://localhost:55364/api/medicinecategories';
        $.ajax({
            url: uri,
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                var option = $('#category-medicine');
                option.attr("disabled", false);
                $.each(data, function (i, item) {
                    option.append($('<option>', {
                        value: item.ID,
                        text: item.Name
                    }));
                });
            },
            error: function (xhr, textStatus, err) {
                console.log(err);
            }
        })
    },

    getMedicineName: function (medicinecategoryid) {
        var uri = 'http://localhost:55364/api/medicines/category/' + medicinecategoryid;
        $.ajax({
            url: uri,
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                var option = $('#medicine-name');
                if (data.length > 0) {
                    option.find('option').remove();
                    option.attr('disabled', false);
                    $.each(data, function (i, item) {
                        option.append($('<option>', {
                            value: item.ID,
                            text: item.Name
                        }));
                    });
                }
            },
            error: function (xhr, textStatus, err) {
                console.log(err);
            }
        })
    },

    createPrescription: function () {
        var appointmentId = $('#frmMedicine').data('id');
        var before_meal = false;
        if ($('#before-meal').is(':checked')) {
            before_meal = true;
        }
        var medicineModel = {
            MedicineId: $('#medicine-name').find('option:selected').val(),
            Quantity: $('#number_medicine').val(),
            TotalDay: $('#number_day').val(),
            Calendar: $('#calendar_medicine').val(),
            BeforeMeal: before_meal,
            AppointmentId: appointmentId
        }
        $.ajax({
            url: '/Admin/Prescription/Create',
            type: 'POST',
            dataType: 'json',
            data: {
                medicineViewModel: JSON.stringify(medicineModel)
            },
            success: function(response) {
                if (response.status) {
                    $(':input', '#frmMedicine')
                        .not(':button, :submit, :reset, :hidden')
                        .val('')
                        .removeAttr('checked')
                        .removeAttr('selected');
                    medicine.loadData();
                } else {
                    if (response.message == 'duplicate-medicine') {
                        alert('duplicate-medicine');
                    } else {
                        console.log('fail create');
                    }
                }
            }
        });
    },

    loadData: function () {
        var appointmentId = $('#frmMedicine').data('id');
        $.ajax({
            url: '/Admin/Prescription/GetAll',
            type: 'GET',
            dataType: 'json',
            data: {
                appointmentId: appointmentId
            },
            success: function (respond) {
                if (respond.status) {
                    $('#cartContent').hide();
                    $('#dataTable').show();
                    $('#cartTotalPrice').show();
                    var template = $('#tplCart').html();
                    var html = '';
                    var data = respond.data;
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            MedicineId: item.MedicineId,
                            MedicineName: item.Medicine.Name,
                            Calendar: item.Calendar,
                            Price: item.Medicine.Price,
                            TotalDay: item.TotalDay,
                            Quantity: item.Quantity,
                            BeforeMeal: item.BeforeMeal,
                            MedicineName: item.Medicine.Name,
                            TotalPrice: (item.Medicine.Price * item.Quantity) + ' VNĐ'
                        });
                    });
                    $('#table-medicine').html(html);
                    $('#lblTotalOrder').text(medicine.getTotalOrder() + ' VNĐ');
                } else {
                    $('#dataTable').hide();
                    $('#cartTotalPrice').hide();
                    $('#cartContent').show();
                    $('#cartContent').text('Không có đơn thuốc nào !');
                }
                medicine.registerEvents();
            }
        });
    }
}