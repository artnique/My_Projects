$(function () {
    //showJackpot('jackpotBonus');

    if ($('#cats li.category').length > 0) {
        $('#cats li.category:first').click();
    }

    //initJackpotList();
    getJackpotList();

    getPTJackpotList();

    getWinner();

    getPTWinner();

    getInstantWinner();
    // 总奖池
    //showJackpotMG('jackpotBonusMG', "3", "", "");

    $('#btnSearch').click(function () {
        doSearchElectron();
    });

    setInterval("rankinfo()", 1000);
});

var pageSize = 20;
var pageIndex = 0;

// 获取游戏列表
var getGames = function (categoryId, type, title) {
    var platform = $('#hiddenPlatform').val();
    var $games = $('#games'); // ul
    $games.html('<div style="color:#bbbbbb;text-align:center;">请稍候, 正在加载游戏列表...</div>');
    switch (platform) {
        case "MG":
            break;
        case "PT":
            break;
        default:
            break;
    }
    if (categoryId != $('#hiddenCategoryId').val() || type != $('#hiddenType').val()) {
        $('#hiddenCategoryId').val(categoryId);
        $('#hiddenType').val(type);
        pageIndex = 0;
    }

    $.post('/game/getelectrones.html', { categoryId: categoryId, type: type, title: title, platform: platform, pageIndex: pageIndex, pageSize: pageSize }, function (data) {
        if (data.success == false) {
            $games.html(data.msg);
            return;
        }
        var list = data.list;
        if (list == null || data.count == 0) {
            $games.html('<div style="color:#bbbbbb;text-align:center;">暂无游戏，敬请期待...</div>');
            initPagination(0);
            return;
        }

        var html = [];
        $.each(list, function (index, item) {
            html.push(createGameHtml(index, item, platform));
        });

        $games.html(html.join(''));

        $(".games-main ul li").hover(function () {
            $(this).find("span").fadeIn();
            $(this).find(".games-enter").fadeIn();
        }, function () {

            $(this).find("span").fadeOut();
            $(this).find(".games-enter").fadeOut();

        });
        if (pageIndex == 0) {
            initPagination(data.count);
        }

        $(".slot-hot-img ul li").hover(function () {
            $(this).find("span").fadeIn();
            $(this).find(".btnw").fadeIn();
        }, function () {
            $(this).find("span").fadeOut();
            $(this).find(".btnw").fadeOut();
        });
        //老虎机切换
        $("#slot-game").slide({
            titCell: ".slot-game-tab li",
            mainCell: ".slot-game-hot",
            switchLoad: "_src",
            trigger: "click",
            delayTime: 0
        });
        //热门游戏滚动
        $(".slot-hot-game").slide({
            titCell: ".slot-hot-btn ul",
            mainCell: ".slot-hot-img ul",
            autoPage: true,
            effect: "left",
            vis: 5,
            trigger: "click"
        });
        //老虎机中奖玩家滚动
        $(".slot-winning").slide({
            titCell: ".slot-winning",
            mainCell: ".slot-winning-main ul",
            autoPage: true,
            effect: "topLoop",
            autoPlay: true,
            trigger: "click"
        });
        $(".slot-winning-inner").slide({
            titCell: ".slot-winning-inner",
            mainCell: ".slot-winning-main-inner ul",
            autoPage: true,
            effect: "topLoop",
            autoPlay: true,
            trigger: "click"
        });
        //老虎机悬停特效
        $(".slot-game-hot .enter").hide();
        $('.slot-game-outer .sloat-a').css('opacity', 0.4);
        $('.slot-game-hot li').hover(function () {
            var el = $(this);
            el.find(".enter").show();
            el.find(".sloat-jackpta").hide();
            el.find("p").addClass('c-black');
            // 先淡出阴影，后淡入文字
            el.find('.slot-game-h-img .sloat-a').stop().animate({
                width: 250,
                height: 150
            }, 'slow', function () {
                el.find('.slot-game-h-img .sloat-game-h-a').fadeIn('fast');
            });
        }, function () {
            var el = $(this);
            el.find(".enter").hide();
            el.find(".sloat-jackpta").show();
            el.find("p").removeClass('c-black');
            // 隐藏文字
            el.find('.slot-game-h-img .sloat-game-h-a').stop(true, true).hide();
            // 去掉遮罩
            el.find('.slot-game-h-img .sloat-a').stop().animate({
                width: 60,
                height: 60
            }, 'fast');
        });
    });
};

var createGameHtml = function (index, item, platform) {
    var html = [];
    var imgServerUrl = $('#hiddenImageServer').val();
    var img = imgServerUrl + item.ImageUrl.replace(/\\/g, "/");
    var title = item.Title;
    var jackpotStyle = 'style="display:none"', jackpotsUrl = '';
    if (item.ShowJackpots) {
        jackpotStyle = '';
        jackpotsUrl = item.Api.LoginUrl2 + "?info=" + item.JackpotsInfo + "&currency=cny";
        var jackpotCode;
        if (item.JackpotsInfo == window._jackpotInfoType.GAMEBASED) {
            jackpotCode = item.GameIdentify;
            if (item.JackpotsParams.length > 0) {
                jackpotCode = item.JackpotsParams;
            }
            jackpotsUrl += "&casino=playtech&game=" + jackpotCode;
        } else if (item.JackpotsInfo == window._jackpotInfoType.CASINOBASED || item.JackpotsInfo == window._jackpotInfoType.CASINOSTOTAL) {
            jackpotsUrl += "&casino=playtech";
        } else if (item.JackpotsInfo == window._jackpotInfoType.GAMEGROUPTOTAL) {
            jackpotCode = item.GameIdentify;
            if (item.JackpotsParams.length > 0) {
                jackpotCode = item.JackpotsParams;
            }
            jackpotsUrl += "&casino=playtech&group=" + jackpotCode;
        }
    }

        html.push('<li>');
        html.push('    <div class="slot-game-outer">');
        html.push('	    <div class="slot-game-h-img">');
        html.push('		    <img src="', img, '" width="188" height="90" />');
        html.push('		    <div class="sloat-game-h-a">');
        html.push('			    <div class="sloat-name " style="display: none;">');
        html.push('				    <a href="#">', title, '</a>');
        html.push('			    </div>');
        html.push('			    <div class="sloat-jackpt RankingInfo"></div>');
        html.push('		    </div>');
        html.push('		    <div class="cornerTL sloat-a" style="opacity: 0.4; width: 60px; height: 60px;"></div>');
        html.push('		    <div class="cornerTR sloat-a" style="opacity: 0.4; width: 60px; height: 60px;"></div>');
        html.push('		    <div class="cornerBL sloat-a" style="opacity: 0.4; width: 60px; height: 60px;"></div>');
        html.push('		    <div class="cornerBR sloat-a" style="opacity: 0.4; width: 60px; height: 60px;"></div>');
        html.push('	    </div>');
        html.push('	    <p class>', title, '</p>');
        html.push('	    <div class="sloat-jackpta RankingInfo"></div>');
        html.push('	    <a href="javascript:void(0);" onclick="load_game_link(\'', item.Api.GamePlatform, '\',\'', item.GameTypeText_EN, '\',\'', item.GameIdentify, '\')" class="enter enter-red l">进入游戏</a>');
        html.push('	    <a href="javascript:void(0);" onclick="load_game_link(\'', item.Api.GamePlatform, '\',\'', item.GameTypeText_EN, '\',\'', item.GameIdentify, '\')" class="enter r">免费试玩</a>');
        html.push('    </div>');
        html.push('</li>');
    
    return html.join('');
};

// 初始化分页
var initPagination = function (count) {
    if (count <= pageSize) {
        $("#pagination").html('');
        return;
    }
    $("#pagination").pagination(count, {
        callback: pageselectCallback,
        prev_text: "上一页",
        next_text: "下一页",
        items_per_page: pageSize,
        num_display_entries: 3,
        num_edge_entries: 2,
        ellipse_text: "...",
        link_to: "javascript:void(0)"
    });
};

var pageselectCallback = function (index, jq) {
    pageIndex = index;
    var categoryId = $('#hiddenCategoryId').val();
    var type = $('#hiddenType').val();
    var title = $('#keys').val();
    getGames(categoryId, type, title);
    return false;
};


function doSearchElectron() {
    pageIndex = 0;
    var title = $('#keys').val()
    getGames("", "", title);
}

// 彩金游戏列表
function initJackpotList() {
    $jackpotList = $('#cg-jackpot-list');
    $.post('/game/getjackpotsgames.html', {}, function (data) {
        if (data.success == false) {
            return;
        }
        var html = [];
        var imgServerUrl = $('#hiddenImageServer').val();
        $.each(data.responseText, function (index, item) {
            var img = imgServerUrl + item.ImageUrl.replace(/\\/g, "/");
            html.push('<li class="anitem receiver">');
            html.push('<div class="list-item-jp-icon">');
            html.push('<div id="MegaMoolah" class="prog-jackpot-list" style="background-image:url(/Content/images/scheduled/casino/pro-jackpots/mgs_RubyMegaMoolahIsis.png)"></div>');
            html.push('</div>');
            html.push('<div class="list-mask">');
            html.push('<div class="movbl-content">');
            html.push('<a class="play-4-real popup-new w772 h620 ryes icsn" href="javascript:void(0);">');
            html.push('<span class="li-title ellipsis">', item.Title, '</span>');
            var jackpot = (Math.random() * 100000).toFixed(2);
            html.push('<span class="li-jackpot anitem-detail progid_MegaMoolah" data-value="', jackpot, '">￥', jackpot, '</span>');
            html.push('</a>');
            html.push('</div>');
            html.push('</div>');
            html.push('</li>');
        });

        $jackpotList.html(html.join(''));
        changePrice();
    });
}

function getJackpotList() {
    $jackpotList = $('#jackpotList');
    if ($jackpotList.length == 0) {
        return;
    }
    $.post('/game/getjackpotsgames.html', {}, function (data) {
        if (data.success == false) {
            return;
        }
        var imgServerUrl = $('#hiddenImageServer').val();
        var html = [];
        $.each(data.responseText, function (index, item) {
            var num;
            switch (index) {
                case 0:
                    num = [9, 0, 1, 3, 4, 5, 8, 7];
                    break;
                case 1:
                    num = [7, 9, 1, 1, 3, 5, 1, 7];
                    break;
                case 2:
                    num = [6, 0, 1, 3, 4, 5, 8, 7];
                    break;
                case 3:
                    num = [4, 1, 1, 3, 1, 5, 4, 7];
                    break;
                case 4:
                    num = [2, 0, 5, 3, 4, 5, 3, 2];
                    break;
                default:
                    num = [0, 0, 0, 0, 0, 0, 0, 0];
                    break;
            }
            html.push('<div class="item">');
            html.push('<span class="RankingInfo fr">');
            html.push('<span class="red fl">￥</span>');
            html.push('<span class="RankingNum">', num[0], '</span>');
            html.push('<span class="RankingNum">', num[1], '</span>');
            html.push('<span class="RankingNum">', num[2], '</span>');
            html.push('<span class="RankingNum">', num[3], '</span>');
            html.push('<span class="RankingNum">', num[4], '</span>');
            html.push('<span class="RankingNum">', num[5], '</span>');
            html.push('<span class="RankingNum">', num[6], '</span>');
            html.push('<span class="RankingNum">', num[7], '</span>');
            html.push('</span>', item.Title);
            html.push('</div>');
            html.push('<div class="cl"></div>');
        });

        $jackpotList.html(html.join(''));
    });
}

function getPTJackpotList() {
    $ptjackpotList = $('#ptjackpotList');
    if ($ptjackpotList.length == 0) {
        return;
    }
    $.post('/game/getjackpotsgames.html', { pageIndex: 0, pageSize: 3 }, function (data) {
        if (data.success == false) {
            return;
        }
        var imgServerUrl = $('#hiddenImageServer').val();
        var html = [];
        $.each(data.responseText, function (index, item) {
            var num;
            switch (index) {
                case 0:
                    num = [9, 0, 1, 3, 4, 5, 8, 7];
                    break;
                case 1:
                    num = [7, 9, 1, 1, 3, 5, 1, 7];
                    break;
                case 2:
                    num = [6, 0, 1, 3, 4, 5, 8, 7];
                    break;
                case 3:
                    num = [4, 1, 1, 3, 1, 5, 4, 7];
                    break;
                case 4:
                    num = [2, 0, 5, 3, 4, 5, 3, 2];
                    break;
                default:
                    num = [0, 0, 0, 0, 0, 0, 0, 0];
                    break;
            }
            html.push('<div class="pt_jc_game">');
            html.push('<h3>', item.Title, '</h3>');
            html.push('<i class="RankingInfo yuan">');
            html.push('<span class="rmb">￥</span>');
            html.push('<span class="RankingNum">', num[0], '</span>');
            html.push('<span class="RankingNum">', num[1], '</span>');
            html.push('<span class="RankingNum">', num[2], '</span>');
            html.push('<span class="RankingNum">', num[3], '</span>');
            html.push('<span class="RankingNum">', num[4], '</span>');
            html.push('<span class="RankingNum">', num[5], '</span>');
            html.push('<span class="RankingNum">', num[6], '</span>');
            html.push('<span class="RankingNum">', num[7], '</span>');
            html.push('</i>');
            html.push('</div>');
        });

        $ptjackpotList.html(html.join(''));
    });
}

// 创建jackpot信息
function createJackpotInfo(url, $tmpJackSpan) {
    $.ajax({
        type: "post",
        url: '/game/getjackpots.html',
        data: { url: url },
        datatype: "json",
        success: function (data) {
            if (data.success == false) {
                return;
            }
            var amount = data.msg;
            $tmpJackSpan.attr("data-value", amount.formatMoney())
        }
    });
}

// MG总奖池
var showJackpotMG = function (ctrlId, jackpotInfoType, gameNameId, jackpotId) {
    // jackpots
    $.support.cors = true;
    var $jackpots = $('#' + ctrlId);
    if ($jackpots.html() == '' || $jackpots.html() == '￥' || $jackpots.html() == 'loading...') {
        $.ajax({
            type: "post",
            url: '/game/GetJackpotsForGamePlatForm',
            data: {
                game: "MG",
                jackpotInfoType: jackpotInfoType,
                gameNameId: gameNameId,
                jackpotId: jackpotId,
                currency: "cny",
                extendInfo: ''
            },
            datatype: "json",
            success: function (data) {
                var amount = "0";
                if (data.success == false) {
                } else {
                    amount = data.msg;
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
};


function getWinner() {
    var $winner = $('#winnerUl');
    if ($winner.length == 0) {
        return;
    }
    $.post('/news/getbykey', { key: 'slot_winner' }, function (data) {
        if (data.success == false) {
            return;
        }
        var winner = data.responseText;
        $winner.html(winner.Content);
        $(".cjyj").slide({ mainCell: ".bd", autoPage: true, effect: "topLoop", autoPlay: true, vis: 2 });
    });
}

function getPTWinner() {
    var $winner = $('#ptwinnerUl');
    if ($winner.length == 0) {
        return;
    }
    $.post('/news/getbykey', { key: 'slot_pt_winner' }, function (data) {
        if (data.success == false) {
            return;
        }
        var winner = data.responseText;
        $winner.html(winner.Content);
        $(".pt_cjyj").slide({ mainCell: ".pt_gd", autoPage: true, effect: "topLoop", autoPlay: true, vis: 1 });
    });
}

function getInstantWinner() {
    var $winner = $('#instant_winner');
    if ($winner.length == 0) {
        return;
    }
    $.post('/news/getbykey', { key: 'instant_winner' }, function (data) {
        if (data.success == false) {
            return;
        }
        var winner = data.responseText;
        $winner.html(winner.Content);
        $(".jszj").slide({ mainCell: ".bd ul", autoPlay: true, effect: "leftMarquee", vis: 2, interTime: 50 });
    });
}