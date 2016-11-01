using YHC.Portal.Extensions;
using YHC.Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace YHC.Portal.Services
{
    public class ConfigService : BaseService
    {
        public Config Get()
        {
            Config config = null;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/config/items");
            var response = Client.SendAsync(request).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseText = response.Content.ReadAsStringAsync().Result;//.Replace("<", "").Replace(">k__BackingField", ""); // 替换序列化产生的k__BackingField，否则反序列化时会不成功
                config = JSSerializer.Deserialize<Config>(responseText);
            }
            return config;
        }

        public async Task<ApiResult<IList<Bank>>> GetBankList()
        {
            var request = GetRequest("api/Config/GetBankList");
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<IList<Bank>>();
        }

        public async Task<ApiResult<IList<Province>>> GetProvinces()
        {
            var request = GetRequest("api/Config/GetProvinceList");
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<IList<Province>>();
        }

        public async Task<ApiResult<IList<City>>> GetCites(int provinceId)
        {
            var request = GetRequest("api/Config/GetCityList?provinceId=" + provinceId);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<IList<City>>();
        }

        public async Task<ApiResult<string>> GetNewsContent(string key)
        {
            var request = GetRequest("api/Config/GetNewsContent?key=" + key);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<string>();
        }

        public async Task<ApiResult<VUserLevel>> GetFirstLevel()
        {
            var request = GetRequest("api/Config/GetFirstLevel");
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<VUserLevel>();
        }

        public async Task<ApiResult<IList<SpareDomain>>> GetSpareDomain()
        {
            var request = GetRequest("api/Config/GetSpareDomain");
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<IList<SpareDomain>>();
        }
    }
}