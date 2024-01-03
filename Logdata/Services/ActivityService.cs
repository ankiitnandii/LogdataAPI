using LogdataAPI.Models;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace LogdataAPI.Services
{
    public class ActivityService
    {
        private SqlConnection GetConnection()
        {

            string connectionString = "Server=tcp:apppserver.database.windows.net,1433;Initial Catalog=appdb;Persist Security Info=False;User ID=sqladmin;Password=Microsoft@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            return new SqlConnection(connectionString);
        }
        public List<Activity> GetActivities()
        {
            List<Activity> activityList = new List<Activity>();
            string statement = "SELECT Id,Operationname,Status,Eventcategory,Resourcetype,Resource from logdata";
            SqlConnection sqlConnection = GetConnection();

            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(statement, sqlConnection);

            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
                while (sqlDataReader.Read())
                {
                    Activity _activity = new Activity()
                    {
                        Id = sqlDataReader.GetInt32(0),
                        Operationname = sqlDataReader.GetString(1),
                        Status = sqlDataReader.GetString(2),
                        Eventcategory = sqlDataReader.GetString(3),
                        Resourcetype = sqlDataReader.GetString(4),
                        Resource = sqlDataReader.GetString(5)

                    };

                    activityList.Add(_activity);
                }
            }
            sqlConnection.Close();
            return activityList;
        }


        public Activity GetActivity(string _Id)
        {
            int Id = int.Parse(_Id);
            string statement = String.Format("SELECT Id,Operationname,Status,Eventcategory,Resourcetype,Resource from logdata WHERE Id={0}", Id);
            SqlConnection sqlConnection = GetConnection();

            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(statement, sqlConnection);
            Activity activity = new Activity();
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
                sqlDataReader.Read();
                activity.Id = sqlDataReader.GetInt32(0);
                activity.Operationname = sqlDataReader.GetString(1);
                activity.Status = sqlDataReader.GetString(2);
                activity.Eventcategory=sqlDataReader.GetString(3);
                activity.Resourcetype=sqlDataReader.GetString(4);
                activity.Resource=sqlDataReader.GetString(5);
                return activity;
            }
        }

        public int AddActivity(Activity activity)
        {
            string statement = String.Format("INSERT INTO [logdata] (Id,Operationname,Status,Eventcategory,Resourcetype,Resource) VALUES({0},'{1}','{2}','{3}','{4}','{5}')", activity.Id, activity.Operationname, activity.Status, activity.Eventcategory, activity.Resourcetype, activity.Resource);
            SqlConnection sqlConnection = GetConnection();

            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(statement, sqlConnection);
            int rowsAffected= sqlCommand.ExecuteNonQuery();
            return rowsAffected;
        }
    }

 }
