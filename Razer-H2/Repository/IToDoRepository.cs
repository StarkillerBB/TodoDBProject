using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Razer_H2.Modul;

namespace Razer_H2.Repository
{
    public interface IToDoRepository
    {
        void CreateToDo(ToDo toDo);

        void UpdateToDo(ToDo obj);

        void DeleteToDo(ToDo toDo);

        List<ToDo> ReadAllToDo();

        ToDo FindToDo(int id);

        List<Contact> ReadContacts();

        List<Priority> ReadPriorities();

    }
}
