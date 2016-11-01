using YHC.Portal.Core;
using YHC.Portal.Extensions;
using YHC.Portal.Helper;
using YHC.Portal.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace YHC.Portal.Services
{
    public class UserService : BaseService
    {
        public async Task<ApiResult<User>> GetLoginedUser()
        {
            var request = CreateRequest("api/account/GetLoginUser", HttpMethod.Get);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<User>();
        }

        public async Task<int> GetUserBankCount(string userName)
        {
            int result = 0;
            var request = CreateRequest("api/user/GetUserBankCount?userName=" + userName, HttpMethod.Get);
            var response = await Client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                result = JSSerializer.Deserialize<int>(responseText);
            }
            return result;
        }
        public async Task<int> GetUserBet(int userId, DateTime startTime, DateTime endTime, string gamePlatform, string sort, string order, int pageIndex, int pageSize)
        {
            int result = 0;
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("userId", userId.ToString());
            nvc.Add("startTime", startTime.ToString("yyyy-MM-dd 00:00:00"));
            nvc.Add("endTime", endTime.ToString("yyyy-MM-dd 00:00:00"));
            nvc.Add("gamePlatform", gamePlatform);
            nvc.Add("sort", sort);
            nvc.Add("order", order);
            nvc.Add("pageIndex", pageIndex.ToString());
            nvc.Add("pageSize", pageSize.ToString());

            String parms = UtilHelper.ConvertNameValueString(nvc);

            var request = CreateRequest("api/bet/GetUserBet", HttpMethod.Get);
            var response = await Client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                result = JSSerializer.Deserialize<int>(responseText);
            }
            return result;
        }

        #region 手机 邮箱验证

        public async Task<ApiResult<bool>> ValidatePhone(int userId, string phone, string phoneValidateCode)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("Id", userId.ToString());
            parm.Add("phone", phone);
            parm.Add("phoneValidateCode", phoneValidateCode);

            var request = PostRequest("api/User/ValidatePhone", parm);

            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<bool>();
        }

        public async Task<ApiResult<bool>> SendMobileValidateCode(String userName,String phone,string ip)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("userName", userName);
            parm.Add("phone", phone);
            parm.Add("ip", ip);

            var request = PostRequest("api/User/SendMobileValidateCode", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<bool>();
        }

        public async Task<ApiResult<bool>> ValidateEmail(int id, string email, string emailValidateCode)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("id", id.ToString());
            parm.Add("email", email);
            parm.Add("emailValidateCode", emailValidateCode);

            var request = PostRequest("api/User/ValidateEmail", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<bool>();
        }

        public async Task<ApiResult<bool>> SendEmailValidateCode(int userId, string email, string validateUrl, string contactUrl, string ip)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("userId", userId.ToString());
            parm.Add("email", email);
            parm.Add("validateUrl", validateUrl);
            parm.Add("contactUrl", contactUrl);
            parm.Add("ip", ip);

            var request = PostRequest("api/User/SendEmailValidateCode", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<bool>();
        }

        #endregion

        #region 站内信

        public async Task<ApiResult<PagerFrontViewModel<VUserMessage>>> GetMessageList(string userName, DateTime? beginTime, DateTime? endTime, Int32? status, Int32? pageIndex, Int32? pageSize)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("userName", userName);
            parm.Add("beginTime", beginTime.ToString());
            parm.Add("endTime", endTime.ToString());
            parm.Add("status", status.ToString());
            parm.Add("pageIndex", pageIndex.GetValueOrDefault(0).ToString());
            parm.Add("pageSize", pageSize.GetValueOrDefault(10).ToString());
            var request = GetRequest("api/user/GetMessageList", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<PagerFrontViewModel<VUserMessage>>();
        }

        public async Task<ApiResult<bool>> DeleteMessage(string id, int userId)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("id", id.ToString());
            parm.Add("userId", userId.ToString());

            var request = PostRequest("api/User/DeleteMessage", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<bool>();
        }

        public async Task<ApiResult<bool>> ReadMessage(string id, int userId)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("id", id.ToString());
            parm.Add("userId", userId.ToString());

            var request = PostRequest("api/User/ReadMessage", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<bool>();
        }

        #endregion

        public async Task<int> GetMessageCount(DateTime? beginTime, DateTime? endTime)
        {
            int result = 0;
            var request = GetRequest("api/user/GetMessageCount?startTime=" + beginTime + "&endTime=" + endTime);
            var response = await Client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                result = JSSerializer.Deserialize<int>(responseText);
            }
            return result;
        }

        public async Task<int> GetUnreadMessageCount(DateTime? beginTime, DateTime? endTime)
        {
            int result = 0;
            var request = GetRequest("api/user/GetUnreadMessageCount?startTime=" + beginTime + "&endTime=" + endTime);
            var response = await Client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                result = JSSerializer.Deserialize<int>(responseText);
            }
            return result;
        }

        public int GetUnreadMessagesCount(DateTime beginTime, DateTime endTime)
        {
            int result = 0; 
            var request = GetRequest("api/user/GetUnreadMessageCount?startTime=" + beginTime + "&endTime=" + endTime);
            var response = Client.SendAsync(request).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseText = response.Content.ReadAsStringAsync().Result;
                result = JSSerializer.Deserialize<int>(responseText);
            }
            return result;
        }

        public async Task<ApiResult<IList<VUserMessage>>> GetTopMessageList(int userId, int count)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("userId", userId.ToString());
            parm.Add("count", count.ToString());
            var request = GetRequest("api/user/GetTopMessageList", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<IList<VUserMessage>>();
        }

        public async Task<ApiResult<decimal>> GetUserCash(string userName)
        {
            var request = GetRequest("api/User/GetUserCash?userName=" + userName);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<decimal>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="status">初始状态0,申请状态1，发送状态2，取消状态3，拒绝状态4</param>
        /// <returns></returns>
        public async Task<ApiResult<decimal>> GetWalletCash(string userName,int status)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("userName", userName);
            parm.Add("status", status.ToString());
            var request = GetRequest("api/User/GetWalletCash",parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<decimal>();
        }

        public async Task<ApiResult<decimal>> GetGameCash(string game,string ip)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("gamePlatform", game);
            parm.Add("ip", ip);

            var request = GetRequest("api/Game/GetCash",parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<decimal>();
        }

        public async Task<ApiResult<decimal>> GetAllGameCash(string userName)
        {
            var request = GetRequest("api/Game/GetCashSum");
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<decimal>();
        }

        public async Task<ApiResult<IList<UserBank>>> GetUserBankList(string userName)
        {
            var request = GetRequest("api/User/GetUserBankList?userName=" + userName);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<IList<UserBank>>();
        }

        public async Task<ApiResult<IList<Promo>>> GetFirstDepositPromoList(string userName)
        {
            var request = GetRequest("api/Promo/GetFirstDepositPromoList?userName=" + userName);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<IList<Promo>>();
        }

        public async Task<ApiResult<IList<ThirdPay>>> GetUserThirdPay()
        {
            var request = GetRequest("api/Deposit/GetUserThirdPay");
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<IList<ThirdPay>>();
        }

        public async Task<ApiResult<IList<AdminBank>>> GetUserAdminBank()
        {
            var request = GetRequest("api/Deposit/GetUserAdminBank?type=1");
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<IList<AdminBank>>();
        }

        public async Task<ApiResult<IList<AdminBank>>> GetUserAdminBank(int type)
        {
            var request = GetRequest("api/Deposit/GetUserAdminBank?type=" + type);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<IList<AdminBank>>();
        }

        public async Task<ApiResult<bool>> UpdateWithdrawalPwd(int userId, string oldPwd, string newPwd)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("userId", userId.ToString());
            parm.Add("oldPwd", oldPwd);
            parm.Add("newPwd", newPwd);

            var request = PostRequest("api/User/UpdateWithdrawalPwd", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<bool>();
        }

        public async Task<ApiResult<bool>> UpdateUserInfo(string userName, string trueName, string phone, string email, DateTime? birthday, string qq, string province, string city, string address)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("userName", userName);
            parm.Add("trueName", trueName);
            parm.Add("phone", phone);
            parm.Add("email", email);
            parm.Add("birthday", birthday.HasValue ? birthday.Value.ToString("yyyy-MM-dd") : string.Empty);
            parm.Add("qq", qq);
            parm.Add("province", province);
            parm.Add("city", city);
            parm.Add("address", address);

            var request = PostRequest("api/User/UpdateUserInfo", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<bool>();
        }

        public async Task<ApiResult<bool>> CheckUserName(string userName)
        {
            var request = GetRequest("api/User/CheckUserName?userName=" + userName);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<bool>();
        }

        public async Task<ApiResult<User>> GetByUserName(string userName)
        {
            var request = GetRequest("api/User/GetByUserName?userName=" + userName);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<User>();
        }

        public async Task<ApiResult<bool>> GetForgetPwdValidateCode(string userName, string type, string ip)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("userName", userName);
            parm.Add("validatetype", type);
            parm.Add("ip", ip);
            var request = PostRequest("api/User/GetForgetPwdValidateCode", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<bool>();
        }

        public async Task<ApiResult<bool>> ChangePasswordByForget(string userName, string newPassword, string confirmPassword, string emailValidateCode, string phoneValidateCode, string type)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("userName", userName);
            parm.Add("newPassword", newPassword);
            parm.Add("confirmPassword", confirmPassword);
            parm.Add("emailValidateCode", emailValidateCode);
            parm.Add("phoneValidateCode", phoneValidateCode);
            parm.Add("validatetype", type);
            var request = PostRequest("api/User/ChangePasswordByForget", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<bool>();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="oldPwd"></param>
        /// <param name="newPwd"></param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> ChangePasswordByUser(string userName, string oldPwd, string newPwd)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("userName", userName);
            parm.Add("NewPassword", newPwd);
            parm.Add("ConfirmPassword", oldPwd);
            var request = PostRequest("api/User/ChangePasswordByUser", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<bool>();
        }

        public async Task<ApiResult<bool>> Recommend(string code)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("", code);
            var request = PostRequest("api/User/Recommend", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<bool>();
        }

        public async Task<ApiResult<string>> GetRecommendCode()
        {
            var request = GetRequest("api/User/GetRecommendCode");
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<string>();
        }

        public async Task<ApiResult<string>> GetMGGameAccountPwd(string loginPwd)
        {
            var request = GetRequest("api/User/GetMGGameAccountPwd?loginPwd=" + loginPwd);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<string>();
        }
    }
}