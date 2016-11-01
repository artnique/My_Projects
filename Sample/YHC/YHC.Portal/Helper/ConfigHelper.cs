using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections;
using YHC.Portal.Models;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using YHC.Portal.Services;

namespace YHC.Portal.Helper
{
    public static class ConfigHelper
    {
        private static string _memcachedServer;
        private static string _staticResourceVersion;
        private static string _imageServerUrl;
        private static string _cookieDomain;
        private static string _resourceUrl;
        private static string _imageServerPath;
        private static bool? _wcfUploadImg;
        private static string _cometServerUrl;
        private static HashSet<String> _registSaveKey;

        private static string _ekey;
        private static string _eiv;
        private static string _tablePrefix;
        private static bool? _trace;
        private static bool _isPrintExecuteUseTime = false;

        private static string _moblieSite;
        private static Int32? _memberLoginExpired;
        private static Int32? _gameDataRebuildIndexValue;

        private static HashSet<String> _ignoreLogFields = null;

        private static Uri _apiBaseAddress;
        public static Uri ApiBaseAddress
        {
            [DebuggerStepThrough]
            get
            {
                if (_apiBaseAddress == null)
                {
                    _apiBaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["ApiBaseAddress"]);
                }
                return _apiBaseAddress;
            }
        }

        /// <summary>
        /// 代理路径分隔符
        /// </summary>
        public static string AgentPathSplit
        {
            get { return "/"; }
        }

        /// <summary>
        /// Memcache服务器列表，格式：ip,port,weight#ip,port,weight，如：127.0.0.1,11211,1#127.0.0.1,11212,1
        /// </summary>
        public static string MemcachedServer
        {
            [DebuggerStepThrough]
            get
            {
                if (string.IsNullOrEmpty(_memcachedServer))
                {
                    _memcachedServer = System.Configuration.ConfigurationManager.AppSettings["MemcachedServer"];
                }
                return _memcachedServer;
            }
        }

        /// <summary>
        /// 静态资源版本号
        /// </summary>
        public static string StaticResourceVersion
        {
            [DebuggerStepThrough]
            get
            {
                if (string.IsNullOrEmpty(_staticResourceVersion))
                {
                    _staticResourceVersion = System.Configuration.ConfigurationManager.AppSettings["StaticResourceVersion"];
                }
                return _staticResourceVersion;
            }
        }

        /// <summary>
        /// 静态资源URL
        /// </summary>
        public static string ResourceUrl
        {
            [DebuggerStepThrough]
            get
            {
                if (string.IsNullOrEmpty(_resourceUrl))
                {
                    _resourceUrl = System.Configuration.ConfigurationManager.AppSettings["ResourceUrl"];
                    if (_resourceUrl.EndsWith("/") == false)
                    {
                        _resourceUrl += "/";
                    }
                }
                return _resourceUrl;
            }
        }

        /// <summary>
        /// 会员URL地址
        /// </summary>
        public static string CometServerUrl
        {
            [DebuggerStepThrough]
            get
            {
                if (string.IsNullOrEmpty(_cometServerUrl))
                {
                    _cometServerUrl = System.Configuration.ConfigurationManager.AppSettings["CometServerUrl"];
                    if (_cometServerUrl.EndsWith("/") == false)
                    {
                        _cometServerUrl += "/";
                    }
                }
                return _cometServerUrl;
            }
        }

        /// <summary>
        /// 移动端地址
        /// </summary>
        public static string MoblieSite
        {
            [DebuggerStepThrough]
            get
            {
                if (string.IsNullOrEmpty(_moblieSite))
                {
                    _moblieSite = System.Configuration.ConfigurationManager.AppSettings["MobileSite"];
                    if (_moblieSite.EndsWith("/") == false)
                    {
                        _moblieSite += "/";
                    }
                }
                return _moblieSite;
            }
        }

        /// <summary>
        /// 图片服务器URL地址，最后带/
        /// </summary>
        public static string ImageServerUrl
        {
            [DebuggerStepThrough]
            get
            {
                if (string.IsNullOrEmpty(_imageServerUrl))
                {
                    _imageServerUrl = System.Configuration.ConfigurationManager.AppSettings["ImageServerUrl"];
                    if (_imageServerUrl.EndsWith("/") == false)
                    {
                        _imageServerUrl += "/";
                    }
                }
                return _imageServerUrl;
            }
        }

        /// <summary>
        /// 网站CookieDomain
        /// </summary>
        public static string CookieDomain
        {
            [DebuggerStepThrough]
            get
            {
                if (string.IsNullOrEmpty(_cookieDomain))
                {
                    _cookieDomain = System.Configuration.ConfigurationManager.AppSettings["CookieDomain"];
                }
                return _cookieDomain;
            }
        }

        /// <summary>
        /// 是否开启wcf上传
        /// </summary>
        public static Boolean WcfUploadImg
        {
            [DebuggerStepThrough]
            get
            {
                if (_wcfUploadImg.HasValue == false)
                {
                    _wcfUploadImg = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["WcfUploadImg"]);
                }

                return _wcfUploadImg.Value;
            }
        }

        /// <summary>
        /// 图片服务器路径，例如：e:\imageserver\portal\
        /// 目前没有图片服务器，使用当前网站应用程序的根目录
        /// </summary>
        public static string ImageServerPath
        {
            [DebuggerStepThrough]
            get
            {
                if (string.IsNullOrEmpty(_imageServerPath))
                {
                    // 目前没有图片服务器，使用当前网站应用程序的根目录
                    _imageServerPath = System.Configuration.ConfigurationManager.AppSettings["ImageServerPath"];
                    if (_imageServerPath.EndsWith("\\") == false)
                    {
                        _imageServerPath += "\\";
                    }
                }
                return _imageServerPath;
            }
        }

        /// <summary>
        /// 注册保留关键字
        /// </summary>
        public static HashSet<String> RegistSaveKey
        {
            [DebuggerStepThrough]
            get
            {
                if (_registSaveKey == null)
                {
                    _registSaveKey = new HashSet<string>();

                    string key = System.Configuration.ConfigurationManager.AppSettings["RegistSaveKey"];
                    foreach (var item in key.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        _registSaveKey.Add(item.ToLower());
                    }
                }
                return _registSaveKey;
            }
        }

        /// <summary>
        /// 加密KEY
        /// </summary>
        public static String EKey
        {
            [DebuggerStepThrough]
            get
            {
                if (string.IsNullOrWhiteSpace(_ekey))
                {
                    _ekey = System.Configuration.ConfigurationManager.AppSettings["ekey"];
                }

                return _ekey;
            }
        }

        /// <summary>
        /// 加密IV
        /// </summary>
        public static String EIv
        {
            [DebuggerStepThrough]
            get
            {
                if (string.IsNullOrWhiteSpace(_eiv))
                {
                    _eiv = System.Configuration.ConfigurationManager.AppSettings["eiv"];
                }

                return _eiv;
            }
        }

        /// <summary>
        /// 表前缀，用于部分SQL语句，以及数据加密与解密KEY
        /// </summary>
        public static String TablePrefix
        {
            [DebuggerStepThrough]
            get
            {
                if (string.IsNullOrWhiteSpace(_tablePrefix))
                {
                    _tablePrefix = System.Configuration.ConfigurationManager.AppSettings["TablePrefix"];
                }

                return _tablePrefix;
            }
        }

        /// <summary>
        /// 是否启用Trace，true启用，false不启用
        /// </summary>
        public static Boolean Trace
        {
            [DebuggerStepThrough]
            get
            {
                if (_trace.HasValue == false)
                {
                    _trace = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["Trace"]);
                }

                return _trace.Value;
            }
        }

        /// <summary>
        /// 是否打印执行时间
        /// </summary>
        public static bool IsPrintExecuteUseTime
        {
            [DebuggerStepThrough]
            get
            {
                _isPrintExecuteUseTime = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["IsPrintExecuteUseTime"]);

                return _isPrintExecuteUseTime;
            }
        }

        private static Config _config;
        /// <summary>
        /// 数据库的配置
        /// </summary>
        private static Config Config
        {
            //[DebuggerStepThrough]
            get
            {
                if (_config == null)
                {
                    ConfigService configService = new ConfigService();
                    _config = configService.Get();
                }

                return _config;
            }
        }

        private static Int32? _timeZone;
        /// <summary>
        /// 相对于UTC0的时区(从数据库配置中读取的)，这里保存的是分钟
        /// </summary>
        public static Int32 TimeZone
        {
            [DebuggerStepThrough]
            get
            {
                if (!_timeZone.HasValue)
                {
                    _timeZone = Config.TimeZone;
                }

                return _timeZone.Value;
            }
        }

        private static string _timeZoneDisplayName;
        /// <summary>
        /// 相对于UTC0的时区显示名称(从数据库配置中读取的)
        /// </summary>
        public static string TimeZoneDisplayName
        {
            //[DebuggerStepThrough]
            get
            {
                if (string.IsNullOrEmpty(_timeZoneDisplayName))
                {
                    _timeZoneDisplayName = Config.TimeZoneDisplayName;
                }

                return _timeZoneDisplayName;
            }
        }

        private static string _agentSiteDomain;
        public static string AgentSiteDomain
        {
            //[DebuggerStepThrough]
            get
            {
                if (string.IsNullOrEmpty(_agentSiteDomain))
                {
                    _agentSiteDomain = Config.AgentSiteDomain;
                }

                return _agentSiteDomain;
            }
        }

        /// <summary>
        /// 会员登录过期时间，分钟
        /// </summary>
        public static Int32 MemberLoginExpired
        {
            [DebuggerStepThrough]
            get
            {
                if (_memberLoginExpired.HasValue == false)
                {
                    // 默认60分钟
                    _memberLoginExpired = 60;
                    if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("MemberLoginExpired"))
                    {
                        _memberLoginExpired = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["MemberLoginExpired"]);
                    }
                }

                return _memberLoginExpired.Value;
            }
        }

        /// <summary>
        /// 数据更新重新索引的伐值
        /// </summary>
        public static Int32 GameDataRebuildIndexValue
        {
            [DebuggerStepThrough]
            get
            {
                if (_gameDataRebuildIndexValue.HasValue == false)
                {
                    // 默认200次
                    _gameDataRebuildIndexValue = 200;
                    if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("GameDataRebuildIndexValue"))
                    {
                        _gameDataRebuildIndexValue = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["GameDataRebuildIndexValue"]);
                    }
                }

                return _gameDataRebuildIndexValue.Value;
            }
        }


        private static string _lastLoginTimeDays;
        /// <summary>
        /// 相对于UTC0的时区显示名称(从数据库配置中读取的)
        /// </summary>
        public static string LastLoginTimeDays
        {
            //[DebuggerStepThrough]
            get
            {
                if (string.IsNullOrEmpty(_lastLoginTimeDays))
                {
                    _lastLoginTimeDays = ConfigurationManager.AppSettings["LastLoginTimeDays"];
                }

                return _lastLoginTimeDays;
            }
        }

        
        private static int? _recommendedGameLimit;
        /// <summary>
        /// RecommendedGameLimit
        /// </summary>
        public static int RecommendedGameLimit
        {
            [DebuggerStepThrough]
            get
            {
                if (!_recommendedGameLimit.HasValue)
                {
                    _recommendedGameLimit = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["RecommendedGameLimit"]);
                }

                return _recommendedGameLimit.Value;
            }
        }

        private static int? _userTransferWarnTime;
        /// <summary>
        /// 会员转账预警时间间隔
        /// </summary>
        public static int UserTransferWarnTime
        {
            [DebuggerStepThrough]
            get
            {
                if (!_userTransferWarnTime.HasValue)
                {
                    _userTransferWarnTime = Convert.ToInt32(ConfigurationManager.AppSettings["UserTransferWarnTime"]);
                }

                return _userTransferWarnTime.Value;
            }
        }

        /// <summary>
        /// 操作日志忽略字段
        /// </summary>
        public static HashSet<String> IgnoreLogFields
        {
            get
            {
                if (_ignoreLogFields == null)
                {
                    // 都用小写
                    _ignoreLogFields = new HashSet<String>();
                    _ignoreLogFields.Add("password");
                    _ignoreLogFields.Add("oldpwd");
                    _ignoreLogFields.Add("newpwd");
                    _ignoreLogFields.Add("confirmpassword");
                    _ignoreLogFields.Add("newpassword");
                    _ignoreLogFields.Add("oldpassword");
                    _ignoreLogFields.Add("pwd");
                }

                return _ignoreLogFields;
            }
        }

        /// <summary>
        /// Memcache缓存KEY名称
        /// </summary>
        public class MemcacheKey
        {
            /// <summary>
            /// 将字符串改为强命名方式
            /// </summary>
            private static Dictionary<String, String> _keys = new Dictionary<String, String>()
            {
                {"SysConfigCache", "SysConfigCache"},   // 系统配置，前后台共用
                {"MemberOnline", "MemberOnline"},       // 在线会员，前台后共用
                {"OldConfigCacheKey", "OldConfigCacheKey"}, // 旧的系统配置，共用
                {"RolePermisssionCodes", "RolePermisssionCodes"}, // 权限缓存，管理后台使用
                {"RolePermisssionColumnCodes", "RolePermisssionColumnCodes" },
                {"GameLocal", "GameLocal" }, // 游戏记录，共用
                {"MemberUserName", "MemberUserName" }, // 会员用户名，共用
                {"MemberUserSessionId", "MemberUserSessionId" }, // 会员SessionId，共用
                {"ThirdPay", "ThirdPay"}, // 第三方支付缓存，共用
                {"SendSMS","SendSMS"}, //短信发送，公用
                {"GameDataAutoSyncCount", "GameDataAutoSyncCount"}, // 游戏数据更新次数
                {"T188Token", "T188Token"}, // T188体育token数据
                {"LB_KENO_Token", "LB_KENO_Token"}, // LB keno token数据
                {"LB_LOTTERY_Token", "LB_LOTTERY_Token"}, // LB lottery token数据
                {"Game_Token", "Game_Token"}, // game token数据，用于所有平台
            };

            public static String SysConfigCache
            {
                get
                {
                    return _keys["SysConfigCache"];
                }
            }

            public static String MemberOnline
            {
                get
                {
                    return _keys["MemberOnline"];
                }
            }

            public static String OldConfigCacheKey
            {
                get
                {
                    return _keys["OldConfigCacheKey"];
                }
            }

            public static String RolePermisssionCodes
            {
                get
                {
                    return _keys["RolePermisssionCodes"];
                }
            }

            public static String RolePermisssionColumnCodes
            {
                get
                {
                    return _keys["RolePermisssionColumnCodes"];
                }
            }

            public static String MemberUserName
            {
                get
                {
                    return _keys["MemberUserName"];
                }
            }

            public static String MemberUserSessionId
            {
                get
                {
                    return _keys["MemberUserSessionId"];
                }
            }

            /// <summary>
            /// 第三方支付缓存KEY
            /// </summary>
            public static String ThirdPay
            {
                get
                {
                    return _keys["ThirdPay"];
                }
            }

            public static String SendSMS
            {
                get
                {
                    return _keys["SendSMS"];
                }
            }

            public static String GameLocal
            {
                get
                {
                    return _keys["GameLocal"];
                }
            }

            public static String GameDataAutoSyncCount
            {
                get
                {
                    return _keys["GameDataAutoSyncCount"];
                }
            }

            /// <summary>
            /// T188 Token验证数据
            /// </summary>
            public static String T188Token
            {
                get
                {
                    return _keys["T188Token"];
                }
            }

            /// <summary>
            /// LB keno Token验证数据
            /// </summary>
            public static String LB_KENO_Token
            {
                get
                {
                    return _keys["LB_KENO_Token"];
                }
            }

            /// <summary>
            /// Game_Token验证数据
            /// </summary>
            public static String Game_Token
            {
                get
                {
                    return _keys["Game_Token"];
                }
            }

            /// <summary>
            /// LB lottery Token验证数据
            /// </summary>
            public static String LB_LOTTERY_Token
            {
                get
                {
                    return _keys["LB_LOTTERY_Token"];
                }
            }

            /// <summary>
            /// 游戏记录中的游戏中文名称缓存KEY
            /// </summary>
            /// <param name="gamePlatform"></param>
            /// <param name="gameType"></param>
            /// <param name="gameNameId"></param>
            /// <returns></returns>
            public static String GetGameLocal_ChsGameNameKey(String gamePlatform, String gameType, String gameNameId)
            {
                return string.Format("{0}::CHS::{1}::{2}::{3}", GameLocal, gamePlatform, gameType, gameNameId);
            }
        }

        #region wcf相关
        /// <summary>
        /// wcf相关
        /// </summary>
        public class WCF
        {
            private static string _wcfUserName;
            private static string _wcfPassword;

            /// <summary>
            /// wcf用户名
            /// </summary>
            public static string WcfUserName
            {
                [DebuggerStepThrough]
                get
                {
                    if (string.IsNullOrEmpty(_wcfUserName))
                    {
                        _wcfUserName = System.Configuration.ConfigurationManager.AppSettings["WcfUserName"];
                    }
                    return _wcfUserName;
                }
            }

            /// <summary>
            /// wcf密码
            /// </summary>
            public static string WcfPassword
            {
                [DebuggerStepThrough]
                get
                {
                    if (string.IsNullOrEmpty(_wcfPassword))
                    {
                        _wcfPassword = System.Configuration.ConfigurationManager.AppSettings["WcfPassword"];
                    }
                    return _wcfPassword;
                }
            }

        }
        #endregion
    }
}