$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    if ($.fn.dataTable.isDataTable('#tblData')) {
        $('#tblData').DataTable().destroy();
        $('#tblData').empty(); // Clear the table before reinitializing
    }

    $('#tblData').DataTable({
        "ajax": {
            url: '/UserManagement/GetAllUsers',
            type: 'GET',
            dataSrc: 'data',
            error: function (xhr, error, code) {
                console.log('Error:', xhr.responseText);
                toastr.error('Failed to load data.');
            }
        },
        "columns": [
            { "data": "email", "width": "15%" },
            { "data": "firstName", "width": "15%" },
            { "data": "lastName", "width": "15%" },
            {
                "data": "id",
                "render": function (data, type, row) {
                    var today = new Date().getTime();
                    var lockout = new Date(row.lockoutEnd).getTime();

                    if (lockout > today) {
                        return `
                            <div class="text-center">
                                <a onclick="LockUnlock('${data}')" class="btn btn-danger text-white" style="cursor:pointer; width:100px;">
                                    <i class="fa fa-lock"></i> Lock
                                </a>
                            </div>
                        `;
                    } else {
                        return `
                            <div class="text-center">
                                <a onclick="LockUnlock('${data}')" class="btn btn-success text-white" style="cursor:pointer; width:110px;">
                                    <i class="fa fa-unlock"></i> Unlock
                                </a>
                            </div>
                        `;
                    }
                },
                "width": "25%"
            }
        ]
    });
}

function LockUnlock(id) {
    $.ajax({
        type: "POST",
        url: '/UserManagement/LockUnlock',
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                setTimeout(function () {
                    $('#tblData').DataTable().ajax.reload(null, false);
                }, 2000); // Delay the reload by 500ms to allow the toastr message to be seen
            } else {
                toastr.error(data.message);
            }
        },
        error: function () {
            toastr.error('An error occurred while processing your request.');
        }
    });
}

// Initialize toastr configuration
toastr.options = {
    "closeButton": true,
    "debug": false,
    "newestOnTop": false,
    "progressBar": true,
    "positionClass": "toast-top-right",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}
