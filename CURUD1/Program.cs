using System;
using System.Data.SqlClient;
using System.Data;

public class Item
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

public class ItemRepository
{
    private string connectionString;

    public ItemRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public void Create(Item item)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Items (Name, Description) VALUES (@Name, @Description)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", item.Name);
            command.Parameters.AddWithValue("@Description", item.Description);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public Item Read(int id)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT ID, Name, Description FROM Items WHERE ID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", id);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Item
                {
                    ID = (int)reader["ID"],
                    Name = reader["Name"].ToString(),
                    Description = reader["Description"].ToString()
                };
            }
            return null;
        }
    }

    public void Update(Item item)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "UPDATE Items SET Name = @Name, Description = @Description WHERE ID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", item.Name);
            command.Parameters.AddWithValue("@Description", item.Description);
            command.Parameters.AddWithValue("@ID", item.ID);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public void Delete(int id)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM Items WHERE ID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", id);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
