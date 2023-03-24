using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO.IsolatedStorage;
using Microsoft.Data.Sqlite;

namespace Battle
{
    // gets a random lorem ipsum "name" from placeholder api
    public static class MonsterNames
    {
        private static List<JSONDummy> JSONDummies { get; set; }
        private static string fullJSONtext { get; set; }
        public static void LoadFullJSON() //obsolete
        { 
            using (var client = new HttpClient())
            {
                //***** GET
                var endpoint = new Uri("https://jsonplaceholder.typicode.com/albums");
                
                var result = client.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;
                fullJSONtext = json;
            }

            JSONDummies = JsonConvert.DeserializeObject<List<JSONDummy>>(fullJSONtext);
        }

        public static string GetMonsterName()
        {
            const int MONSTERNAMECOUNT = 152;
            Random rng = new Random();
            string monsterName = "";

            //select a random JSONDummy object from JSONDummies list
            //take that dummy title text and find the random lorem ipsum "name" within the title.
            int dummyID = rng.Next(1, MONSTERNAMECOUNT + 1);
            //string[] names = JSONDummies[dummyIndex].title.Split(" ");

            using (var connection = new SqliteConnection("Data Source=Monsters.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT name
                    FROM monsternamelist
                    where id = $dummyID

                ";

                command.Parameters.AddWithValue("$dummyID", dummyID);
                command.ExecuteNonQuery();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var nameholder = reader.GetString(0);

                        monsterName = nameholder;

                        
                    }
                }
            }

            
            //Console.WriteLine("monster name is id " + dummyID + " --- " + monsterName);
            //Console.ReadLine();

            return monsterName;
        }

        //old version...........................................................
        //public static string GetMonsterName()
        //{
        //    Random rng = new Random();

        //    //select a random JSONDummy object from JSONDummies list
        //    //take that dummy title text and find the random lorem ipsum "name" within the title.
        //    int dummyIndex = rng.Next(0, JSONDummies.Count);
        //    string[] names = JSONDummies[dummyIndex].title.Split(" ");

        //    string lowerCaseName;
        //    string properName = ""; //used to have first letter capitalized in name

        //    do
        //    {
        //        lowerCaseName = names[rng.Next(0, names.Length)];
        //        properName = Convert.ToString(lowerCaseName[0]).ToUpper();
        //    } while (lowerCaseName.StartsWith("m") || lowerCaseName.StartsWith("c"));

        //    for (int i = 1; i < lowerCaseName.Length; i++)
        //    {
        //        properName += lowerCaseName[i];
        //    }

        //    return properName;
        //}
    }
}
