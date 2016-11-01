$(function () {
    setting.init();
    info.init();
    mgPwd.init();
});


var setting = {
    $edit: $("a[name='edit']"),
    $mgPwd: $("#btn_mgPwd"),

    init: function () {
        // 弹出编辑窗口
        setting.$edit.on('click', function () { info.show(); });
        //弹出MG窗口
        setting.$mgPwd.on('click', function () { mgPwd.show(); });
    }
};

// 会员信息编辑
var info = {
    $btnSave: $('#btn_save'),

    init: function () {
        info.$btnSave.on('click', function () { info.doSave(); });
    },
    show: function () {
        dialog.show('#eveb_setting_edit');
    },
    hide: function () {
        dialog.hide();
    },
    doSave: function () {
        var trueNameValue = $('#TrueName').val();
        var phoneValue = $('#Phone').val();
        var emailValue = $('#Email').val();
        //var qqValue = $('#QQ').val();
        var provinceValue = '';
        var cityValue = '';
        var addressValue = '';

        if (trueNameValue == "") {
            alert("请输入真实姓名");
            return false;
        }

        if (trueNameValue.length > 0 && !trueNameValue.match("^[\\u4E00-\\u9FA5\\uF900-\\uFA2D]+$")) {
            alert("真实姓名暂只接受中文输入");
            return false;
        }

        if (phoneValue == "") {
            alert("请输入手机号码");
            return false;
        }
        if (phoneValue.length > 0 && !phoneValue.match("^(13|15|18|14)[0-9]{9}$") && !phoneValue.match("^(([0\\+]\\d{2,3}-)?(0\\d{2,3})-)?(\\d{7,8})(-(\\d{3,}))?$")) {
            alert("手机号码格式不正确");
            return false;
        }

        if (emailValue == "") {
            alert("请输入电子邮箱");
            return false;
        }
        if (emailValue.length > 0 && !emailValue.match("^([\\w-.]+)@(([[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.)|(([\\w-]+.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(]?)$")) {
            alert("电子邮箱格式不正确");
            return false;
        }

        //if (qqValue.length > 0 && !(/^[1-9]\d{4,20}$/.test(qqValue))) {
        //    alert("你输入的QQ格式不正确");
        //    return false;
        //}

        $.showLoading();
        $.post('/user/dosavesetting.html',
            {
                trueName: $('#TrueName').val(),
                phone: $('#Phone').val(),
                email: $('#Email').val(),
                birthday: $('#BirthDay').val(),
                //qq: $('#QQ').val()
                qq: "",
                province: provinceValue,
                city: cityValue,
                address: addressValue
            },
            function (data) {
                $.hideLoading();
                if (data.success == false) {
                    alert(data.msg);
                    return false;
                }
                // 显示成功提示
                info.alert();
                return true;
            });
    },
    // 显示成功提示
    alert: function () {
        location.href = '/user/setting.html';
    }
};

// MG密码
var mgPwd = {
    $btnSave: $('#btn_getPwd'),

    init: function () {
        mgPwd.$btnSave.on('click', function () { mgPwd.doSave(); });
    },
    show: function () {
        $('#login_Pwd').val('');
        $('#li1').show();
        $('#li2').hide();
        dialog.show('#mg_pwd_get');
    },
    hide: function () {
        dialog.hide();
    },
    doSave: function () {
        var loginPwd = $('#login_Pwd').val();
        if (loginPwd == "") {
            alert("请输入登录密码");
            return false;
        }
        $.showLoading();
        $.ajax({
            url: '/user/getmggamepwd.html',
            type: "GET",
            data: { loginPwd: loginPwd },
            error: function (xhr, textStatus, errorThrown) {
                $.hideLoading();
                alert(errorThrown);
            },
            success:function (data) {
                $.hideLoading();
                if (data.success == false) {
                    alert(data.msg);
                    return false;
                }
                // 显示成功提示
                $('#show_pwd').html(data.responseText);
                $('#li1').hide();
                $('#li2').show();
                return true;
            }
        });
    },
};