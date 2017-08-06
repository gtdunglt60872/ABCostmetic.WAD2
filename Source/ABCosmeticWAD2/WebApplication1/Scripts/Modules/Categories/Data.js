
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
    Modal: {
        Element: $('#myModal'),
        SaveButton: $('#save-btn'),
        Show: function () {
            page.Modal.Element.modal('show');
            page.Modal.SaveButtonInit();
           
        },
        SaveButtonInit: function () {
            page.Modal.SaveButton.on('click',
                function () {
                    $("#create-form").validate({
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
                    $("#create-form").valid();
                    // Refresh table datasource
                    // Reinitialization table
                    //page.Tables.HandleData();
                });
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