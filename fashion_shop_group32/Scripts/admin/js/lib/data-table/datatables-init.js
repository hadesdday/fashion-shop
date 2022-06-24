(function ($) {
    //    "use strict";

    /*  Data Table
      -------------*/

    // $('#bootstrap-data-table').DataTable();

    $("#bootstrap-data-table").DataTable({
        lengthMenu: [
            [10, 20, 50, -1],
            [10, 20, 50, "All"],
        ],
    });

    jQuery.extend(jQuery.fn.dataTableExt.oSort, {
        "numeric-comma-pre": function (a) {
            var x = (a == "-") ? 0 : a.replace(/,/, ".");
            return parseFloat(x);
        },

        "numeric-comma-asc": function (a, b) {
            return ((a < b) ? -1 : ((a > b) ? 1 : 0));
        },

        "numeric-comma-desc": function (a, b) {
            return ((a < b) ? 1 : ((a > b) ? -1 : 0));
        }
    });

    $("#bootstrap-data-table-export").DataTable({
        dom: "lBfrtip",
        lengthMenu: [
            [10, 25, 50, -1],
            [10, 25, 50, "All"],
        ],
        buttons: ["copy", "csv", "excel", "pdf", "print"],
        language: {
            "url": "./assets/js/lib/data-table/vi.json"
        },
    });

    $("#order-table").DataTable({
        dom: "lBfrtip",
        lengthMenu: [
            [10, 25, 50, -1],
            [10, 25, 50, "All"],
        ],
        buttons: ["copy", "csv", "excel", "pdf", "print"],
        language: {
            "url": "./assets/js/lib/data-table/vi.json"
        },
        columnDefs: [
            {type: 'numeric-comma', targets: 4}
        ],
    });

    $("#products-table").DataTable({
        dom: "lBfrtip",
        lengthMenu: [
            [10, 25, 50, -1],
            [10, 25, 50, "All"],
        ],
        buttons: ["copy", "csv", "excel", "pdf", "print"],
        language: {
            "url": "./assets/js/lib/data-table/vi.json"
        },
        columnDefs: [
            {type: 'numeric-comma', targets: 3}
        ],
    });

    $("#row-select").DataTable({
        initComplete: function () {
            this.api()
                .columns()
                .every(function () {
                    var column = this;
                    var select = $(
                        '<select class="form-control"><option value=""></option></select>'
                    )
                        .appendTo($(column.footer()).empty())
                        .on("change", function () {
                            var val = $.fn.dataTable.util.escapeRegex($(this).val());

                            column.search(val ? "^" + val + "đ" : "", true, false).draw();
                        });

                    column
                        .data()
                        .unique()
                        .sort()
                        .each(function (d, j) {
                            select.append('<option value="' + d + '">' + d + "</option>");
                        });
                });
        },
    });
})(jQuery);
