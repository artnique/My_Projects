

$(function () {
    changeTopMenuStyle();

    // 初始化相关信息
    common.init();
    dialog.init();

    $("#logout").click(function() {
        // 注销
        $.post('/home/Logout.html', {}, function (data) {
            if (data.success == false) {
                return;
            }

            location = '/';
        });
    });
});

// 头部，底部，公告，logo，登录信息
var common = {
    $headerLogo: $('h1.eveb_logo'),
    $header: $('ul.eveb_header_nav'),
    $footer: $('div.eveb_footer'),
    $headerLoginInfo: $('a.eveb_header_info_uname'),
    $headerLiveChat: $('a.eveb_header_info_livechat'),
    $headerNews: $('ul.eveb_header_notice_list'),

    init: function () {
        common.getLogo();
        common.getHeader();
        common.getLoginInfo();
        common.getLiveChat();
        common.getTopNews();
        common.getFooter();
    },

    // 顶部logo
    getLogo: function () {
        // 写死位置，后面改成配置
        common.$headerLogo.find('img').attr('src', '/content/user/image/logo.jpg');
    },

    // 初始化顶部登录信息
    getLoginInfo: function () {
        $.post('/user/getlogininfo.html', {}, function (data) {
            if (data.success == false) {
                return;
            }

            common.$headerLoginInfo.html(data.responseText).attr('href', '/user/index.html');
        });
    },

    // 初始化顶部在线客服链接
    getLiveChat: function () {
        $.post('/user/getlivechat.html', {}, function (data) {
            if (data.success == false) {
                return;
            }

            common.$headerLiveChat.attr('livechat', data.responseText)
                .click(function () {
                    var charUrl = $(this).attr('livechat');

                    var chat;
                    if (parent == undefined) {
                        chat = window.open(charUrl, 'service', 'width=720,height=520,toolbar=0,resizable=1,location=no');
                    }
                    else {
                        chat = parent.window.open(charUrl, 'service', 'width=720,height=520,toolbar=0,resizable=1,location=no');
                    }
                    return chat;
                });
        });
    },

    // 获取顶部公告
    getTopNews: function () {
        $.post('/user/getnews.html', {}, function (data) {
            if (data.success == false) {
                return;
            }

            var html = [];
            $.each(data.responseText, function (index, item) {
                html.push('<li>');
                html.push('<a name="newsDetails" href="javascript:;">', item.Title, '</a>');
                html.push('<span>', Date.fromJson(item.CreateTime).format("yyyy-MM-dd"), '</span>');
                html.push('<input type="hidden" value="', item.Content_RemoveHtml, '"/>');
                html.push('</li>');
            });
            common.$headerNews.html(html.join(''));

            // 增加翻页事件
            var souce = '.eveb_header_notice_control i';
            var target = '.eveb_header_notice_list li';

            var num = 1;
            var notice_speed = 5000;//轮播速度
            var notice_auto = setInterval(function () {
                $('.icon_header_notice_r').click();
            }, notice_speed)
            var promo_length = $(target).length;
            $(target).eq(0).show().siblings().hide();
            $(souce).click(function () {
                if ($(this).hasClass('icon_header_notice_l')) {
                    num <= promo_length && num > 1 ? num-- : num = promo_length;
                } else {
                    num < promo_length ? num++ : num = 1;
                }
                $(target).eq(num - 1).slideDown().siblings().hide();
            });

            // 详情事件
            $("a[name='newsDetails']").on('click', function () {
                var title = $(this).parent().find('a').eq(0).html();
                var details = $(this).parent().find('input').eq(0).val();
                var time = $(this).parent().find('span').eq(0).html();

                common.showNewsDetails(title, time, details);
            });
        });
    },

    // 显示公告详情
    showNewsDetails: function (title, time, details) {
        // 赋值
        var pop_news_id = '#eveb_popup_news';
        var $pop_news = $(pop_news_id);

        $pop_news.find('.pop-title').eq(0).html(title);
        $pop_news.find('.pop-date').eq(0).html(time);
        $pop_news.find('.pop-content').eq(0).html(details);

        dialog.show(pop_news_id);
    },

    // 初始化顶部菜单
    getHeader: function () {
        //var html = [];
        //html.push('<li class="on"><a href="#">会员中心</a></li>');
        //html.push('<li><a href="#">电子游艺</a></li>');
        //html.push('<li><a href="#">真人娱乐</a></li>');
        //html.push('<li><a href="#">体育投注</a></li>');
        //html.push('<li><a href="#">VIP俱乐部</a></li>');
        //html.push('<li><a href="#">优惠活动</a></li>');
        //html.push('<li><a href="#">优惠活动</a></li>');

        $.post('/user/getheader.html', {}, function (data) {
            if (data.success == false) {
                // 失败
                common.$header.html('welcome');
                return;
            }

            common.$header.html(data.responseText);
        });
    },

    // 初始化底部信息
    getFooter: function () {
        //var html = [];
        //html.push('<span>');
        //html.push('<a href="#">关于我们</a>|');
        //html.push('<a href="#">责任博彩</a>|');
        //html.push('<a href="#">公告中心</a>|');
        //html.push('<a href="#">代理登录</a>|');
        //html.push('<a href="#">合作伙伴</a>');
        //html.push('</span>')
        //html.push('<em>Copyright© 2015 <a href="javascript:;">Welcome</a> All Rights Reserved.</em>');

        $.post('/user/getfooter.html', {}, function (data) {
            if (data.success == false) {
                // 失败
                common.$footer.html('welcome');
                return;
            }

            common.$footer.html(data.responseText);
        });
    },


    // 设置今天
    today: function (beginDateId, endDateId) {
        var t1 = (new Date()).getToday("yyyy-MM-dd");

        $(beginDateId).val(t1);
        $(endDateId).val(t1);
    },
    // 设置三天
    threeDay: function (beginDateId, endDateId) {
        var t1 = (new Date()).getBeforeYesterday("yyyy-MM-dd");
        var t2 = (new Date()).getToday("yyyy-MM-dd");
        

        $(beginDateId).val(t1);
        $(endDateId).val(t2);
    },
    // 设置本周
    theWeek: function (beginDateId, endDateId) {
        var t1 = (new Date()).getThisWeekStartDate("yyyy-MM-dd");
        var t2 = (new Date()).getThisWeekEndDate("yyyy-MM-dd");

        $(beginDateId).val(t1);
        $(endDateId).val(t2);
    }
};

// 改变菜单样式，存款，提款这一栏
function changeTopMenuStyle() {
    var navid = $('body').attr('navid');
    if (navid != undefined && navid != '') {
        var nav = navid.replace(/[\r\n\ ]/g, "");//去掉回车换行空格
        var top_id = nav;
        if (top_id != undefined && top_id != null && top_id != '') {
            $('i.' + top_id).parent().parent().addClass('on');
        }
    }
};

// 弹出层
var dialog = {
    scrollHeight: $(document).height(),
    // 初始化
    init: function () {
        $(window).resize(function () {
            $('.eveb_popup_wrap').height(dialog.scrollHeight);
        })
    },
    //弹窗 定位
    position: function (_target) {
        var popupW = $(_target).width() / 2;
        var popupH = $(_target).height() / 2;
        $(_target).css('margin-left', -popupW).css('margin-top', -popupH);
    },
    //弹窗 显示
    show: function (_target) {
        $('.eveb_popup_wrap').height(dialog.scrollHeight);
        dialog.position(_target);
        $('.eveb_popup').fadeOut('fast');
        $('.eveb_popup_wrap').fadeIn('fast');
        $(_target).fadeIn('fast');
        $(".eveb_popup_wrap,.eveb_popup_hd a.icon_close,.eveb_popup_bd a[name='pop_close']").click(function () {
            dialog.hide();
        })
    },
    //弹窗 关闭
    hide: function () {
        $('.eveb_popup_wrap').fadeOut('fast');
        $('.eveb_popup').fadeOut('fast');
    }
};