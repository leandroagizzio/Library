// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('#table-books').DataTable();
    $('#table-users').DataTable();

    $('.btn-books-possession').click(function () {

        var userId = $(this).attr('user-id');

        $.ajax({
            type: "GET",
            url: '/User/GetBookListInUserPossession/' + userId,
            success: function (result) {
                $('#listUserBookPossession').html(result);
                $('#modalUserBookListPossession').modal('show');
                $('#table-books-possession').DataTable();
            }
        });

    });

});


$('.close-alert').click(function () {
    $('.alert').hide('hide');
});