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
        const string LOCALENAME = "locale";
        const string USA_Locale = "en-US";
        const string PLANETS = "Planet";
        const string DEPARTINGPLANET = "DepartingPlanet";
        const string ARRIVINGPLANET  = "ArrivingPlanet";

        static Random rand = new Random();

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

            //var PlayerId = _api.RegisterPlayer("RILEYRILEY");
            //_api.MoveRightTest(PlayerId);
            if (input.Request.Type.Equals(AlexaConstants.LaunchRequest))
            {
                _api.LogTest("Riley test");
            }
            else if (input.Request.Type.Equals(AlexaConstants.SessionEndedRequest))
            {
                _api.LogTest("cancel intent");
            }
            else
            {
                _api.LogTest(input.Request.Type);
            }
                
            
            return null; 
        }
    }
}
