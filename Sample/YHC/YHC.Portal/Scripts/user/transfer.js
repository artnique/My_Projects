$(function () {
    transfer.init();
});

var transfer = {
    $platforms: $('p[author=game]'),
    $outAccount: $('#outAccount'),
    $inAccount: $('#inAccount'),
    $amount: $('#amount'),
    $btnSave: $('#btn_save'),

    init: function () {
        // 获取游戏平台余额
        $.each(transfer.$platforms, function () {
            var game = $(this).attr('game');
            transfer.getBalance(game);
        });

        // 初始化下拉事件
        transfer.$outAccount.on('change', function () { transfer.outChangeEvent(); });
        transfer.$inAccount.on('change', function () { transfer.inChangeEvent(); });

        // 绑定交换平台事件
        $('#switchAccount').on('click', function () { transfer.switchAccount(); });

        // 触发一次事件
        transfer.outChangeEvent();

        // 绑定保存事件
        transfer.$btnSave.on('click', function () { transfer.doSave(); });
    },

    // 获取游戏平台余额
    getBalance: function (gamePlatform) {
        var $amount = $('#amount_' + gamePlatform);
        $amount.text('loading...');

        $.post('/user/getgamecash.html?game=' + gamePlatform, {}, function (data) {
            if (data.success == false) {
                $amount.text(data.msg);
                return;
            }

            var amount = data.responseText;
            $amount.html(amount.format("#,###.##"));
        });
    },
    // 转出下拉事件
    outChangeEvent: function () {
        if (transfer.$outAccount.val() == '0') {
            var maxIndex = transfer.$inAccount.find("option:last").get(0).index;
            if (maxIndex > 0) {
                transfer.$inAccount.get(0).selectedIndex = 1;
            }
        }
        else {
            transfer.$inAccount.get(0).selectedIndex = 0;
        }
    },
    // 转入下拉事件
    inChangeEvent: function () {
        if (transfer.$inAccount.val() == '0') {
            var maxIndex = transfer.$outAccount.find("option:last").get(0).index;
            if (maxIndex > 0) {
                transfer.$outAccount.get(0).selectedIndex = 1;
            }
        }
        else {
            transfer.$outAccount.get(0).selectedIndex = 0;
        }
    },
    // 交换两个账号
    switchAccount: function () {
        var s1 = transfer.$outAccount.val();
        var s2 = transfer.$inAccount.val()

        transfer.$outAccount.val(s2);
        transfer.$inAccount.val(s1);

        transfer.$amount.val('');
        transfer.$amount.focus();
    },
    // 弹出提示
    doSave: function () {
        if (!transfer.validate()) {
            return false;
        }

        var from = transfer.$outAccount.val();
        var to = transfer.$inAccount.val();
        var amount = transfer.$amount.val();

        $.showLoading();
        $.post('/user/dotransfer.html', { from: from, to: to, amount: amount },
            function (data) {
                $.hideLoading();
                if (data.success == false) {
                    alert(data.msg);
                    return false;
                }

                transfer.alert();
                return true;
            });
        return true;
    },
    // 弹出提示
    alert: function () {
        dialog.show('#eveb_popup_transefer_ok');
    },
    validate: function () {
        var from = transfer.$outAccount.val();
        var to = transfer.$inAccount.val();
        var amount = transfer.$amount.val();


        if (from == "") {
            alert('请选择转出平台');
            return false;
        }
        if (to == "") {
            alert('请选择转入平台');
            return false;
        }
        if (from == to) {
            alert('同账户不允许互转');
            return false;
        }
        if (from != "0" && to != "0") {
            alert('游戏平台账户不允许互转');
            return false;
        }
        if (amount == "") {
            alert('请填写金额');
            return false;
        }
        if (parseFloat(amount) < 1) {
            alert('金额不能小于1');
            return false;
        }
        if (parseFloat(amount) > 200000) {
            alert('金额不能大于200000');
            return false;
        }

        return true;
    }
};