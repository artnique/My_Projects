var G_ISLOGIN = false;

$(function () {
    $("form").submit(function (event) {
        login($("#top_username"), $("#top_password"), $('#top_code'));
        return false;
    });
    bindLoginEnter();
    getFooter();
    getNotices();
    //getFloat();

});

function getFooter() {
    $.post('/news/getpagefooter', {}, function (data) {
        if (data.success == false) {
            // 失败
            $('#footer').html('');
            return;
        }
        $('#footer').html(data.responseText);
    });
}

function getFloat() {
    $.post('/news/getfloat', {}, function (data) {
        if (data.success == false) {
            return;
        }
        $('body').prepend(data.responseText);

        $(window).scroll(function () {
            popLeft();
            popRight();
        });

        popLeft();
        popRight();

        $("#pop_left_close").click(function () {
            $("#pop_left").hide();
        });
        $("#pop_zxkf_close").click(function () {
            $("#pop_right").hide();
        });
    });
}

function getIndexDialog() {
    $.post('/news/getIndexdialog', {}, function (data) {
        if (data.success == false) {
            return;
        }
        //layer.open({
        //    type: 1,
        //    title: '系统提示',
        //    content: data.responseText
        //});
        show(data.responseText, 580, 355)
    });
}

/**
* 绑定回车键
*/
function bindLoginEnter() {
    $("#top_username,#top_password", '').unbind("keydown", function (event) { keyDownHandler(event); }).bind("keydown", function (event) { keyDownHandler(event); });
};

/**
* 回车键事件
*/
function keyDownHandler(event) {
    if (event.keyCode == 13) {
        event.preventDefault();
        login($("#top_username"), $("#top_password"), $("#top_code"));
    }
};

function login($userName, $password, $authCode) {
    if ($userName.val() == "" || $userName.val() == "用户名") {
        alert('请输入用户名');
        return;
    }
    if ($password.val() == "" || $password.val() == "密码") {
        alert('请输入密码');
        return;
    }
    var authCode = '';
    if ($authCode && $authCode.length > 0) {
        if ($authCode.val() == "" || $authCode.val() == "验证码") {
            alert('请输入验证码');
            return;
        }
        authCode = $authCode.val();
    }
    $.showLoading();
    $.ajax({
        url: "/home/dologin?t=" + Math.random(),
        type: 'POST',
        data: {
            userName: $userName.val(),
            password: $password.val(),
            authCode: authCode
        },
        error: function (jqr) {
            alert('网络故障，请稍候重试');
            $.hideLoading();
            return false;
        },
        success: function (data) {
            $.hideLoading();
            if (data.success == false) {
                alert(data.msg);
                return;
            }
            if (data.msg == 'logined') {
                alert('您的账号已登录，请联系在线客服');
                return;
            }
            var redirectUrl = getQueryString('redirectUrl');
            if (redirectUrl != '' && redirectUrl != undefined) {
                location.href = redirectUrl;
            } else {
                window.location.reload();
            }
        }
    });
}

function logout() {
    $.showLoading();
    $.ajax({
        url: "/logout.html?r=" + Math.random(),
        type: 'GET',
        error: function (jqr) {
            $.hideLoading();
            alert('网络故障，请稍候重试');
            return false;
        },
        success: function (data) {
            location.href = "/index.html";
        }
    });
}

function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]);
    return null;
}
function getNotices() {
    var $notice = $('#ele-Notices');
    if ($notice.length == 0) {
        return;
    }
    $.post('/news/getnotices?r=' + new Date().getTime(), { pageIndex: 0, pageSize: 10 }, function (data) {
        if (data.success == false || data.count == 0) {
            $notice.html('');
            return;
        }
        var html = [];
        $.each(data.list, function (index, item) {
            html.push('<span>', item.Content_RemoveHtml, '</span>&nbsp;&nbsp;&nbsp;&nbsp;');
        });
        $notice.html(html.join(''));
        $("#scrollobj span").click(function () {
            show('/news.html', 700, 500, true);
        });
    });
};

//function showLogin(id, title, content, height, width, lock, time) {
//    art.dialog({
//        id: id,
//        padding: 0,
//        margin: 0,
//        title: false,
//        content: content,
//        height: height,
//        width: width,
//        lock: true,
//        cancel: false,
//        cancelVal: false

//    });
//}
//closeDialog
function closeDialog(objId, show) {
    if (show) {
        setCookie('show', 1);
    }
    var dialog = art.dialog.list[objId];
    dialog.close();
}

function setCookie(name, value)		//cookies设置
{
    var argv = setCookie.arguments;
    var argc = setCookie.arguments.length;
    var expires = (argc > 2) ? argv[2] : null;
    if (expires != null) {
        var LargeExpDate = new Date();
        LargeExpDate.setTime(LargeExpDate.getTime() + (expires * 1000 * 3600 * 24));
    }
    document.cookie = name + "=" + escape(value) + ((expires == null) ? "" : ("; expires=" + LargeExpDate.toGMTString()));

}
//setCookie("ft"+self.location.hostname.replace(/\./g,""),"1");

function getCookie(Name)			//cookies读取
{
    var search = Name + "=";
    if (document.cookie.length > 0) {
        offset = document.cookie.indexOf(search)
        if (offset != -1) {
            offset += search.length;
            end = document.cookie.indexOf(";", offset);
            if (end == -1) end = document.cookie.length;
            return unescape(document.cookie.substring(offset, end));
        }
        else return ""
    }
}

var load_game_link = function (game, type, gameId) {
    if (game == "IBC") {
        window.open('/game/launchsport.html?game=IBC&type=' + type, 'IBC', 'width=1070,height=700');
        return false;
    }
    if (!G_ISLOGIN) {
         alert("请先登录！");
        
        return false;
    }
    window.open('/game/play.html?game={0}&type={1}&gameId={2}'.format(game, type, gameId), game);
    return false;
};

var showJackpot = function (ctrlId) {
    // http://tickers.playtech.com/jackpots/new_jackpotxml.php?info=4&currency=cny
    // jackpots
    $.support.cors = true;
    var $jackpots = $('#' + ctrlId);
    if ($jackpots.html() == '' || $jackpots.html() == '￥' || $jackpots.html() == 'loading..') {
        var url = $jackpots.attr('url');
        if (url != '') {
            url += '&q=' + Math.floor(Math.random() * 10000000);
            $.ajax({
                type: "post",
                url: '/game/getjackpots',
                data: { url: url },
                datatype: "json",
                success: function (data) {
                    if (data.success == false) {
                        return;
                    }
                    var amount = data.msg;
                    if (amount == "0") {
                        amount = Math.floor(Math.random() * (1000000)) / 100;
                        amount += "";
                    }
                    $jackpots.html(amount.formatMoney());

                    setInterval(function () {
                        if ($jackpots == undefined || $jackpots.html() == undefined) {
                            return;
                        }
                        var value = $jackpots.html();
                        value = value.replace(/,/g, "");
                        value = parseFloat(value) + 1.11;
                        $jackpots.html(formatMoney(value));

                    }, 300);
                }
            });
        }
    }
};

function convertJsonDate(jsonDate, format) {
    var date = new Date(parseInt(jsonDate.replace("/Date(", "").replace(")/", ""), 10));
    return date.format(format);
}

function getUserCash() {
    $('#userCash').html('loading');
    $.ajax({
        url: '/user/getusercash?r=' + Math.random(),
        type: 'post',
        success: function (data) {
            if (data.success == false) {
                alert(data.msg);
                return;
            }
            var cash = data.responseText;
            $("#userCash").html(cash);
        },
        error: function (xhr, textStatus, errorThrown) {
            alert(errorThrown);
            return;
        }
    });
};

/*
//關閉效果
function FloatClose(t) { event.cancelBubble = true; $(t).parents('.TplFloatSet').hide(); }
function duilian() {
    var duilian = $("ul.TplFloatSet");
    var screen_w = screen.width;
    if (screen_w > 1024) {
        duilian.show();
        var scrollTop = $(window).scrollTop();
        duilian.stop().animate({ top: scrollTop + 150 });
    }
    $(window).scroll(function () {
        var scrollTop = $(window).scrollTop();
        duilian.stop().animate({ top: scrollTop + 150 });
    });
}
*/
function add_favorite(title, url) {
    url = url || encodeURI(window.location);
    title = title || a.title;
    try { // IE
        window.external.addFavorite(url, title);
    } catch (e) {
        try { // Firefox
            window.sidebar.addPanel(title, url, "");
        } catch (e) {
            if (/Opera/.test(window.navigator.userAgent)) { // Opera
                a.rel = "sidebar";
                a.href = url;
                return true;
            }
            alert('加入收藏失败，请使用 Ctrl+D 进行添加');
        }
    }
    return false;
}

function set_homepage(url) {
    var tip = '您的浏览器不支持此操作\n请使用浏览器的"选项"或"设置"等功能设置首页';
    if (/360se/i.test(window.navigator.userAgent)) {
        alert(tip);
        return false;
    }
    url = url || encodeURI(window.location);
    try {
        a.style.behavior = 'url(#default#homepage)';
        a.setHomePage(url);
    } catch (e) {
        alert(tip);
    }
    return false;
}