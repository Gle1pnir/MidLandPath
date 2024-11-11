using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Dapper;
using System.Numerics;

namespace MidLandPath
{
    internal class DbManager
    {
        private SQLiteConnection connection;

        public DbManager()
        {
            connection = new SQLiteConnection("Data Source=Users.db");
        }

        
        public int GetPlayerSteps(int tg) => connection.Query<int>($"SELECT steps FROM Users WHERE TgId = '{tg}'").ToList()[0];
        public void AddPlayerSteps(int tg, int steps) => connection.Execute($"UPDATE Users SET steps = '{GetPlayerSteps(tg) + steps}' WHERE TgId = '{tg}'");
        public void SetLocation(int tg, int location) => connection.Execute($"UPDATE Users SET location = '{location}' WHERE TgId = '{tg}'");
        public int GetLocation(int tg) => connection.Query<int>($"SELECT location FROM Users WHERE TgId = '{tg}'").ToList()[0];

        public void AddPlayer(User user)
        {
            if (connection.Query<int>($"SELECT steps FROM Users WHERE TgId = '{user.TgId}'").ToList().Count() == 0)
            {
                connection.Execute("INSERT INTO Users (TgId, steps, location) VALUES (@TgId, @steps, @location)",
            new { TgId = user.TgId, steps = 0, location = 0 });
            }
            else
            {
                connection.Execute($"UPDATE Users SET steps = '0' location = '0' WHERE TgId = '{user.TgId}'");
            }
        }
    }
}
