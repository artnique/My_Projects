$(function () {
    cash.init();
    info.init();

});


// 扩展信息，站内信，优惠信息
var info = {
    imgServerUrl: $('#_imgServerUrl').val(),

    init: function () {
        info.getUserExtendInfo();
        info.getMessages();
        info.GetPromoInfo();
    },

    // 获取相关信息，中间投注信息，优惠钱包信息，
    getUserExtendInfo: function () {
        $.post('/user/getuserextendinfo.html', {}, function (data) {
            if (data.success == false) {
                return;
            }

            var obj = data.responseText;

            $('#lastWeekRealBet').text(obj.LastWeekRealBet);
            $('#theWeekRealBet').text(obj.TheWeekRealBet);
            $('#lastLoginGameName').text(obj.LastLoginGameName)
            if (obj.LastLoginGameName == '---') {
                $('#lastLoginGameName').removeAttr('href');
            }
            else {
                $('#lastLoginGameName')
                    .attr('href', '/game/play.html?game=' + obj.LastLoginGamePlatform + '&type=' + obj.LastLoginGameType + '&gameId=' + obj.LastLoginGameId)
                    .attr('target', '_blank');
            }
            $('#lastLoginGameTime').text(obj.LastLoginGameTime);
            $('#lastDepositTime').text(obj.LastDepositTime);
            $('#lastWithdrawalTime').text(obj.LastWithdrawalTime);
        });
    },

    // 站内信
    getMessages: function () {
        $.post('/user/getmessagestop4.html', {}, function (data) {
            var $message = $('.eveb_index_letter');
            var html = [];
            if (data.success == false) {
                html.push('<li>');
                html.push('<a href="javascript:;">加载错误</a>');
                html.push('<span></span>');
                html.push('</li>');

                $message.html(html.join(''));
                return;
            }

            if (data.responseText.length == 0) {
                html.push('<li>');
                html.push('<a href="javascript:;">暂无消息</a>');
                html.push('<span></span>');
                html.push('</li>');

                $message.html(html.join(''));
                return;
            }

            $.each(data.responseText, function (index, item) {
                if (item.Status == 0) {
                    // 未读时显示标识
                    html.push('<li class="new">');
                    html.push('<em>新</em>');
                }
                else {
                    html.push('<li>');
                }
                html.push('<a name="messageDetails" href="javascript:;" msgId="', item.Id, '" status="', item.Status, '">', item.Message, '</a>');
                html.push('<span>', Date.fromJson(item.SendTime).format('MM-dd'), '</span>');
                html.push('<input type="hidden" value="', item.Message, '"/>');
                html.push('</li>');
            });

            $message.html(html.join(''));

            // 绑定details事件
            $("a[name='messageDetails']").on('click', function () {
                var details = $(this).parent().find('input').eq(0).val();
                var time = $(this).parent().find('span').eq(0).html();
                var msgId = $(this).attr('msgId');
                var status = $(this).attr('status');

                info.showMessageDetails(msgId, details, time, status);
            });
        });

    },

    // 站内信详情
    showMessageDetails: function (msgId, details, time, status) {
        // 赋值
        var pop_message_id = '#eveb_popup_message';
        var $pop_message = $(pop_message_id);

        $pop_message.find('.pop-date').eq(0).html(time);
        $pop_message.find('.pop-content').eq(0).html(details);

        dialog.show(pop_message_id);

        if (status == 0) {
            // 设置为已读
            $.post('/user/doreadmessage.html', { ids: msgId }, function (data) {
                if (data.success == false) {
                    alert(data.msg);
                    return;
                }

                // 样式处理
                var $msgLi = $("a[msgId='" + msgId + "']").parent();
                $msgLi.removeClass();
                $msgLi.find('em').remove();
            });
        }
    },

    // 获取优惠列表
    GetPromoInfo: function () {
        $.post('/user/getpromoinfo.html', {}, function (data) {
            if (data.success == false) {
                return;
            }
            // 总数
            var count = data.count;
            $('div.eveb_index_promo_num').find('em').text(count);

            if (count == 0) {
                return;
            }

            // 列表
            var html = [];
            $.each(data.list, function (index, item) {
                html.push('<li>');
                html.push('<a href="/promo.html" target="_blank">');
                html.push('<img src="', info.imgServerUrl + item.Thumbnail, '" />');
                html.push('</a></li>');
            });
            $('ul.eveb_index_promo_banner').html(html.join(''));

            // 切换优惠的事件
            var num = 1;
            $('.eveb_index_promo_num em').text(count);
            $('.eveb_index_promo_btn a').click(function () {
                if ($(this).hasClass('icon_promo_l')) {
                    num <= count && num > 1 ? num-- : num = count;
                } else {
                    num < count ? num++ : num = 1;
                }
                $('.eveb_index_promo_banner li').eq(num - 1).fadeIn().siblings().hide();
                $('.eveb_index_promo_num span').text(num + '/');
            })
        });
    }
};
// 我的资金相关操作
var cash = {
    $masterCash: $('#masterCash'),
    $gameCash: $('#gameCash'),
    $walletCash: $('#walletCash'),

    init: function () {
        cash.$masterCash.on('click', function () { cash.getMasterCash(); });
        cash.$gameCash.on('click', function () { cash.getGameCash(); });
        cash.$walletCash.on('click', function () { cash.getWalletCash(); });
        // 获取一次游戏余额，锁定优惠
        cash.getGameCash();
        cash.getWalletCash();
    },

    // 获取主账户余额
    getMasterCash: function () {
        cash.$masterCash.unbind('click');
        var $cash = cash.$masterCash.parent().find('em');
        $cash.text('loading...');
        $.post('/user/getmastercash.html', {}, function (data) {
            cash.$masterCash.on('click', function () { cash.getMasterCash(this); });
            if (data.success == false) {
                $cash.text('0');
                return;
            }
            $cash.text(parseFloat(data.responseText).format("#,###.##"));
        });
    },

    // 获取游戏余额
    getGameCash: function () {
        var $cash = cash.$gameCash.parent().find('em');
        $cash.text('loading...');
        $.post('/user/getallgamecash.html', {}, function (data) {
            cash.$gameCash.on('click', function () { cash.getGameCash(this); });
            if (data.success == false) {
                $cash.text('0');
                return;
            }
            $cash.text(parseFloat(data.responseText).format("#,###.##"));
        });
    },

    // 获取锁定优惠余额
    getWalletCash: function () {
        var $cash = cash.$walletCash.parent().find('em');
        $cash.text('loading...');
        $.post('/user/getwalletcash.html', {}, function (data) {
            cash.$walletCash.on('click', function () { cash.getWalletCash(this); });
            if (data.success == false) {
                $cash.text('0');
                return;
            }
            $cash.text(parseFloat(data.responseText).format("#,###.##"));
        });
    }
};