﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using TruckApp.Models;
namespace TruckApp
{
    public class TRUCKRepository
    {
        private static string connectionString = System.IO.File.ReadAllText("connectionString.txt");

        public List<Truck> GetAllTRUCKS()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM TRUCKS;";
            using (conn)
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                List<Truck> allTRUCKS = new List<Truck>();
                var reviewRepo = new ReviewRepository();
                while (reader.Read() == true)
                {
                    var currentTRUCK = new Truck();
                    currentTRUCK.ID = reader.GetInt32("ID");
                    currentTRUCK.Model = reader.GetString("MODEL");
                    currentTRUCK.Year = reader.GetInt32("YEAR");
                    currentTRUCK.ReviewID = reader.GetInt32("Review_ID");
                    currentTRUCK.ImageLink = reader.GetString("IMAGE");
                    currentTRUCK.Review = reviewRepo.GetReviewsPerTruck(currentTRUCK.ID);

                    allTRUCKS.Add(currentTRUCK);
                }
                return allTRUCKS;
            }
        }


        public Truck GetTruck(int id)
        {

            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM TRUCKS WHERE ID = @id;";
            cmd.Parameters.AddWithValue("id", id);

            using (conn)
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                var currentTRUCK = new Truck();
                var reviewRepo = new ReviewRepository();

                while (reader.Read() == true)
                {
                    
                    currentTRUCK.ID = reader.GetInt32("ID");
                    currentTRUCK.Model = reader.GetString("MODEL");
                    currentTRUCK.Year = reader.GetInt32("YEAR");
                    currentTRUCK.ReviewID = reader.GetInt32("Review_ID");
                    currentTRUCK.ImageLink = reader.GetString("IMAGE");
                    currentTRUCK.Review = reviewRepo.GetReviewsPerTruck(currentTRUCK.ID);
                }
                return currentTRUCK;
            }
        }
    }
}