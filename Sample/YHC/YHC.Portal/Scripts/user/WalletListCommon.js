$(function () {
    getWalletInfo();
});

// 获取玩家游戏相关信息
var getWalletInfo = function () {
    $.post('/user/getwalletcount.html', {}, function (data) {
        if (data.success == false) {
            return;
        }

        var obj = data.responseText;
        $('#applycount').text(obj.ApplyCount);
        $('#cancelcount').text(obj.CancelCount);
        $('#initcount').text(obj.InitCount);
        $('#invalidcount').text(obj.InvalidCount);
        $('#sendcount').text(obj.SendCount);
    });
};