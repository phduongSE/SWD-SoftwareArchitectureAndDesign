using System.Data.SqlClient;namespace PipeAndFilter
{
    using System;
    using System.Data;
    using System.Threading;

    public class DBBookFirst : Source
    {
        private static DBBookFirst instance;

        // Constructor
        public DBBookFirst()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DBBookFirst getInstance()
        {
            if (instance == null)
            {
                instance = new DBBookFirst();
            }

            return instance;
        }

        /// <summary>
        /// 
        /// </summary>
        public void InsertToDBPiprAndFilter1()
        {
            string command = "Insert Book values(@Id, @Name, @Author, @DatePublish, @Version, @Price, @ExtraData, @ExtraData1, @ExtraData2, @ExtraData3, @ExtraData4, @ExtraData5)";
            SqlConnection cnn = new SqlConnection(ConstantDataManager.sqlCnn1);
            SqlCommand cmd = new SqlCommand(command, cnn);

            Random rd = new Random();

            for (int i = 0; i < 10000; i++)
            {
                Book newBook = new Book
                {
                    Id = i + 1,
                    Name = "Book" + (i + 1),
                    Author = "Author" + (i + 1),
                    DatePublish = DateTime.Now,
                    Version = "Version 6",
                    Price = (decimal)rd.NextDouble() * 1000 + 1,
                    ExtraData = "Some extra " + (i + 1),
                    ExtraData1 = "Some extra " + (i + 1),
                    ExtraData2 = "Some extra " + (i + 1),
                    ExtraData3 = "Some extra " + (i + 1),
                    ExtraData4 = "Some extra " + (i + 1),
                    ExtraData5 = "Some extra " + (i + 1),
                };
                cmd.Parameters.AddWithValue("@Id", newBook.Id);
                cmd.Parameters.AddWithValue("@Name", newBook.Name);
                cmd.Parameters.AddWithValue("@Author", newBook.Author);
                cmd.Parameters.AddWithValue("@DatePublish", newBook.DatePublish);
                cmd.Parameters.AddWithValue("@Version", newBook.Version);
                cmd.Parameters.AddWithValue("@Price", newBook.Price);
                cmd.Parameters.AddWithValue("@ExtraData", newBook.ExtraData);
                cmd.Parameters.AddWithValue("@ExtraData1", newBook.ExtraData1);
                cmd.Parameters.AddWithValue("@ExtraData2", newBook.ExtraData2);
                cmd.Parameters.AddWithValue("@ExtraData3", newBook.ExtraData3);
                cmd.Parameters.AddWithValue("@ExtraData4", newBook.ExtraData4);
                cmd.Parameters.AddWithValue("@ExtraData5", newBook.ExtraData5);

                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
        }

        public Book ReadRecord(int id)
        {
            string command = "SELECT * FROM Book WHERE Id = @Id";
            SqlConnection cnn = new SqlConnection(ConstantDataManager.sqlCnn1);
            SqlCommand cmd = new SqlCommand(command, cnn);

            if (cnn.State == System.Data.ConnectionState.Closed)
            {
                cnn.Open();
            }

            cmd.Parameters.AddWithValue("@Id", id);

            SqlDataReader reader = cmd.ExecuteReader();

            Book result = new Book();

            while (reader.Read())
            {
                result.Id = int.Parse(reader["Id"].ToString());
                result.Name = reader["Name"].ToString();
                result.Author = reader["Author"].ToString();
                result.Price = decimal.Parse( reader["Price"].ToString());
                result.Version = reader["Version"].ToString();
                result.DatePublish = DateTime.Parse( reader["DatePublish"].ToString());
                result.ExtraData = reader["ExtraData"].ToString();
                result.ExtraData1 = reader["ExtraData1"].ToString();
                result.ExtraData2 = reader["ExtraData2"].ToString();
                result.ExtraData3 = reader["ExtraData3"].ToString();
                result.ExtraData4 = reader["ExtraData4"].ToString();
                result.ExtraData5 = reader["ExtraData5"].ToString();
            }

            return result;
        }

        public override void Run()
        {
            int i = 1;
            while (i < 10000)
            {
                Book book = this.ReadRecord(i);
                if (book != null)
                {
                    this.Write(book);
                }
                
                i += 1;
                Thread.Sleep(100);
            }
        }
    }
}
