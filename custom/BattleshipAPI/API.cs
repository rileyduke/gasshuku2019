using Amazon.Lambda.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace sampleFactCsharp.BattleshipAPI
{
    public class API
    {
        private ILambdaContext context { get; set; }

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
        /// <param name="player_id"></param>
        public void MoveRight(string player_id)
        {
            try
            {
                APIHelper.Get(APIHelper.RIGHT);
            }
            catch (Exception e)
            {
                this.Log(e.Message);
            }
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
                return APIHelper.Get(String.Format(APIHelper.REGISTER, player_name));
            }
            catch (Exception e)
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
