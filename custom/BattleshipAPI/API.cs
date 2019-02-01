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
            _client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });
            _client.BaseAddress = new Uri(APIHelper.HOST + ":" + APIHelper.PORT + "/api/");

            //_client.BaseAddress = new Uri("http://localhost:64195/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void LogTest(string log)
        {
            this.Log(log);
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
                //this.Log(String.Format(APIHelper.REGISTER, player_name));
                this._playerId = await this.Get("player/add/riley2");
            }
            catch (Exception e)
            {
                this.Log(e.Message);
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

            HttpResponseMessage response = _client.GetAsync(uri).Result;
            response.EnsureSuccessStatusCode();
            string result = response.Content.ReadAsStringAsync().Result;

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
