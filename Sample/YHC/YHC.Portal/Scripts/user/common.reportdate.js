var now = new Date(CONFIGTIMENOWSTRING); //当前日期   
var nowDayOfWeek = now.getDay(); //今天本周的第几天
nowDayOfWeek = nowDayOfWeek == 0 ? 6 : nowDayOfWeek - 1;
var nowDay = now.getDate(); //当前日   
var nowMonth = now.getMonth(); //当前月   
var nowYear = now.getYear(); //当前年   
nowYear += (nowYear < 2000) ? 1900 : 0; //   
var lastMonthDate = new Date(CONFIGTIMENOWSTRING); //上月日期   
lastMonthDate.setDate(1);
lastMonthDate.setMonth(lastMonthDate.getMonth() - 1);
var lastYear = lastMonthDate.getYear();
var lastMonth = lastMonthDate.getMonth();
function updateNow() {
    $.ajax({
        type: "POST",
        async: false,
        url: "/user/getconfigtimesetting.html",
        success: function (data) {
            //配置时区的当前时间字符串
            CONFIGTIMENOWSTRING = data.CONFIGTIMENOWSTRING;
            //Web服务器的时区
            WEBSERVERTIMEZONE = data.WEBSERVERTIMEZONE;
            //Web服务器的时区当前是为夏令时
            ISDAYLIGHTSAVINGTIME = data.ISDAYLIGHTSAVINGTIME.toString().toLowerCase() == 'true'.toLowerCase();
        }
    });
    now = new Date(CONFIGTIMENOWSTRING); //当前日期   
    nowDayOfWeek = now.getDay(); //今天本周的第几天
    nowDayOfWeek = nowDayOfWeek == 0 ? 6 : nowDayOfWeek - 1;
    nowDay = now.getDate(); //当前日   
    nowMonth = now.getMonth(); //当前月   
    nowYear = now.getYear(); //当前年   
    nowYear += (nowYear < 2000) ? 1900 : 0; //   
    lastMonthDate = new Date(CONFIGTIMENOWSTRING); //上月日期   
    lastMonthDate.setDate(1);
    lastMonthDate.setMonth(lastMonthDate.getMonth() - 1);
    lastYear = lastMonthDate.getYear();
    lastMonth = lastMonthDate.getMonth();
}

// 客户端时间与服务器时间偏差
function getLocalServerOffset() {
    return 0;
    //var d = new Date(); //创建一个Date对象
    //var localOffset = d.getTimezoneOffset(); //获得当地时间偏移的分钟数
    //var localServerOffset = localOffset + WEBSERVERTIMEZONE - (ISDAYLIGHTSAVINGTIME ? -60 : 0) + CONFIGTIMEZONE;//客户端时间与服务器时间偏差
    //return localServerOffset;
};

//获得某月的天数   
Date.prototype.getMonthDays = function (myMonth) {
    updateNow();
    var monthStartDate = new Date(nowYear, myMonth, 1);
    var monthEndDate = new Date(nowYear, myMonth + 1, 1);
    var days = (monthEndDate - monthStartDate) / (1000 * 60 * 60 * 24);
    return days;
};
//获得今天
Date.prototype.getToday = function (pattern) {
    updateNow();
    // 增加时区处理
    var offset = getLocalServerOffset();
    if (pattern == undefined || pattern.length <= 0) {
        return now;
    }
    return now.addMinutes(offset).format(pattern);
};
//获得昨天
Date.prototype.getYesterday = function (pattern) {
    updateNow();
    // 增加时区处理
    var offset = getLocalServerOffset();
    var yesterday = now.addMinutes(offset).addDays(-1);
    if (pattern == undefined || pattern.length <= 0) {
        return yesterday;
    }
    return yesterday.format(pattern);
};
//获取前天
Date.prototype.getBeforeYesterday = function (pattern) {
    updateNow();
    // 增加时区处理
    var offset = getLocalServerOffset();
    var beforeyesterday = now.addMinutes(offset).addDays(-2);
    if (pattern == undefined || pattern.length <= 0) {
        return beforeyesterday;
    }
    return beforeyesterday.format(pattern);
};
//获得本周开始日期
Date.prototype.getThisWeekStartDate = function (pattern) {
    updateNow();
    var thisWeekStartDate = new Date(nowYear, nowMonth, nowDay - nowDayOfWeek);
    if (pattern == undefined || pattern.length <= 0) {
        return thisWeekStartDate;
    }
    return thisWeekStartDate.format(pattern);
};
//获得本周结束日期
Date.prototype.getThisWeekEndDate = function (pattern) {
    updateNow();
    var thisWeekEndDate = new Date(nowYear, nowMonth, nowDay + (6 - nowDayOfWeek));
    if (pattern == undefined || pattern.length <= 0) {
        return thisWeekEndDate;
    }
    return thisWeekEndDate.format(pattern);
};
//获得上周的开始日期   
Date.prototype.getLastWeekStartDate = function (pattern) {
    updateNow();
    var lastWeekStartDate = new Date(nowYear, nowMonth, nowDay - nowDayOfWeek - 7);
    if (pattern == undefined || pattern.length <= 0) {
        return lastWeekStartDate;
    }
    return lastWeekStartDate.format(pattern);
};
//获得上周的结束日期   
Date.prototype.getLastWeekEndDate = function (pattern) {
    updateNow();
    var lastWeekEndDate = new Date(nowYear, nowMonth, nowDay - nowDayOfWeek - 1);
    if (pattern == undefined || pattern.length <= 0) {
        return lastWeekEndDate;
    }
    return lastWeekEndDate.format(pattern);
};

//获得本月的开始日期   
Date.prototype.getThisMonthStartDate = function (pattern) {
    updateNow();
    var thisMonthStartDate = new Date(nowYear, nowMonth, 1);
    if (pattern == undefined || pattern.length <= 0) {
        return thisMonthStartDate;
    }
    return thisMonthStartDate.format(pattern);
};
//获得本月的结束日期   
Date.prototype.getThisMonthEndDate = function (pattern) {
    updateNow();
    var thisMonthEndDate = new Date(nowYear, nowMonth, now.getMonthDays(nowMonth));
    if (pattern == undefined || pattern.length <= 0) {
        return thisMonthEndDate;
    }
    return thisMonthEndDate.format(pattern);
};
//获得上月开始时间   
Date.prototype.getLastMonthStartDate = function (pattern) {
    updateNow();
    var lastMonthStartDate = new Date(nowYear, lastMonth, 1);
    if (pattern == undefined || pattern.length <= 0) {
        return lastMonthStartDate;
    }
    return lastMonthStartDate.format(pattern);
};
//获得上月结束时间   
Date.prototype.getLastMonthEndDate = function (pattern) {
    updateNow();
    var lastMonthEndDate = new Date(nowYear, lastMonth, lastMonthDate.getMonthDays(lastMonth));
    if (pattern == undefined || pattern.length <= 0) {
        return lastMonthEndDate;
    }
    return lastMonthEndDate.format(pattern);
};
//获得本季度的开始日期   
Date.prototype.getThisQuarterStartDate = function (pattern) {
    updateNow();
    var thisQuarterStartDate = new Date(nowYear, Date.getThisQuarterStartMonth(), 1);
    if (pattern == undefined || pattern.length <= 0) {
        return thisQuarterStartDate;
    }
    return thisQuarterStartDate.format(pattern);
};
//获得本季度的结束日期   
Date.prototype.getThisQuarterEndDate = function (pattern) {
    updateNow();
    var thisQuarterEndMonth = getQuarterStartMonth() + 2;
    var thisQuarterEndDate = new Date(nowYear, thisQuarterEndMonth, getMonthDays(thisQuarterEndMonth));
    if (pattern == undefined || pattern.length <= 0) {
        return thisQuarterEndDate;
    }
    return thisQuarterEndDate.format(pattern);
};
//获得本季度的开始月份   
function getQuarterStartMonth() {
    updateNow();
    var quarterStartMonth = 0;
    if (nowMonth < 3) {
        quarterStartMonth = 0;
    }
    if (2 < nowMonth && nowMonth < 6) {
        quarterStartMonth = 3;
    }
    if (5 < nowMonth && nowMonth < 9) {
        quarterStartMonth = 6;
    }
    if (nowMonth > 8) {
        quarterStartMonth = 9;
    }
    return quarterStartMonth;
}