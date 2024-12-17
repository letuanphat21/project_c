
$(document).ready(function () {
    var menuContainer = $('#menuContainer');
    var originalTop = menuContainer.offset().top; // Lưu tọa độ ban đầu của menuContainer

    $(window).scroll(function () {
        if ($(window).scrollTop() > originalTop) {
            menuContainer.addClass('fixed-top');
        } else {
            menuContainer.removeClass('fixed-top');
        }
    });
});
