$(document).ready(function () {
    console.log("Initializing DataTable");
    var table = $('#customTable').DataTable({
        "paging": true,
        "searching": true,
        "ordering": true,
        "lengthMenu": [10, 25, 50, 100],
        "columnDefs": [
            { "orderable": false, "targets": [3, 4] }
        ]
    });

    console.log("DataTable initialized");

    $('#customTable').on('click', '.modify-button', function () {
        var row = $(this).closest('tr');
        row.find('.text-field').hide();
        row.find('.input-field').show();
        row.find('.save-button').removeClass('d-none');
        row.find('.password-recovery-button').addClass('d-none'); // Hide password recovery button

        $(this).addClass('d-none');
    });

    $('#customTable').on('click', '.save-button', function () {
        var row = $(this).closest('tr');
        var id = row.find('.user-id').val();
        var email = row.find('input[name="Email"]').val();
        var firstName = row.find('input[name="FirstName"]').val();
        var lastName = row.find('input[name="LastName"]').val();

        $.ajax({
            type: "POST",
            url: '/UserManagement/UpdateUser',
            data: {
                id: id,
                email: email,
                firstName: firstName,
                lastName: lastName
            },
            success: function (response) {
                if (response.success) {
                    toastr.success(response.message);
                    row.find('.text-field').each(function () {
                        var inputField = $(this).next('.input-field');
                        $(this).text(inputField.val()).show();
                        inputField.hide();
                    });
                    row.find('.modify-button').removeClass('d-none');
                    row.find('.password-recovery-button').removeClass('d-none'); // Show password recovery button
                    row.find('.save-button').addClass('d-none');
                } else {
                    toastr.error(response.message);
                }
            },
            error: function () {
                toastr.error('An error occurred while processing your request.');
            }
        });
    });
});

function LockUnlock(id) {
    $.ajax({
        type: "POST",
        url: '/UserManagement/LockUnlock',
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                updateLockButton(id, data.message.includes("Unlocked"));
            } else {
                toastr.error(data.message);
            }
        },
        error: function () {
            toastr.error('An error occurred while processing your request.');
        }
    });
}

function updateLockButton(id, isUnlocked) {
    var button = $('a[onclick="LockUnlock(\'' + id + '\')"]');
    if (isUnlocked) {
        button.removeClass('btn-danger').addClass('btn-success');
        button.html('<i class="fas fa-unlock"></i> Active');
    } else {
        button.removeClass('btn-success').addClass('btn-danger');
        button.html('<i class="fas fa-lock"></i> Inactive');
    }
}

function passwordRecovery(email) {
    $.ajax({
        type: "POST",
        url: '/UserManagement/PasswordRecovery',
        data: { email: email },
        success: function (response) {
            if (response.success) {
                toastr.success(response.message);
            } else {
                toastr.error(response.message);
            }
        },
        error: function () {
            toastr.error('An error occurred while processing your request.');
        }
    });
}
