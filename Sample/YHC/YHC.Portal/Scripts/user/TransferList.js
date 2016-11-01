var pageSize = 10;
var pageIndex = 0;

$(function () {
    doSearch(true);
    $('#refreshDataList').click(function () { doSearch(true); });
    $('#theDay').on('click', function () { common.today('#beginTime', '#endTime'); });
    $('#threeDay').on('click', function () { common.threeDay('#beginTime', '#endTime'); });
    $('#theWeek').on('click', function () { common.theWeek('#beginTime', '#endTime'); });
});

function doSearch(resetPageIndex) {
    resetPageIndex = resetPageIndex || false;
    if (resetPageIndex)
        pageIndex = 0;

    $.showLoading();
    $.post("/user/gettransferlist.html", {
        'beginTime': $('#beginTime').val(),
        'endTime': $('#endTime').val(),
        'gamePlatform': $('#gamePlatform').val(),
        'type': $('#type').val(),
        'status': '',
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
        //$('#totalAmount').text(data.extend.TotalAmount.format("#,###.##"));
        $('#totalPageAmount').text(data.extend.TotalPageAmount.format("#,###.##"));
        // 表格
        jQuery("#mainBody tr").remove();
        jQuery("#mainBody").append(TrimPath.processDOMTemplate("template_jst_list", data));

        $.post('/User/gettotaltransfer.html', {
            'beginTime': $('#beginTime').val(),
            'endTime': $('#endTime').val(),
            'status': $('#status').val()
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

            $('#totalAmount').text(jsonData.format("#,###.##"));
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