using System;
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
        public static string PORT = @"3001";
        public static string API = @"/api/player/";
        public static string URL = HOST + ":" + PORT + API;

        // movement
        public static string LEFT = URL + @"{0}/move/left";
        public static string UP = URL + @"{0}/move/up";
        public static string DOWN = URL + @"{0}/move/down";
        public static string RIGHT = URL + @"{0}/move/right";

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
