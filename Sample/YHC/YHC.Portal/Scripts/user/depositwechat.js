$(function () {
    $('#wechat_save').on('click', function () {
        doSave();
    });

    $('#wechat_prefKey').change(function () { preferential(); });

    // 关闭对话框
    $("#pop_close").click(function() {
        dialog.hide("#eveb_popup_bank_ok");
    });
});

function doSave() {
    var minWechat = parseFloat($('#hidWXMinAmount').val()) || 0;
    var maxWechat = parseFloat($('#hidWXMaxAmount').val()) || 0;

    var accountName = $('#wechat_accountName').val();
    var amount = $('#wechat_amount').val();
    var transType = $('#transType_wechat').val();
    var bankId = $('#bankId_wechat').val();
    var prefKey = $('input[name=wechat_prefKey]:checked').val(); // $('#wechat_prefKey').val();

    if (bankId == '') {
        alert("选择收款微信");
        return false;
    }
    if (accountName == '') {
        alert("请填存款人微信昵称");
        return false;
    }

    if (amount == 0 || amount == "") {
        alert("请输入存款金额");
        return false;
    }

    if (parseFloat(amount) < minWechat) {
        alert("存款金额不能小于" + minWechat);
        return false;
    }
    if (parseFloat(amount) > maxWechat) {
        alert("存款金额不能大于" + maxWechat);
        return false;
    }

    $.showLoading();
    $.post('/user/dodeposit.html',
    {
        bank: bankId,
        amount: amount,
        prefKey: prefKey,
        transType: transType,
        accountName: accountName
    },
    function (data) {
        $.hideLoading();
        if (data.success == false) {
            alert(data.msg);
            return;
        }
        // 显示订单号
        $('#orderId').html(data.responseText);
        // 显示提示层
        dalert();
    });
    return true;
};

function preferential() {
    if ($("#wechat_prefKey").find("option:selected").attr("description") != undefined)
        $("#preferentialTxt").html("投注额要求：" + $("#wechat_prefKey").find("option:selected").attr("description"));
    else
        $("#preferentialTxt").html("");
}

// 提示层
function dalert() {
    dialog.show('#eveb_popup_bank_ok');
}