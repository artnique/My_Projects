$(function () {
    bindOpt.init();
});

var bindOpt = {
    $mobile: $('#mobile'),
    $validateCode: $('#validateCode'),
    $sendCode: $('#sendCode'),
    $next: $('#next'),

    init: function () {
        bindOpt.$sendCode.on('click', function () { bindOpt.sendValidateCode(); });
        bindOpt.$next.on('click', function () { bindOpt.validate(); });
    },
    // 发送验证信息
    sendValidateCode: function () {
        $.showLoading();
        $.post('/user/sendmobilevalidatecode.html', { mobile: $('#mobile').val() }, function (data) {
            $.hideLoading();
            if (data.success == false) {
                alert(data.msg);
                return;
            }
            alert('验证码已发送到您的手机，请查收！');
        });
    },
    // 验证操作
    validate: function () {
        var mobile = bindOpt.$mobile.val();
        var code = bindOpt.$validateCode.val();
        if (code == '') {
            alert('请输入手机验证码');
            return false;
        }

        $.showLoading();
        $.post('/user/dobindmobile.html', { mobile: mobile, validateCode: code }, function (data) {
            $.hideLoading();
            if (data.success == false) {
                alert(data.msg);
                return;
            }
            location.href = '/user/bindmobilesuccess.html';
        });
    }
};