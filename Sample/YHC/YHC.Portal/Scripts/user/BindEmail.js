$(function () {
    bindOpt.init();
});

var bindOpt = {
    email: $('#email').val(),
    $next: $('#next'),

    init: function () {
        bindOpt.$next.on('click', function () { bindOpt.sendValidateCode(); });
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
            location.href = '/user/bindemailstep2.html'; 
        });
    }
};