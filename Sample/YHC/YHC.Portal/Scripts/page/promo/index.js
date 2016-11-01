var pageIndex = 0;
var pageSize = 10;

$(function () {
    $('a#promo_' + $('#hiddenType').val()).click();
});

// 获取游戏列表
var getPromoList = function (type) {
    var $promolist = $('#PromoList'); // ul
    $promolist.html('<div style="text-align:center;margin:50px;">请稍候, 正在加载...</div>');
    if (type != $('#hiddenType').val()) {
        $('#hiddenType').val(type);
        //pageIndex = 0;
    }

    $.post('/promo/getlist', { type: type, pageIndex: pageIndex, pageSize: pageSize }, function (data) {
        if (data.success == false) {
            $promolist.html(data.msg);
            return;
        }

        var obj = data.responseText
        var list = obj.list;
        if (list == null || obj.count == 0) {
            $promolist.html('<div style="text-align:center;margin:50px;">暂无优惠活动，敬请期待...</div>');
            //initPagination(0);
            return;
        }

        var html = [];
        html.push('<div class="cl10"></div>');
        $.each(list, function (index, item) {
            html.push(createGameHtml(index, item));
        });

        $promolist.html(html.join(''));

        $(".ckyh").click(function () {
            //$(this).parents(".timecic").find(".timecic-bg").fadeIn(200);
            //$(this).parents(".timecic").find(".timecic-main").fadeIn(200);

            var index = $(this).attr("index");
            $(".timecic-bg").fadeIn(200);
            $('#promo_detail').html($('#promoItem_' + index).html()).fadeIn(200);
            $("#promo_detail .timecic-closed").click(function () {
                $(".timecic-bg").hide();
                $("#promo_detail").hide();
            });
        });
        //$(".timecic-closed").click(function () {
        //    $(".timecic-bg").hide();
        //    $(".timecic-main").hide();
        //});
        //if (pageIndex == 0) {
        //    initPagination(obj.count);
        //}
    });
};

var createGameHtml = function (index, item) {
    var imgServerUrl = $('#hiddenImageServer').val();
    var img = imgServerUrl + item.Img.replace(/\\/g, "/");

    var html = [];
    var isLeft = ((index % 2) == 0);
    if (isLeft) {
        html.push('<div class="fl timecic p10">');
    }
    else {
        html.push('<div class="fr timecic p10 t50">');
    }
    //html.push('<div class="fl timecic p10">');
    html.push('<img src="/Content/images/hot.png" alt="" class="biaoshi" />');
    html.push('<span class="timedot"></span>');
    html.push('<p class="f16 color-3838">', item.Title, '</p>');
    html.push('<div class="t10">');
    html.push('<img src="', img, '" alt="" class="fl pr10" width="283" height="241" />');

    //html.push('<div class="timecic-bg"></div>');
    html.push('<div class="timecic-main" id="promoItem_',index,'">');
    html.push('<div class="timecic-bt">', item.Title, '<span class="timecic-closed"></span></div>');
    html.push('<div class="timecic-img"><img src="', (imgServerUrl + item.Thumbnail.replace(/\\/g, "/")), '" width="680" height="150" /></div>');
    html.push('<div class="timecic-main-nr">', item.Content, '</div>');
    html.push('</div>');

    html.push('<div class="fl cic f12">');
    html.push('<p class="t10 hui">');
    html.push(item.Description);
    html.push('</p>');
    html.push('<div class="bottom">');
    html.push('<p class="lan">起始：', Date.fromJson(item.StartTime).format("yyyy-MM-dd"), '</p>');
    html.push('<p class="lan">截止：', Date.fromJson(item.EndTime).format("yyyy-MM-dd"), '</p>');
    html.push('<p class="t10">');
    html.push('<span class="ckyh" index="', index, '"></span>');
    html.push('</p>');
    html.push('</div>');
    html.push('</div>');
    html.push('<div class="cl"></div>');
    html.push('</div>');
    html.push('</div>');
    if (!isLeft) {
        html.push('<div class="cl"></div>');
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
    var type = $('#hiddenType').val();
    getPromoList(type);
    return false;
};

//function setTab(obj, type) {
//    $(obj).removeClass('Discountb_Btb').addClass('Discountb_Bta').siblings().removeClass('Discountb_Bta').addClass('Discountb_Btb');
//    getPromoList(type);
//}

//function openPromoInfo(box) {
//    var $box = $('#' + box);
//    if ($box.is(':hidden')) {
//        $('.CollapsiblePanelContent').hide();
//        $box.show();
//    }
//    else {
//        $box.hide();
//    }
//}