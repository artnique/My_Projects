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
    if ($('#hiddenPlatform').val() == "MG") {
        showJackpotMG('jackpotBonusMG', "3", "", "");
    }
    else {
        showJackpot('jackpotBonus');
    }

    $('#btnSearch').click(function () {
        doSearchElectron();
    });

    //setInterval("rankinfo()", 1000);
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

        //$('.jackpot').each(function () {
        //    if (platform == "MG") {
        //        showJackpotMG($(this).attr('id'), $(this).attr('jackpotInfoType'), $(this).attr('gameNameId'), $(this).attr('jackpotId'));
        //    }
        //    else {
        //        showJackpot($(this).attr('id'));
        //    }
        //});

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

    if (platform == "PT") {
        html.push('<li>');
        html.push('<a href="javascript:void(0);" onclick="load_game_link(\'', item.Api.GamePlatform, '\',\'', item.GameTypeText_EN, '\',\'', item.GameIdentify, '\')">');
        html.push('<small></small>');
        html.push('<i class="yuan">进入游戏</i>');
        html.push('<img src="', img, '" width="173" height="133" alt="" />');
        html.push('<p>', title, '</p>');
        html.push('</a>');
        html.push('</li>');
    }
    if (platform == "MG") {
        html.push('<li>');
        html.push('<img src="', img, '" />');
        html.push('<span></span>');
        html.push('<a href="javascript:void(0);" onclick="load_game_link(\'', item.Api.GamePlatform, '\',\'', item.GameTypeText_EN, '\',\'', item.GameIdentify, '\')" class="games-enter"></a>');
        html.push('<a href="javascript:void(0);" onclick="load_game_link(\'', item.Api.GamePlatform, '\',\'', item.GameTypeText_EN, '\',\'', item.GameIdentify, '\')" class="games-name">', title, '</a>');
        html.push('</li>');
    }
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
            var jackpotsUrl = item.Api.LoginUrl2 + "?info=" + item.JackpotsInfo + "&currency=cny";
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

            html.push('<div class="item">');
            html.push('<span class="RankingInfo fr">');
            html.push('<span class="red fl">￥<span id="jpt_', index ,'" class="jackpotC" url="', jackpotsUrl, '"></span></span>');
            html.push('</span>', item.Title);
            html.push('</div>');
            html.push('<div class="cl"></div>');
        });

        $jackpotList.html(html.join(''));
        $('span.jackpotC').each(function () {
            showJackpot($(this).attr('id'));
        });
    });
}

function getPTJackpotList() {
    $ptjackpotList = $('#ptjackpotList');
    if ($ptjackpotList.length == 0) {
        return;
    }
    $.post('/game/getjackpotsgames.html', {pageIndex : 0, pageSize: 3}, function (data) {
        if (data.success == false) {
            return;
        }
        var imgServerUrl = $('#hiddenImageServer').val();
        var html = [];
        $.each(data.responseText, function (index, item) {
            var jackpotsUrl = item.Api.LoginUrl2 + "?info=" + item.JackpotsInfo + "&currency=cny";
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
            html.push('<div class="pt_jc_game">');
            html.push('<h3>', item.Title, '</h3>');
            html.push('<i class="RankingInfo yuan">');
            html.push('<span class="rmb">￥<span id="jpt_', index, '" class="jackpotC" url="', jackpotsUrl, '"></span></span>');
            html.push('</i>');
            html.push('</div>');
        });

        $ptjackpotList.html(html.join(''));
        $('span.jackpotC').each(function () {
            showJackpot($(this).attr('id'));
        });
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