using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckApp.Models;
namespace TruckApp
{
    public class TRUCKRepository
    {
        private static string connectionString = System.IO.File.ReadAllText("connectionString.txt");
        public List<TRUCK> GetAllTRUCKS()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM TRUCKS;";
            using (conn)
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                List<TRUCK> allTRUCKS = new List<TRUCK>();
                while (reader.Read() == true)
                {
                    var currentTRUCK = new TRUCK();
                    currentTRUCK.ID = reader.GetInt32("TRUCKID");
                    currentTRUCK.Model = reader.GetString("MODEL");
                    currentTRUCK.Year = reader.GetInt32("YEAR");
                    currentTRUCK.ReviewID = reader.GetInt32("ReviewID");
                    allTRUCKS.Add(currentTRUCK);
                }
                return allTRUCKS;
            }
        }
    }
}