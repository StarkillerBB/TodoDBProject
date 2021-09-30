using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Razer_H2.Modul;


namespace Razer_H2.Repository
{
    public class ToDoRepository : IToDoRepository
    {
        private List<ToDo> toDos = new List<ToDo>();
        private List<Contact> contacts = new List<Contact>();
        private List<Priority> priorities = new List<Priority>();

        internal static IConfigurationRoot configuration { get; set; }
        static string connectionString;
        SqlConnection sqlCon = new SqlConnection(connectionString);

        public static void SetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
            configuration = builder.Build();
            connectionString = configuration.GetConnectionString("HelpMe");
        }


        /// <summary>
        /// Create new ToDo
        /// </summary>
        /// <param name="desc"></param>
        public void CreateToDo(ToDo toDo)
        {
            using (var cmd = sqlCon.CreateCommand())
            {
                cmd.CommandText = "EXEC CreateTodo @Contact_Id, @Priority_Id, @Description, @CreatedTime, @IsCompleted, @IsDeleted";
                sqlCon.Open();

                try
                {
                    cmd.Parameters.AddWithValue("@Contact_Id", toDo.ContactId);
                    cmd.Parameters.AddWithValue("@Priority_Id", toDo.PriorityId);
                    cmd.Parameters.AddWithValue("@Description", toDo.TaskDescription);
                    cmd.Parameters.AddWithValue("@CreatedTime", toDo.CreatedTime);
                    cmd.Parameters.AddWithValue("@IsCompleted", toDo.IsCompleted);
                    cmd.Parameters.AddWithValue("@IsDeleted", 0);

                    cmd.ExecuteNonQuery();
                }

                finally
                {
                    sqlCon.Close();
                }
            }
        }

        /// <summary>
        /// Delete ToDo
        /// </summary>
        /// <param name="id"></param>
        public void DeleteToDo(ToDo toDo)
        {
            toDos.Remove(toDo);
        }

        /// <summary>
        /// Read all ToDo's
        /// </summary>
        public List<ToDo> ReadAllToDo()
        {

            using (var cmd = sqlCon.CreateCommand())
            {
                cmd.CommandText = "EXEC GetTodo";

                sqlCon.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    //Todo table
                    var indexOfColumn1 = reader.GetOrdinal("Todo_Id");
                    var indexOfColumn2 = reader.GetOrdinal("Contact_Id");
                    var indexOfColumn3 = reader.GetOrdinal("Priority_Id");
                    var indexOfColumn4 = reader.GetOrdinal("Description");
                    var indexOfColumn5 = reader.GetOrdinal("CreatedTime");
                    var indexOfColumn6 = reader.GetOrdinal("IsCompleted");

                    //Priority table
                    var indexOfColumn7 = reader.GetOrdinal("Priority");

                    while (reader.Read())
                    {
                        //Todo table
                        int TodoId = Convert.ToInt32(reader.GetValue(indexOfColumn1));
                        int ContactId = Convert.ToInt32(reader.GetValue(indexOfColumn2));
                        int PriorityId = Convert.ToInt32(reader.GetValue(indexOfColumn3));
                        string Description = reader.GetValue(indexOfColumn4).ToString();
                        DateTime CreatedAt = Convert.ToDateTime(reader.GetValue(indexOfColumn5));
                        bool IsCompleted = Convert.ToBoolean(reader.GetValue(indexOfColumn6));

                        //Priority table
                        string Priority = (reader.GetValue(indexOfColumn7).ToString());

                        toDos.Add(new ToDo(ContactId, PriorityId, Description) { ID = TodoId, CreatedTime = CreatedAt, Priority = Priority, IsCompleted = IsCompleted });

                    }

                }

            }
            sqlCon.Close();
            return toDos;
        }

        public List<Contact> ReadContacts()
        {
            using (var cmd = sqlCon.CreateCommand())
            {
                cmd.CommandText = "EXEC GetContact";

                sqlCon.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    //Contact table
                    var indexOfColumn1 = reader.GetOrdinal("Contact_Id");
                    var indexOfColumn2 = reader.GetOrdinal("Name");

                    while (reader.Read())
                    {
                        //Contact table
                        int ContactId = Convert.ToInt32(reader.GetValue(indexOfColumn1));
                        string Name = (reader.GetValue(indexOfColumn2).ToString());

                        contacts.Add(new Contact { Id = ContactId, Name = Name });

                    }

                }
                sqlCon.Close();

                return contacts;

            }
        }

        public List<Priority> ReadPriorities()
        {
            using (var cmd = sqlCon.CreateCommand())
            {
                cmd.CommandText = "EXEC GetPriority";

                sqlCon.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    //Contact table
                    var indexOfColumn1 = reader.GetOrdinal("Priority_Id");
                    var indexOfColumn2 = reader.GetOrdinal("Priority");

                    while (reader.Read())
                    {
                        //Contact table
                        int PriorityId = Convert.ToInt32(reader.GetValue(indexOfColumn1));
                        string Priority = (reader.GetValue(indexOfColumn2).ToString());

                        priorities.Add(new Priority { PrioId = PriorityId, Prio = Priority });

                    }

                }
                sqlCon.Close();

                return priorities;

            }
        }
        /// <summary>
        /// Update ToDo
        /// </summary>
        /// <param name="id"></param>
        public void UpdateToDo(ToDo obj)
        {
            int index = toDos.FindIndex(x => x.ID == obj.ID);
            toDos[index] = obj;
        }

        /// <summary>
        /// Find ToDo with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ToDo FindToDo(int id)
        {
            ToDo obj = toDos.Find(x => x.ID == id);
            return obj;
        }
    }
}
