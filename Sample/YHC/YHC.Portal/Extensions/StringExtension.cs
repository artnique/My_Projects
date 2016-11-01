namespace YHC.Portal.Extensions
{
    using System;
    using System.Diagnostics;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Collections;
    using System.Collections.Generic;
    using YHC.Portal.Helper;

    public static class StringExtension
    {
        private static readonly Regex WebUrlExpression = new Regex(@"(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?",  RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex EmailExpression = new Regex(@"^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$", RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex StripHTMLExpression = new Regex("<\\S[^><]*>", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled);

        private static readonly char[] IllegalUrlCharacters = new[] { ';', '/', '\\', '?', ':', '@', '&', '=', '+', '$', ',', '<', '>', '#', '%', '.', '!', '*', '\'', '"', '(', ')', '[', ']', '{', '}', '|', '^', '`', '~', '–', '‘', '’', '“', '”', '»', '«' };

        [DebuggerStepThrough]
        public static bool IsWebUrl(this string target)
        {
            return !string.IsNullOrEmpty(target) && WebUrlExpression.IsMatch(target);
        }

        [DebuggerStepThrough]
        public static bool IsEmail(this string target)
        {
            return !string.IsNullOrEmpty(target) && EmailExpression.IsMatch(target);
        }

        [DebuggerStepThrough]
        public static string NullSafe(this string target)
        {
            return (target ?? string.Empty).Trim();
        }

        [DebuggerStepThrough]
        public static string FormatWith(this string target, params object[] args)
        {
            Check.Argument.IsNotEmpty(target, "target");

            return string.Format(Constants.CurrentCulture, target, args);
        }

        [DebuggerStepThrough]
        public static string Hash(this string target)
        {
            Check.Argument.IsNotEmpty(target, "target");

            using (MD5 md5 = MD5.Create())
            {
                byte[] data = Encoding.Unicode.GetBytes(target);
                byte[] hash = md5.ComputeHash(data);

                return Convert.ToBase64String(hash);
            }
        }

        [DebuggerStepThrough]
        public static string ToMD5(this string target)
        {
            MD5 m = new MD5CryptoServiceProvider();
            byte[] s = m.ComputeHash(UnicodeEncoding.UTF8.GetBytes(target));
            return BitConverter.ToString(s).Replace("-", "");
        }

        [DebuggerStepThrough]
        public static string ToMD5(this string target, String codepage)
        {
            MD5 m = new MD5CryptoServiceProvider();
            byte[] s = m.ComputeHash(UnicodeEncoding.GetEncoding(codepage).GetBytes(target));
            return BitConverter.ToString(s).Replace("-", "");
        }

        [DebuggerStepThrough]
        public static string ToBase64(this string target, Encoding encode)
        {
            string result = "";
            byte[] bytes = encode.GetBytes(target);
            try
            {
                result = Convert.ToBase64String(bytes);
            }
            catch
            {
                result = target;
            }
            return result;
        }

        public static string FromBase64(this string target, Encoding encode)
        {
            string result = "";
            // 将2进制编码转换为8位无符号整数数组. 
            byte[] bytes = Convert.FromBase64String(target);
            try
            {
                // 将指定字节数组中的一个字节序列解码为一个字符串。 
                result = encode.GetString(bytes);
            }
            catch
            {
                result = target;
            }
            return result;
        }

        /// <summary>
        /// index需大于3
        /// </summary>
        /// <param name="target"></param>
        /// <param name="index"></param>
        /// <returns></returns>

        [DebuggerStepThrough]
        public static string WrapAt(this string target, int index)
        {
            if (string.IsNullOrWhiteSpace(target)) {
                return string.Empty;
            }
            const int DotCount = 3;
            
            if (index <= DotCount)
            {
                throw new ArgumentOutOfRangeException("index不能小于3");
            }

            Check.Argument.IsNotEmpty(target, "target");
            Check.Argument.IsNotNegativeOrZero(index, "index");

            return (target.Length <= index) ? target : string.Concat(target.Substring(0, index - DotCount), new string('.', DotCount));
        }

        [DebuggerStepThrough]
        public static string StripHtml(this string target)
        {
            return StripHTMLExpression.Replace(target, string.Empty);
        }

        [DebuggerStepThrough]
        public static string ToInParam(this string target)
        {
            return target.Replace(" ", "").TrimStart(',').TrimEnd(',').Replace(new String[2] { ",", "，" }, "','");
        }

        [DebuggerStepThrough]
        public static string[] ToArrayStr(this string target)
        {
            return target.Replace(" ", "").Replace(new String[2] { ",", "，" }, "','").TrimStart(',').TrimEnd(',').Split(',');
        }
        /// <summary>
        /// 去掉空格，开始和结束的全角，半角 逗号，Split全角，半角 逗号
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string[] SplitComma(this string target)
        {
            return target.Replace(" ", "").Replace(new[] { ",", "，" }, ",").TrimStart(',').TrimEnd(',').Split(',');
        }


        [DebuggerStepThrough]
        public static Guid ToGuid(this string target)
        {
            Guid result = Guid.Empty;

            if ((!string.IsNullOrEmpty(target)) && (target.Trim().Length == 22))
            {
                string encoded = string.Concat(target.Trim().Replace("-", "+").Replace("_", "/"), "==");

                try
                {
                    byte[] base64 = Convert.FromBase64String(encoded);

                    result = new Guid(base64);
                }
                catch(FormatException)
                {
                }
            }

            return result;
        }

        [DebuggerStepThrough]
        public static T ToEnum<T>(this string target, T defaultValue) where T : IComparable, IFormattable
        {
            T convertedValue = defaultValue;

            if (!string.IsNullOrEmpty(target))
            {
                try
                {
                    convertedValue = (T) Enum.Parse(typeof(T), target.Trim(), true);
                }
                catch (ArgumentException)
                {
                }
            }

            return convertedValue;
        }

        [DebuggerStepThrough]
        public static string ToLegalUrl(this string target)
        {
            if (string.IsNullOrEmpty(target))
            {
                return target;
            }

            target = target.Trim();

            if (target.IndexOfAny(IllegalUrlCharacters) > -1)
            {
                foreach (char character in IllegalUrlCharacters)
                {
                    target = target.Replace(character.ToString(Constants.CurrentCulture), string.Empty);
                }
            }

            target = target.Replace(" ", "-");

            while (target.Contains("--"))
            {
                target = target.Replace("--", "-");
            }

            return target;
        }

        [DebuggerStepThrough]
        public static string UrlEncode(this string target)
        {
            return HttpUtility.UrlEncode(target);
        }

        [DebuggerStepThrough]
        public static string UrlDecode(this string target)
        {
            return HttpUtility.UrlDecode(target);
        }

        [DebuggerStepThrough]
        public static string AttributeEncode(this string target)
        {
            return HttpUtility.HtmlAttributeEncode(target);
        }

        [DebuggerStepThrough]
        public static string HtmlEncode(this string target)
        {
            return HttpUtility.HtmlEncode(target);
        }

        [DebuggerStepThrough]
        public static string HtmlDecode(this string target)
        {
            return HttpUtility.HtmlDecode(target);
        }

        [DebuggerStepThrough]
        public static string Replace(this string target, ICollection<string> oldValues, string newValue)
        {
            oldValues.ForEach(oldValue => target = target.Replace(oldValue, newValue));
            return target;
        }

        /// <summary>
        /// 替换HTML标签中的尖括号
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string ReplaceHtmlTag(this string target)
        {
            return target.Replace("<", "&lt;").Replace(">", "&gt;");
        }

        /// <summary>
        /// 还原HTML标签中的尖括号
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string RestoreHtmlTag(this string target)
        {
            return target.Replace("&lt;", "<").Replace("&gt;", ">");
        }

        /// <summary>
        /// 替换特殊的HTML标签
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string ReplaceScriptTag(this string target)
        {
            return target.Replace("<script", "&lt;")
                .Replace("</script>", "&lt;/script&gt;");
        }

        /// <summary>
        /// 替换\r\n为br标签
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string ReplaceLineChar(this string target)
        {
            return target.Replace(new string[] { @"\r\n", @"\n" }, "<br />");
        }

        /// <summary>
        /// 中文为2个长度
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Int32 LengthOfChs(this string target)
        {
            byte[] byteStr = Encoding.GetEncoding("big5").GetBytes(target);
            return byteStr.Length;
        }

        /// <summary>
        /// 处理银行卡号，格式：**1221
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static String BankCardNoAsteriskText(this string target, Int32 hideLen = 2)
        {
            if (string.IsNullOrWhiteSpace(target))
            {
                return "";
            }
            if (target.Length <= 4)
            {
                return target;
            }
            String str = "";
            while (str.Length < hideLen)
            {
                str += "*";
            }
            return str + target.Substring(target.Length - 4);
        }

        ///// <summary>
        ///// 处理银行卡号，格式：**1221
        ///// </summary>
        ///// <param name="target"></param>
        ///// <param name="showLength">显示长度</param>
        ///// <returns></returns>
        //[DebuggerStepThrough]
        //public static string BankCardNoShowText(this string target, int showLength = 4)
        //{
        //    if (string.IsNullOrWhiteSpace(target))
        //    {
        //        return "";
        //    }
        //    if (target.Length <= 4)
        //    {
        //        return target;
        //    }
        //    return target.Substring(target.Length - 4);
        //}

        /// <summary>
        /// 格式化银行卡号，例如：2334 2332 2334 232
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static String BankFormat(this string target)
        {
            if (string.IsNullOrWhiteSpace(target) || target.Length < 4)
            {
                return target;
            }

            var cardNo = target;
            cardNo = cardNo.Replace(" ", "");
            var newCardNo = cardNo.Substring(0, 4);
            cardNo = cardNo.Substring(4);
            while (string.IsNullOrWhiteSpace(cardNo) == false && cardNo.Length > 0)
            {
                int len = cardNo.Length;
                if (len >= 4)
                {
                    len = 4;
                }

                newCardNo += " " + cardNo.Substring(0, len);

                if (len < 4)
                {
                    break;
                }

                cardNo = cardNo.Substring(4);
            }

            return newCardNo;
        }

        /// <summary>
        /// 处理邮箱，格式：apo***@qq.com
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static String EmailAsteriskText(this string target)
        {
            if (string.IsNullOrWhiteSpace(target))
            {
                return "";
            }
            if (target.IsEmail() == false)
            {
                return target;
            }
            String[] body = target.Split('@');

            String processName = string.Empty;
            String name = body[0];
            Int32 nameLen = name.Length;

            if (nameLen == 1)
            {
                processName = name + "******";
            }
            else if (nameLen == 2)
            {
                processName = name + "*****";
            }
            else if (nameLen == 3)
            {
                processName = name + "****";
            }
            else
            {
                processName = name.Substring(0, 3) + "****";
            }

            return processName + "@" + body[1];
        }

        /// <summary>
        /// 返回yyyy-MM-dd HH:mm:ss格式的日期
        /// </summary>
        /// <param name="target">日期格式字符串，yyyyMMddHHmmss</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static DateTime ToDateTimeOfLongFormat(this string target)
        {
            return DateTime.ParseExact(target, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// 格式化手机号，135****1234
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static String MobileMask(this string target)
        {
            if (string.IsNullOrEmpty(target)) return target;
            Regex re = new Regex("(\\d{3})(\\d{4})(\\d{4})", RegexOptions.None);
            return re.Replace(target, "$1****$3");

        }

        /// <summary>
        /// 给字符加*号
        /// </summary>
        /// <param name="target"></param>
        /// <param name="maskLenth"></param>
        /// <returns></returns>
        public static String MaskStart(this string target, Int32 maskLenth)
        {
            if (string.IsNullOrEmpty(target)) return target;
            Int32 strLength = target.Length;
            if (strLength <= maskLenth)
            {
                var str = "";
                for (var i = 0; i < strLength; i++)
                {
                    str += "*";
                }

                return str;
            }

            Int32 index = 0;
            Char[] result = new Char[strLength];
            foreach (char c in target)
            {
                Char newC;
                if (index < maskLenth)
                {
                    newC = '*';
                }
                else
                {
                    newC = c;
                }
                result[index] = newC;

                index++;
            }

            return new String(result);
        }

        /// <summary>
        /// 后面加*号
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static String MaskEndForUserName(this string target)
        {
            if (string.IsNullOrWhiteSpace(target))
            {
                return string.Empty;
            }

            if (target.Length > 4)
            {
                var a = target.Substring(0, 3);
                var b = target.Substring(3);
                var c = "";
                for (var i = 0; i < b.Length; i++)
                {
                    c += "*";
                }
                return a + c;
            }
            else if (target.Length == 2)
            {
                return target.Substring(0, 1) + "*";
            }
            else if (target.Length == 3)
            {
                return target.Substring(0, 2) + "*";
            }
            else if (target.Length == 4)
            {
                return target.Substring(0, 2) + "**";
            }
            else if (target.Length == 1)
            {
                return "*";
            }
            else
            {
                return "";
            }
        }

        public static String MaskForEmail(this string target)
        {
            if (string.IsNullOrWhiteSpace(target))
            {
                return string.Empty;
            }

            string[] email = target.Split('@');
            target = email[0];
            if (target.Length > 4)
            {
                var a = target.Substring(0, 3);
                var b = target.Substring(3);
                var c = "";
                for (var i = 0; i < b.Length; i++)
                {
                    c += "*";
                }
                return a + c+ "@" + email[1];
            }
            else if (target.Length == 2)
            {
                return target.Substring(0, 1) + "*@" + email[1];
            }
            else if (target.Length == 3)
            {
                return target.Substring(0, 2) + "*@" + email[1];
            }
            else if (target.Length == 4)
            {
                return target.Substring(0, 2) + "**@" + email[1];
            }
            else if (target.Length == 1)
            {
                return "*@" + email[1];
            }
            else
            {
                return "" + email[1];
            }
        }

        /// <summary>
        /// 移除指定前缀
        /// </summary>
        /// <param name="target"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static String RemovePrefix(this string target, String prefix)
        {
            String removePrefix = target;
            if (string.IsNullOrWhiteSpace(prefix) == false)
            {
                Int32 prefixLen = prefix.Length;
                Int32 userLen = target.Length;
                if (prefixLen >= userLen)
                {
                    return removePrefix;
                }

                String u = target.Substring(0, prefixLen);

                // 判断用户名前缀是否相同，不区分大小写
                if (u.Equals(prefix, StringComparison.InvariantCultureIgnoreCase))
                {
                    removePrefix = removePrefix.Remove(0, prefixLen);
                }
            }

            return removePrefix;
        }


        #region 字段加密与解密

        /// <summary>
        /// 数据加密
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static String DataEncrypt(this string target)
        {
            if (string.IsNullOrWhiteSpace(target)) return string.Empty;

            string str = "";
            for (int i = 0; i < target.ToCharArray().Length; i++)
            {
                char c = target.ToCharArray()[i];
                int keyLen = _getKey();
                str = str + ((char)(c + keyLen));
            }
            return str;
        }

        /// <summary>
        /// 数据解密
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static String DataDecrypt(this string target)
        {
            if (string.IsNullOrWhiteSpace(target)) return string.Empty;

            string str = "";
            for (int i = 0; i < target.ToCharArray().Length; i++)
            {
                char c = target.ToCharArray()[i];
                int keyLen = _getKey();
                str = str + ((char)(c - keyLen));
            }
            return str;
        }

        private static int _getKey()
        {
            String key = ConfigHelper.TablePrefix;
            
            int num = 0;
            for (int i = 0; i < key.ToCharArray().Length; i++)
            {
                num += key.ToCharArray()[i];
            }
            return num;
        }

        #endregion


        #region 加密与解密

        private static byte[] encryptKey = Encoding.ASCII.GetBytes(ConfigHelper.EKey);
        private static byte[] encryptIv = Encoding.ASCII.GetBytes(ConfigHelper.EIv);

        #region DES 加密

        /// <summary>
        /// 进行DES加密
        /// </summary>
        /// <param name="target">要加密的字符串。</param>
        /// <returns>以Base64格式返回的加密字符串。</returns>
        [DebuggerStepThrough]
        public static string DESEncrypt(this string target)
        {
            if (string.IsNullOrEmpty(target))
                return "";

            try
            {
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    byte[] inputByteArray = Encoding.UTF8.GetBytes(target);
                    des.Key = encryptKey;
                    des.IV = encryptIv;
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                        cs.Close();
                    }
                    string str = Convert.ToBase64String(ms.ToArray());
                    ms.Close();
                    return str;
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion

        #region DES 解密

        /// <summary>
        /// 进行DES解密。
        /// </summary>
        /// <param name="pToDecrypt">要解密的以Base64</param>
        /// <returns>已解密的字符串。</returns>
        [DebuggerStepThrough]
        public static string DESDecrypt(this string target)
        {
            if (string.IsNullOrEmpty(target))
                return "";

            try
            {
                byte[] inputByteArray = Convert.FromBase64String(target);
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    des.Key = encryptKey;
                    des.IV = encryptIv;
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                        cs.Close();
                    }
                    string str = Encoding.UTF8.GetString(ms.ToArray());
                    ms.Close();
                    return str;
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion

        #endregion

        #region AsiaPay DES加密与解密

        //static string key = "Ki3CgDAz";

        /// <summary>
        /// 对字符串进行加密
        /// </summary>
        /// <param name="strText">被加密的字符串</param>
        /// <param name="strEncrKey">加密时使用的密钥(密钥长度只能是8位)</param>
        /// <returns>加密码后的字符串</returns>
        public static string AsiaDesEncrypt(this string strText, string strEncrKey = "Ki3CgDAz")
        {
            if (strEncrKey.Length < 8 || string.IsNullOrEmpty(strText))
            {
                throw new Exception("加密key小于8或者加密字符串为空！");
            }
            byte[] bKey = Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
            byte[] bIV = Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
            byte[] bStr = Encoding.UTF8.GetBytes(strText);
            try
            {
                DESCryptoServiceProvider desc = new DESCryptoServiceProvider();
                desc.Padding = PaddingMode.PKCS7;//补位        
                desc.Mode = CipherMode.CBC;//CipherMode.CBC         
                using (System.IO.MemoryStream mStream = new System.IO.MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, desc.CreateEncryptor(bKey, bIV), CryptoStreamMode.Write))
                    {
                        cStream.Write(bStr, 0, bStr.Length);
                        cStream.FlushFinalBlock();
                        StringBuilder ret = new StringBuilder();
                        byte[] res = mStream.ToArray();
                        foreach (byte b in res)
                        {
                            ret.AppendFormat("{0:x2}", b);
                        }
                        return ret.ToString();
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 对字符串进行解密
        /// </summary>
        /// <param name="strText">被解密的字符串</param>
        /// <param name="sDecrKey">与加密时使用相同的密钥(密钥长度只能是8位)</param>
        /// <returns>解密后的字符串</returns>
        public static string AsiaDesDecrypt(this string strText, string sDecrKey = "Ki3CgDAz")
        {

            //    HttpContext.Current.Response.Write(pToDecrypt + "<br>" + sKey);  
            //    HttpContext.Current.Response.End();  
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                des.Mode = System.Security.Cryptography.CipherMode.CBC;
                byte[] inputByteArray = new byte[strText.Length / 2];
                for (int x = 0; x < strText.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(strText.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }
                des.Key = ASCIIEncoding.UTF8.GetBytes(sDecrKey);
                des.IV = ASCIIEncoding.UTF8.GetBytes(sDecrKey);
                // byte[] IV = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 };
                // des.IV = IV;
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                StringBuilder ret = new StringBuilder();
                return System.Text.Encoding.UTF8.GetString(ms.ToArray());
                //return HttpContext.Current.Server.UrlDecode(System.Text.Encoding.UTF8.GetString(ms.ToArray()));
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }

        [DebuggerStepThrough]
        public static string ToMD5ForAsia(this string target)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(target, "MD5");
        }

        #endregion

        /// <summary>
        /// HMACSHA1加密
        /// </summary>
        /// <param name="target"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static String HMACSHA1(this string target, string key)
        {
            HMACSHA1 hmacsha1 = new HMACSHA1();
            hmacsha1.Key = Encoding.UTF8.GetBytes(key);
            byte[] dataBuffer = Encoding.UTF8.GetBytes(target);
            byte[] hashBytes = hmacsha1.ComputeHash(dataBuffer);

            return Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// 获取url地址的域名部分
        /// </summary>
        /// <param name="target"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetDomainName(this string target, string url)
        {
            if (url == null)
            {
                return string.Empty;
            }
            Regex reg = new Regex(@"(?<=://)([\w-]+\.)+[\w-]+(?<=/?)");
            return reg.Match(url, 0).Value.Replace("/", string.Empty);
        }
    }
}