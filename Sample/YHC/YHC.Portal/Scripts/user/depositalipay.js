$(function () {
    $('#alipay_save').on('click', function () {
        doSave();
    });

    $('#alipay_prefKey').change(function () { preferential(); });

    $('#alipay_receiveBank').change(function () { bankChange(); });

    // 关闭对话框
    $("#pop_close").click(function () {
        dialog.hide("#eveb_popup_bank_ok");
    });
});

function doSave() {
    var minAlipay = parseFloat($('#hidAliPayMinAmount').val()) || 0;
    var maxAlipay = parseFloat($('#hidAliPayMaxAmount').val()) || 0;

    var bankId = $("#alipay_receiveBank").val();
    var accountName = $('#alipay_accountName').val();
    var amount = $('#alipay_amount').val();
    var transType = $('#transType_alipay').val();

    var prefKey = $('input[name=alipay_prefKey]:checked').val(); // $('#alipay_prefKey').val();

    if (bankId == '') {
        alert("选择收款银行");
        return false;
    }
    if (accountName == '') {
        alert("请填存款人姓名");
        return false;
    }
    if (amount == 0 || amount == '') {
        alert("请输入存款金额");
        return false;
    }

    if (parseFloat(amount) < minAlipay) {
        alert("存款金额不能小于" + minAlipay);
        return false;
    }
    if (parseFloat(amount) > maxAlipay) {
        alert("存款金额不能大于" + maxAlipay);
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
    if ($("#alipay_prefKey").find("option:selected").attr("description") != undefined)
        $("#preferentialTxt").html("投注额要求：" + $("#alipay_prefKey").find("option:selected").attr("description"));
    else
        $("#preferentialTxt").html("");
}
function bankChange() {
    var $bank = $("#alipay_receiveBank").find("option:selected");
    var accountName = "", accountNo = "", openBank = "", bankName = "";
    if ($bank && $bank.length > 0 && $bank.val() != "") {
        accountName = $bank.attr("bankAccountName");
        openBank = $bank.attr("openingBank");
        bankName = $bank.attr("bankName");
        accountNo = $bank.attr("bankAccountNo");
    }
    var str = ""
    if (accountName != "" && bankName != "") {
        str = accountName + "-" + bankName;
    }
    $('#lbl_alipay_accountName').html(str);
    $('#lbl_alipay_openbankName').html(openBank);
    $('#lbl_alipay_accountNo').html(accountNo);
}
// 提示层
function dalert() {
    dialog.show('#eveb_popup_bank_ok');
}