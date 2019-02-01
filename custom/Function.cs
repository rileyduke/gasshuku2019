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

        /// <summary>
        /// Application entry point
        /// </summary>
        /// <param name="input"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext ctx)
        {
            context = ctx;
            this._api = new API(ctx);
            _api.RegisterPlayer("Riley");

            this.Log(APIHelper.URL);

            var PlayerId = "";

            if (input.Request.Type.Equals(AlexaConstants.LaunchRequest))
            {
                _api.LogTest(PlayerId);
            }
            else if (input.Request.Type.Equals(AlexaConstants.SessionEndedRequest))
            {
                _api.LogTest("cancel intent");
            }
            else
            {
                _api.LogTest(input.Request.Type);
            }

            // ƒGƒ“ƒh
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
