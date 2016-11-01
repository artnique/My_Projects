$(function () {
    getSportAds();
});
function getSportAds() {
    var $ads = $('#Topic');
    if ($ads.length == 0) {
        return;
    }
    $.post('/news/getads', { type: 'sports_top' }, function (data) {
        if (data.success == false) {
            return;
        }

        var ads = data.responseText;
        $.each(ads.list, function (index, item) {
            $ads.html('<h3>体育头条</h3><a href="' + (item.Link != '' ? item.Link : '#') + '" ' + (item.LinkTarget != '' ? 'target="' + item.LinkTarget + '"' : '') + '><img style="width:228px;height:277px;" src="' + $('#hiddenImageServer').val() + item.ImgUrl + '" alt="' + item.Title + '" /></a>');
        });
    });
}