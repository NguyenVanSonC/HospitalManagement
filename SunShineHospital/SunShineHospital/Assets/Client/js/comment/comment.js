$(document).ready(function () {
    comment.registerEvents();
    var doctorId = parseInt($('#comment-doctor-id').data('id'));
    comment.loadData(doctorId);
});

var comment = {
    registerEvents: function () {
        $('.btnReplyComment').off('click').on('click', function (e) {
            e.preventDefault();
            var parentCommentId = $(this).data('id');
            $('#frmComment-' + parentCommentId).show();
        });

        $('.btnDeletecomment').off('click').on('click', function (e) {
            e.preventDefault();
            var commentId = $(this).data('id');
            var doctorId = $(this).data('doctor');
            bootbox.confirm({
                message: "Bạn muốn xóa bình luận không ?",
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
                        comment.deleteComment(commentId, doctorId);
                    }
                }
            });
        });

        $('.btnEditcomment').off('click').on('click', function (e) {
            e.preventDefault();
            var commentId = $(this).data('id');
            $('#frmComment-' + commentId).attr("data-status", "edit");
            $('#frmComment-' + commentId).show();
            comment.getCommentById(commentId);
        });

        $('.btnCreateComment').off('click').on('click', function (e) {
            e.preventDefault();
            var parentCommentId = $(this).data('id');
            var doctorId = $(this).data('doctor');
            $('#frmComment-' + parentCommentId).validate({
                rules: {
                    comments: {
                        required: true,
                        maxlength: 100
                    }
                },
                messages: {
                    comments: {
                        required: "Nhập bình luận",
                        maxlength: "Bình luận của bạn quá dài"
                    }
                }
            });
            var isValid = $('#frmComment-' + parentCommentId).valid();
            if (isValid) {
                var content = $('.content-' + parentCommentId).val();
                var status = $('#frmComment-' + parentCommentId).data('status');
                if (status == 'edit') {
                    comment.updateComment(parentCommentId, doctorId, content);
                } else {
                    comment.createComment(parentCommentId, doctorId, content);
                }
            }
        });

        $('.btnCreateMainComment').off('click').on('click', function(e) {
            e.preventDefault();
            var content = $('.content-main').val();
            var doctorId = $(this).data('doctor');
            $('#frmComment').validate({
                rules: {
                    comments: {
                        required: true,
                        maxlength: 100
                    }
                },
                messages: {
                    comments: {
                        required: "Nhập bình luận",
                        maxlength: "Bình luận của bạn quá dài"
                    }
                }
            });
            var isValid = $('#frmComment').valid();
            if (isValid) {
                comment.createComment(null, doctorId, content);
            }
        });
    },

    deleteComment: function (commentId, doctorId) {
        $.ajax({
            url: '/Comment/DeleteComment',
            type: 'POST',
            dataType: 'json',
            data: {
                commentId: commentId
            },
            success: function (res) {
                if (res.status) {
                    comment.loadData(doctorId);
                }
            }
        });
    },

    updateComment: function (commentId, doctorId, content) {
        var commentModel = {
            DoctorID: doctorId,
            ID: commentId,
            Content: content
        }
        $.ajax({
            url: '/Comment/UpdateComment',
            type: 'POST',
            dataType: 'json',
            data: {
                commentViewModel: JSON.stringify(commentModel)
            },
            success: function (res) {
                if (res.status) {
                    $('#frmComment-' + commentId).attr("data-status", "");
                    $('#frmComment-' + commentId).hide();
                    comment.loadData(doctorId);
                }
            }
        });
    },

    getCommentById: function(commentId) {
        $.ajax({
            url: '/Comment/GetComment',
            type: 'GET',
            dataType: 'json',
            data: {
                commentId: commentId
            },
            success: function (res) {
                if (res.status) {
                    var contentComment = res.data.Content;
                    $('.content-' + commentId).val(contentComment);
                }
            }
        });
    },

    createComment: function (parentId, doctorId, content) {
        var commentModel = {
            DoctorID: doctorId,
            ParentID: parentId,
            Content: content
        }
        $.ajax({
            url: '/Comment/CreateComment',
            type: 'POST',
            dataType: 'json',
            data: {
                commentViewModel: JSON.stringify(commentModel)
            },
            success: function(res) {
                if (res.status) {
                    $('#frmComment-' + parentId).hide();
                    comment.loadData(doctorId);
                }
            }
        });
    },

    loadData: function (doctorId) {
        $.ajax({
            url: '/Comment/GetAll',
            type: 'GET',
            dataType: 'json',
            data: {
                doctorId: doctorId
            },
            success: function (res) {
                if (res.status) {
                    var template = $('#tplComments').html();
                    var html = '';
                    var data = res.data;
                    $.each(data, function(i, item) {
                        if (item.ParentID == null) {
                            var parentHtml = '';
                            var childTemplate = $('#tplReplyComments').html();
                            var childHtml = '';
                            var dateComment = new Date(parseInt(item.CreatedDate.replace('/Date(', '')));
                            var formatDate = "Lúc "+ dateComment.getHours() + " Giờ Ngày "+dateComment.getDay()+ "/" + dateComment.getMonth();
                            parentHtml += Mustache.render(template, {
                                CommentId: item.ID,
                                UserName: item.User.FullName,
                                CreatedDate: formatDate,
                                Content: item.Content
                            });
                            var lengthStr = (parentHtml.length);
                            parentHtml = parentHtml.substr(0, lengthStr - 6);
                            $.each(data, function (j, childItem) {
                                var childDateComment = new Date(parseInt(childItem.CreatedDate.replace('/Date(', '')));
                                var childFormatDate = "Lúc " + childDateComment.getHours() + " Giờ Ngày " + childDateComment.getDay() + "/" + childDateComment.getMonth();
                                if (childItem.ParentID == item.ID) {
                                    childHtml += Mustache.render(childTemplate, {
                                        childCommentId: childItem.ID,
                                        childUserName: childItem.User.FullName,
                                        childCreatedDate: childFormatDate,
                                        childContent: childItem.Content
                                    });
                                }
                            });
                            html += parentHtml + childHtml + '</li>';
                        }

                    });
                    var formTemplate = $('#tplCreateComment').html();
                    html += formTemplate;
                    $('#commentBody').html(html);
                    comment.registerEvents();
                }
            }
        });
    }
}