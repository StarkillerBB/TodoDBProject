using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Razer_H2.Modul;
using System.Data;


namespace Razer_H2.Repository
{
    public class ToDoRepository : IToDoRepository
    {
        private List<ToDo> toDos = new List<ToDo>();
        private List<Contact> contacts = new List<Contact>();
        private List<Priority> priorities = new List<Priority>();

        internal static IConfigurationRoot configuration { get; set; }
        static string connectionString;
        SqlConnection sqlCon;

        public ToDoRepository()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
            configuration = builder.Build();
            connectionString = configuration.GetConnectionString("DefaultConnection");
            sqlCon = new SqlConnection(connectionString);
        }


        /// <summary>
        /// Create new ToDo
        /// </summary>
        /// <param name="desc"></param>
        public void CreateToDo(ToDo toDo)
        {

            SqlCommand cmd = new SqlCommand("CreateTodo", sqlCon);
            cmd.CommandType = CommandType.StoredProcedure;
            sqlCon.Open();

            try
            {
                cmd.Parameters.AddWithValue("@Contact_Id", toDo.ContactId);
                cmd.Parameters.AddWithValue("@Priority_Id", toDo.PriorityId);
                cmd.Parameters.AddWithValue("@Description", toDo.TaskDescription);

                cmd.ExecuteNonQuery();
            }

            finally
            {
                sqlCon.Close();
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
            if (toDos != null)
            {
                toDos.Clear();
            }

            SqlCommand cmd = new SqlCommand("GetTodo", sqlCon);
            cmd.CommandType = CommandType.StoredProcedure;

            sqlCon.Open();
            try
            {
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

                        toDos.Add(new ToDo { ID = TodoId, ContactId = ContactId, PriorityId = PriorityId, CreatedTime = CreatedAt, Priority = Priority, IsCompleted = IsCompleted, TaskDescription = Description });

                    }

                }
            }
            finally
            {
                sqlCon.Close();
            }

            return toDos;
        }

        /// <summary>
        /// Loads all contacts into list
        /// </summary>
        /// <returns></returns>
        public List<Contact> ReadContacts()
        {
            if (contacts != null)
            {
                contacts.Clear();
            }

            SqlCommand cmd = new SqlCommand("GetContact", sqlCon);
            cmd.CommandType = CommandType.StoredProcedure;

            sqlCon.Open();
            try
            {
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
            }
            finally
            {
                sqlCon.Close();
            }


            return contacts;


        }

        /// <summary>
        /// Loads all priorities into radio buttons.
        /// </summary>
        /// <returns></returns>
        public List<Priority> ReadPriorities()
        {
            if (priorities != null)
            {
                priorities.Clear();
            }

            SqlCommand cmd = new SqlCommand("GetPriority", sqlCon);
            cmd.CommandType = CommandType.StoredProcedure;

            sqlCon.Open();

            try
            {
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
            }
            finally
            {
                sqlCon.Close();
            }
            return priorities;

        }
        /// <summary>
        /// Update ToDo
        /// </summary>
        /// <param name="id"></param>
        public void UpdateToDo()
        {

            SqlCommand cmd = new SqlCommand("UpdateTodo", sqlCon);
            cmd.CommandType = CommandType.StoredProcedure;
            sqlCon.Open();

            try
            {
                //cmd.Parameters.AddWithValue("@Id", );
                //cmd.Parameters.AddWithValue("@Priority_Id", toDo.PriorityId);
                //cmd.Parameters.AddWithValue("@Description", toDo.TaskDescription);

                cmd.ExecuteNonQuery();
            }

            finally
            {
                sqlCon.Close();
            }

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
