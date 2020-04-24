var user = {
    init: function () {
        user.event();
    },
    event: function () {
        $('.btn-stt').on("click", function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data("id");
            $.ajax({
                url: "/Admin/User/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "Post",
                success: function (res) {
                    if (res.status == true) {
                        btn.text ("Kích hoạt")
                    }
                    else {
                        btn.text("Khóa")
                    }
                }
            })
        })
    }
}
user.init();