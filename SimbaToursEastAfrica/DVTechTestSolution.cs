using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RobotManipulation
{
    public class DVTechTestSolution
    {
        public class User
        {
            public int id { get; set; }
            public int page { get; set; }
            public string username { get; set; }
            public string about { get; set; }
            public DateTime submitted { get; set; }
            public DateTime updated_at { get; set; }
            public DateTime created_at { get; set; }
            public int comment_count { get; set; }
        }
        public class ApiUserDataResult
        {
            public int page { get; set; }
            public int per_page { get; set; }
            public int patotalge { get; set; }
            public int total_pages { get; set; }
            public int submission_count { get; set; }
            public User[] data { get; set; }
        }
        class Result
        {

            /*
             * Complete the 'getUsernames' function below.
             *
             * The function is expected to return a STRING_ARRAY.
             * The function accepts INTEGER threshold as parameter.
             *
             * URL for cut and paste
             * https://jsonmock.hackerrank.com/api/article_users?page=<pageNumber>
             */

            public static async Task<List<string>> getUsernames(int threshold)
            {
                var httpClient = new HttpClient();
                var requestUrl = $"https://jsonmock.hackerrank.com/api/article_users?page={threshold.ToString()}";
                var response = await httpClient.GetAsync(requestUrl);
                response.EnsureSuccessStatusCode();
                var responseBody = response.ToString();
                var respData = JsonConvert.DeserializeObject<ApiUserDataResult>(responseBody);
                return respData.data.Where(p => p.submission_count > threshold).Select(us => us.username).ToList<string>();
            }

        }

        public static void commonSubstring(List<string> a, List<string> b)
        {
            for(var i=0; i< a.Count; i++)
            {
                bool contains = false;
                for (var j = 0; j < b.Count; j++)
                {
                    if (i == j)
                    {

                        foreach (char n in a[i])
                        {
                            foreach (char n1 in b[i])
                            {
                                if (n1 == n)
                                {
                                    contains = true;
                                    break;
                                }
                            }
                        }
                        break;
                    }
                }

                Console.WriteLine(contains ? "YES" : "NO");
            }
        }
    }
}
