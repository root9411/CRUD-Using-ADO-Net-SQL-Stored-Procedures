using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ADO_Example.Models;

namespace ADO_Example.DAL
{
    public class Product_DAL
    {
        string conString = ConfigurationManager.ConnectionStrings["adoConnectionstring"].ToString();

        //Get All Products
        public List<Product> GetAllProducts()
        {
            List<Product> productList = new List<Product>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetAllProducts";
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtProducts = new DataTable();

                connection.Open();
                sqlDA.Fill(dtProducts);
                connection.Close();

                foreach(DataRow dr in dtProducts.Rows)
                {
                    productList.Add(new Product
                    {
                        ProductId = Convert.ToInt32(dr["ProductID"]),
                        ProductName = dr["ProductName"].ToString(),
                        Price = Convert.ToDecimal(dr["Price"]),
                        Qty = Convert.ToInt32(dr["Qty"]),
                        Remarks = dr["Remarks"].ToString()
                    });
                }
            }
            return productList;
        }

        //Insert Products
        public bool InsertProduct(Product product)
        {
            int id = 0;
            using (SqlConnection conn = new SqlConnection(conString))
            {
                SqlCommand comm = new SqlCommand("sp_InsertProducts", conn);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@ProductName", product.ProductName);
                comm.Parameters.AddWithValue("@Price", product.Price);
                comm.Parameters.AddWithValue("@Qty", product.Qty);
                comm.Parameters.AddWithValue("@Remarks", product.Remarks);

                conn.Open();
                id = comm.ExecuteNonQuery();
                conn.Close();
            }
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Product> GetById(int id)
        {
            List<Product> ProductDetails = new List<Product>();
            int pid = id;
            using(SqlConnection conn = new SqlConnection(conString))
            {
                SqlCommand comm = new SqlCommand("sp_GetDetail", conn);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@ProductID", pid);                             
                SqlDataAdapter sqlDA = new SqlDataAdapter(comm);
                DataTable dtProducts = new DataTable();

                conn.Open();
                    sqlDA.Fill(dtProducts);
                conn.Close();
                
                foreach(DataRow dr in dtProducts.Rows)
                    {
                        ProductDetails.Add(new Product
                        {
                            ProductId = Convert.ToInt32(dr["ProductID"]),
                            ProductName = dr["ProductName"].ToString(),
                            Price = Convert.ToDecimal(dr["Price"]),
                            Qty = Convert.ToInt32(dr["Qty"]),
                            Remarks = dr["Remarks"].ToString()
                        });
                    }
                
            }
            return ProductDetails;
        }

        public bool CheckDelete(int id)
        {
            int check = 0;
            using(SqlConnection conn = new SqlConnection(conString))
            {
                SqlCommand comm = new SqlCommand("sp_DeleteDetail", conn);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@ProductID", id);

                conn.Open();
                check = comm.ExecuteNonQuery();
                conn.Close();

                if(check > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}