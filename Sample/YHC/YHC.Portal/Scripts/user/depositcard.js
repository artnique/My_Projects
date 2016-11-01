$(function () {
    pay.init();
});


var pay = {
    init: function () {
        // 提交事件
        $('#btn_save').on('click', function () { return pay.doSave(); });

        // 银行logo点击事件
        pay.initBankEvent();

        // 绑定展开和收起事件
        $('#eveb_bank_open').on('click', function () { pay.expand(); });
        $('#eveb_bank_close').on('click', function () { pay.collapse(); });
    },
    // 展开
    expand: function () {
        $('.eveb_bank_list').animate({ height: '200px' });
        $('#eveb_bank_open').hide();
        $('#eveb_bank_close').show();
    },
    // 收起
    collapse: function () {
        $('.eveb_bank_list').animate({ height: '90px' });
        $('#eveb_bank_open').show();
        $('#eveb_bank_close').hide();
    },
    // 银行logo点击事件
    initBankEvent: function () {
        $('.eveb_bank_list label').on('click', function (event) {
            var $input = $(this).find('input');

            $input.prop("checked", true); // ie6下面使用
            $('#payBankCode').val($input.attr('bankCode'));
            $('#payBankName').val($input.attr('bankName'));
            $('#payMerCode').val($input.attr('merCode'));

            // 相关状态修改
            BankInfo();
        });
    },
    // 提交表单
    doSave: function () {
        var min = $('#thirdPayDepositSingleMin').val();
        var max = $('#thirdPayDepositSingleMax').val();
        var platform = $("input[name='payPlatform']:checked");
        var amount = $('#online_amount').val();
        var faceCode = $('#faceCode').find('option:selected').val();
        var faceNo = $('#faceNo').val();
        var facePassword = $("#facePassword").val();

        if (amount == '') {
            alert('请填写充值金额');
            $('#online_amount').focus();
            return false;
        }
        if (platform.length < 1) {
            alert("请选择银行");
            return false;
        }
        if (parseFloat(amount) < min) {
            alert("充值金额不能小于" + min);
            return false;
        }
        if (parseFloat(amount) > max) {
            alert("充值金额不能大于" + max);
            return false;
        }
        if (faceCode == '') {
            alert('请选择卡面值');
            return false;
        }
        if (faceNo == '') {
            alert('请输入卡号');
            $('#faceNo').focus();
            return false;
        }
        if (facePassword == '') {
            alert('请输入卡密码');
            $('#facePassword').focus();
            return false;
        }

        // 弹出提示层
        pay.alert();

        return true;
    },
    // 弹出提示
    alert: function () {
        dialog.show('#eveb_popup_bank_ok');
    }
};

function BankInfo() {
    // 联动修改点卡面值
    $('#faceCode').html('<option value="">正在加载卡面值...</option>');
    $.post('/user/getdepositcardfacelist.html', { bankCode: $('#payBankCode').val() }, function (data) {
        var html = [];
        if (data.success == false) {
            html.push('<option value="">不存在卡面值或加载失败</option>');
            return;
        }
        else {
            html.push('<option value="">--请选择--</option>');
            $.each(data.responseText, function (index, item) {
                html.push('<option value="', item.FaceCode, '">', item.FaceName, '</option>');
            });
        }
        $('#faceCode').html(html.join(''));
    });
};