$(function () {
    $("#btn_regist").click(function () { regist(); });
});
var regist = function () {
    //var acceptService = $("input[name='agree']:checked").val();
    //if (acceptService == undefined || acceptService == "") {
    //    alert("请接受所有规则与条款", '提示');
    //    return;
    //}

    var $userName = $("#a_username");
    var $trueName = $("#a_truename");
    var $password = $("#a_password");
    var $rePassword = $("#a_repassword");
    var $email = $("#a_email");
    var $phone = $("#a_phone");
    var $qq = $("#a_qq");
    var $domain = $("#a_domain");
    var $extendDes = $('#a_extenddes');
    var $authCode = $('#a_authcode');

    var userNameValue = $userName.val();
    var trueNameValue = $trueName.val();
    var passwordValue = $password.val();
    var rePasswordValue = $rePassword.val();
    var emailValue = $email.val();
    var phoneValue = $phone.val();
    var qqValue = $qq.val();
    var domainValue = $domain.val();
    var extendDesValue = $extendDes.val();
    var authCodeValue = $authCode.val();


    //表单验证
    if (Validator.Account.test(userNameValue) == false) {
        alert('用户名格式错误');
        return;
    }

    if (Validator.Password.test(passwordValue) == false) {
        alert('密码格式错误');
        return;
    }
    if (passwordValue != rePasswordValue) {
        alert('确认密码不正确');
        return;
    }
    
    //if (qqValue && Validator.QQ.test(qqValue) == false) {
    //    alert('QQ号码格式错误');
    //    return;
    //}
    if (Validator.Email.test(emailValue) == false) {
        alert('电子邮件格式错误');
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
    if (Validator.Empty.test(domainValue) == false) {
        if (Validator.Url.test(domainValue) == false) {
            alert("您的网址格式不正确", '提示');
            return;
        }
    }
    if (Validator.Url.test(domainValue) == false && Validator.Empty.test(extendDesValue) == true) {
        alert("推广网站和推广说明至少填写一项", '提示');
        return;
    }
    if (authCodeValue == '' || authCodeValue == undefined) {
        alert('请输入验证码');
        return;
    }

    // 打开提示
    $.showLoading();
    // 提交
    $.post("/agent/doregist", {
        userName: userNameValue,
        trueName: trueNameValue,
        password: passwordValue,
        phone: phoneValue,
        email: emailValue,
        source: $.cookie("Source"),
        qq: qqValue,
        domain: domainValue,
        extendDesc: extendDesValue,
        authCode: authCodeValue
    }, function (data) {
        $.hideLoading();
        if (data.success == false) {
            alert(data.msg);
            return;
        }
        alert('网站将对您的信息进行审核，需要1-2个工作日，请耐心等待。');
        window.location.reload();
    });
};

function reset() {
    $('input[type=text]').val('');
    $('input[type=password]').val('');
    $('#acceptService').attr("checked", 'checked');
}