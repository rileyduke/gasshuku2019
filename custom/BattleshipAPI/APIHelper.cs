﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Http;
using Amazon.Lambda.Core;

namespace sampleFactCsharp.BattleshipAPI
{
    public static class APIHelper
    {
        private static readonly HttpClient client = new HttpClient();
        
        // API URL info
        public static string HOST = @"http://ec2-54-238-173-103.ap-northeast-1.compute.amazonaws.com";
        public static string PORT = @"3003";
        public static string API = @"/api/player/";
        public static string URL = HOST + ":" + PORT + API;

        // movement
        public static string LEFT = URL + @"{0}/move/left";
        public static string UP = URL + @"{0}/move/up";
        public static string DOWN = URL + @"{0}/move/down";
        public static string RIGHT = URL + @"{0}/move/right";

        // turn
        public static string TURN_LEFT = URL + @"{0}/turn/left";
        public static string TURN_RIGHT = URL + @"{0}/turn/right";
        public static string wARP = URL + @"{0}/warp";

        // battle
        public static string FIRE = URL + @"{0}/fire";
        public static string FIX_ENGINE = URL + @"{0}/fix/engine";
        public static string FIX_ARM = URL + @"{0}/fix/arm";
        public static string FIX_THRUSTER = URL + @"{0}/fix/thruster";
        public static string CANCEL_ACTIONS = URL + @"{0}/cancelactions";

        //intents
        public static string INTENT_LEFT = "left";
        public static string INTENT_UP = "up";
        public static string INTENT_DOWN = "downaction";
        public static string INTENT_RIGHT = "right";

        public static string INTENT_TURN_LEFT = "turn_left";
        public static string INTENT_TURN_RIGHT = "turn_right";
        public static string INTENT_wARP = "warp";

        public static string INTENT_FIRE = "fire";
        public static string INTENT_FIX_ENGINE = "fix_engine";
        public static string INTENT_FIX_ARM = "fix_arm";
        public static string INTENT_FIX_THRUSTER = "fix_thruster";
        public static string INTENT_CANCEL_ACTIONS = "cancel_actions";

        public static string INTENT_REGISTER = "register";

        // intents

        // register
        public static string REGISTER = URL + @"add/{0}";
        
        /// <summary>
        /// simple get request. 
        /// returns the response as a string
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string Get(string uri)
        {
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri(uri);
                HttpResponseMessage response = client.GetAsync("").Result;
                response.EnsureSuccessStatusCode();
                string result = response.Content.ReadAsStringAsync().Result;
                //Console.WriteLine("Result: " + result);

                return result;
            }
        }

        public static string Append(this string a, string b)
        {
            return String.Format(a, b);
        }
    }
}
