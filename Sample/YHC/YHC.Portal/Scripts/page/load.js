/* index */
var AutoPlayObj = null;
var timer = null;

$(document).ready(function () {
    $("#HuoJiang .box:odd").addClass("no_bg")

    key("用户名", "#top_username")
    key("密码", "#top_password")
    key("验证码", "#top_code")

    $(window).on("scroll", windowScroll);
    $(window).resize(function () {
        $(window).on("scroll", windowScroll);
    });
    function windowScroll() {
        //var top = $(window).scrollTop();
        //var menu_top = $('a[href="#navmain"]').offset().top;
        var scroll_top;
        if (document.documentElement && document.documentElement.scrollTop) {
            scroll_top = document.documentElement.scrollTop;
        }
        else if (document.body) {
            scroll_top = document.body.scrollTop;/*某些情况下Chrome不认document.documentElement.scrollTop则对于Chrome的处理。*/
        }

        //if (scroll_top > menu_top) {
        //    $('#menu').addClass('menu_fixed');
        //} else {
        //    $('#menu').removeClass('menu_fixed');
        //}

        if ($("#playbgad").length > 0) {
            var value = $(window).scrollTop();
            var bg_position = 139 - value;
            if ((value >= 1) && (value <= 140)) {
                $("#playbgad a.current").css('background-position', 'center ' + bg_position + 'px');
            } else if (value == 0) {
                $("#playbgad a.current").css('background-position', 'center 139px');
            } else {
                $("#playbgad a.current").css('background-position', 'center top');
            }
        }
        if ($("[data-floor]").length > 0)
        {
            h();
        }
    }

    //$("#scrollobj span").click(function () {
    //    show('announcement.html', 700, 500);
    //});

    if ($("#Banner").length > 0)
    {
        $("#Banner").Xslider({
            speed: 1200,
            space: 3000,
            auto: true, //自动滚动
            affect: 'fade',
            ctag: 'div'
        });
    }



    //if ($(".playAD").length > 0)
    //{
    //    $(".playAD").Xslider({
    //        speed: 1200,
    //        space: 8000,
    //        auto: true, //自动滚动
    //        affect: 'fade',
    //        ctag: 'div'
    //    });
    //}
	
	//if ($(".scrolltop").length>0)
    //{
    //    $(".scrolltop").imgscroll({
    //        speed: 40,    //图片滚动速度
    //        amount: 0,    //图片滚动过渡时间
    //        width: 1,     //图片滚动步数
    //        dir: "up"   // "left" 或 "up" 向左或向上滚动
    //    });
    //    //changePrice();
		
    //}	


    //if ($("#scrollobj").length > 0) {
    //    var t = setInterval("scroll(document.getElementById('scrollobj'))", 30);

    //    $("#scrollobj").hover(function () {
    //        window.clearInterval(t);
    //    }, function () {
    //        t = setInterval("scroll(document.getElementById('scrollobj'))", 30);
    //    })
    //}


    //$(".hoverAD").hover(function () {
    //    $(this).find(".icon").css("left", "-228px").stop(true, false).animate({
    //        "left": 0
    //    }, 500);


    //}, function () {
    //    $(this).find(".icon").stop(true, false).animate({
    //        "left": "228px"
    //    }, 500);
    //})


    $(".HelpDoc").hover(function () {
        $(this).addClass("hoverhelpdoc")
    }, function () {
        $(this).removeClass("hoverhelpdoc")
    })

    if ($(".Youhuibox").length > 0)
    {
        container_top = $("#container").position().top
        $(".Youhuibox").each(function () {
            Youhuibox.push($(this).position().top + container_top)
        })
        YouhuiNav = $("#YouhuiNav")
        h()
    }



    $(".GameList_pt li,.GameList_bbin li,.GameList_mg li").hover(function () {
        $(this).addClass("hoverGame");
    }, function () {
        $(this).removeClass("hoverGame");
    })
    /*投奖点击*/

/*
    $("#ChouJiang").find(".zz").focus(function () {
        var href = $(this).attr("href")
        objplay.click_zz(href)
        return false;
    })
    $("#ChouJiang").find(".zz").focus();
*/
    /*优惠侧栏点击*/
    $(".SortList li a").click(function () {
        var index = $(this).attr("data-floor") - 1
        $("html,body").animate({
            scrollTop: Youhuibox[index]
        }, 1000);
        $('.SortList li').removeClass('change');
        $(this).parent().addClass('change');
        return false;
    });



    //$('#GameListTab li').each(function (index) {
    //    if (!$(this).is(".change"))
    //    {
    //        $("#GameListTab").parent().find(".Tab_nr:eq(" + index + ")").hide();
    //    }

    //    $(this).click(function ()
    //    {

    //        $('#GameListTab li').removeClass('change');
    //        $(this).addClass('change');
    //        $("#GameListTab").parent().find(".Tab_nr").hide();
    //        $("#GameListTab").parent().find(".Tab_nr:eq(" + index + ")").show();
    //        return false;
    //    }
    //    )
    //})

	

    /*在线客服悬停效果*/
    $("#kefu").hover(
            function () {
                $(this).stop(true, false).animate({
                    "bottom": 0
                }, 400);
            },
            function () {
                $(this).stop(true, false).animate({
                    "bottom": "-192px"
                }, 400);
            }

    )


    if ($("#Games").length > 0)
    {


        $("#Games .gundong").each(function (index) {

            $("#Games .jCarouselLite:eq(" + index + ")").jCarouselLite({
                btnPrev: "#Games .move_left:eq(" + index + ")",
                btnNext: "#Games .move_right:eq(" + index + ")",
                visible: 5,
                auto: 2500,
                speed: 1000
            });

        })


    }


    $('#ClubTab li').each(function (index) {
        if (!$(this).is(".change"))
        {
            $("#ClubTab").parent().find(".ClubBox:eq(" + index + ")").hide();
        }

        $(this).mouseover(function ()
        {

            $('#ClubTab li').removeClass('change');
            $(this).addClass('change');
            $("#ClubTab").parent().find(".ClubBox").hide();
            $("#ClubTab").parent().find(".ClubBox:eq(" + index + ")").show();

        }
        )
    })



    if ($("#playbgad").length > 0)
    {
        $(".switcher a.cur").click();
        $("#playbgad").Xslider({
            speed: 500,
            space: 5000,
            auto: true, //自动滚动
            affect: 'fade',
            ctag: 'div'
        });
        $('#playbgad a').eq(0).addClass('current');
    }




    $("#HappycolorGame .box").hover(function () {
        $(this).find(".hoverbg").fadeIn(600)
    }, function () {
        $(this).find(".hoverbg").fadeOut(500)
    })


    //var dataOn = $('#header').attr('data-on');
    //if (dataOn && dataOn != "undefined") {
    //    $("." + dataOn).find('a').addClass('current');
    //}

    $('#okBtn').click(function () {
        $('#userlogin').hide();
        $('#forgetpwd').hide();
        $('#userloginafter').show();
    });



    //if ($('#JiangChi').length > 0) {
    //    changePrice();
    //}

    //if ($('#List1 li').length > 0) {
    //    $('#List1 li').hide();
    //    $('#List1 li').eq(0).show().addClass('active');
    //    ISL_Show();


    //    $('.leftarrow').click(function () {
    //        var curr = $('#List1 li.active');
    //        var next = curr.prev().length ? curr.prev() : $('#List1 li').eq(-1);
    //        if (timer) {
    //            clearInterval(timer);
    //            timer = null;
    //        }
    //        ISL_change(curr, next);

    //    });
    //    $('.rightarrow').click(function () {
    //        var curr = $('#List1 li.active');
    //        var next = curr.next().length ? curr.next() : $('#List1 li').eq(0);
    //        if (timer) {
    //            clearInterval(timer);
    //            timer = null;
    //        }
    //        ISL_change(curr, next);
    //    });
    //}
})

var Youhuibox = [], container_top, YouhuiNav


function scroll(obj) {
    var tmp = (obj.scrollLeft)++;
    //当滚动条到达右边顶端时
    if (obj.scrollLeft == tmp)
        obj.innerHTML += obj.innerHTML;
    //当滚动条滚动了初始内容的宽度时滚动条回到最左端
    if (obj.scrollLeft >= obj.firstChild.offsetWidth)
        obj.scrollLeft = 0;
}
function key(a, mb) {
    var keywords = a;

    $(mb).val(keywords).bind("focus", function () {
        if (this.value == keywords) {
            this.value = "";
            this.style.color = "#fff"
        }
    }).bind("blur", function () {
        if (this.value == "") {
            this.value = keywords;
            this.style.color = "#fff"
        }
    });
}


$(window).resize(function () {
    h();
})

function h() {

    if ($("[data-floor]").length == 0)
    {
        $(window).unbind("scroll")
        $(window).unbind("resize")
        return false;
    }

    var scrolltop = $(document).scrollTop();
    if (scrolltop > Youhuibox[0])
    {
        if (YouhuiNav.css("top") == 0)
            return false;
        YouhuiNav.css({
            "top": 0
        })
    }
    else
    {

        YouhuiNav.css({
            "top": Youhuibox[0] - scrolltop
        })
    }
    $("[data-floor]").each(function (index) {
        if (scrolltop > parseInt(Youhuibox[index]))
        {
            $(this).parents("div").find(".change").removeClass()
            $(this).parent().addClass("change")

        }
    })

}


function changePrice()
{
    $("#JiangChi dd").each(function () {
        var mb = $(this).find("span");
        var number = parseFloat(mb.attr("data-value"));
        var random_number = "0." + createRandom(1, 0, 99);
        number += parseFloat(random_number);
        mb.attr("data-value", number);
        mb.html("￥" + formatCurrency(number));
    });
	
	//$(".game_hover").each(function () {
    //    var mb2 = $(this);
    //    var number2 = parseFloat(mb2.attr("data-value"));
    //    var random_number2 = "0." + createRandom(1, 0, 99);
    //    number2 += parseFloat(random_number2);
    //    mb2.attr("data-value", number2);
    //    mb2.html("￥" + formatCurrency(number2));
    //});
	
    setTimeout("changePrice()", 600);
}

function formatCurrency(num) {
    num = num.toString().replace(/\$|\,/g, '');
    if (isNaN(num))
        num = "0";
    sign = (num == (num = Math.abs(num)));
    num = Math.floor(num * 100 + 0.50000000001);
    cents = num % 100;
    num = Math.floor(num / 100).toString();
    if (cents < 10)
        cents = "0" + cents;
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
        num = num.substring(0, num.length - (4 * i + 3)) + ',' +
                num.substring(num.length - (4 * i + 3));
    return (((sign) ? '' : '-') + num + '.' + cents);
}

function createRandom(num, from, to)
{
    var arr = [];
    var json = {};
    while (arr.length < num)
    {
        var ranNum = Math.ceil(Math.random() * (to - from)) + from;
        if (!json[ranNum])
        {
            json[ranNum] = 1;
            arr.push(ranNum);
        }
    }
    return arr;
}

//弹窗口
function show(content, width, height, file)
{
    height = height || 355;
    $("#window").empty().remove();
    if (file) {
        $("body").append('<div id="window"><div id="float_window"><iframe src="' + content + '" style="width:' + width + ';height:' + height + ';background-color:transparent;overflow:hidden;border-radius: 15px;" scrolling="no" frameborder="0" allowtransparency="true" id="popupFrame" name="popupFrame" width="100%" height="100%"></iframe></div></div>');
    }
    else {
        $("body").append('<div id="window"><div id="float_window">' + content + '</div></div>');
    }
    $("#window").show();
    $("#float_window").css({
        "width": width,
        "height": height,
        "margin-left": "-" + parseInt(width / 2) + "px",
        "margin-top": "-" + parseInt(height / 2) + "px",
        "border-radius": "15px"
    });

}
//关闭窗口
function closewindow() {
    $("#window").empty().remove();
}


/* index */
$("#ISL_Cont").onmouseover = function () {
    clearInterval(AutoPlayObj);
}
$("#ISL_Cont").onmouseout = function () {
    AutoPlay();
}
function AutoPlay() { //自动滚动
    clearInterval(AutoPlayObj);
    AutoPlayObj = setInterval('ISL_Show()', 3000); //间隔时间
}
function ISL_Show() {
    timer = setInterval(function () {
        var curr = $('#List1 li.active');
        var next = curr.next().length ? curr.next() : $('#List1 li').eq(0);
        ISL_change(curr, next);
    }, 1000 * 5);
}
function ISL_change(curr, next) {
    next.show();
    curr.fadeOut(function () {
        next.addClass('active').siblings().removeClass('active'); 
        if (!timer) {
            ISL_Show();
        }
    })
}

                        