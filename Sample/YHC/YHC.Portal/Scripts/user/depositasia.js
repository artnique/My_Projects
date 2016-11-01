$(function () {
    $('#btnAtm').click(function () { doSave(); });
    $('#btnCancel').click(function () { doCancel($('#hidWaittingId').val()); });
    $('#province').change(function () { getCity($(this).val()); });
    $('#bankId').change(function () { getPayWay($(this).val()); });
    //$('#youhui').change(function () { getDescription(); });
    //$("input[name='TransType']").click(function () { TransTypeChange(); });

    var waittingId = $('#hidWaittingId').val();
    if (waittingId > 0) {
        $('#btnAtm').hide();
        $('#btnCancel').show();
        if (confirm("您有未完成的订单，是否撤销该订单？")) {
            doCancel(waittingId);
        }
    }
    $.post('/user/getasiabank', null,
        function (data) {
            if (data.logout)//已退出
            {
                top.location.href = '/index.html';
            }
            if (data.success != undefined && data.success == false) {
                alert(data.msg);
                return false;
            }

            var $bank = $('#bankId');
            $bank.empty();
            $bank.append('<option value="">请选择</option>');
            $.each(data, function (key, value) {
                $bank.append('<option value="' + key + '">' + value + '</option>');
                //var bankName = item.BankName;
                //var bankId = item.Id;
                //$bank.append('<option value="' + bankId + '">' + bankName + '</option>');
            });
        });

    $.post('/user/getasiaprovince', null,
        function (data) {
            if (data.logout)//已退出
            {
                top.location.href = '/index.html';
            }
            if (data.success != undefined && data.success == false) {
                alert(data.msg);
                return false;
            }

            var $province = $('#province');
            $province.empty();
            $province.append('<option value="请选择">请选择</option>');
            $.each(data, function (index, value) {
                $province.append('<option value="' + value + '">' + value + '</option>');
            });
        });

    function doSave() {
        if ($('#bankId').val() == "") {
            alert('请选择存入银行');
            return false;
        }

        if ($('#payWay').val() == "") {
            alert('请选择存款方式');
            return false;
        }

        var amount = $('#amount').val();
        var min = $('#hidMinDeposit').val();
        var max = $('#hidMaxDeposit').val();
        var prefKey = $("input[name='prefKey']:checked").val();
        var bankId = $('#bankId').val();
        var payWay = $('#payWay').val();
        var payWayName = $("#payWay").find("option:selected").text();
        var province = $('#province').find('option:selected').text();
        var city = $('#city').find('option:selected').text();
        var accountName = $('#accountName').val();
        var idCard = $('#idCard').val();

        if (province == '请选择') {
            alert('请选择支行省份');
            return false;
        }

        if (city == '请选择') {
            alert('请选择支行城市');
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

        //ATM柜台
        //if (bankObj.length >= 1) {
        //var bank = bankObj.val();
        $.showLoading();
        $.post('/user/doAsiaPay',
            {
                bankId: bankId,
                amount: amount,
                prefKey: prefKey,
                payWay: payWay,
                payWayName: payWayName,
                province: province,
                city: city,
                idCard: idCard,
                accountName: accountName
            },
            function (data) {
                $.hideLoading();
                if (data.success == false) {
                    alert(data.msg);
                    return false;
                }
                var depositId = data.msg;
                $.layer({
                    type: 2,
                    title: ['系统提示', true],
                    iframe: { src: '/pay/asiapayconfirm?id=' + depositId },
                    area: ['600px', '360px'],
                    offset: ['100px', '']
                });

                //var i = $.layer({
                //    type: 1,
                //    title: '系统提示',
                //    style: ['background-color:#78BA32; text-align:center;', '#78BA32'],
                //    closeBtn: [0, true],
                //    border: [5, 0.5, '#666', true],
                //    offset: [($(window).height() - 290) / 2 + 'px', ''],
                //    move: ['.juanmove', true],
                //    area: ['400px', 'auto'],
                //    fadeIn: 300,
                //    page: {
                //        html: data.msg
                //    },
                //    success: function () {
                //    }
                //});
            });
        //}
        //else {
        //    //在线支付
        //    openDialog();
        //}
    };
});

function comfirmDeposit(orderId) {
    $.post('/user/paycomplete',
        { orderNo: orderId },
        function (data) {
            if (data.logout)//已退出
            {
                top.location.href = '/home/login?redirectUrl=' + window.location.href;
            }
            if (data.success != undefined && data.success == false) {
                alert(data.msg);
                return false;
            }
            window.location.href = "/user/depositlist.html";
        });
}

function initThirdPayEvent() {
    $(".pm_cont img,.selectpayimg").click(function () {
        $(this).parents("li").eq(0).find("input[type=radio]").click();
    });

    $(".pm_cont input[type=radio]").click(function () {
        $(this).parents(".pm_cont").eq(0).find(".selectpayimg").removeClass("on");
        $(this).parents("li").eq(0).find(".selectpayimg").addClass("on");

        // 相关状态修改
        BankInfo(this);
    });
};

//function getDescription() {
//    if ($("#youhui").find("option:selected").attr("description") != undefined)
//        $("#description").text("投注额要求：" + $("#youhui").find("option:selected").attr("description") + "倍");
//    else
//        $("#description").text("");
//}

function openDialog2() {
    var diag = new Dialog();
    diag.Width = 580;
    diag.Height = 290;
    diag.Title = "系统提示";
    diag.URL = '/pay/payconfirm.html';
    diag.show();
};

function openDialog() {
    $.layer({
        type: 2,
        title: ['系统提示', true],
        iframe: { src: '/pay/payconfirm.html' },
        area: ['580px', '290px'],
        offset: ['100px', '']
    });
};

function BankInfo(arg) {
    $('#divBankInfo').show();

    var openingBank = $(arg).attr('openingBank');
    var bankname = $(arg).attr('bankname');
    var name = $(arg).attr('accountname');
    var no = $(arg).attr('accountno');
    var imgurl = $(arg).attr('imgurl');
    //alert(imgurl);
    $('#accountName').html("姓名：" + name);
    $('#accountNo').html(no);
    $('#openingBank').html("地址：" + openingBank);
    $('#bankName').html(bankname);
    $('#bankImg').show();
    $('#bankImg').attr('src', imgurl);
    $('#divTransType').show();

}

function TransTypeChange() {
    var transferType = $("input[name='TransType']:checked").val();
    if (transferType == TRANSTYPE.atmtrans) {
        $('#trAddress').show();
        $('#tdTitle').text("ATM支行：");
    }
    else if (transferType == TRANSTYPE.atmdeposit) {
        $('#trAddress').show();
        $('#tdTitle').text("ATM支行：");
    }
    else if (transferType == TRANSTYPE.bank) {
        $('#trAddress').show();
        $('#tdTitle').text("银行所属支行：");
    }
    else {
        $('#trAddress').hide();
    }
}

// 获取城市
function getCity(province) {
    $.post('/user/getasiacities',
        { province: province },
        function (data) {
            if (data.logout)//已退出
            {
                top.location.href = '/home/login?redirectUrl=' + window.location.href;
            }
            if (data.success != undefined && data.success == false) {
                alert(data.msg);
                return false;
            }
            var $city = $('#city');
            $city.empty();
            $city.append('<option value="请选择">请选择</option>');
            $.each(data, function (index, value) {
                $city.append('<option value="' + value + '">' + value + '</option>');
            });
        });
}

//获取支付方式
function getPayWay(bankId) {
    $.post('/user/getasiapayway',
        { bankId: bankId },
        function (data) {
            if (data.logout)//已退出
            {
                top.location.href = '/home/login?redirectUrl=' + window.location.href;
            }
            if (data.success != undefined && data.success == false) {
                alert(data.msg);
                return false;
            }
            var $payWay = $('#payWay');
            $payWay.empty();
            $payWay.append('<option value="">请选择</option>');
            $.each(data, function (key, value) {
                $payWay.append('<option value="' + value + '">' + key + '</option>');
            });
        });
}

function doCancel(id) {
    $.showLoading();
    $.post('/user/paycancel.html', { id: id },
        function (data) {
            $.hideLoading();
            if (!data.success) {
                alert(data.msg);
                return false;
            }
            alert(data.msg);
            window.location.href = '/user/depositasia.html';
            return data.success;
        });
}