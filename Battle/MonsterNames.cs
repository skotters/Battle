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
    public static class MonsterNames
    {
        public static string GetMonsterName()
        {
            const int MONSTERNAMECOUNT = 152; //There are 152 total monsters in database
            Random rng = new Random();
            string monsterName = "";

            int dummyID = rng.Next(1, MONSTERNAMECOUNT + 1);

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

            return monsterName;
        }
    }
}
