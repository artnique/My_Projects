$(function () {
    bindOpt.init();
});

var bindOpt = {
    mobile: $('#mobile').val(),
    $next: $('#next'),

    init: function () {
        bindOpt.$next.on('click', function () { bindOpt.sendValidateCode(); });
    },
    // 发送验证信息
    sendValidateCode: function () {
        $.showLoading();
        $.post('/user/sendmobilevalidatecode.html', { mobile: bindOpt.mobile }, function (data) {
            $.hideLoading();
            if (data.success == false) {
                alert(data.msg);
                return;
            }
            location.href = '/user/bindmobilestep2.html';
        });
    }
};