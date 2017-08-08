
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
        ModalElement: $('#myModal'),
        ModalBody: $("#modal-body"),
        ModalFooter: $("#modal-footer"),
        SaveButton: $('#save-btn'),
        EditButton: $('#edit-btn'),
        DeleteButton: $('#delete-btn'),
        FormCreate: 'create-form',
        FormEdit:'edit-form',
        Show: function (data) {
            page.Modal.Init(data);
            page.Modal.ModalElement.modal('show');
        },
        SaveButtonInit: function () {
            page.Modal.ModalFooter.on('click', '#save-btn',
                function () {
                    page.Modal.FormValidate(page.Modal.FormCreate);
                    // Refresh table datasource
                    // Reinitialization table
                    //page.Tables.HandleData();
                });
        },
        EditButtonInit: function () {
            page.Modal.ModalFooter.delegate('#edit-btn', 'click',
                function () {
                    page.Modal.FormValidate(page.Modal.FormEdit);
                });
        },
        DeleteButtonInit: function () {
            page.Modal.ModalFooter.delegate('#delete-btn', 'click',
                function () {
                    page.Modal.FormValidate(page.Modal.FormEdit);
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
        FormValidate: function (form) {
            $("#" + form).validate({
                rules: {
                    categoryName: {
                        required: true,
                        minlength: 6
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
                        minlength: jQuery.validator.format("At least {0} characters required!")
                    }
                }
            });
            $("#" + form).valid();
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