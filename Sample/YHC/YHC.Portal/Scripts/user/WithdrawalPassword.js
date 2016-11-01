$(function () {
    withdrawalPassword.init();
});


// 会员信息编辑
var withdrawalPassword = {
    $btnSave: $('#btn_save'),

    init: function() {
        withdrawalPassword.$btnSave.on('click', function () { withdrawalPassword.doSave(); });
    },
    doSave: function () {
        var pwd = $("#OldPwd").val();
        if (pwd == "") {
            alert("原密码不能为空！");
            return false;
        }
        var newpwd1 = $("#NewPwd").val();
        if (newpwd1 == "") {
            alert("新密码不能为空！");
            return false;
        }
        if (/^([A-Z]|[a-z]|[0-9]|[_@,.]){6,16}$/g.test(newpwd1) == false) {
            alert('提款密码由6-16位字符组成');
            return false;
        }
        var newpwd2 = $("#ConfirmNewPwd").val();
        if (newpwd2 == "") {
            alert("确认密码不能为空！");
            return false;
        }
        if (newpwd1 != newpwd2) {
            alert("两次输入的密码不相同，请核实");
            return false;
        }
        if (!confirm("确定要修改提款密码吗？")) {
            return false;
        }

        $.showLoading();
        $.post('/user/dosavewithdrawalpassword.html', { oldPwd: pwd, newPwd: newpwd1 },
            function (data) {
                $.hideLoading();
                if (data.success == false) {
                    alert(data.msg);
                    return false;
                }

                withdrawalPassword.alert();
                return true;
            });
    },
    // 显示成功提示
    alert: function () {
        dialog.show('#eveb_popup_password_ok');
    }
};