$(function () {
    getPromoList();
    getWinner();
    getJackpotList();
    getLeftCode();
    getRightCode();
    setInterval("rankinfo()", 1000);
});
var getPromoList = function (type) {
    var $promolist = $('#Promotions_Ul'); // ul
    $.post('/promo/getlist', { type: '', pageIndex: 0, pageSize: 20 }, function (data) {
        if (data.success == false) {
            return;
        }

        var obj = data.responseText
        var list = obj.list;
        if (list == null || obj.count == 0) {
            return;
        }
        var imgServerUrl = $('#hiddenImageServer').val();
        
        var html = [];
        $.each(list, function (index, item) {
            var img = imgServerUrl + item.Img.replace(/\\/g, "/");
            html.push('<li>');
            html.push('<a href="/promo.html?type=', item.InformationTypeId, '&id=', item.Id, '"> <img src="', img, '"/></a>');
            html.push('<div class="HoverInfo">');
            html.push('<div><span class="icon-round"></span><strong>', item.Title, '</strong></div>');
            html.push('<div class="font12 fontst">', item.Description, '</div>');
            html.push('</div>');
            html.push('</li>');
        });
        $promolist.html(html.join(''));
        $(".Promotions").slide({ triggle: "click", mainCell: ".Promotions_Ul", effect: "leftLoop", delayTime: 700, vis: 4, scroll: 1 });
        $(".Promotions_Ul li").mouseenter(function () {
            $(this).children(".HoverInfo").show()
        });
        $(".Promotions_Ul li").mouseleave(function () {
            $(this).children(".HoverInfo").hide()
        });
    });
};

function getWinner() {
    var $winner = $('#winnerUl');
    if ($winner.length == 0) {
        return;
    }
    $.post('/news/getbykey', {key: 'winner'}, function (data) {
        if (data.success == false) {
            return;
        }
        var winner = data.responseText;
        $winner.html(winner.Content);
        $(".Lottery").slide({ mainCell: ".LotteryUl", effect: "topLoop", autoPlay: true, delayTime: 1200, vis: 3, scroll: 1 });
    });
}

function getLeftCode() {
    var $lCode = $('.Video_l');
    if ($lCode.length == 0) {
        return;
    }
    $.post('/news/getads', { type: 'index_video_leftcode' }, function (data) {
        if (data.success == false) {
            return;
        }
        var ads = data.responseText;
        $.each(ads.list, function (index, item) {
            $lCode.html('<img src="' + $('#hiddenImageServer').val() + item.ImgUrl + '" />');
        });
    });
}
function getRightCode() {
    var $rCode = $('.Video_r');
    if ($rCode.length == 0) {
        return;
    }
    $.post('/news/getads', { type: 'index_video_rightcode' }, function (data) {
        if (data.success == false) {
            return;
        }
        var ads = data.responseText;
        $.each(ads.list, function (index, item) {
            $rCode.html('<img src="' + $('#hiddenImageServer').val() + item.ImgUrl + '" />');
        });
    });
}

// 彩金游戏列表
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
            var img = imgServerUrl + item.ImageUrl.replace(/\\/g, "/");

            var className, styleText;
            switch (index) {
                case 0:
                    className = 'First';
                    styleText = '';
                    break;
                case 1:
                    className = 'Second';
                    styleText = 'class="bc-ed"';
                    break;
                case 2:
                    className = 'Third';
                    styleText = '';
                    break;
                case 3:
                    className = 'Fourth';
                    styleText = 'class="bc-ed"';
                    break;
                case 4:
                    className = 'Fifth';
                    styleText = '';
                    break;
                default:
                    className = '';
                    styleText = '';
                    break;
            }
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

            html.push('<li ', styleText, '>');
            html.push('<div class="li-left"><span class="', className, '"></span></div>');
            html.push('<img class="left" src="', img, '" width="95" height="60" />');
            html.push('<div class="RankingInfo">');
            html.push('<div class="RankingName">', item.Title,'</div>');
            html.push('<span style="font-size:22px;color:rgb(128, 91, 15);">￥<span id="jpt_', index, '" url="', jackpotsUrl, '" class="jackpotC"></span></span>');
            html.push('</div>');
            html.push('</li>');
        });

        $jackpotList.html(html.join(''));
        $('span.jackpotC').each(function () {
            showJackpot($(this).attr('id'));
        });
    });
}