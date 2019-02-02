using Amazon.Lambda.Core;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace sampleFactCsharp.BattleshipAPI
{
    public class API
    {
        private ILambdaContext context { get; set; }
        private string _playerId { get; set; }
        private HttpClient _client { get; set; }

        public API(ILambdaContext ctx)
        {
            this.context = ctx;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(APIHelper.HOST + ":" + APIHelper.PORT + "/api/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
            _client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9,ja;q=0.8");
            _client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            //_client.DefaultRequestHeaders.Add("Content-Type", "application/json; charset=utf-8");
        }

        public void LogTest(string log)
        {
            this.Log(log);
        }

        public void DoAction(string ACTION)
        {
            var r = this.ApiGet(ACTION);
        }

        /// <summary>
        /// move right
        /// </summary>
        public void MoveRight()
        {
            var r = this.ApiGet(APIHelper.RIGHT);
        }

        /// <summary>
        /// move left
        /// </summary>
        public void MoveLeft()
        {
            var r = this.ApiGet(APIHelper.LEFT);
        }

        /// <summary>
        /// move up
        /// </summary>
        public void MoveUp()
        {
            var r = this.ApiGet(APIHelper.UP);
        }

        /// <summary>
        /// move down
        /// </summary>
        public void MoveDown()
        {
            var r = this.ApiGet(APIHelper.DOWN);
        }

        /// <summary>
        /// register player
        /// </summary>
        /// <param name="player_name"></param>
        /// <returns></returns>
        public async Task<string> RegisterPlayer(string player_name)
        {
            try
            {
                var resp = await this.Get(String.Format("player/add/{0}",player_name));
                this._playerId = resp.Split(',')[1].Split(':')[1].Replace("\"", "").Replace("}","");
                this.Log(_playerId);
            }
            catch (Exception e)
            {
                this.Log("error: " + e.Message);
            }

            return null;
        }

        /// <summary>
        /// adds the player id to the string and then gets the URL
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<string> ApiGet(string uri)
        {
            try
            {
                this.Log(uri.Append(_playerId));
                return APIHelper.Get(uri.Append(_playerId));
            }
            catch(Exception e)
            {
                this.Log(e.Message);
            }

            return null;
        }

        /// <summary>
        /// simple get request. 
        /// returns the response as a string
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<string> Get(string uri)
        {
            this.Log(this._client.BaseAddress.ToString());
            this.Log(uri);
            
            HttpResponseMessage response = await _client.GetAsync(uri);

            string result = "";
            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsStringAsync().Result;
            }
            return result;
        }

        /// <summary>
        /// logger interface
        /// </summary>
        /// <param name="text"></param>
        /// <returns>void</returns>
        private void Log(string text)
        {
            if (context != null)
            {
                context.Logger.LogLine(text);
            }
        }
        
    }
}
