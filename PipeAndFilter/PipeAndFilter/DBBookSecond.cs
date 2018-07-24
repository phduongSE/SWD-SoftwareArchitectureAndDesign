using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace PipeAndFilter
{
    public class DBBookSecond : Source
    {
        private static DBBookSecond instance;

        public DBBookSecond()
        {

        }

        public static DBBookSecond getInstance()
        {
            if (instance == null)
            {
                instance = new DBBookSecond();
            }

            return instance;
        }

        public void ReadDBPipeAndFilter1()
        {
            string command = "SELECT * FROM Book";
            SqlConnection cnn = new SqlConnection(ConstantDataManager.sqlCnn1);
            SqlCommand cmd = new SqlCommand(command, cnn);

            if (cnn.State == System.Data.ConnectionState.Closed)
            {
                cnn.Open();
            }

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                System.Console.WriteLine("ID: " + reader["Id"]);
            }

        }

        public bool WriteRecord(Book newBook)
        {
            string command = "Insert Book values(@Id, @Name, @Author, @DatePublish, @Version, @Price, @ExtraData, @ExtraData1, @ExtraData2, @ExtraData3, @ExtraData4, @ExtraData5)";
            SqlConnection cnn = new SqlConnection(ConstantDataManager.sqlCnn2);
            SqlCommand cmd = new SqlCommand(command, cnn);

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

            return true;
        }

        public override void Run()
        {
            int i = 1;
            while (i < 10000)
            {
                Book book = this.Read();
                if (book != null)
                {
                    this.WriteRecord(book);
                }
                Thread.Sleep(100);
                i += 1;
            }
        }
    }
}
