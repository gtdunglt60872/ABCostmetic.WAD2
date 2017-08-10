
var page = {
    Tables: {
        Element: $('#example'),
        Opt: {
            data: [],
            columns: [
                { data: 'CategoryId' },
                { data: 'CategoryName' },
                { data: 'Description' }
            ],
            "paging": true,
            "lengthChange": true,
            "searching": true,
            "ordering": true,
            "info": true,
            "autoWidth": true
        },
        Table: function () {
            var table = page.Tables.Element.DataTable(page.Tables.Opt);

            $('#example tbody').on('click',
                'tr',
                function () {
                    var data = table.row(this).data();
                    page.Modal.Show(data);
                    //alert('You clicked on ' + data.CategoryName + '\'s row');

                });

            return table;
        },
        GetData: function () {
            var ajax = $.ajax({
                type: "POST",
                url: "/Categories/ListData",
                dataType: "json"
            });

            return ajax;
        },
        HandleData: function () {
            var loadData = page.Tables.GetData();
            // done: new feature in jquery 3.1.1
            loadData.done(function (resp) {
                // Destroy old table
                page.Tables.Element.dataTable().fnDestroy();

                // Set data for table
                page.Tables.Opt.data = resp.ListData;
                if (page.Tables.Opt.aaData) {
                    page.Tables.Opt.aaData = resp.ListData;
                }
                // Draw table
                page.Tables.Table();
            });
        }
    },
    Template: {
        ModalBodyTmpl: $("#modal-body-template"),
        ModalBodyEditTmpl: $("#modal-body-edit-template"),
        ModalFooterTmpl: $("#modal-footer-template"),
        ModalFooterEditTmpl: $("#modal-footer-edit-template")
    },
    Modal: {
        ModalMsg: $('#msg-modal'),
        ModalElement: $('#myModal'),
        ModalBody: $("#modal-body"),
        ModalFooter: $("#modal-footer"),
        SaveButton: $('#save-btn'),
        EditButton: $('#edit-btn'),
        DeleteButton: $('#delete-btn'),
        FormCreate: 'create-form',
        FormEdit: 'edit-form',
        Show: function (data) {
            page.Modal.Init(data);
            page.Modal.ModalElement.modal('show');
        },
        SaveButtonInit: function () {
            page.Modal.ModalFooter.on('click', '#save-btn',
                function () {
                    var formValid = page.Modal.Form.Validate(page.Modal.FormCreate);

                    if (formValid) {
                        var formData = page.Modal.Form.GetFormData();
                        $.ajax({
                            type: "POST",
                            url: "/Categories/Create",
                            data: formData,
                            dataType: "json",
                            async: false,
                            success: function (resp) {
                                if (resp.Msg === 'Success') {
                                    page.Modal.ModalElement.modal('hide');
                                    // Refresh table datasource
                                    // Reinitialization table
                                    page.Tables.HandleData();
                                } else {
                                    $("#msg-modal-body").empty();
                                    $('#msg-modal-body-template').tmpl(resp).appendTo("#msg-modal-body");
                                    page.Modal.ModalMsg.modal('show');
                                }
                            }
                        });
                    }
                });
        },
        EditButtonInit: function () {
            page.Modal.ModalFooter.delegate('#edit-btn', 'click',
                function () {
                    var formValid = page.Modal.Form.Validate(page.Modal.FormEdit);
                    if (formValid) {
                        var formData = page.Modal.Form.GetFormData();
                        $.ajax({
                            type: "POST",
                            url: "/Categories/Edit",
                            data: formData,
                            dataType: "json",
                            async: false,
                            success: function (resp) {
                                if (resp.Msg === 'Success') {
                                    page.Modal.ModalElement.modal('hide');
                                    // Refresh table datasource
                                    // Reinitialization table
                                    page.Tables.HandleData();
                                } else {
                                    $("#msg-modal-body").empty();
                                    $('#msg-modal-body-template').tmpl(resp).appendTo("#msg-modal-body");
                                    page.Modal.ModalMsg.modal('show');
                                }
                            }
                        });
                    }
                });
        },
        DeleteButtonInit: function () {
            page.Modal.ModalFooter.delegate('#delete-btn', 'click',
                function () {
                    var formData = page.Modal.Form.GetFormData();
                    $.ajax({
                        type: "POST",
                        url: "/Categories/Delete",
                        data: { id: formData.CategoryId },
                        dataType: "json",
                        async: false,
                        success: function (resp) {
                            if (resp.Msg === 'Success') {
                                page.Modal.ModalElement.modal('hide');
                                // Refresh table datasource
                                // Reinitialization table
                                page.Tables.HandleData();
                            } else {
                                $("#msg-modal-body").empty();
                                $('#msg-modal-body-template').tmpl(resp).appendTo("#msg-modal-body");
                                page.Modal.ModalMsg.modal('show');
                            }
                        }
                    });
                });
        },
        Init: function (data) {
            if (data) {
                // Show modal edit
                page.Modal.ModalFooter.empty();
                page.Template.ModalFooterEditTmpl.tmpl().appendTo(page.Modal.ModalFooter);

                page.Modal.ModalBody.empty();
                page.Template.ModalBodyEditTmpl.tmpl(data).appendTo(page.Modal.ModalBody);

                //Init button
                page.Modal.EditButtonInit();
                page.Modal.DeleteButtonInit();
            } else {
                page.Modal.ModalFooter.empty();
                page.Template.ModalFooterTmpl.tmpl().appendTo(page.Modal.ModalFooter);

                page.Modal.ModalBody.empty();
                page.Template.ModalBodyTmpl.tmpl().appendTo(page.Modal.ModalBody);
                page.Modal.SaveButtonInit();
            }
        },
        Form: {
            Validate: function (form) {
                $("#" + form).validate({
                    rules: {
                        categoryName: {
                            required: true,
                            minlength: 6,
                            maxlength: 15
                        },
                        categoryDescription: {
                            required: true,
                            minlength: 6
                        }
                    },
                    messages: {
                        categoryName: {
                            required: "This field is required.",
                            minlength: jQuery.validator.format("At least {0} characters required!")
                        },
                        categoryDescription: {
                            required: "This field is required.",
                            minlength: jQuery.validator.format("At least {0} characters required!"),
                            maxlength: jQuery.validator.format("Maximum {0} characters!")
                        }
                    }
                });
                return $("#" + form).valid();
            },
            GetFormData: function () {
                var formData = {
                    CategoryId: $('#categoryId').val(),
                    CategoryName: $('#categoryName').val(),
                    Description: $('#categoryDescription').val()
                }

                return formData;
            }
        }
    },
    Init: {
        NewCategoryBtn: $("#new-category-btn"),
        RefreshBtn: $('#refresh-btn'),
        Load: function () {
            page.Init.NewCategoryBtn.on('click', function () {
                page.Modal.Show();
            });

            page.Init.RefreshBtn.on('click', function () {
                page.Tables.HandleData();
            });

            page.Tables.HandleData();
        }
    }
}

$(function () {
    page.Init.Load();
});