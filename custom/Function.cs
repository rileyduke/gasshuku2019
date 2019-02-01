using System;
using System.Collections.Generic;
using Amazon.Lambda.Core;
using Newtonsoft.Json;
using AlexaAPI;
using AlexaAPI.Request;
using AlexaAPI.Response;
using System.IO;
using System.Text.RegularExpressions;
using sampleFactCsharp.BattleshipAPI;
using System.Threading.Tasks;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializerAttribute(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace sampleFactCsharp
{
    public class Function
    {
        private SkillResponse response = null;
        private ILambdaContext context = null;

        // API object
        private API _api { get; set; }

        private string teststring { get; set; }

        /// <summary>
        /// Application entry point
        /// </summary>
        /// <param name="input"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<SkillResponse> FunctionHandler(SkillRequest input, ILambdaContext ctx)
        {
            context = ctx;
            
            if (input.Request.Type.Equals(AlexaConstants.LaunchRequest))
            {
                // Alexa register!
                this._api = new API(ctx);
                this.Log(APIHelper.URL);
                this.Log(await _api.RegisterPlayer("Riley"));
            }
            else if (input.Request.Type.Equals(AlexaConstants.SessionEndedRequest))
            {
                _api.LogTest("cancel intent");
                this.teststring = "BLA BLA2";
            }
            else
            {
                _api.LogTest(input.Request.Type);
            }

            // �G���h
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
