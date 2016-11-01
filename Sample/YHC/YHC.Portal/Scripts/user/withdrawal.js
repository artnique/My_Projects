$(function () {
    withdrawal.init();
    bank.init();
});


var withdrawal = {
    imgServerUrl: $('#imgServerUrl').val(),

    $bankList: $('.eveb_withdraw_list'),

    init: function () {
        withdrawal.bankEvent();
        $('#btn_save').on('click', function () { withdrawal.doSave(); });
    },
    // 银行选择事件
    bankEvent: function () {
        var _source = '.eveb_withdraw_list li';
        $(_source).click(function () {
            $(_source).children('div').removeClass('on');
            $(this).children('div').addClass('eveb_withdraow_box on');
            var accountName = $(this).find('.withdraw_name').text();
            var accountNo = $(this).find('.withdraw_bank').text();
            var bankCardId = $(this).find('input').val();

            $('#bankAccountName').val(accountName);
            $('#bankAccountNoFormat').val(accountNo);
            $('#bankCardId').val(bankCardId);
        })
    },
    // 保存
    doSave: function () {
        var bankObj = $("#bankCardId");
        if (bankObj.length < 1 || bankObj.val() == '') {
            alert("请选择银行账号");
            return false;
        }

        if ($('#bankAccountName').val() == '' || $('#bankAccountNoFormat').val() == '') {
            alert("请选择银行账号");
            return false;
        }

        var bankId = bankObj.val();
        var amount = $('#amount').val();
        if (amount == "") {
            alert('请填写提款金额');
            $('#amount').focus();
            return false;
        }

        if (/^\d+$/g.test(amount) == false) {
            alert('提款金额只能为整数');
            $('#amount').focus();
            return false;
        }

        var min = $('#minWithdraw').val();
        var max = $('#maxWithdraw').val();

        if (parseFloat(amount) < min) {
            alert("提款金额不能小于" + min);
            return false;
        }
        if (parseFloat(amount) > max) {
            alert("提款金额不能大于" + max);
            return false;
        }
        var withdrawalPwd = $('#withdrawalPwd').val();
        if (withdrawalPwd == '') {
            alert('请输入提款密码');
            return;
        }
        //if (/^([A-Z]|[a-z]|[0-9]|[_@,.]){4,16}$/g.test(withdrawalPwd) == false) {
        //    alert('提款密码由4-16位字符组成');
        //    return;
        //}

        $.showLoading();
        $.post('/user/dowithdrawal.html', { bankAcconutId: bankId, amount: amount, withdrawlPwd: withdrawalPwd },
            function (data) {
                $.hideLoading();
                if (data.success == false) {
                    alert(data.msg);
                    return false;
                }

                // 稽核通过了，不管什么条件，直接取款
                if (data.msg != 'needInspect') {
                    // 显示成功信息
                    withdrawal.alertSuccess();
                    return;
                }

                // 显示稽核结果
                var allowWithdrawal = data.responseText.AllowWithdrawal;
                var inspectes = data.responseText.Inspectes;
                var transferFee = data.responseText.TransferFee;
                var wallets = ''; //data.responseText.Wallets;
                var walletsCount = data.responseText.WalletsCount;

                if (inspectes == undefined || inspectes == null) {
                    inspect.openErrorDialog();
                    return;
                }
                var html = '';
                if (allowWithdrawal == false) {
                    // 不允许取款
                    html = inspect.notWithdrawalHtml(inspectes, transferFee, amount, wallets, walletsCount);
                    inspect.openNotWithdrawalDialog(html);
                }
                else {
                    // 允许取款
                    html = inspect.needDebitHtml(inspectes, transferFee, amount, wallets, walletsCount);
                    inspect.openDebitDialog(html);
                }

            });
    },
    // 提示层
    alertSuccess: function () {
        dialog.show('#eveb_popup_bank_ok');
    },
    // 刷新银行列表
    refreshBank: function () {
        $.post('/user/getbindbanks.html', {}, function (data) {
            if (data.success == false) {
                return;
            }

            var html = [];
            $.each(data.responseText, function (index, item) {
                html.push('<li>');
                html.push('<div class="eveb_withdraow_box">');
                html.push('<img src="', withdrawal.imgServerUrl + item.Bank.LogoImg, '" alt="', item.Bank.BankName, '" />');
                html.push('<p class="withdraw_bank">', item.AccountNoFormat, '</p>');
                html.push('<input id="bank', item.Id, '" style="display:none;" name="bank" type="radio" value="', item.Id, '" />');
                html.push('<em>');
                html.push('<div class="fl">开户人：<strong class="withdraw_name">', item.AccountName, '</strong></div>');
                html.push('<div class="fr">开户行：<strong>', item.Province, ' ', item.City, ' ', item.BranchName, '</strong></div>');
                html.push('</em>');
                html.push('</div>');
                html.push('</li>');
            });
            html.push('<li>');
            html.push('<div class="eveb_withdraow_box eveb_withdraow_add">');
            html.push('<a href="javascript:;">添加银行卡</a>');
            html.push('</div>');
            html.push('</li>');

            withdrawal.$bankList.html(html.join(''));
            // 重新绑定银行相关事件
            withdrawal.bankEvent();
            // 绑定银行操作事件
            bank.init();
        });
    }
};


// 稽核
var inspect = {
    $bankCardId: $('#bankCardId'),
    $amount: $('#amount'),
    $withdrawalPwd: $('#withdrawalPwd'),

    // 错误提示
    openErrorDialog: function () {
        var i = $.layer({
            type: 1,
            title: '系统提示',
            closeBtn: [0, true],
            border: [5, 0.5, '#666', true],
            offset: [($(window).height() - 290) / 2 + 'px', ''],
            move: ['.juanmove', true],
            area: ['600px', '200px'],
            fadeIn: 300,
            page: {
                html: '网络故障，请稍候重试或联系客服人员'
            },
            success: function () {
            }
        });
    },

    // 实际提交存款
    doCofirmSave: function (dlg) {
        // 重新调用取款逻辑，还是需要做稽核，解决用户停留在当前页面，又去存款或玩游戏，导致数据不一致问题
        var $dlg_save = $('.inspect_save');
        $dlg_save.text('正在提交').unbind('click');

        $.post('/user/dowithdrawal.html', { bankAcconutId: inspect.$bankCardId.val(), amount: inspect.$amount.val(), confirmSave: true, withdrawlPwd: inspect.$withdrawalPwd.val() },
            function (data) {
                if (data.success == false) {
                    $dlg_save.text('确定提款').click(function () { inspect.doCofirmSave(dlg); });
                    alert(data.msg);
                    return false;
                }
                // 稽核通过了，不管什么条件，直接取款
                if (data.msg != 'needInspect') {
                    //alert(data.msg);
                }
                else {
                    alert('页面信息过期，点击确定重新载入页面');
                }
                // location.href = '/user/withdrawal_success.html';
                layer.close(dlg); // 隐藏弹出层
                withdrawal.alertSuccess(); // 显示成功提示
            });
    },

    // 不允许取款提示框
    openNotWithdrawalDialog: function (html) {
        var i = $.layer({
            type: 1,
            title: '系统提示',
            closeBtn: [0, true],
            border: [5, 0.5, '#666', true],
            offset: [($(window).height() - 290) / 2 + 'px', ''],
            move: ['.juanmove', true],
            area: ['710px', 'auto'],
            fadeIn: 300,
            page: {
                html: html
            },
            success: function () {
            }
        });
        $('.inspect_close').on('click', function () {
            layer.close(i);
        });
    },

    // 可以取款，需扣款
    openDebitDialog: function (html) {
        var i = $.layer({
            type: 1,
            title: '系统提示',
            closeBtn: [0, true],
            border: [5, 0.5, '#666', true],
            offset: [($(window).height() - 290) / 2 + 'px', ''],
            move: ['.juanmove', true],
            area: ['710px', '350px'],
            fadeIn: 300,
            page: {
                html: html
            },
            success: function () {
            }
        });
        $('.inspect_close').on('click', function () {
            layer.close(i);
        });
        $('.inspect_save').on('click', function () {
            inspect.doCofirmSave(i);
        });
    },

    notWithdrawalHtml: function (inspectes, transferFee, withdrawalAmount, wallets, walletsCount) {
        var html = [];
        var pattern = "yyyy-MM-dd HH:mm:ss";
        var totalFee = 0;

        // 表头
        html.push('<table class="inspect_master" style="display:none">');
        html.push('<tr style="background-color:#fbf6c1">');
        html.push('<td>序号</td>');
        html.push('<td>日期</td>');
        html.push('<td>存款</td>');
        html.push('<td>优惠</td>');
        html.push('<td>优惠活动</td>');
        html.push('<td>现流水</td>');
        html.push('<td>优惠要求流水</td>');
        html.push('<td>存款要求流水</td>');
        html.push('</tr>');
        $.each(inspectes, function (index, item) {
            var prefLaterText = '';
            var prefLaterColor = 'black';
            if (item.PrefLaterSended) {
                prefLaterText = '<br/>（存款已稽核）';
                prefLaterColor = 'red';
            }

            html.push('<tr>');
            html.push('<td>', index + 1, '</td>');
            html.push('<td>起始：', Date.fromJson(item.BeginTime).format(pattern), '<br />结束：', Date.fromJson(item.EndTime).format(pattern), '</td>');
            html.push('<td style="color:' + prefLaterColor + '">', item.DepositAmount + (item.Type == REPAIRTYPE ? "(修正)" : ""), prefLaterText, '</td>');
            html.push('<td>', item.PrefAmount + (item.Type == BONUSTYPE ? "(手工)" : ""), '</td>');
            html.push('<td>', (item.PrefName == undefined || item.PrefName == null || item.PrefName) ? '-' : item.PrefName, '</td>');
            html.push('<td title="总派彩：', item.TotalPayOut, '，分平台：', item.GamePayOutAmounts, '">', item.TotalBetAmountOfRevise, '</td>');
            html.push('<td>', item.PrefInspectNeedBetAmount, '</td>'); // 要求流水
            html.push('<td>', item.NormalityInspectNeedBetAmount, '</td>'); // 要求流水
            html.push('</tr>');

            totalFee += item.PrefInspectFee + item.NormalityInspectFee;
        });
        html.push('</table>');
        html.push('<table class="inspect_footer" style="display:none">');
        var transfer = '';
        var fixTotalFee = totalFee.toFixed(2);
        var fixTransferFee = transferFee.toFixed(2);
        var t = (fixTotalFee + fixTransferFee).toFixed(2);
        if (fixTransferFee > 0) {
            transfer = '&nbsp;&nbsp;+&nbsp;&nbsp;转账手续费：' + fixTransferFee + '&nbsp;&nbsp;=&nbsp;&nbsp;' + t;
        }
        html.push('<tr><td class="blue">&nbsp;&nbsp;&nbsp;&nbsp;预计扣款合计：', fixTotalFee, transfer, '。不允许取款</td></tr>');
        html.push('<tr><td>&nbsp;&nbsp;&nbsp;&nbsp;如对扣款有疑问，请联系客服人员</td></tr>');
        html.push('</table>');

        //var actualAmount = (withdrawalAmount - t).toFixed(2);

        // 概述
        var htmlDesc = [];
        htmlDesc.push('<div class="inspect_master_div">');
        if (walletsCount > 0) {
            htmlDesc.push('<div class="inspect_desc" style="margin-bottom:-10px;">您还有一个【锁定中】的优惠会占用稽核投注额，如果您不想它占用，请将锁定优惠取消。<a href="javascript:;" onclick="inspect.toWallet()" class="inspect_link">点击查看</a></div>');
        }
        htmlDesc.push('<div class="inspect_desc">申请提款：&nbsp;&nbsp;', withdrawalAmount, '&nbsp;&nbsp;，预计扣款合计：&nbsp;&nbsp;', t, '&nbsp;&nbsp;，不允许提款&nbsp;&nbsp;。<a href="javascript:;" onclick="inspect.showInspectDetails()" class="inspect_link">点击查看详情</a></div>');
        htmlDesc.push(html.join(''));
        htmlDesc.push('<div class="inspect_btn"><a href="javascript:;" class="inspect_close">关闭</a></div>');
        htmlDesc.push('</div>');

        return htmlDesc.join('');
    },

    needDebitHtml: function (inspectes, transferFee, withdrawalAmount, wallets, walletsCount) {
        var html = [];
        var pattern = "yyyy-MM-dd HH:mm:ss";
        var totalFee = 0;

        // 表头
        html.push('<table class="inspect_master" style="display:none">');
        html.push('<tr style="background-color:#fbf6c1">');
        html.push('<td>序号</td>');
        html.push('<td>日期</td>');
        html.push('<td>存款</td>');
        html.push('<td>优惠</td>');
        html.push('<td>优惠活动</td>');
        html.push('<td>现流水</td>');
        html.push('<td>要求<br/>优惠流水</td>');
        html.push('<td>需扣除<br/>优惠</td>');
        html.push('<td>要求<br/>存款流水</td>');
        html.push('<td>需扣除<br/>行政费用</td>');
        html.push('</tr>');
        $.each(inspectes, function (index, item) {
            html.push('<tr>');
            html.push('<td>', index + 1, '</td>');
            html.push('<td>起始：', Date.fromJson(item.BeginTime).format(pattern), '<br />结束：', Date.fromJson(item.EndTime).format(pattern), '</td>');
            html.push('<td>', item.DepositAmount + (item.Type == REPAIRTYPE ? "(修正)" : ""), '</td>');
            html.push('<td>', item.PrefAmount + (item.Type == BONUSTYPE ? "(手工)" : ""), '</td>');
            html.push('<td>', (item.PrefName == undefined || item.PrefName == null || item.PrefName) ? '-' : item.PrefName, '</td>');
            html.push('<td>', item.TotalBetAmountOfRevise, '</td>');
            html.push('<td>', item.PrefInspectNeedBetAmount, '</td>'); // 要求优惠流水
            html.push('<td>', item.PrefInspectFee, '</td>'); // 需扣除优惠
            html.push('<td>', item.NormalityInspectNeedBetAmount, '</td>'); // 要求存款流水
            html.push('<td>', item.NormalityInspectFee, '</td>'); // 需扣除行政费用
            html.push('</tr>');

            totalFee += item.PrefInspectFee + item.NormalityInspectFee;
        });
        html.push('</table>');
        html.push('<table class="inspect_footer" style="display:none">');
        var transfer = '';
        var fixTotalFee = totalFee.toFixed(2);
        var fixTransferFee = transferFee.toFixed(2);
        var t = (fixTotalFee + fixTransferFee).toFixed(2);
        if (fixTransferFee > 0) {
            transfer = '&nbsp;&nbsp;+&nbsp;&nbsp;转账手续费：' + fixTransferFee + '&nbsp;&nbsp;=&nbsp;&nbsp;' + t;
        }
        html.push('<tr><td class="blue">&nbsp;&nbsp;&nbsp;&nbsp;预计扣款合计：', fixTotalFee, transfer, '</td></tr>');
        html.push('<tr><td>&nbsp;&nbsp;&nbsp;&nbsp;如对扣款有疑问，请联系客服人员</td></tr>');
        html.push('</table>');

        var actualAmount = (withdrawalAmount - t).toFixed(2);

        // 概述
        var htmlDesc = [];
        htmlDesc.push('<div class="inspect_master_div">');
        if (walletsCount > 0) {
            htmlDesc.push('<div class="inspect_desc" style="margin-bottom:-10px;">您还有一个【锁定中】的优惠会占用稽核投注额，如果您不想它占用，请将锁定优惠取消。<a href="javascript:;" onclick="inspect.toWallet()" class="inspect_link">点击查看</a></div>');
        }
        htmlDesc.push('<div class="inspect_desc">申请提款：&nbsp;&nbsp;', withdrawalAmount, '&nbsp;&nbsp;，预计扣款合计：&nbsp;&nbsp;', t, '&nbsp;&nbsp;，实际提款：&nbsp;&nbsp;', actualAmount, '&nbsp;&nbsp;。<a href="javascript:;" onclick="inspect.showInspectDetails()" class="inspect_link">点击查看详情</a></div>');
        htmlDesc.push(html.join(''));
        htmlDesc.push('<div class="inspect_btn"><a href="javascript:;" class="inspect_save">确定提款</a><a href="javascript:;" class="inspect_close">取消</a></div>');
        htmlDesc.push('</div>');

        return htmlDesc.join('');
    },

    showInspectDetails: function () {
        $('.inspect_master,.inspect_footer').show();
    },

    // 父页面转入优惠钱包页面
    toWallet: function () {
        parent.location.href = '/user/walletapplylist.html';
    }
};


// 银行操作
var bank = {
    $bank: $('#bindcard_bankId'),
    $btn_save: $('#btn_save_bank_card'),

    init: function () {
        // 显示弹出窗
        $('.eveb_withdraow_add').find('a').on('click', function () { bank.show(); });
        // 保存
        bank.$btn_save.unbind('click').on('click', function () { return bank.doSave(); });
    },
    show: function () {
        dialog.show('#eveb_popup_bank_bind');

        // 加载数据
        // 银行
        bank.getBanks();

        // 省份
        city.init();
    },
    hide: function () {
        dialog.hide();
    },
    getBanks: function () {
        $.post('/user/getbanks.html', {}, function (data) {
            if (data.success == false) {
                return;
            }

            bank.$bank.empty();
            bank.$bank.append('<option value="0">请选择</option>');
            $.each(data.responseText, function (index, item) {
                var name = item.BankName;
                var id = item.Id;
                bank.$bank.append('<option value="' + id + '">' + name + '</option>');
            });
            return true;
        });
    },
    doSave: function () {
        if ($('#bindcard_bankId').val() == "0") {
            alert('请选择所属银行');
            return false;
        }
        if ($('#bindcard_accountHolder').val() == "") {
            alert('请填写开户人');
            return false;
        }
        if ($('#bindcard_province').val() == "0") {
            alert('请选择省份');
            return false;
        }

        if ($('#bindcard_city').val() == "0") {
            alert('请选择城市');
            return false;
        }
        if ($('#bindcard_openingBank').val() == "") {
            alert('请填写开户支行');
            return false;
        }
        if ($('#bindcard_account').val() == "") {
            alert('请填写银行账号');
            return false;
        }

        var withdrawalPwd = '';
        //var $withdrawalPwd = $("#withdrawalPwd");
        //if ($withdrawalPwd.length > 0) {
        //    var tempWithPwd = $withdrawalPwd.val();
        //    if (tempWithPwd == "") {
        //        alert("提款密码不能为空！");
        //        return false;
        //    }
        //    if (/^([A-Z]|[a-z]|[0-9]|[_@,.]){6,16}$/g.test(tempWithPwd) == false) {
        //        alert('提款密码由6-16位字符组成');
        //        return false;
        //    }

        //    withdrawalPwd = tempWithPwd;
        //}

        bank.$btn_save.unbind('click');
        $.showLoading();
        var bankId = $('#bindcard_bankId').val();
        var bankName = $('#bindcard_bankId').find('option:selected').text();
        var province = $('#bindcard_province').find('option:selected').text();
        var city = $('#bindcard_city').find('option:selected').text();
        var accountNo = $('#bindcard_account').val();
        var accountName = $('#bindcard_accountHolder').val();
        var branchName = $('#bindcard_openingBank').val();
        $.post('/user/dosavebankcard.html', {
            bankId: bankId,
            province: province,
            city: city,
            branchName: branchName,
            accountNo: accountNo,
            accountName: accountName,
            withdrawalPwd: withdrawalPwd
        },
            function (data) {
                $.hideLoading();
                bank.$btn_save.on('click', function () { return bank.doSave(); });
                if (data.success == false) {
                    alert(data.msg);
                    return false;
                }

                // 显示成功提示
                bank.alert();

                // 刷新父页面的银行卡列表
                withdrawal.refreshBank();
            });
    },
    // 显示成功提示
    alert: function () {
        dialog.show('#eveb_popup_bank_bind_ok');
    }
};


// 城市操作
var city = {
    $city: $('#bindcard_city'),
    $province: $('#bindcard_province'),

    init: function () {
        city.$province.on('change', function () { city.getCity($(this).val()); });
        // 加载省份
        city.getProvince();
    },
    // 加载省份
    getProvince: function () {
        $.post('/user/getprovinces.html', {}, function (data) {
            if (data.success == false) {
                return;
            }

            city.$province.empty();
            city.$province.append('<option value="0">请选择</option>');
            $.each(data.responseText, function (index, item) {
                var name = item.Name;
                var id = item.Id;
                city.$province.append('<option value="' + id + '">' + name + '</option>');
            });
            return true;
        });
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