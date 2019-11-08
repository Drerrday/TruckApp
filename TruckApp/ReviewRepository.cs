using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using TruckApp.Models;
namespace TruckApp
{
    public class ReviewRepository
    {
        private static string connectionString = System.IO.File.ReadAllText("connectionString.txt");

        public List<Review> GetAllReviews()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM REVIEW;";
            using (conn)
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                List<Review> allReviews = new List<Review>();
                while (reader.Read() == true)
                {
                    var currentReview = new Review();
                    currentReview.REVIEW_ID = reader.GetInt32("REVIEW_ID");
                    currentReview.TRUCK_ID = reader.GetInt32("TRUCK_ID");
                    currentReview.REVIEWER = reader.GetString("REVIEWER");
                    currentReview.RATING = reader.GetInt32("RATING");
                    currentReview.REVIEW = reader.GetString("REVIEW");
                    allReviews.Add(currentReview);
                }
                return allReviews;
            }
        }


        public Review GetReviewsPerTruck(int id)
        {

            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = conn.CreateCommand();
            //cmd.CommandText = "SELECT * FROM REVIEWS WHERE ID = @id;";
            cmd.CommandText = "SELECT * FROM TRUCKS AS t INNER JOIN REVIEW as r ON r.TRUCK_ID = t.ID WHERE t.ID = @id;";
            cmd.Parameters.AddWithValue("id", id);

            using (conn)
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                var currentReview = new Review();

                var list = new List<string>();
                while (reader.Read() == true)
                {
                    currentReview.REVIEW_ID = reader.GetInt32("REVIEW_ID");
                    currentReview.TRUCK_ID = reader.GetInt32("TRUCK_ID");
                    currentReview.REVIEWER = reader.GetString("REVIEWER");
                    currentReview.RATING = reader.GetInt32("RATING");
                    currentReview.REVIEW = reader.GetString("REVIEW");
                }
                return currentReview;
            }
        }
    }
}