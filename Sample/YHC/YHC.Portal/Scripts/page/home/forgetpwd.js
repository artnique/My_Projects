$(function () {
    $('#btn_step1').click(function () { doForgetPwd(); });
    $('#btn_step2').click(function () {
        doDoGetValidateCode();
    });
    $('#btn_save').click(function () { doSave(); });
    $('#sel_code_type_mobile').click(function () {
        $('#vtype').val("Mobile");
    });
    $('#sel_code_type_email').click(function () {
        $('#vtype').val("Email");
    });
});
function doForgetPwd() {
    var userName = $('#sourceName').val();
    var authCode = $('#checkcode1').val();
    if (userName == '' || userName == null) {
        alert('请输入用户名');
        return false;
    }

    if (authCode == '' || authCode == null) {
        alert('请输入验证码');
        return false;
    }
    $.post('/doforgetpwd.html', { userName: userName, authCode: authCode }, function (data) {
        if (data.success == false) {
            alert(data.msg);
            return;
        }
        location.href = '/forgetpwd2.html?userName=' + userName;
    });
};

function doDoGetValidateCode() {
    var type = $('#vtype').val();
    var email = $('#email').val();
    var mobile = $('#mobile').val();
    var authCode = '';//$('#authCode').val();
    if (type == '' || type == null) {
        jAlert("请选择找回方式！", '提示');
        return false;
    }
    switch (type) {
        case 'Email':
            if (email == '') {
                jAlert("您没有填写邮箱，无法找回密码，请联系客服人员", '提示');
                return false;
            }
            break;
        case 'Mobile':
            if (mobile == '') {
                jAlert("您没有填写手机，无法找回密码，请联系客服人员", '提示');
                return false;
            }
            break;
    }
    //if (authCode == '' || authCode == null) {
    //    jAlert('请输入验证码', '提示');
    //    return false;
    //}
    var userName = $('#userName').val();
    //$btnNext.unbind('click').val('正在发送');
    $.showLoading();

    $.ajax({
        url: '/dogetforgetpwdvalidatecode.html',
        type: 'post',
        data: { userName: userName, type: type, authCode: authCode },
        error: function (jqr) {
            $.hideLoading();
            jAlert('网络故障，请稍候重试', '提示');
            //$btnNext.click(function () { doDoGetValidateCode(); }).val('确定发送');
            return false;
        },
        success: function (data) {
            $.hideLoading();
            if (data.success == false) {
                jAlert(data.msg, '提示');
                //$btnNext.click(function () { doDoGetValidateCode(); }).val('确定发送');
                return;
            }

            switch (type) {
                case 'Email':
                    jAlert('验证码已发到您的邮箱，请注意查收', '提示', function () {
                        location.href = '/forgetpwdmodifypwd.html?userName=' + userName + '&type=' + type;
                    });
                    break;
                case 'Mobile':
                    jAlert('验证码已发到您的手机，请注意查收', '提示', function () {
                        location.href = '/forgetpwdmodifypwd.html?userName=' + userName + '&type=' + type;
                    });
                    break;
            }
        }
    });
    return true;
};

function doSave() {
    var type = $('#type').val();
    var newPassword = $("#newpwd").val();
    if (newPassword == "") {
        jAlert("新密码不能为空！", '提示');
        return false;
    }
    if (newPassword == "" || newPassword.length < 6 || newPassword.length > 16) {
        jAlert("密码长度为6-16位", '提示');
        return false;
    }
    var confirmPassword = $("#confirmNewPwd").val();
    if (confirmPassword == "") {
        jAlert("确认密码不能为空！", '提示');
        return false;
    }
    if (newPassword != confirmPassword) {
        jAlert("两次输入的密码不相同，请核实！", '提示');
        return false;
    }
    var emailValidateCode = $("#securitycode").val();
    var phoneValidateCode = $("#securitycode").val();
    if (type == "Email") {
        if (emailValidateCode == "") {
            jAlert("邮箱验证码不能为空！", '提示');
            return false;
        }
    }
    if (type == "Mobile") {
        if (phoneValidateCode == "") {
            jAlert("短信验证码不能为空！", '提示');
            return false;
        }
    }

    if (!confirm("确认要修改您的密码？")) {
        return false;
    }
    $.showLoading();
    $.post('/doforgetpwdmodifypwd.html',
        {
            userName: $("#userName").val(),
            newPassword: newPassword,
            confirmPassword: confirmPassword,
            emailValidateCode: emailValidateCode,
            phoneValidateCode: phoneValidateCode,
            type: type
        },
        function (data) {
            $.hideLoading();
            if (data.success == false) {
                jAlert(data.msg, '提示');
                return false;
            }
            jAlert("密码修改成功，请牢记您的新密码", '提示', function () {
                location.href = '/index.html';
            });
            //解决jquery 1.8 与IE10“Date"未定义的问题

            return true;
        });
    return true;
}