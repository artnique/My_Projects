var b = false;
$(function() {
    $("#userid").blur(function() {
        checkUseName($(this));
    });
    var checkUseName = function(obj) {
        var username = $.trim(obj.val());
        if (!/^([0-9](?=[0-9]*?[a-zA-z])\w{4,12})|([a-zA-Z](?=[a-zA-Z]*?[0-9])\w{4,12})$/.test(username)) {
            obj.closest("td").siblings(".helptext").html('<i class="ico ico-error"></i>账号由4-12位字母数字组成');
            b = false;
        } else {
            obj.closest("td").siblings(".helptext").html('<i class="ico ico-right"></i>');
            b = true;
        }
    }
    $("#password1").blur(function() {
        checkPassword($(this));
    });
    var checkrealname = function(obj) {
        var checkrealname = $.trim(obj.val());
        var reg1 = /^[\u4e00-\u9fa5]{2,4}$/;
        if (!reg1.test(checkrealname)) {
            obj.closest("td").siblings(".helptext").html('<i class="ico ico-error"></i>请添写您的真实姓名');
            b = false;
        } else {
            obj.closest("td").siblings(".helptext").html('<i class="ico ico-right"></i>');
            b = true;
        }
    }
    $("#realname").blur(function() {
        checkrealname($(this));
    });
    var checktel = function(obj) {
        var checktel = $.trim(obj.val());
        var reg2 = /^1[3,4,5,7,8]\d{9}$/;
        if (!reg2.test(checktel)) {
            obj.closest("td").siblings(".helptext").html('<i class="ico ico-error"></i>请正确添写11位手机号码');
            b = false;
        } else {
            obj.closest("td").siblings(".helptext").html('<i class="ico ico-right"></i>');
            b = true;
        }
    }
    $("#tel").blur(function() {
        checktel($(this));
    });
    var checkEmail = function(obj) {
        var email = $.trim(obj.val());
        if (email == '') {
            obj.closest("td").siblings(".helptext").html('<i class="ico ico-error"></i>邮箱不能为空');
            b = false;
        } else if (!/^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/.test(obj.val())) {
            obj.closest("td").siblings(".helptext").html('<i class="ico ico-error"></i>邮箱格式不正确');
            b = false;
        } else {
            obj.closest("td").siblings(".helptext").html('<i class="ico ico-right"></i>');
            b = true;
        }
    }
    $("#email").blur(function() {
        checkEmail($(this))
    });
    var checkPassword = function(obj) {
        var pwd = $.trim(obj.val());
        if (!/^\w{6,15}$/.test(pwd)) {
            obj.closest("td").siblings(".helptext").html('<i class="ico ico-error"></i>密码由6-15位由字母数字组成');
            b = false;
        } else {
            obj.closest("td").siblings(".helptext").html('<i class="ico ico-right"></i>');
            b = true;
        }
    }
    $("#confirmpassword1").blur(function() {
        checkConfirmPassword($(this));
    });
    var checkConfirmPassword = function(obj) {
        var confirmPwd = $.trim(obj.val());
        if ($.trim($("#password1").val()) != '') {
            if ($("#password1").val() != confirmPwd) {
                obj.closest("td").siblings(".helptext").html('<i class="ico ico-error"></i>两次密码不一样');
                b = false;
            } else {
                obj.closest("td").siblings(".helptext").html('<i class="ico ico-right"></i>');
                b = true;
            }
        }
    }
    $("#qq").blur(function() {
        checkQQ($(this));
    });
    var checkQQ = function(obj) {
            var qq = $.trim(obj.val());
            if (qq != '' && !/^\d+$/.test(qq)) {
                obj.closest("td").siblings(".helptext").html('<i class="ico ico-error"></i>格式不正确');
                b = false;
            } else {
                obj.closest("td").siblings(".helptext").html('<i class="ico ico-right"></i>');
                b = true;
            }
        }
        //点击获取验证码
    var getcode = function(obj) {
        $(obj).attr("src", "/getcode?r=" + Math.rand());
    }
    
    var findPwd = {
        checkUserName: function(obj) {
            var username = $.trim(obj.val());
            if (username == '') {
                obj.closest("td").siblings(".helptext").html('<i class="ico ico-error"></i>用户名不能为空');
                return false;
            } else {
                obj.closest("td").siblings(".helptext").html('<i class="ico ico-right"></i>');
                return true;
            }
        },
        checkCode1: function(obj) {
            var code = obj.val();
            if (code == '') {
                obj.closest("td").siblings(".helptext").html('<i class="ico ico-error"></i>验证码不能为空');
                return false;
            } else {
                //var c = false;
                //$.ajax({
                //    type: 'POST',
                //    url: "/getcode", //url
                //    async: false,
                //    data: {
                //        code: $.trim(obj.val())
                //    },
                //    success: function(data) {
                //        c = data; //返回一个bool值
                //        if (!c) obj.closest("td").siblings(".helptext").html('<i class="ico ico-error"></i>验证码不正确');
                //    },
                //    dataType: 'json'
                //});
                //return c;
                return true;
            }
        },
        checkCode2: function(obj) {
            var code = obj.val();
            if (code == '') {
                obj.closest("td").siblings(".helptext").html('<i class="ico ico-error"></i>验证码不能为空');
                return false;
            } else {
                //var c = false;
                //$.ajax({
                //    type: 'POST',
                //    url: "/getcode", //url
                //    async: false,
                //    data: {
                //        code: $.trim(obj.val())
                //    },
                //    success: function(data) {
                //        c = data; //返回一个bool值
                //        if (!c) obj.closest("td").siblings(".helptext").html('<i class="ico ico-error"></i>验证码不正确');
                //    },
                //    dataType: 'json'
                //});
                //return c;
                return true;
            }
        },
        newPassword: function(obj) {
            if ($.trim(obj.val()) == '') {
                obj.closest("td").siblings(".helptext").html('<i class="ico ico-error"></i>密码不能为空');
                return false;
            } else if (!/^\w{6,15}$/.test($.trim(obj.val()))) {
                obj.closest("td").siblings(".helptext").html('<i class="ico ico-error"></i>密码格式不正确');
                return false;
            } else {
                obj.closest("td").siblings(".helptext").html('<i class="ico ico-right"></i>');
                return true;
            }
        },
        confirmNewPassword: function(obj) {
            if ($.trim(obj.val()) == '') {
                obj.closest("td").siblings(".helptext").html('<i class="ico ico-error"></i>密码不能为空');
                return false;
            } else if ($("#newpwd").val() != obj.val()) {
                obj.closest("td").siblings(".helptext").html('<i class="ico ico-error"></i>两次密码不一致');
                return false;
            } else {
                obj.closest("td").siblings(".helptext").html('<i class="ico ico-right"></i>');
                return true;
            }
        },
        codeOrEmail: function(obj) {
            if ($.trim(obj.val()) == '') {
                obj.closest("td").siblings(".helptext").html('<i class="ico ico-error"></i>验证码不能为空');
                return false;
            }
            if ($(".zhfs li.on").attr("data-value") == 2) {
                if (!/^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/.test(obj.val())) {
                    obj.closest("td").siblings(".helptext").html('<i class="ico ico-error"></i>邮箱格式不正确');
                    return false;
                }
            } else {
                obj.closest("td").siblings(".helptext").html('<i class="ico ico-right"></i>');
                return true;
            }
        },
        init: function() {
            $("#sourceName").blur(function() {
                findPwd.checkUserName($(this))
            });
            $("#checkcode1").blur(function() {
                findPwd.checkCode1($(this))
            });
            $("#nextzh1").click(function() {
                if (findPwd.checkUserName($("#sourceName")) && findPwd.checkCode1($("#checkcode1"))) {
                    //下一步
                    $('#zhaohui1').hide();
                    $('#zhaohui2').show();
                } else {
                    //下一步
                    $('#zhaohui1').hide();
                    $('#zhaohui2').show();
                }
            });
            $("#checkcode2").blur(function() {
                findPwd.checkCode2($(this))
            });
            $("#checkcode2").siblings("a").click(function() {
                $(this).prev().attr("src", "/getcode?r=" + Math.rand()); //获取验证码
            });
            $("#nextzh2").click(function() {
                if (findPwd.checkCode2($("#checkcode2"))) {
                    //下一步
                    $('#zhaohui2').hide();
                    $('#zhaohui3').show();
                } else {
                    $('#zhaohui2').hide();
                    $('#zhaohui3').show();
                }
            });
            $("#newpwd").blur(function() {
                findPwd.newPassword($(this));
            });
            $("#confirmNewPwd").blur(function() {
                findPwd.confirmNewPassword($(this));
            });
            $("#checkcode3").blur(function() {
                findPwd.codeOrEmail($(this));
            });
            $("#prevbtn").click(function() {
                if (findPwd.newPassword($("#newpwd")) && findPwd.confirmNewPassword($("#confirmNewPwd")) && findPwd.codeOrEmail($("#checkcode3"))) {
                    $.post("/editpwd", {
                        pwd: $("#newpwd").val(),
                        confirmpwd: $("#confirmNewPwd").val(),
                        code: $("#checkcode3").val()
                    }, function(data) {
                        if (data) //返回bool
                            alert("修改成功");
                        else alert("修改失败");
                    });
                }
            });
            $("#nextbtn").click(function() {
                $.post("/sendcode", {}, function(data) {
                    if (data) //返回bool类型
                        alert("发送成功");
                    else alert("发送失败");
                });
            });
        }
    }
    findPwd.init();
});