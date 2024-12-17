$(document).ready(function () {
    $('form.d-inline').submit(function (e) {
        e.preventDefault(); // Ngăn chặn form mặc định gửi dữ liệu

        var form = $(this);

        $.ajax({
            url: form.attr('action'),
            method: form.attr('method'),
            data: form.serialize(),
            success: function (response) {
                // Cập nhật nội dung phần Header sau khi thêm vào giỏ hàng thành công
                $('#menuContainer').load(window.location.href + ' #menuContainer');
                const notyf = new Notyf();
                notyf.open({
                    duration: 1000,
                    position: {
                        x: 'right',
                        y: 'bottom',
                    },
                    type: 'success', // Loại thông báo thành công
                    message: 'Thêm vào giỏ hàng thành công!', // Nội dung thông báo
                });
            },
            error: function (response) {
                // Xử lý lỗi (nếu có)
                alert('Có lỗi xảy ra khi thêm vào giỏ hàng.');
            }
        });
    });
});