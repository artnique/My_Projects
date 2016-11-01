$(function () {
    $('#btnOnline').click(function () { return doSave(); });
    $('#youhui').change(function () { getDescription(); });

    // 提交存款
    function doSave() {
        var amount = $('#online_amount').val();
        var min = $('#hidMinDeposit').val();
        var max = $('#hidMaxDeposit').val();

        var prefKey = $('#online_prefKey').val();
        var payObj = $("input[name='payPlatform']:checked").attr("platform");
        var payMerCode = $("input[name='payPlatform']:checked").attr("mercode");

        if (payObj == '') {
            alert('支付平台错误');
            return false;
        }

        if (payMerCode == '') {
            alert('支付商户错误');
            return false;
        }

        if (amount == '') {
            alert('请填写存款金额');
            return false;
        }

        if (parseFloat(amount) < min) {
            alert("存款金额不能小于" + min);
            return false;
        }
        if (parseFloat(amount) > max) {
            alert("存款金额不能大于" + max);
            return false;
        }

        $("#btnOnline").attr("disabled", "disabled").val("正在提交");
        // 异步请求
        $.post('/pay/dopay.html',
                    {
                        online_prefKey: prefKey,
                        payPlatform: payObj,
                        payMerCode: payMerCode,
                        payBankCode: '',
                        payBankName: '',
                        online_amount: amount,
                        faceCode: '',
                        faceNo: '',
                        facePassword: ''
                    },
                    function (data) {
                        $("#btnOnline").removeAttr("disabled").val("提交");

                        // 弹窗显示银行卡信息或支付宝二维码
                        openDialog(data);
                    });
    };
});

// 弹窗
function openDialog(data) {
    var html='';

    if (data.indexOf('成功') > -1 && data.indexOf('|') > -1) {
        var orderNo = data.split('|')[1];
        if (orderNo == undefined || orderNo == '') {
            alert(data);
            return;
        }
        html = '<div style="margin-top:10px; font-size:13px">支付单号：<font style="color:red;font-weight:bold;">' + orderNo + '</font>, <br />' +
            '请用微信扫描下面的二维码, 如支付有任何疑问，请联系在线客服。</div><br />' +
           '<img style="margin-top:5px;" src="' + imagePath + 'upload/qrcode/' + orderNo + '.jpg" width=247 height=247 />';
    } else {
        alert(data);
        return;
    }

    // 弹窗
    var diag = new Dialog();
    diag.Width = 600;
    diag.Height = 350;
    diag.Title = "微信二维码信息";
    diag.InnerHtml = html;
    diag.CancelEvent = function () {
        // 关闭时清空填写的信息
        $("#online_amount").val("");

        diag.close();
    };
    
    diag.show();
};