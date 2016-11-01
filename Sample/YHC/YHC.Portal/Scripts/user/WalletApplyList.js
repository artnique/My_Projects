var pageSize = 10;
var pageIndex = 0;

$(function () {
    doSearch(true);
});

function doSearch(resetPageIndex) {
    resetPageIndex = resetPageIndex || false;
    if (resetPageIndex)
        pageIndex = 0;

    $.showLoading();
    $.post("/user/getwalletlist.html", {
        'status': $('#hiddenStatus').val(),
        'isInvalid': false,
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
        // 表格
        jQuery("#mainBody tr").remove();
        jQuery("#mainBody").append(TrimPath.processDOMTemplate("template_jst_list", data));
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

function doCancel(id) {
    jConfirm('确认取消吗？', '系统提示', function (r) {
        if (r) {
            $.showLoading();
            $.post('/user/dowalletcancel.html', { id: id },
                function (data) {
                    $.hideLoading();
                    if (!data.success) {
                        alert(data.msg);
                        return false;
                    }
                    alert(data.msg);
                    doSearch(false);
                    getWalletInfo();
                });
        }
    });
};