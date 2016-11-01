var utility={api:'http://api.3728z.com/api/',apiTag:'',imageServer:'http://img.3728z.com/',cometServer:'http://comet.3728z.com/',liveChat:'http://f18.livechatvalue.com/chat/chatClient/chatbox.jsp?companyID=39999&configID=59823&jid=8848794622&lan=zh&subject=%E5%92%A8%E8%AF%A2&prechatinfoexist=1',WebServerTimeZoneInfo:function(){return utility.api+'Config/WebServerTimeZoneInfo'},GetBetTypeText_EN:function(type){if(type===1){return"casino";}
else if(type===2){return"lottery";}
else if(type===3){return"sport";}
else{return"slot";}},trim:function(value){if(value)
return value.replace(/(^[\\s]*)|([\\s]*$)/g,"");else
return'';},isEmpty:function(value){if(value=="null")
return true;if(typeof value=="undefined")
return true;switch(value){case"":case"[]":case 0:case"0":case null:case false:case typeof this=="undefined":return true;default:return false;}},setCookie:function(key,value){Cookies.set(key,value,{expires:10,path:'/'});},getCookie:function(key){return Cookies.get(key);},keyboardEvent:function(event){var keyCode=event.keyCode?event.keyCode:event.which?event.which:event.charCode;if(keyCode==13){$("#loginBtn").click();}},queryString:function(key){var params={};var e,a=/\+/g,r=/([^&=]+)=?([^&]*)/g,d=function(s){return decodeURIComponent(s.replace(a," "));},q=window.location.search.substring(1);while(e=r.exec(q)){params[d(e[1])]=d(e[2]);}
return params[key];},countdown:function(obj,m,s){$(obj).find('.m').html(m);$(obj).find('.s').html(s);showtime(obj);function showtime(obj){var t="";var m=$(obj).find('.m').html();var s=$(obj).find('.s').html();s=s-1;if(s==0){m=m-1;s=60}
if(m<1){$(obj).closest('li').find('.mask').remove();$(obj).remove();clearInterval(t);}
clearInterval(t);$(obj).find('.m').html(m);$(obj).find('.s').html(s);t=setInterval(showtime(obj),2000);}},withdrawCallback:function(){},mobilyselect:function(){$('.selecter').mobilyselect({collection:'all',animation:'absolute',duration:100,listClass:'selecterContent',btnsClass:'selecterBtns',btnActiveClass:'active',elements:'li',onChange:function(){},onComplete:function(){}});},popup_show:function(){$.showLoading();},popup_hide:function(){$.hideLoading();},formatCurrency:function(num){num=num.toString().replace(/\$|\,/g,'');if(isNaN(num))
num="0";sign=(num==(num=Math.abs(num)));num=Math.floor(num*100+0.50000000001);cents=num%100;num=Math.floor(num/100).toString();if(cents<10)
cents="0"+cents;for(var i=0;i<Math.floor((num.length-(1+i))/3);i++)
num=num.substring(0,num.length-(4*i+3))+','+num.substring(num.length-(4*i+3));return(((sign)?'':'-')+num+'.'+cents);},getCity:function(provinceId){var param={'provinceId':provinceId};$.when(utility.getData('Config/GetCityList',param)).done(function(data){if(!utility.isEmpty(data)){var html='';$.each(data,function(key,item){html+='<option value="'+item.Id+'">'+item.Name+'</option>';});$('#city').html(html);}})},loadGame:function(game,type,gameId,path,ptCallback){if(game=="188"){alert("敬请期待");return false;}
utility.popup_show();var gameWin=window.open("/loading.html",game);if(!gameWin){alert('请在浏览口中设置允许本网站的弹出式窗口');return;}
if(game=="IBC"){$.when(utility.getData('Game/GetGameUrl',{"gamePlatform":"IBC","gameType":"sport"})).done(function(url){utility.popup_hide();if(url){gameWin.location.href=url;}
else{gameWin.close();}
return false;})}
$.when(utility.getData('Account/GetLoginStatus')).done(function(status){if(status>0){utility.popup_hide();gameWin.close();alert('请先登录');return false;}
$.when(utility.getData('Game/GetGameUrlForLogin',{"gamePlatform":game,"gameType":type,"gameId":gameId,"customGameUrl":''})).done(function(url){utility.popup_hide();if(ptCallback){ptCallback(url,gameWin);return;}
if(!utility.isEmpty(url)&&url.StatusCode){gameWin.close();alert(url.Message);return;}
if(url){gameWin.location.href=url;}
else{gameWin.close();}
return false;}).fail(function(xhr,textStatus,errorThrown){alert(errorThrown);});});},today:function(beginDateId,endDateId){var today=(new Date());var t1=today.getFullYear()+"-"+GetFullDate(today.getMonth()+1)+"-"+GetFullDate(today.getDate());$(beginDateId).val(t1);$(endDateId).val(t1);},threeDay:function(beginDateId,endDateId){var today=(new Date());var t2=today.getFullYear()+"-"+GetFullDate(today.getMonth()+1)+"-"+GetFullDate(today.getDate())
today.setDate(today.getDate()-2);var t1=today.getFullYear()+"-"+GetFullDate(today.getMonth()+1)+"-"+GetFullDate(today.getDate());$(beginDateId).val(t1);$(endDateId).val(t2);},theWeek:function(beginDateId,endDateId){var now=new Date();var nowYear=now.getYear();nowYear+=(nowYear<2000)?1900:0;var nowMonth=now.getMonth();var nowDay=now.getDate();var nowDayOfWeek=now.getDay();nowDayOfWeek=nowDayOfWeek==0?7:nowDayOfWeek;nowDayOfWeek-=1;var weekStartDate=new Date(nowYear,nowMonth,nowDay-nowDayOfWeek).format("yyyy-MM-dd");var weekEndDate=new Date(nowYear,nowMonth,nowDay+(6-nowDayOfWeek)).format("yyyy-MM-dd");$(beginDateId).val(weekStartDate);$(endDateId).val(weekEndDate);},changeGamePrice:function(){$(".selecterContent li").each(function(){var mb=$(this).find(".rmb")
var number=parseInt(mb.attr("data-value"))
var random_number=utility.createRandom2(1,0,50)+"."+utility.createRandom2(1,0,99)
number+=parseFloat(random_number)
mb.attr("data-value",number)
mb.html("￥"+utility.formatCurrency(number))})
setTimeout("utility.changeGamePrice()",1000);},createRandom2:function(num,from,to){var arr=[];var json={};while(arr.length<num){var ranNum=Math.ceil(Math.random()*(to-from))+from;if(!json[ranNum]){json[ranNum]=1;arr.push(ranNum);}}
return arr;},toDateTime:function(datetime){try{if(datetime.constructor==Date||typeof(datetime)=="object")
return datetime;if(datetime.constructor==String||typeof(datetime)=="string"){datetime=new Date(datetime.replace(/-/g,"/"));if(isNaN(datetime))
return null;else
return datetime;}
else
return null;}
catch(ex){return null;}},dateAdd:function(interval,number,datetime){try{switch(interval.toUpperCase()){case"S":number=number*1000;break;case"M":number=number*60*1000
break;case"H":number=number*60*60*1000;break;case"D":number=number*24*60*60*1000;break;default:throw"Invalid parameter";break;}
var dt=this.toDateTime(datetime);dt=new Date(Date.parse(dt)+number);return dt;}
catch(ex){return datetime;}},getPostData:function(path,param){path=path||'';param=param||'';return utility.getData(path,param,'POST');},getData:function(path,param,type){var defer=$.Deferred();path=path||'';param=param||'';type=type||'GET';var url=utility.api+path;$.ajax({type:type,url:url,data:param,dataType:'json',xhrFields:{withCredentials:true},success:function(data){defer.resolve(data);},error:function(xhr,textStatus,errorThrown){defer.reject(xhr,textStatus,errorThrown);},statusCode:{400:function(){alert('400 status code! user error');},500:function(){alert('500 status code! server error');}}});return defer.promise();},}
String.prototype.format=function(){var args=arguments;return this.replace(/\{(\d+)\}/g,function(m,i){return args[i];});};function formatMoney(s,type){if(/[^0-9\.]/.test(s))
return"0";if(s==null||s=="")
return"0";s=s.toString().replace(/^(\d*)$/,"$1.");s=(s+"00").replace(/(\d*\.\d\d)\d*/,"$1");s=s.replace(".",",");var re=/(\d)(\d{3},)/;while(re.test(s))
s=s.replace(re,"$1,$2");s=s.replace(/,(\d\d)$/,".$1");if(type==0){var a=s.split(".");if(a[1]=="00"){s=a[0];}}
return s;}
function chagenAuthCode(obj){obj.src=utility.api+'AuthCode/CreateImageCode?r='+Math.random();}
Date.prototype.addSeconds=function(s){var lTime=this.getTime();lTime+=s*1000;var dtDate=new Date(lTime);return dtDate;};Date.prototype.addMinutes=function(m){return this.addSeconds(m*60);};Date.prototype.addHours=function(h){return this.addMinutes(h*60);};Date.prototype.addDays=function(d){return this.addHours(d*24);};Date.prototype.addMonths=function(m){var dtDate=new Date(this.getTime());dtDate.setMonth(dtDate.getMonth()+m);return dtDate;};Date.prototype.addYears=function(y){return this.addMonths(y*12);};Date.fromJson=function(jsondate){jsondate=(jsondate+"").replace("/Date(","").replace(")/","");var iCharIndex=jsondate.indexOf("+");if(iCharIndex<0)
iCharIndex=jsondate.indexOf("-");if(iCharIndex>=0)
jsondate=jsondate.substr(jsondate,iCharIndex);var date=new Date(parseInt(jsondate,10));var d=new Date();var localOffset=d.getTimezoneOffset();var localServerOffset=localOffset+WEBSERVERTIMEZONE-(ISDAYLIGHTSAVINGTIME?-60:0);date=date.addMinutes(localServerOffset);return date;};function formatMoney(s,type){if(/[^0-9\.]/.test(s))
return"0";if(s==null||s=="")
return"0";s=s.toString().replace(/^(\d*)$/,"$1.");s=(s+"00").replace(/(\d*\.\d\d)\d*/,"$1");s=s.replace(".",",");var re=/(\d)(\d{3},)/;while(re.test(s))
s=s.replace(re,"$1,$2");s=s.replace(/,(\d\d)$/,".$1");if(type==0){var a=s.split(".");if(a[1]=="00"){s=a[0];}}
return s;}
String.prototype.formatMoney=function(){if(/[^0-9\.]/.test(this))
return"0";if(this=="")
return"0";var s=this.toString().replace(/^(\d*)$/,"$1.");s=(s+"00").replace(/(\d*\.\d\d)\d*/,"$1");s=s.replace(".",",");var re=/(\d)(\d{3},)/;while(re.test(s))
s=s.replace(re,"$1,$2");s=s.replace(/,(\d\d)$/,".$1");return s;};Date.prototype.format=function(fmt){var o={"M+":this.getMonth()+1,"d+":this.getDate(),"h+":this.getHours(),"m+":this.getMinutes(),"s+":this.getSeconds(),"q+":Math.floor((this.getMonth()+3)/3),"S":this.getMilliseconds()};if(/(y+)/.test(fmt))
fmt=fmt.replace(RegExp.$1,(this.getFullYear()+"").substr(4-RegExp.$1.length));for(var k in o)
if(new RegExp("("+k+")").test(fmt))
fmt=fmt.replace(RegExp.$1,(RegExp.$1.length==1)?(o[k]):(("00"+o[k]).substr((""+o[k]).length)));return fmt;}
function showJackpot(ctrlId){$.support.cors=true;var $jackpots=$('#'+ctrlId);if($jackpots.html()==''||$jackpots.html()=='￥'){var url=$jackpots.attr('url')+'&q='+Math.floor(Math.random()*10000000);$.when(utility.getPostData('Game/GetJackpotsByUrl',{'url':url})).done(function(data){if(data){if(data.StatusCode){return;}
if(data.Data){var amount=data.Data;$jackpots.html(amount.formatMoney());setInterval(function(){if($jackpots==undefined||$jackpots.html()==undefined){return;}
var value=$jackpots.html();value=value.replace(/,/g,"");value=parseFloat(value)+1.11;$jackpots.html(formatMoney(value));},300);}}});}};function ptLoading(url,ptWin){$.when(utility.getData('Game/GetApi',{'gamePlatform':'PT'})).done(function(data){if(data&&data.ApiUrl2){$.when(utility.getData('Game/GetUserAccount',{'gamePlatform':'PT'})).done(function(data2){if(data2&&data2.Account&&data2.Password){utility.popup_hide();ptWin.document.open();ptWin.document.write('<!DOCTYPE html>');ptWin.document.write('<html>');ptWin.document.write('<head>');ptWin.document.write('<meta name="viewport" content="width=device-width" />');ptWin.document.write('<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />');ptWin.document.write('<title>正在加载游戏...</title>');ptWin.document.write('<script type="text/javascript" src="skin/js/tool/jquery-1.7.2.min.js?t=1601261028"></script>');ptWin.document.write('<script type="text/javascript" src="'+data.ApiUrl2+'"></script>');ptWin.document.write('</head>');ptWin.document.write('<body>');ptWin.document.write('<div>');ptWin.document.write('<span id="loading">请稍候，正在加载游戏....</span>');ptWin.document.write('<input type="hidden" id="username" name="username" value="'+data2.Account+'">');ptWin.document.write('<input type="password" id="password" name="password" value="'+data2.Password+'" style="display:none">');ptWin.document.write('<input type="hidden" id="gameUrl" name="gameUrl" value="'+url+'" />');ptWin.document.write('</div>');ptWin.document.write('<script type="text/javascript">');ptWin.document.write('iapiSetCallout("Login", calloutLogin);');ptWin.document.write('iapiSetCallout("Logout", calloutLogout);');ptWin.document.write('$(function () { login(1); });');ptWin.document.write('function login(realMode) { iapiLogin($("#username").val().toUpperCase(), $("#password").val(), realMode, "ch"); };');ptWin.document.write('function logout(allSessions, realMode) { iapiLogout(allSessions, realMode); };');ptWin.document.write('function calloutLogin(response) {');ptWin.document.write('var code = response.errorCode;');ptWin.document.write('if (code && code != 6) { alert("登录失败,错误码:" + code + "," + response.errorText).show(); return; }');ptWin.document.write('location.href = $("#gameUrl").val();');ptWin.document.write('};');ptWin.document.write('function calloutLogout(response) {');ptWin.document.write('if (response.errorCode) { alert("登录失败, " + response.errorCode); }');ptWin.document.write('else { }');ptWin.document.write('};');ptWin.document.write('</script>');ptWin.document.write('</body>');ptWin.document.write('</html>');ptWin.document.close();}
else{utility.popup_hide();if(data2&&data2.StatusCode){utility.popup_show(data2.Message);}
else{utility.popup_show('网络错误！');}}});}
else{utility.popup_hide();if(data&&data.StatusCode){utility.popup_show(data.Message);}
else{utility.popup_show('网络错误！');}}});}
var _jackpotInfoType={CASINOBASED:'2',CASINOSTOTAL:'4',GAMEBASED:'1',GAMEGROUPTOTAL:'5',GAMETOTAL:'3'};