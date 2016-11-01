$(function () {
    bindOpt.init();
});

var bindOpt = {
    email: $('#email').val(),
    $sendCode: $('#sendCode'),

    init: function () {
        bindOpt.$sendCode.on('click', function () { bindOpt.sendValidateCode(); });
    },
    // 发送验证信息
    sendValidateCode: function () {
        $.showLoading();
        $.post('/user/sendemailvalidatecode.html', { email: bindOpt.email }, function (data) {
            $.hideLoading();
            if (data.success == false) {
                alert(data.msg);
                return;
            }
            alert('验证邮件已发送到您的邮箱，请查收！');
        });
    }
};