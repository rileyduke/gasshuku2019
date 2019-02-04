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
            try
            {
                this.Log("1");
                response = new SkillResponse();
                response.Response = new ResponseBody();
                response.Response.ShouldEndSession = false;
                response.Version = AlexaConstants.AlexaVersion;
                this.Log("2");
                if (input.Request.Type == AlexaConstants.LaunchRequest)
                {
                    this.Log("3");
                    // Alexa register!
                    this._api = new API(ctx);
                    this.Log("4");
                    this.Log(APIHelper.URL);
                    this.Log(await _api.RegisterPlayer("Riley"));
                    this.Log("5");

                    //IOutputSpeech innerResponse = new SsmlOutputSpeech();
                    //(innerResponse as SsmlOutputSpeech).Ssml = "launch message";
                    //response.Response.OutputSpeech = innerResponse;
                    //IOutputSpeech prompt = new PlainTextOutputSpeech();
                    //(prompt as PlainTextOutputSpeech).Text = "test";
                    //response.Response.Reprompt = new Reprompt()
                    //{
                    //    OutputSpeech = prompt
                    //};

                }
                else if (input.Request.Type == AlexaConstants.IntentRequest)
                {
                    if(input.Request.Intent.Name == APIHelper.INTENT_REGISTER)
                    {
                        // Alexa register!
                        this._api = new API(ctx);
                        this.Log(APIHelper.URL);
                        this.Log(await _api.RegisterPlayer("Riley"));
                    }
                    else if (input.Request.Intent.Name == "testintent")
                    {
                        this._api.DoAction(APIHelper.RIGHT);
                    }
                    else if (input.Request.Intent.Name == APIHelper.INTENT_LEFT)
                    {
                        this._api.DoAction(APIHelper.LEFT);
                    }
                    else if (input.Request.Intent.Name == APIHelper.INTENT_UP)
                    {
                        this._api.DoAction(APIHelper.UP);
                    }
                    else if (input.Request.Intent.Name == APIHelper.INTENT_DOWN)
                    {
                        this._api.DoAction(APIHelper.DOWN);
                    }
                    else if (input.Request.Intent.Name == APIHelper.INTENT_RIGHT)
                    {
                        this._api.DoAction(APIHelper.RIGHT);
                    }
                    else if (input.Request.Intent.Name == APIHelper.INTENT_TURN_LEFT)
                    {
                        this._api.DoAction(APIHelper.TURN_LEFT);
                    }
                    else if (input.Request.Intent.Name == APIHelper.INTENT_TURN_RIGHT)
                    {
                        this._api.DoAction(APIHelper.TURN_RIGHT);
                    }
                    else if (input.Request.Intent.Name == APIHelper.INTENT_wARP)
                    {
                        this._api.DoAction(APIHelper.wARP);
                    }
                    else if (input.Request.Intent.Name == APIHelper.INTENT_FIRE)
                    {
                        this._api.DoAction(APIHelper.FIRE);
                    }
                    else if (input.Request.Intent.Name == APIHelper.INTENT_FIX_ENGINE)
                    {
                        this._api.DoAction(APIHelper.FIX_ENGINE);
                    }
                    else if (input.Request.Intent.Name == APIHelper.INTENT_FIX_ARM)
                    {
                        this._api.DoAction(APIHelper.FIX_ARM);
                    }
                    else if (input.Request.Intent.Name == APIHelper.INTENT_FIX_THRUSTER)
                    {
                        this._api.DoAction(APIHelper.FIX_THRUSTER);
                    }
                    else if (input.Request.Intent.Name == APIHelper.INTENT_CANCEL_ACTIONS)
                    {
                        this._api.DoAction(APIHelper.CANCEL_ACTIONS);
                    }
                    // _api.LogTest("move right");
                    //_api.DoAction(APIHelper.RIGHT);
                }
                else
                {
                    _api.LogTest(input.Request.Type);
                }

                return response;
            }
            catch(Exception e)
            {
                this.Log(e.Message);
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
