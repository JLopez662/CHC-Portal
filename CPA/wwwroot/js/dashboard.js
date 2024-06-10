$(document).ready(function () {
    console.log("Document is ready");

    $('#dashboardTabs a').on('click', function (e) {
        e.preventDefault();
        $(this).tab('show');
        console.log($(this).attr('id') + " tab clicked");
    });

    // Show Toastr success message if TempData contains Success
    if (typeof TempData !== 'undefined' && TempData["Success"] !== null) {
        console.log('Showing Toastr success message');
        toastr.success(TempData["Success"]);
    }

    // Initialize DataTable if not already initialized
    if (!$.fn.dataTable.isDataTable('#tblData')) {
        console.log("Initializing DataTable on #tblData");
        $('#tblData').DataTable({
            "paging": true,
            "searching": true,
            "ordering": true,
            "lengthMenu": [10, 25, 50, 100],
            "columns": [
                { "data": "ID" },
                { "data": "Nombre" },
                { "data": "NombreComercial" },
                { "data": "Dir" },
                { "data": "Tipo" },
                { "data": "Patronal" },
                { "data": "SSN" },
                { "data": "Incorporacion" },
                { "data": "Operaciones" },
                { "data": "Industria" },
                { "data": "NAICS" },
                { "data": "Descripcion" },
                { "data": "Contacto" },
                { "data": "Telefono" },
                { "data": "Celular" },
                { "data": "DirFisica" },
                { "data": "DirPostal" },
                { "data": "Email" },
                { "data": "Email2" },
                { "data": "CID" },
                { "data": "MID" }
            ]
        });
    } else {
        console.log("#tblData is already a DataTable, no need to initialize");
    }
});
