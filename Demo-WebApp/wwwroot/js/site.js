// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    // Auto-dismiss alerts after 5 seconds
    setTimeout(function () {
        $('.alert').fadeOut('slow');
    }, 5000);

    // Confirm delete actions
    $('.delete-confirm').on('click', function (e) {
        if (!confirm('Are you sure you want to delete this item?')) {
            e.preventDefault();
        }
    });

    // Toggle complete functionality with AJAX (optional enhancement)
    $('.toggle-complete').on('click', function (e) {
        e.preventDefault();
        var form = $(this).closest('form');
        var card = $(this).closest('.card');

        $.ajax({
            url: form.attr('action'),
            type: 'POST',
            data: form.serialize(),
            success: function (result) {
                // Optionally refresh the page or update the UI
                location.reload();
            },
            error: function () {
                alert('An error occurred. Please try again.');
            }
        });
    });
});