// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.


var dataTable;
function loadDataTable(controller, columns, order = 1, method = 'getall', tableName = '#DT_load', headCreated = false) {
    if (!headCreated) {
        tableHead = $(tableName + ' thead');
        tableHeadColumns = $(tableName + ' thead th');
        tableHeadColumnsLength = tableHeadColumns.length;
        tableHeadContent = "<tr>"
        let j = 0;
        tableHeadColumns.each(function (i) {
            var title = $(tableName + ' thead th').eq($(this).index()).text();
            if (j == 0 || $(this)[0].classList.value.includes("text-center") || j == tableHeadColumnsLength - 1)
            {
                tableHeadContent += '<td></td>';
            }
            else
            {
                tableHeadContent += '<td><input style="width:100%" type="text" placeholder="Search ' + title + '" data-index="' + i + '" /></td>';
            }
            j += 1
        });
        tableHeadContent += "</tr>";
        tableHead.append(tableHeadContent);
    }


    dataTable = $(tableName).DataTable({
        "scrollCollapse": true,
        //"paging": false,
        "scrollX": true,
        //"fixedColumns": true,
        //"scrollY": 400,
        "orderable": false,
        "lengthMenu": [[10, 25, 50, 10000000], [10, 25, 50, "All"]],
        "orderCellsTop": true,
        "fixedHeader": true,
        "serverSide": true,
        "processing": true,
        "dom": 'Bfrtip',
        "buttons": ['pageLength', 'colvis',
            {
                extend: 'copyHtml5',
                text: '<i class="fa fa-files-o"></i>',
                titleAttr: 'Copy Table',
                //className: 'btn-primary'
            },
            {
                extend: 'excelHtml5',
                text: '<i class="fa fa-file-excel-o"></i>',
                titleAttr: 'Export as Excel'
            },
            {
                extend: 'csvHtml5',
                text: '<i class="fa fa-file-text-o"></i>',
                titleAttr: 'Export as CSV'
            },
            {
                extend: 'pdfHtml5',
                text: '<i class="fa fa-file-pdf-o"></i>',
                titleAttr: 'Export as PDF'
            },
            {
                extend: 'print',
                text: '<i class="fa fa-print"></i>',
                titleAttr: 'Print Table'
            }
        ],
        "ajax": {
            "url": "/" + controller + "/" + method,
            "timeout": 600000,
            "type": "POST",

            //"datatype": "json",
            //"success": function (data) {
            //    console.log(data);
            //} 
        },
        'columnDefs': [{
            'targets': columns.length - 1,
            'searchable': false,
            'orderable': false,
            'data': 'id',
            'type': 'checkbox',
            'checkboxes': {
                'selectRow': true
            },
        },
        {
            "searchable": false,
            "orderable": false,
            "targets": 0
        }],
        //'select': {
        //    'style': 'multi'
        //},
        'order': [[order, 'desc']],
        "columns": columns,
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });

    dataTable.on('draw.dt', function () {
        dataTable.column(0, { search: 'applied', order: 'applied', page: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = dataTable.page.info().length * dataTable.page.info().page + i + 1;
        });
    }).draw();

    // Filter event handler
    $(dataTable.table().container()).on('keyup', 'thead input', function () {
        dataTable
            .column($(this).data('index'))
            .search(this.value)
            .draw();
    });

}

function Delete(controller, productId) {
    console.log("Entered");
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover. (Only Admins can delete)",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: '/' + controller + '/delete?productId=' + productId,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}