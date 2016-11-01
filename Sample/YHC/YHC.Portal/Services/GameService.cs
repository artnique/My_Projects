using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

using YHC.Portal.Models;
using YHC.Portal.Extensions;
using System.Collections.Specialized;
using YHC.Portal.Core;

namespace YHC.Portal.Services
{
    public class GameService : BaseService
    {
        public async Task<ApiResult<string>> LaunchSport(string game, string type)
        {
            var request = CreateRequest("api/Game/GetGameUrl?gamePlatform=" + game + "&gameType=" + type, HttpMethod.Get);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<string>();
        }

        public async Task<ApiResult<string>> Play(String game, String type, String gameId, Boolean? demo, String customGameUrl)
        {
            var request = CreateRequest("api/Game/GetGameUrlForLogin?gamePlatform=" + game + "&gameType=" + type + "&gameId=" + gameId + "&customGameUrl=" + customGameUrl, HttpMethod.Get);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<string>();
        }

        public async Task<ApiResult<string>> TryPlay(String game, String type, String gameId)
        {
            var request = GetRequest("api/Game/GetGameUrlForDemo?gamePlatform=" + game + "&gameType=" + type + "&gameId=" + gameId);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<string>();
        }

        public async Task<ApiResult<GameApi>> GetGameApi(string gamePlatform)
        {
            var request = CreateRequest("api/Game/GetApi?gamePlatform=" + gamePlatform, HttpMethod.Get);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<GameApi>();
        }

        public async Task<ApiResult<GameApi>> GetGameCash(string gamePlatform, string ipAddress)
        {
            var request = CreateRequest("api/Game/GetCash?gamePlatform=" + gamePlatform + "&ip=" + ipAddress, HttpMethod.Get);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<GameApi>();
        }

        public async Task<ApiResult<IList<GameCategory>>> GetGameCategories(string code)
        {
            var request = CreateRequest("api/Game/GetCategories?code=" + code, HttpMethod.Get);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<IList<GameCategory>>();
        }

        public async Task<ApiResult<PagerFrontViewModel<Game>>> GetGameList(NameValueCollection nvc)
        {
            var request = GetRequest("api/Game/GetList", nvc);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<PagerFrontViewModel<Game>>();
        }

        public async Task<ApiResult<IList<Game>>> GetGameTopRecommended(NameValueCollection nvc)
        {
            var request = GetRequest("api/Game/GetTopRecommended", nvc);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<IList<Game>>(true);
        }

        public async Task<ApiResult<IList<GameApi>>> GetAllGameApi()
        {
            var request = CreateRequest("api/Game/GetAllApi", HttpMethod.Get);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<IList<GameApi>>();
        }

        public async Task<ApiResult<IList<Game>>> GetJackpotsGames(int pageIndex = 0, int pageSize = 5)
        {
            var request = CreateRequest("api/Game/GetJackpotsGames?pageIndex=" + pageIndex + "&pageSize=" + pageSize, HttpMethod.Get);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<IList<Game>>();
        }

        public async Task<ApiResult<UserGameAccount>> GetUserGameAccount(string game)
        {
            var request = CreateRequest("api/Game/GetUserAccount?gamePlatform=" + game, HttpMethod.Get);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<UserGameAccount>();
        }

        public async Task<ApiResult<decimal>> GetJackpots(String game, Int32? jackpotInfoType, string gameNameId, string jackpotId, string currency, string extendInfo)
        {
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("game", game);
            nvc.Add("jackpotInfoType", jackpotInfoType.HasValue ? jackpotInfoType.Value.ToString() : "");
            nvc.Add("gameNameId", gameNameId);
            nvc.Add("jackpotId", jackpotId);
            nvc.Add("currency", currency);
            nvc.Add("extendInfo", extendInfo);
            var request = PostRequest("api/Game/GetJackpots", nvc);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<decimal>();
        }
    }
}