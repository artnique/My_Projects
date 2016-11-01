$(function () {
    city.init();
    deposit.init();
});


var deposit = {
    init: function () {
        // 下拉银行事件
        deposit.bankEvent();
        // 保存
        $('#btn_save').on('click', function () { return deposit.doSave(); });
        // 转账方式事件
        $("input[name='TransType']").on('click', function () { deposit.transferTypeEvent(); });
    },
    // 复制开户名和账号(falsh copy), 内部方法，外部不能调用
    copyEvent: function () {
        $("#eveb_copy_accountName").zclip({
            path: "/scripts/lib/zclip/ZeroClipboard.swf",
            copy: function () {
                return $('#manual_receiveBank').children('option:selected').attr('bankAccountName');
            }
        });

        $("#eveb_copy_accountNo").zclip({
            path: "/scripts/lib/zclip/ZeroClipboard.swf",
            copy: function () {
                return $('#manual_receiveBank').children('option:selected').attr('bankAccountNo');
            }
        });
    },
    // 下拉银行事件
    bankEvent: function () {
        $('#manual_receiveBank').on('change', function () {
            var $bank = $(this).children('option:selected');
            if ($bank.val() != '') {
                $('.eveb_copy a').fadeIn('slow');

                // 添加copy事件
                deposit.copyEvent();
            } else {
                $('.eveb_copy a').fadeOut();
            }
        })
    },
    // 转账方式事件
    transferTypeEvent: function () {
        var $trans = $("input[name='TransType']:checked");
        var transferType = $trans.val();

        // 添加选中事件
        var $label = $trans.parent().parent().find("label[author='eveb']");
        $label.removeClass();
        if (transferType == window._TRANS_TYPE.atmtrans
            || transferType == window._TRANS_TYPE.atmdeposit
            || transferType == window._TRANS_TYPE.bank) {
            $trans.parent().addClass('on');
            $('#branch').show();
        }
        else {
            $('#branch').hide();
        }
    },
    // 保存
    doSave: function (event) {
        var amount = $('#manual_amount').val();
        var min = $('#hidMinDeposit').val();
        var max = $('#hidMaxDeposit').val();
        var prefKey = $("input[name='prefKey']:checked").val();
        var bankId = $("#manual_receiveBank").val();
        var province = $('#province').find('option:selected').text();
        var city = $('#city').find('option:selected').text();
        var address = $("#address").val();
        var transType = $("input[name='TransType']:checked").val();

        if (amount == '') {
            alert('请输入充值金额');
            $('#manual_amount').focus();
            return;
        }
        if (bankId == '') {
            alert("请选择收款账号");
            return false;
        }

        if (bankId != '') {
            if (transType == "" || transType == null) {
                alert("请选择存款方式");
                return false;
            }
            if (transType == window._TRANS_TYPE.atmdeposit || transType == window._TRANS_TYPE.atmtrans) {
                if (province == "请选择" || city == "请选择" || address == "") {
                    alert("请输入ATM机所属支行");
                    return false;
                }
            }
            else if (transType == window._TRANS_TYPE.bank) {
                if (province == "请选择" || city == "请选择" || address == "") {
                    alert("请输入银行所属支行");
                    return false;
                }
            }
        }

        if (parseFloat(amount) < min) {
            alert('充值金额不能小于' + min);
            return false;
        }
        if (parseFloat(amount) > max) {
            alert('充值金额不能大于' + max);
            return false;
        }

        if (province == '请选择') {
            province = '';
        }

        if (city == '请选择') {
            city = '';
        }

        $.post('/user/dodeposit.html',
        {
            bank: bankId,
            amount: amount,
            prefKey: prefKey,
            transType: transType,
            province: province,
            city: city,
            address: address
        },
        function (data) {
            if (data.success == false) {
                alert(data.msg);
                return;
            }
            // 显示订单号
            $('#orderId').html(data.responseText);
            // 显示提示层
            deposit.alert();
        });
        return true;
    },
    // 提示层
    alert: function () {
        dialog.show('#eveb_popup_bank_ok');
    }
};

// 城市操作
var city = {
    $city: $('#city'),
    $province: $('#province'),

    init: function () {
        city.$province.on('change', function () { city.getCity($(this).val()); });
    },
    // 获取城市
    getCity: function (provinceId) {
        $.post('/user/getcities.html', { provinceId: provinceId }, function (data) {
            if (data.success == false) {
                return false;
            }

            city.$city.empty();
            city.$city.append('<option value="0">请选择</option>');
            $.each(data.responseText, function (index, item) {
                var cityName = item.Name;
                var cityId = item.Id;
                city.$city.append('<option value="' + cityId + '">' + cityName + '</option>');
            });
            return true;
        });
    }
};