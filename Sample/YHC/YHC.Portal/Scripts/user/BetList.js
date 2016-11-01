var pageSize = 10;
var pageIndex = 0;

$(function () {
    doSearch(true);
    $('#refreshDataList').on('click', function () { doSearch(true); });
    $('#theDay').on('click', function () { common.today('#beginTime', '#endTime'); });
    $('#threeDay').on('click', function () { common.threeDay('#beginTime', '#endTime'); });
    $('#theWeek').on('click', function () { common.theWeek('#beginTime', '#endTime'); });
});

function doSearch(resetPageIndex) {
    resetPageIndex = resetPageIndex || false;
    if (resetPageIndex)
        pageIndex = 0;

    $.showLoading();
    $.post("/user/getbetlist.html", {
        'beginTime': $('#beginTime').val(),
        'endTime': $('#endTime').val(),
        'gamePlatform': $('#gamePlatform').val(),
        'pageIndex': pageIndex,
        'pageSize': pageSize
    }, function (data) {
        $.hideLoading();
        if (data.success != undefined && data.success == false) {
            initPagination(0);
            jQuery("#mainBody tr td").html(data.msg);
            return;
        }
        if (pageIndex == 0) {
            initPagination(data.count);
        }
        // 小计与总额
        //$('#totalBetAmount').text(data.extend.TotalBetAmount.format("#,###.##"));
        $('#totalPageBetAmount').text(data.extend.TotalPageBetAmount.format("#,###.##"));

        //$('#totalRealBetAmount').text(data.extend.TotalRealBetAmount.format("#,###.##"));
        $('#totalPageRealBetAmount').text(data.extend.TotalPageRealBetAmount.format("#,###.##"));

        //$('#totalPayOutAmount').text(data.extend.TotalPayOutAmount.format("#,###.##"));
        $('#totalPagePayOutAmount').text(data.extend.TotalPagePayOutAmount.format("#,###.##"));

        //$('#totalBetNum').text(data.extend.TotalBetNum);
        //$('#totalPageBetNum').text(data.extend.TotalPageBetNum);
        // 表格
        jQuery("#mainBody tr").remove();
        jQuery("#mainBody").append(TrimPath.processDOMTemplate("template_jst_list", data));

        $.post('/User/gettotalbet.html', {
            'beginTime': $('#beginTime').val(),
            'endTime': $('#endTime').val(),
            'gamePlatform': $('#gamePlatform').val()
        },
        function (jsonData) {
            if (jsonData.success != undefined) {
                if (jsonData.success == false) {
                    alert(jsonData.msg);
                    if (jsonData.logout)//已退出
                    {
                        top.location.href = '/home/login?redirectUrl=' + window.location.href;
                    }
                }
                return;
            }

            $('#totalBetAmount').text(jsonData.Bet.format("#,###.##"));
            $('#totalRealBetAmount').text(jsonData.RealBet.format("#,###.##"));
            $('#totalPayOutAmount').text(jsonData.PayOut.format("#,###.##"));
        });
    });
}

// 初始化分页
function initPagination(count) {
    if (count <= pageSize) {
        $("#pagination").html('');
        return;
    }
    $("#pagination").pagination(count, {
        callback: pageselectCallback,
        prev_text: "<",
        next_text: ">",
        items_per_page: pageSize,
        num_display_entries: 3,
        num_edge_entries: 2,
        ellipse_text: "...",
        link_to: "javascript:void(0)"
    });
};

function pageselectCallback(page_index, jq) {
    pageIndex = page_index;
    doSearch();
    return false;
};