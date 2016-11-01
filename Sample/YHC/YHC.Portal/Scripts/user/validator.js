/**
 * 公共校验类
 * 参数列表:
 * placeholder : 用于在控件内部显示提示用户输入的内容 HTML5支持 通过当前的js向下兼容
 * dataType : 表明当前控件的数据校验类型
 * require : 表明当前控件中的数据是否必输
 * serverValidate : 表明当前控件除了客户端校验外，还需要服务器端的校验 参照方法:serverValidate
 * rig : 当在控件获取焦点的状态下，用于提示用户输入的消息
 * msg : 当在控件验证(离开焦点)时，出错时的消息
 * serverMsg : 当服务器端校验不通过时，则显示该消息，如果该消息不存在，则显示msg
 * msgConId : 用于指定集中显示错误消息的容器ID 当没有指定的时候，系统则向当前控件所在的父控件中增加一个提示块
 * dependName : 用于当前控件的验证是依赖前一个控件的控件名称
 * min : 当指定范围时的最小值
 * max : 当指定范围时的最大值
 * to : 当指定副本时的参照值
 * format : 日期类型的时候，表示日期格式
 * 示例
 * <input type="text" require="true" dataType="Mobile" serverValid="UniqueMobile" rig="请输入手机号" msg="手机号码必须唯一" serverMsg="该手机号已注册,请重新填写" msgConId="divMsgs"/>
 */
var Validator = {
    theObj: "", //当前的对象
    Require: /.+/,
    Empty: /^$/,
    Email: /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/,
    Phone: /^0\d{2,3}-?[1-9]\d{5,7}$/,
    Mobile: /^((13)|(14)|(15)|(18)|(17))\d{9}$/,
    MPhone: /(^((13)|(14)|(15)|(18)|(17))\d{9}$)|(^0\d{2,3}-?[1-9]\d{5,7}$)/,
    Url: /^(https?|ftp|mms):\/\/([A-Za-z0-9]+[_-]?[A-z0-9]*\.)+([A-Za-z0-9]+)(:[0-9]+)?\/?.*$/, /*'*/
    Currency: /^\d+(\.\d{1,2})?$/,
    Number: /^\d+$/,
    Year: /^\d{4}$/,
    Rate: /^[-\+]?\d+(\.\d+)?(%)?$/,
    Zip: /^[1-9]\d{5}$/,
    QQ: /^[1-9]\d{4,20}$/,
    Integer: /^[-\+]?\d+$/,
    Double: /^[-\+]?\d+(\.\d+)?$/,
    English: /^[A-Za-z]+$/,
    Chinese: /^[\u0391-\uFFE5]+$/,
    CompanyName: "/^([\u0391-\uFFE5]|[a-zA-Z0-9]){4,20}$/.test(theObj.val()) && /[\u0391-\uFFE5]+/.test(theObj.val())",
    General: /^([\u0391-\uFFE5]|\w)+$/, //一般词校验 如:单位名称 部门 职务等
    CustomNo: /^[a-zA-Z0-9._\-]{3,30}$/, //表示自定义的号码 如中介公司的合同号
    ReceiptUrl: /^[a-zA-Z0-9]{4,15}$/, //收款链接
    Account: /^[a-z0-9]{3,12}$/i,
    MAccount: /^([a-z]\w{3,15})|(((13)|(14)|(15)|(18))\d{9})$/i, //手机号
    OAccount: /^[0-9a-zA-Z_\-\+\.@]{3,30}$/i, //不做标准校验的帐号，用于中介帐号 并且能用邮箱登录
    UserName: /^[\u0391-\uFFE5]{2,10}$/,
    Password: /^([A-Z]|[a-z]|[0-9]|[_@,.]){6,16}$/,
    NewPassword: /^(|[a-z]|[0-9]){6,12}$/,
    WithdrawalPassword: /^([A-Z]|[a-z]|[0-9]|[_@,.]){4,16}$/, // 提款密码
    ValidCode: /^[0-9a-zA-Z]{4,6}$/,
    UnSafe: /^(([A-Z]*|[a-z]*|\d*|[-_\~!@#\$%\^&\*\.\(\)\[\]\{\}<>\?\\\/\'\"]*)|.{0,5})$|\s/,  /*'*/
    IsSafe: function (str) { return !this.UnSafe.test(str); },
    IdCard: "this.isIdCard(theObj.val().trim())",
    CreditCard: /^[0-9]\d{3}(\s\d{4}){2,4}(\s\d{1,4})?$/, //12-22位数字
    SafeString: "this.isSafe(theObj.val().trim())",
    Filter: "this.doFilter(theObj.val().trim(), theObj.attr('accept'))",
    Limit: "this.limit(theObj.val().trim().length,theObj.attr('min'),  theObj.attr('max'))",
    LimitB: "this.limit(this.LenB(theObj.val().trim()), theObj.attr('min'), theObj.attr('max'))",
    LimitNum: "this.limitNum(theObj.val().trim(), theObj.attr('min'), theObj.attr('max'))",
    Date: "this.isDate(theObj.val().trim(), theObj.attr('min'), theObj.attr('format'))",
    Repeat: "theObj.val().trim() == document.getElementsByName(theObj.attr('to'))[0].value",
    Range: "Number(theObj.attr('min')) <= Number(theObj.val().trim()) && Number(theObj.val().trim()) <= Number(theObj.attr('max'))",
    Compare: "this.compare(theObj.val().trim(),theObj.attr('operator'),theObj.attr('to'))",
    Custom: "this.exec(theObj.val().trim(), theObj.attr('regexp'))",
    Group: "this.mustChecked(theObj.attr('name'), theObj.attr('min'), theObj.attr('max'))",

    /**
    * 绑定验证表单
    */
    bind: function (con) {
        var objChecks; //需要校验的对象
        var objHolders; //默认值处理的对象
        if (con && con instanceof jQuery) {
            objChecks = con.find("input[dataType],textarea[dataType],select[dataType]");
            objHolders = con.find("input[placeholder],textarea[placeholder]");
        }
        else if (con && typeof con == "string") {
            objChecks = $("#" + con).find("input[dataType],textarea[dataType],select[dataType]");
            objHolders = $("#" + con).find("input[placeholder],textarea[placeholder]");
        }
        else if (Object.isElement(con)) {
            objChecks = $(con).find("input[dataType],textarea[dataType],select[dataType]");
            objHolders = $(con).find("input[placeholder],textarea[placeholder]");
        }
        else {
            objChecks = $("input[dataType],textarea[dataType],select[dataType]");
            objHolders = $("input[placeholder],textarea[placeholder]");
        }
        //防止重复绑定
        objChecks.unbind("focus", Validator.remind).bind("focus", Validator.remind); //绑定的提醒事件
        objChecks.unbind("blur", Validator.validate).bind("blur", Validator.validate); //绑定的校验事件
        //低版本的浏览器不支持placeholder属性的处理
        var objTest = document.createElement('input');
        var isSupportPlaceholder = 'placeholder' in objTest;
        if (!isSupportPlaceholder) {//如果支持该属性，则不需要处理
            objHolders.each(Validator.placeholder);
        }
    },
    /**
    * 校验指定的容器内的控件， con为容器控件或ID
    * 
    */
    validateForm: function (con) {
        var objChecks;
        if (con && con instanceof jQuery) {
            objChecks = con.find("input[dataType]:visible,textarea[dataType]:visible,select[dataType]:visible");
        }
        else if (con && typeof con == "string") {
            objChecks = $("#" + con).find("input[dataType]:visible,textarea[dataType]:visible,select[dataType]:visible");
        }
        else if (Object.isElement(con)) {
            objChecks = $(con).find("input[dataType]:visible,textarea[dataType]:visible,select[dataType]:visible");
        }
        else {
            objChecks = $("input[dataType]:visible,textarea[dataType]:visible,select[dataType]:visible");
        }
        //如果指定容器内已经有校验失败的，则直接返回false
        for (var index = 0; index < objChecks.length; index++) {
            if ($(objChecks[index]).attr("isValidateFaild")) {
                return false;
            }
        }
        Validator.clearMsgCon(objChecks); //清除现有的集中显示错误
        var isAllSuccess = true;
        //否则重新遍历一次
        objChecks.each(function () {
            var obj = $(this);
            if (!Validator.validateObject(obj, true)) {
                isAllSuccess = false;
                var faildMsgId = obj.attr("msgConId");
                if (!String.isNullOrWhiteSpace(faildMsgId)) {
                    return isAllSuccess;
                }
            }
        }); //第二个参数表明只需要做client验证
        return isAllSuccess;
    },
    /**
    * 内部方法：当控件获取焦点的时候，增加提醒框，提醒用户输入
    */
    remind: function () {
        var obj = $(this);
        Validator.clearState(obj, false); //先清除状态
        var rigMsg = obj.attr("rig");
        if (String.isNullOrWhiteSpace(rigMsg)) return; //没有增加属性，则不需要处理
        var span = $("<div id='__RemindMessagePanel' class='remindmessage'></div>");
        span.attr("targetId", obj.attr("id"));
        span.text(rigMsg);
        obj.parent().parent().append(span);
    },
    /**
    * 内部方法：当焦点从控件上移开的时候，校验当前的输入是否正确有效
    */
    validate: function () {
        var obj = $(this);
        Validator.validateObject(obj);
    },
    /**
    * 校验指定的对象
    * @param obj
    * @param clientValid boolean值，表明是否是只需要客户端校验
    * @param isDepend boolean值，表示是依赖项时，则不参照原有的是否必须字段，为全必须
    */
    validateObject: function (obj, clientValid, isDepend) {
        var flag = true;
        var serverMsg = false; //是否优先显示服务器凋错误消息  服务器端校验时使用
        Validator.clearState(obj, clientValid); //先清除状态
        var dataTypes = obj.attr("dataType");
        if (String.isNullOrWhiteSpace(dataTypes)) return true; //没有标记，不需要校验
        var inputValue = obj.val();
        if (!isDepend && obj.attr("require") != "true" && (inputValue.trim().lenght == 0 || new Number(inputValue) == 0)) return true; //非必须输入的控件校验
        dataTypes = dataTypes.split("|");
        try {
            for (var index = 0; index < dataTypes.length; index++) {
                var dataType = dataTypes[index];
                switch (dataType) {
                    case "IdCard":
                    case "Date":
                    case "Repeat":
                    case "Range":
                    case "Compare":
                    case "Custom":
                    case "Group":
                    case "Limit":
                    case "LimitB":
                    case "LimitNum":
                    case "SafeString":
                    case "Filter":
                    case "CompanyName":
                        theObj = obj;
                        flag = eval(this[dataType]);
                        break;
                    case "StartEndTime": //租房起始和终止日期
                        var startTime = $("[name=" + obj.attr("dependName") + "]").val();
                        var dtStartTime = Date.fromFormat(startTime);
                        var dtEndTime = Date.fromFormat(obj.val());
                        flag = dtEndTime >= dtStartTime;
                        break;
                    case "Depend": //依赖项
                        var depandObjs = $("[name=" + obj.attr("dependName") + "]");
                        for (var i = 0; i < depandObjs.length; i++) {
                            var depandObj = $(depandObjs[i]);
                            if (depandObj.is(":hidden")) continue; //如果依赖没有显示则不处理
                            var dependFlag = Validator.validateObject($(depandObjs[i]), clientValid, true);
                            if (!dependFlag) {
                                flag = false;
                                break;
                            }
                        }
                        break;
                    default:
                        //目前帐号用手机号注册  
                        flag = this[dataType].test(obj.val().trim());
                        break;
                }
                if (!flag) break; //有一个未通过，则全部未通过
            }
            if (flag && !clientValid && !obj.attr("isValidateFaild")) {//如果只需要做客户端校验 或者没有校验成功
                var serverValidName = obj.attr("serverValid");
                if (serverValidName) {
                    var serverValidFlag = Validator.serverValidate(obj, serverValidName);
                    if (!serverValidFlag) {//如果服务器端没有校验成功
                        flag = serverValidFlag;
                        serverMsg = true;
                    }
                }
            }
        } catch (e) {
            $("<div class='comm_dialog_cont'>" + e + "</div>").dialog({ title: "错误提示", modal: true, buttons: [{ text: "确定", "class": 'comm_dialog_btn_save', click: function () { $(this).dialog("destroy"); } }] });
        }
        if (flag) {
            Validator.showSuccessMsg(obj);
            if (!isDepend && obj.attr("name")) {//不是依赖项触发的话，则需要重新校验依赖它的所有输入
                $("[dependName=" + obj.attr("name") + "]").each(function () {
                    Validator.clearState($(this), false);
                    //Validator.validateObject($(this), clientValid, isDepend);
                });
            }
        } else {
            Validator.showFaildMsg(obj, clientValid, serverMsg);
        }
        return flag;
    },
    /**
    * 内部方法：清除掉原有的状态，表明需要重新验证
    * @param obj
    * @param clearCon 如果错误消息是集中在窗口中显示的，通过该参数来判断是否清除容器中的错误
    */
    clearState: function (obj, clearCon) {
        if (!clearCon) {//如果不清理窗口的话，则需要判断对象错误是否在容器中
            var faildMsgId = obj.attr("msgConId");
            if (!String.isNullOrWhiteSpace(faildMsgId)) {
                return;
            }
        }
        var strTargetId = obj.attr("id");
        $("div[id=__ServerMessagePanel][targetId=" + strTargetId + "],div[id=__ErrorMessagePanel][targetId=" + strTargetId + "],div[id=__RemindMessagePanel][targetId=" + strTargetId + "],div[id=__AllRightMessagePanel][targetId=" + strTargetId + "]").remove();
        obj.removeAttr("isValidateFaild"); //校验失败的标记
        obj.removeAttr("markedSuccess"); //标记成功的标记
        if (obj.attr("dependName")) {//去除依赖
            Validator.clearState($("[name=" + obj.attr("dependName") + "]"), clearCon);
        }
    },
    /**
    * 内部方法：显示指定控件的错误消息
    * @param serverMsg 如果为true，则显示serverMsg属性的消息
    */
    showFaildMsg: function (obj, clientValid, serverMsg) {
        if (obj.attr("isValidateFaild")) return; //如果该控件已经标记过错误消息，则不再重复
        Validator.clearState(obj, clientValid); //清除状态当前可能已经标记了成功的标记
        var faildMsgId = obj.attr("msgConId");
        var strId = obj.attr("id");
        var faildMsg = obj.attr("msg");
        if (serverMsg && obj.attr("serverMsg")) {
            faildMsg = obj.attr("serverMsg"); //当是服务器端校验发生错误时，默认显示服务器端消息
        }
        if (String.isNullOrWhiteSpace(faildMsg)) return; //没有增加属性，则不需要处理
        var span = $("<div id='__ErrorMessagePanel' class='error'></div>");
        span.text(faildMsg);
        span.attr("targetId", obj.attr("id")); //设置对象的目标ID
        if (faildMsgId && strId) {
            if (!clientValid) return; //如果不是客户端校验的话，不显示错误消息
            var faildMsgPanel = $("#" + faildMsgId);
            if (faildMsgPanel.length > 0) {
                $("div[id=__ErrorMessagePanel][targetId=" + strId + "]").remove();
                faildMsgPanel.append(span);
                faildMsgPanel.show();
                return;
            }
        }
        obj.attr("isValidateFaild", "true"); //标明控件校验出错
        obj.parent().parent().append(span);
    },
    /**
    * 内部方法：显示指定控件的正确消息
    * @param obj
    */
    showSuccessMsg: function (obj) {
        if (obj.attr("isValidateFaild")) return; //如果该控件已经标记过错误消息，则不再重复
        if (obj.attr("markedSuccess")) return; //如果已经标记过成功的，则不再需要处理
        var rigMsg = obj.attr("rig");
        if (String.isNullOrWhiteSpace(rigMsg)) return; //没有提示属性，则不需要处理
        obj.attr("markedSuccess", "true"); //标记
        var faildMsgId = obj.attr("msgConId");
        if (faildMsgId) return; //如果指定了容器，则不需要处理
        var img = $("<div id='__AllRightMessagePanel' class='allright'/>");
        img.attr("targetId", obj.attr("id")); //设置对象的目标ID
        obj.parent().parent().append(img);
    },
    /**
    * 外部调用显示指定对象的错误消息
    * @param obj 指定的对象
    * @param faildMsg 显示的消息
    * @param isTip 是否只是提示，如果是的话则不采用error
    */
    showMsg: function (obj, faildMsg, isTip) {
        if (obj && obj instanceof jQuery) {
        }
        else if (obj && typeof obj == "string") {
            obj = $("#" + obj);
        }
        else if (Object.isElement(obj)) {
            obj = $(con);
        }
        else {
            return; //对象未知不处理
        }
        Validator.clearState(obj, true); //清除状态当前可能已经标记了成功的标记
        var faildMsgId = obj.attr("msgConId");
        var strId = obj.attr("id");
        var span = $("<div id='__ServerMessagePanel' class='{0}'></div>".format(isTip ? "right" : "error")); //如果只是提示，则使用提示的标签，否则采用错误
        span.text(faildMsg);
        span.attr("targetId", obj.attr("id")); //设置对象的目标ID
        if (faildMsgId && strId) {
            var faildMsgPanel = $("#" + faildMsgId);
            if (faildMsgPanel.length > 0) {
                $("div[id=__ServerMessagePanel][targetId=" + strId + "]").remove();
                faildMsgPanel.append(span);
                faildMsgPanel.show();
                return;
            }
        }
        obj.parent().parent().append(span);
    },
    /**
    * 内部方法：清除指定对象的集中错误显示, 所有对象必须要有ID否则无效
    * @param objs 为空的话，则清除所有
    */
    clearMsgCon: function (objs) {
        objs.each(function () {
            var obj = $(this);
            var faildMsgId = obj.attr("msgConId");
            var strId = obj.attr("id");
            if (faildMsgId && strId) {
                $("#" + faildMsgId).hide();
                $("div[id=__ErrorMessagePanel][targetId=" + strId + "],div[id=__RemindMessagePanel][targetId=" + strId + "]").remove();
            }
        });
    },
    /**
    * 转到错误的位置
    */
    scrollToError: function () {
        var errMsgObj = $("#__ErrorMessagePanel");
        if (errMsgObj.length == 0) return;
        var offsettop = errMsgObj.offset();
        window.scrollTo(0, offsettop.top - 100);
    },
    /**
    * 内部方法：实现placeholder控制
    */
    placeholder: function () {
        var obj = $(this);
        var strPlaceholder = obj.attr("placeholder");
        if (strPlaceholder.trim().length == 0)
            return;
        var objDiv = obj.parent();
        objDiv.removeClass("show-text,focus-text,hide-text");
        //获取提示
        var objTips = objDiv.find(".text-tips");
        if (objTips.length == 0) {
            var labObj = $("<label class=\"text-tips\"></label>");
            labObj.text(strPlaceholder);
            labObj.insertBefore(obj);
            labObj.click(function () { obj.focus(); });
        }
        var strVal = obj.val();
        //如果当前控件没有值的时候，则使用显示层
        if (strVal.trim().length == 0) {
            if ($("*:focus") == obj) {//当当前控件为焦点控件时
                objDiv.addClass("focus-text");
            } else {
                objDiv.addClass("show-text");
            }
        } else {
            objDiv.addClass("hide-text");
        }
        obj.unbind("focus", Validator.holderFocus).bind("focus", Validator.holderFocus);
        obj.unbind("keyup", Validator.holderFocus).bind("keyup", Validator.holderFocus);
        obj.unbind("blur", Validator.holderBlur).bind("blur", Validator.holderBlur);
    },
    /**
    * 内部方法：当控件获取焦点的时候，实现placeholder 改变颜色的控制
    */
    holderFocus: function () {
        var obj = $(this);
        var objDiv = obj.parent();
        objDiv.removeClass("show-text");
        objDiv.removeClass("focus-text");
        objDiv.removeClass("hide-text");
        var strVal = obj.val();
        //如果当前控件没有值的时候，则使用显示层
        if (strVal.trim().length == 0) {
            objDiv.addClass("focus-text");
        } else {
            objDiv.addClass("hide-text");
        }
    },
    /**
    * 内部方法：当控件焦点移开的时候，实现placeholder 的控制
    */
    holderBlur: function () {
        var obj = $(this);
        var objDiv = obj.parent();
        objDiv.removeClass("show-text");
        objDiv.removeClass("focus-text");
        objDiv.removeClass("hide-text");
        var strVal = obj.val();
        //如果当前控件没有值的时候，则使用显示层
        if (strVal.trim().length == 0) {
            objDiv.addClass("show-text");
        } else {
            objDiv.addClass("hide-text");
        }
    },

    /**
    * 身份证验证
    */
    isIdCard: function (idcard) {
        idcard = idcard.toUpperCase(); // 对身份证号码做处理               
        var area = {
            11: "北京", 12: "天津", 13: "河北", 14: "山西", 15: "内蒙古", 21: "辽宁", 22: "吉林", 23: "黑龙江", 31: "上海",
            32: "江苏", 33: "浙江", 34: "安徽", 35: "福建", 36: "江西", 37: "山东", 41: "河南", 42: "湖北", 43: "湖南", 44: "广东",
            45: "广西", 46: "海南", 50: "重庆", 51: "四川", 52: "贵州", 53: "云南", 54: "西藏", 61: "陕西", 62: "甘肃", 63: "青海",
            64: "宁夏", 65: "新疆", 71: "台湾", 81: "香港", 82: "澳门", 91: "国外"
        };
        var Y, JYM;
        var S, M;
        /*基本校验*/
        if (String.isNullOrEmpty(idcard)) return false;
        var idcard_array = new Array();
        idcard_array = idcard.split("");
        /*地区检验*/
        if (area[parseInt(idcard.substr(0, 2))] == null) return false;

        /*身份号码位数及格式检验*/
        switch (idcard.length) {
            case 15:
                if ((parseInt(idcard.substr(6, 2)) + 1900) % 4 == 0 || ((parseInt(idcard.substr(6, 2)) + 1900) % 100 == 0 && (parseInt(idcard.substr(6, 2)) + 1900) % 4 == 0)) {
                    ereg = /^[1-9][0-9]{5}[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|[1-2][0-9]))[0-9]{3}$/; //测试出生日期的合法性
                } else {
                    ereg = /^[1-9][0-9]{5}[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|1[0-9]|2[0-8]))[0-9]{3}$/; //测试出生日期的合法性
                }
                if (ereg.test(idcard)) return true; //15位验证通过
                else return false;
                break;
            case 18:
                //18位身份号码检测
                //出生日期的合法性检查
                //闰年月日:((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|[1-2][0-9]))
                //平年月日:((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|1[0-9]|2[0-8]))
                if (parseInt(idcard.substr(6, 4)) % 4 == 0 || (parseInt(idcard.substr(6, 4)) % 100 == 0 && parseInt(idcard.substr(6, 4)) % 4 == 0)) {
                    ereg = /^[1-9][0-9]{5}19[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|[1-2][0-9]))[0-9]{3}[0-9XxAa]$/; //闰年出生日期的合法性正则表达式
                } else {
                    ereg = /^[1-9][0-9]{5}19[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|1[0-9]|2[0-8]))[0-9]{3}[0-9XxAa]$/; //平年出生日期的合法性正则表达式
                }
                if (ereg.test(idcard)) {//测试出生日期的合法性
                    //计算校验位
                    S = (parseInt(idcard_array[0]) + parseInt(idcard_array[10])) * 7
						+ (parseInt(idcard_array[1]) + parseInt(idcard_array[11])) * 9
						+ (parseInt(idcard_array[2]) + parseInt(idcard_array[12])) * 10
						+ (parseInt(idcard_array[3]) + parseInt(idcard_array[13])) * 5
						+ (parseInt(idcard_array[4]) + parseInt(idcard_array[14])) * 8
						+ (parseInt(idcard_array[5]) + parseInt(idcard_array[15])) * 4
						+ (parseInt(idcard_array[6]) + parseInt(idcard_array[16])) * 2
						+ parseInt(idcard_array[7]) * 1
						+ parseInt(idcard_array[8]) * 6
						+ parseInt(idcard_array[9]) * 3;
                    Y = S % 11;
                    M = "F";
                    JYM = "10X98765432";
                    M = JYM.substr(Y, 1); /*判断校验位*/
                    if (M == idcard_array[17]) {  /*检测ID的校验位false;*/
                        return true;
                    } else if (idcard_array[17] == 'A') {//A结尾不校验规则
                        return true; /*检测ID的校验位false;*/
                    }
                    else {
                        return false;
                    }
                }
                else {
                    return false;
                }
                break;
            default:
                return false;
        }
    },
    limit: function (len, min, max) {
        min = min || 0;
        max = max || Number.MAX_VALUE;
        return min <= len && len <= max;
    },
    limitNum: function (input, min, max) {
        min = min || 0;
        max = max || Number.MAX_VALUE;
        var patrn = "^[0-9]{" + min + "," + max + "}$";
        return new RegExp(patrn, "i").test(input);
    },
    lenB: function (str) {
        return str.replace(/[^\x00-\xff]/g, "**").length;
    },
    exec: function (op, reg) {
        return new RegExp(reg, "g").test(op);
    },
    compare: function (op1, operator, op2) {
        op1 = parseFloat(op1);
        op2 = parseFloat(op2);
        if (isNaN(op1)) {
            op1 = 1;
        }
        switch (operator) {
            case "NotEqual":
                return (op1 != op2);
            case "GreaterThan":
                return (op1 > op2);
            case "GreaterThanEqual":
                return (op1 >= op2);
            case "LessThan":
                return (op1 < op2);
            case "LessThanEqual":
                return (op1 <= op2);
            default:
                return (op1 == op2);
        }
    },
    mustChecked: function (name, min, max) {
        var groups = document.getElementsByName(name);
        var hasChecked = 0;
        min = min || 1;
        max = max || groups.length;
        for (var i = groups.length - 1; i >= 0; i--)
            if (groups[i].checked) hasChecked++;
        return min <= hasChecked && hasChecked <= max;
    },
    doFilter: function (input, filter) {
        return new RegExp("^.+\.(?=EXT)(EXT)$".replace(/EXT/g, filter.split(/\s*,\s*/).join("|")), "gi").test(input);
    },
    /**
    * 指定字符串是否是日期
    * @param op
    * @param formatString
    * @returns {Boolean}
    */
    isDate: function (op, min, formatString) {
        formatString = formatString || "ymd";
        var m, year, month, day;
        switch (formatString) {
            case "dmy":
                m = op.match(new RegExp("^(\\d{1,2})([-./])(\\d{1,2})\\2((\\d{4})|(\\d{2}))$"));
                if (m == null) return false;
                day = m[1];
                month = m[3] * 1;
                year = (m[5].length == 4) ? m[5] : (parseInt(m[6], 10) + 1900);
                break;
            default:
                m = op.match(new RegExp("^((\\d{4})|(\\d{2}))([-./])(\\d{1,2})\\4(\\d{1,2})$"));
                if (m == null) return false;
                day = m[6];
                month = m[5] * 1;
                year = (m[2].length == 4) ? m[2] : (parseInt(m[3], 10) + 1900);
                break;
        }
        if (!parseInt(month)) return false;
        month = month == 0 ? 12 : month;
        var date = new Date(year, month - 1, day);
        var isSuccess = (typeof (date) == "object" && year == date.getFullYear() && month == (date.getMonth() + 1) && day == date.getDate());
        if (isSuccess && !String.isNullOrWhiteSpace(min)) {
            var minDate = Date.fromFormat(min);
            isSuccess = minDate <= date;
        }
        return isSuccess;
    },
    /**
    * 执行各项服务器校验项
    */
    serverValidate: function (obj, validName) {
        switch (validName) {
            case "UniqueMobile":
                //唯一手机号校验
                var jsonData = $.postJsonSync("/safety/checkmobile.html", { mobile: obj.val() });
                return !jsonData.success;
            case "UniqueEmail":
                //唯一邮箱校验
                var jsonData = $.postJsonSync("/safety/checkemail.html", { email: obj.val() });
                return !jsonData.success;
            case "UniqueAccount":
                //唯一帐号
                var jsonData = $.postJsonSync("/common/checkusername", { userName: obj.val() });
                return !jsonData.success;
            case "UniqueIdCard":
                //唯一身份证号
                var jsonData = $.postJsonSync("/safety/checkidcardno.html", { card_no: obj.val() });
                return !jsonData.success;
            case "UniqueBusinessLicenceNo":
                //唯一营业执照号
                var jsonData = $.postJsonSync("/receipt/checkbusinesslicenceno.html", { no: obj.val() });
                return !jsonData.success;
            case "smsvc":
                //短信验证码
                // true表示验证码正确
                var jsonData = $.postJsonSync("/auth/checksmscode.html", { mobile: $("[name=" + obj.attr("dependName") + "]").val(), code: obj.val() });
                return jsonData.success;
            default:
                break;
        }
        return true;
    }
};
//页面加载完成后自动绑定一次
$(Validator.bind);