var validated = true;
$(function () {
    getRecommendCode();
    $('#btnSubmit').click(function () {
        var agree = $("input[name='agree']:checked").val();
        if (agree == undefined || agree == "") {
            alert("请接受开户条约");
            return;
        }
        var $trueName = $("#realname");
        var $email = $("#email");
        var $phone = $("#tel");
        var $qq = $("#qq");
        //var $birthday = $("#birth");
        var $userName = $("#userid");
        var $password = $("#password1");
        var $rePassword = $("#confirmpassword1");
        //var $isReceiveSMS = $("#IsReceiveSMS");
        //var $isReceiveEmail = $("#IsReceiveEmail");
        var $extendCode = $('#affiliatecode');
        var $authCode = $('#authCode');
        //var $withdrawalPassword = $('#WithdrawalPassword');
        //var $reWithdrawalPassword = $('#ReWithdrawalPassword');

        var extendCodeValue = $extendCode.val();
        var userNameValue = $userName.val();
        var trueNameValue = $trueName.val();
        var phoneValue = $phone.val();
        var emailValue = $email.val();
        var qqValue = $qq.val();
        var birthdayValue = '';//$birthday.val();
        var passwordValue = $password.val();
        var rePasswordValue = $rePassword.val();
        var authCodeValue = $authCode.val();

        //var qk1 = $('#pwd1').val();
        //var qk2 = $('#pwd2').val();
        //var qk3 = $('#pwd3').val();
        //var qk4 = $('#pwd4').val();
        
        var withdrawalPasswordValue = "";// + qk1 + qk2 + qk3 + qk4;
        //var reWithdrawalPasswordValue = $reWithdrawalPassword.val();

        // 表单验证
        if (Validator.Account.test(userNameValue) == false) {
            alert('用户名格式错误，由4-15位字母或数字组成');
            return;
        }
        if (Validator.Password.test(passwordValue) == false) {
            alert('登录密码格式错误，密码由6-12位小写字母(a-z)或数字(0-9)组成');
            return;
        }
        if (passwordValue != rePasswordValue) {
            alert('确认登录密码不正确');
            return;
        }
        if (Validator.UserName.test(trueNameValue) == false) {
            alert('真实姓名格式错误');
            return;
        }
        if (Validator.Mobile.test(phoneValue) == false) {
            alert('手机号码格式错误');
            return;
        }
        //if (qk1 == "" || qk2 == "" || qk3 == "" || qk4 == "") {
        //    alert('请输入正确的取款密码');
        //    return;
        //}
        if (Validator.Email.test(emailValue) == false) {
            alert('邮箱地址格式错误');
            return;
        }
        
        if (Validator.QQ.test(qqValue) == false) {
            alert('QQ号码格式错误');
            return;
        }
        //if (/^((?:19|20)\d\d)-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01])$/.test(birthdayValue) == false) {
        //    alert('生日格式错误');
        //    return;
        //}
        
        //if (Validator.WithdrawalPassword.test(withdrawalPasswordValue) == false) {
        //    alert('提款密码格式错误，提款密码由4-16位字母、数字或下划线组成');
        //    return;
        //}
        //if (withdrawalPasswordValue != reWithdrawalPasswordValue) {
        //    alert('确认提款密码不正确');
        //    return;
        //}
        if (authCodeValue == '' || authCodeValue == undefined) {
            alert('请输入验证码');
            return;
        }

        // 打开提示
        $.showLoading();
        // 提交
        $.ajax({
            type: "POST",
            url: "/home/doregist",
            data: {
                extendCode: extendCodeValue,
                userName: userNameValue,
                trueName: trueNameValue,
                password: passwordValue,
                phone: phoneValue,
                email: emailValue,
                source: $.cookie("Source"),
                qq: qqValue,
                birthday: birthdayValue,
                isReceiveSMS: false,
                isReceiveEmail: true,
                authCode: authCodeValue,
                withdrawalPassword: withdrawalPasswordValue
            },
            success: function (data, textStatus) {
                $.hideLoading();
                if (data.success == false) {
                    alert(data.msg);
                    return;
                }
                alert('恭喜您，注册成功');
                location.href = '/registration_success.html';
            },
            error: function (xhr, textStatus, errorThrown) {
                $.hideLoading();
                var data = eval('(' + xhr.responseText + ')');
                if (data.exceptionMessage) {
                    alert(data.exceptionMessage);
                }
                else if (date.message) {
                    alert(data.message);
                }
            }
        });
    });
});

function reset() {
    $('input[type=text]').val('');
    $('input[type=password]').val('');
    $('#agree').attr("checked", 'checked');
}

function getRecommendCode() {
    $.ajax({
        url: '/home/getRecommendCode?r=' + Math.random(),
        type: 'post',
        success: function (data) {
            if (data.success == false) {
                return;
            }
            var code = data.responseText;
            if (code != "") {
                $("#extendCode").val(code).attr("readonly", "readonly");
            }
        }
    });
};