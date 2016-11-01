// 下拉
$(function() {
    $(".dropt").on("click", function(event) {
        event.stopPropagation();
        if ($(this).parents(".drop").hasClass("xlk")) {
            $(this).parents(".drop").removeClass("xlk");
            $(this).siblings(".dropc").slideUp();
        } else {
            $(this).parents(".drop").addClass("xlk");
            $(this).siblings(".dropc").slideDown();
        }
    });
    $(".drop .sel").on("click", function() {
        event.stopPropagation();
        $(this).parents(".dropc").siblings(".dropt").find(".get").text($(this).text());
        $(this).parents(".drop").removeClass("xlk");
        $(this).parents(".dropc").slideUp();
    });
    $(document).on("click", function() {
        $(".drop").removeClass("xlk");
        $(".dropc").slideUp();
    })
});
// tab
$(function() {
    $(".tabclick").slide({
        trigger: "click"
    });
    $(".tabhover").slide({})
});
// 导航
$(document).ready(function() {
    $('.nav li a').each(function() {
        if ($($(this))[0].href == String(window.location)) {
            $(this).parents(".nLi");
        }
    });
    $(".nav").slide({
        type: "menu",
        titCell: ".nLi",
        targetCell: ".sub",
        effect: "slideDown",
        delayTime: 300,
        triggerTime: 10,
        returnDefault: true
    });
});
// 闪动
function shandong() {
    $(".shandong").toggleClass("color");
};
setInterval("shandong()", 500);
/*奖金数二*/
$(function() {
    var jackpotStyleHtml = $(".jackpotStyle1");
    var webFuny = {
            init: function() {
                //显示分页
                $(".page").show();
                //奖池金额随时间增加
                var $Num1Rel = [(new Date().getTime() - new Date("2015/1/1 00:00").getTime()) / 1000] / 60,
                    $Num1Rel1 = $Num1Rel / 60,
                    $Num1Rel2 = Math.round($Num1Rel1) * 1230 + 685696500;
                jackpotStyleHtml.html("￥<b>" + webFuny.AngelMoney($Num1Rel2.toString()) + "</b>.00");
                //每300毫秒添加一次金额
                setInterval(webFuny.addNum, 300);
            },
            //三位数加,
            AngelMoney: function(val) {
                return val.split('').reverse().join('').replace(/(\d{3})/g, '$1,').replace(/\,$/, '').split('').reverse().join('');
            },
            //随机数
            GetRandomNum: function(Min, Max) {
                var Range = Max - Min;
                var Rand = Math.random();
                return (Min + Math.round(Rand * Range));
            },
            //添加金额
            addNum: function() {
                var me = this,
                    addNum = webFuny.GetRandomNum(1, 10) + Number(jackpotStyleHtml.find("b").text().replace(/,/g, ""));
                jackpotStyleHtml.html("￥<b>" + webFuny.AngelMoney(addNum.toString()) + "</b>.00");
            }
        }
        //执行函数
    webFuny.init();
});
// 在线客服
$(function() {
        $(window).bind("scroll", function() {
            var offsetTop = 170 + $(window).scrollTop();
            $(".service-bg,.float-left-bg").animate({
                top: offsetTop
            }, {
                duration: 600,
                queue: false //此动画将不进入动画队列
            });
        });
    })
    // 登录
$(function() {
    $(".loginbox .itxt").focus(function() {
        if ($(this).val() == "") {
            $(this).siblings("label").animate({
                'opacity': '0.5'
            }, 300)
        }
    });
    $(".loginbox .itxt").blur(function() {
        if ($(this).val() == "") {
            $(this).siblings("label").show().animate({
                'opacity': '1'
            }, 300);
        }
    });
    $(".loginbox .itxt").on("input propertychange", function() {
        $(this).siblings("label").hide();
    })
})
$(function() {
        //公告
        $(".notice").slide({
            titCell: "h3",
            targetCell: "p",
            defaultIndex: 1,
            effect: "slideDown",
            delayTime: 300,
            trigger: "click"
        });
    })
    // 娱乐场caniso
$(function() {
    $(".hall").hover(function() {
        $(this).find(".casino-b,.casino-t").slideDown(200);
        $(this).find(".picw").removeClass("grey");
        $(this).find(".info").hide();
    }, function() {
        $(this).find(".casino-b,.casino-t").slideUp(200);
        $(this).find(".picw").addClass("grey");
        $(this).find(".info").show();
    })
});
//老虎机
$(function() {
    //老虎机热门游戏
    $(".slot-hot-img ul li").hover(function() {
        $(this).find("span").fadeIn();
        $(this).find(".btnw").fadeIn();
    }, function() {
        $(this).find("span").fadeOut();
        $(this).find(".btnw").fadeOut();
    });
    //老虎机切换
    $("#slot-game").slide({
        titCell: ".slot-game-tab li",
        mainCell: ".slot-game-hot",
        switchLoad: "_src",
        trigger: "click",
        delayTime: 0
    });
    //热门游戏滚动
    $(".slot-hot-game").slide({
        titCell: ".slot-hot-btn ul",
        mainCell: ".slot-hot-img ul",
        autoPage: true,
        effect: "left",
        vis: 5,
        trigger: "click"
    });
    //老虎机中奖玩家滚动
    $(".slot-winning").slide({
        titCell: ".slot-winning",
        mainCell: ".slot-winning-main ul",
        autoPage: true,
        effect: "topLoop",
        autoPlay: true,
        trigger: "click"
    });
    $(".slot-winning-inner").slide({
        titCell: ".slot-winning-inner",
        mainCell: ".slot-winning-main-inner ul",
        autoPage: true,
        effect: "topLoop",
        autoPlay: true,
        trigger: "click"
    });
    //老虎机悬停特效
    $(".slot-game-hot .enter").hide();
    $('.slot-game-outer .sloat-a').css('opacity', 0.4);
    $('.slot-game-hot li').hover(function() {
        var el = $(this);
        el.find(".enter").show();
        el.find(".sloat-jackpta").hide();
        el.find("p").addClass('c-black');
        // 先淡出阴影，后淡入文字
        el.find('.slot-game-h-img .sloat-a').stop().animate({
            width: 250,
            height: 150
        }, 'slow', function() {
            el.find('.slot-game-h-img .sloat-game-h-a').fadeIn('fast');
        });
    }, function() {
        var el = $(this);
        el.find(".enter").hide();
        el.find(".sloat-jackpta").show();
        el.find("p").removeClass('c-black');
        // 隐藏文字
        el.find('.slot-game-h-img .sloat-game-h-a').stop(true, true).hide();
        // 去掉遮罩
        el.find('.slot-game-h-img .sloat-a').stop().animate({
            width: 60,
            height: 60
        }, 'fast');
    });
});
// 优惠活动
$(function() {
    $(".hdlist li .hdt").on("click", function() {
        if ($(this).siblings(".hdc").is(":hidden")) {
            $(".hdc").hide();
            $(this).siblings(".hdc").show();
        } else {
            $(".hdc").hide();
        }
    })
});
// 新手指南
$(function() {
    var index = window.location.search;
    var t = index.replace('?tab', '') - 1;
    if (t == -1) {
        t = 0
    }
    $(".guidetab").slide({
        trigger: "click",
        defaultIndex: t
    })
});
// 随机数
function random() {
    $(".random").each(function() {
        var random = Math.round(Math.random() * 9);
        $(this).text(random)
    })
};
$(function() {
    random();
});
var max_num = 99999999;
var range = 100;
var min_num = 100000;

function rankinfo() {
    $(".RankingInfo").each(function() {
        var _nums = $(this).find(".RankingNum");
        var cur_num = "";
        $(_nums).each(function() {
            cur_num += $(this).text();
        });
        var r_num = Math.round(Math.random() * range);
        var re_num = Number(cur_num) + r_num;
        if (re_num > max_num) {
            re_num = min_num;
        }
        var num_arr = re_num.toString().split("");
        var l = 8 - num_arr.length;
        for (var i = 0; i < l; i++) {
            num_arr.unshift("");
        }
        $(_nums).each(function(n, e) {
            $(this).text(num_arr[n]);
        });
    });
};
$(function() {
        setInterval("rankinfo()", 1000);
    })
    // 定义弹窗
    // 定义弹出关闭事件
function tanchu(chaunti) {
    $("body").append("<div class='baibg'></div>").addClass("ovn");
    $(chaunti).show(0, function() {
        $(chaunti).css({
            "top": ($(window).height() - $(chaunti).height()) / 2,
            "left": ($(window).width() - $(chaunti).width()) / 2
        });
    });
};

function guanbi() {
    $(".tanchu").hide(0, function() {
        $(".baibg").remove();
        $("body").removeClass("ovn");
    });
};
$(function() {
    $(".js-guanbi,.baibg").on("click", function() {
        guanbi()
    });
});
// 找回密码
$(function() {
    $(".zhfs li").on("click", function() {
        $(".zhfs li").removeClass("on");
        $(this).addClass("on");
    })
});
// 关闭低版本浏览器提示
$(function () {
    $(".browser .closed").on("click",function () {
        $(".browser").slideUp();
    })
})