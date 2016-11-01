var pageSize = 10;
var pageIndex = 0;

$(function () {
    doSearch(true);
    $('#refreshDataList').click(function () { doSearch(true); });
    $('#theDay').on('click', function () { common.today('#beginTime', '#endTime'); });
    $('#threeDay').on('click', function () { common.threeDay('#beginTime', '#endTime'); });
    $('#theWeek').on('click', function () { common.theWeek('#beginTime', '#endTime'); });

    // 绑定事件
    $('#btnDelete').click(function () { deleteSelect(); });
    $("#btnRead").click(function () { read(); })
    $("#selectAll").click(function () { selectAll(); });

    // 全部，已读，未读事件
    messageTab.init();
});

function doSearch(resetPageIndex) {
    resetPageIndex = resetPageIndex || false;
    if (resetPageIndex)
        pageIndex = 0;

    $.showLoading();
    $.post("/user/getmessagelist.html", {
        'beginTime': $('#beginTime').val(),
        'endTime': $('#endTime').val(),
        'status': $('#status').val(),
        'pageIndex': pageIndex,
        'pageSize': pageSize
    }, function (data) {
        $.hideLoading();
        if (data.success != undefined && data.success == false) {
            initPagination(0);
            jQuery("#mainBody li div span").html(data.msg);
            return;
        }
        if (pageIndex == 0) {
            initPagination(data.count);
        }
        jQuery("#mainBody li").remove();
        jQuery("#mainBody").append(TrimPath.processDOMTemplate("template_jst_list", data));

        // 绑定展开事件
        $('.eveb_letter_title a').click(function () {
            $(this).parent().parent().parent('li').css('background', '#F5F5F5').siblings().css('background', '#FFF');
            if (!$(this).parent().parent().siblings('.detail').is(':visible')) {
                $(this).parent().parent().siblings('.detail').slideDown().parent('li').siblings().children('.detail').slideUp();
            } else {
                $(this).parent().parent().siblings('.detail').slideUp();
                $(this).parent().parent().parent('li').css('background', '#FFF');
            }

            // 设置为已读状态
            readRow(this);
        });
        $('.eveb_letter_list li:last').css('border-bottom', 0);

        // 删除单个
        $("a[name='deleteMessage']").on('click', function () { var msgid = $(this).attr('msgid'); deleteRow(msgid); });
    });
};

// 初始化分页
function initPagination(count) {
    if (count <= pageSize) {
        $('#pagination').html('');
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

// 删除一条
function deleteRow(id) {
    jConfirm('您确定要删除所选内容？', '系统提示', function (r) {
        if (r) {
            $.post('/user/dodeletemessage.html', { ids: id }, function (data) {
                if (data.success == false) {
                    alert(data.msg);
                    return;
                }
                doSearch();
            });
        }
    });
};

// 删除选择的记录
function deleteSelect() {
    var ids = getIds();
    if (ids.length <= 0) {
        alert('请先选中记录');
        return false;
    }

    jConfirm('您确定要删除所选内容？', '系统提示', function (r) {
        if (r) {
            $.post('/user/dodeletemessage.html', { ids: ids }, function (data) {
                if (data.success == false) {
                    alert(data.msg);
                    return;
                }
                doSearch(true);
            });
        }
    });
};

function getIds() {
    var ids = [];
    $("input[name='Ids']:checked").each(function () {
        if ($(this).parent().find('a.new').length > 0) {
            ids.push($(this).val());
        }
    });
    return ids.toString(",");
};

function selectAll() {
    var checked = $("#selectAll:checked").length == 1;
    $("input[name='Ids']").each(function () {
        $(this).prop("checked", checked);
    });
};

// 标记为已读（列表刷新方式，按钮操作）
function read() {
    var ids = getIds();
    if (ids.length <= 0) {
        alert('请先选中记录，或者您选中的记录中没有未读信息！');
        return;
    }

    $.showLoading();
    $.post('/user/doreadmessage.html', { ids: ids }, function (data) {
        $.hideLoading();
        if (data.success == false) {
            alert(data.msg);
            return;
        }
        // 未读的数量
        var readCount = 0;
        $("input[name='Ids']:checked").each(function () {
            if ($(this).parent().find('a.new').length > 0) {
                readCount++;
            }
        });

        editCount(readCount);
        doSearch(true);
    });
};

// 读一个，刷新当前记录（行操作）
function readRow(rowEvent) {
    var msgId = $(rowEvent).parent().find('input').eq(0).val();
    var status = $(rowEvent).parent().find('input').eq(0).attr('status');
    if (status == _MESSAGE_STATUS.Read) {
        return;
    }

    // 标读已读
    $.post('/user/doreadmessage.html', { ids: msgId }, function (data) {
        if (data.success == false) {
            alert(data.msg);
            return;
        }

        // 样式处理
        $(rowEvent).removeClass();
        $(rowEvent).parent().find('em').hide();

        editCount(1);

        // 更新已读状态
        $(rowEvent).parent().find('input').eq(0).attr('status',1);
    });
};

// 修改已读和未读数
function editCount(count) {
    var $unReadCountObj = $('#showUnReaded').find('em').eq(0);
    var $readCountObj = $('#showReaded').find('em').eq(0);

    var unReadCount = $unReadCountObj.text();
    var readCount = $readCountObj.text();

    var totalUnRead = parseInt(unReadCount) - count;
    if (totalUnRead < 0) {
        totalUnRead = 0;
    }

    $unReadCountObj.text(totalUnRead);
    $readCountObj.text(parseInt(readCount) + count);
};

// tab操作
var messageTab = {
    $status: $('#status'),

    init: function () {
        $('#showAll').click(function () { messageTab.showAll(this); });
        $("#showUnReaded").click(function () { messageTab.showUnReaded(this); })
        $("#showReaded").click(function () { messageTab.showReaded(this); });
    },
    getList: function () {
        doSearch(true);
    },
    showAll: function (event) {
        messageTab.$status.val('');

        // tab style
        messageTab.changeTab(event);
        messageTab.getList();
    },

    showReaded: function (event) {
        messageTab.$status.val(_MESSAGE_STATUS.Read);

        // tab style
        messageTab.changeTab(event);
        messageTab.getList();
    },

    showUnReaded: function (event) {
        messageTab.$status.val(_MESSAGE_STATUS.UnRead);

        // tab style
        messageTab.changeTab(event);
        messageTab.getList();
    },
    changeTab: function (event) {
        // tab style
        $(event).parent().parent().find('li').removeClass();
        $(event).parent().addClass('on');
    }
};