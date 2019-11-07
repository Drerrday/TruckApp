using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model1.Models;

namespace TruckApp

{
    public class MakeRepository
    {
        private static string connectionString = System.IO.File.ReadAllText("ConnectionString.txt");

        public List<Product> GetAllMakes()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Makes;";

            using (conn)
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                List<Product> allMakes = new List<Make>();

                while (reader.Read() == true)
                {
                    var currentMake = new Make();
                    currentMake.ID = reader.GetInt32("MakeID");
                    currentMake.Name = reader.GetString("Name");
                    currentMake.Price = reader.GetDecimal("Price");

                    allMakes.Add(currentMake);


                }
                return allMakes;
            }
        }

        public Make GetMake(int id)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Makes WHERE MakeID = @id;";
            cmd.Parameters.AddWithValue("id", id);

            using (conn)
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                var make = new Make();

                while (reader.Read() == true)
                {
                    make.ID = reader.GetInt32("ProductID");
                    make.Name = reader.GetString("Name");
                    make.Price = reader.GetDecimal("Price");
                    make.CategoryID = reader.GetInt32("OnSale");


                    if (reader.IsDBNull(reader.GetOrdinal("StockLevel")))

                    {
                        product.StockLevel = null;
                    }
                    else
                    {
                        product.StockLevel = reader.GetString("StockLevel");
                    }


                }
                return product;
            }
        }
        public List<Product> GetOnSaleProducts()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Products WHERE OnSale = 1;";


            using (conn)
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                List<Product> allProducts = new List<Product>();

                while (reader.Read() == true)
                {
                    var currentProduct = new Product();
                    currentProduct.ID = reader.GetInt32("ProductID");
                    currentProduct.Name = reader.GetString("Name");
                    currentProduct.Price = reader.GetDecimal("Price");
                    currentProduct.OnSale = reader.GetInt32("OnSale");
                    currentProduct.StockLevel = reader.GetString("OnSale");
                    currentProduct.CategoryID = reader.GetInt32("CategoryID");

                    allProducts.Add(currentProduct);
                }
                return allProducts;
            }
        }
        public void UpdateProduct(Product productToUpdate)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE products SET Name = @name, Price = @price WHERE ProductID = @id";
            cmd.Parameters.AddWithValue("name", productToUpdate.Name);
            cmd.Parameters.AddWithValue("price", productToUpdate.Price);
            cmd.Parameters.AddWithValue("id", productToUpdate.ID);

            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();

            }

        }
        public void InsertProduct(Product productToInsert)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO products (NAME, PRICE, CATEGORYID) VALUES (@name, @price, @categoryID);";

            cmd.Parameters.AddWithValue("name", productToInsert.Name);
            cmd.Parameters.AddWithValue("price", productToInsert.Price);
            cmd.Parameters.AddWithValue("categoryID", productToInsert.CategoryID);

            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }

        }

        public Product AssignCategories()
        {
            var catRepo = new CategoryRepository();

            var catList = catRepo.GetCategories();

            Product product = new Product();
            product.Categories = catList;

            return product;
        }

        public void DeleteProduct(int id)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM products WHERE ProductID = @id";
            cmd.Parameters.AddWithValue("id", id);

            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteProductFromSales(int id)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM sales WHERE ProductID = @id";
            cmd.Parameters.AddWithValue("id", id);

            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteProductFromReviews(int id)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM reviews WHERE ProductID = @id";
            cmd.Parameters.AddWithValue("id", id);

            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteProductFromAllTables(int productID)
        {
            DeleteProductFromSales(productID);
            DeleteProductFromReviews(productID);
            DeleteProduct(productID);
        }






    }
}

