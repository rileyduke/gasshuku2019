using Amazon.Lambda.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace sampleFactCsharp.BattleshipAPI
{
    public class API
    {
        private ILambdaContext context { get; set; }
        private string _playerId { get; set; }

        public API(ILambdaContext ctx)
        {
            this.context = ctx;
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
        public string RegisterPlayer(string player_name)
        {
            try
            {
                this._playerId = APIHelper.Get(String.Format(APIHelper.REGISTER, player_name));
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
        private string ApiGet(string uri)
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
