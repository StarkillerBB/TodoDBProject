using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Razer_H2.Modul;
using Razer_H2.Repository;



namespace Razer_H2.Pages
{
    public class IndexModel : PageModel
    {

        private readonly IToDoRepository _doRepository;



        public IndexModel(IToDoRepository doRepository)
        {
            _doRepository = doRepository;
        }

        //---------------Get-----------------
        [BindProperty]
        public IList<ToDo> ToDos { get; set; }

        public List<SelectListItem> ContactsId {get; set;}

        public List<SelectListItem> PrioritiesId { get; set; }


        //---------------Add-----------------
        [BindProperty, Required, MaxLength(24)]
        public string TextDescrip { get; set; }

        [BindProperty]
        public int RadioPriority { get; set; }

        public Array PriorityList => Enum.GetValues(typeof(int));

        //---------------Edit-----------------
        [BindProperty]
        public List<int> IsChecked { get; set; }

        [BindProperty]
        public int ToPriority { get; set; }

        [BindProperty]
        public ToDo Todo { get; set; }



        /// <summary>
        /// on startup load list
        /// </summary>
        public void OnGet()
        {
            ToDos = _doRepository.ReadAllToDo();
            ContactsId = _doRepository.ReadContacts().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList();
            PrioritiesId = _doRepository.ReadPriorities().Select(c => new SelectListItem { Value = c.PrioId.ToString(), Text = c.Prio }).ToList();

            ToDos = ToDos.Where(x => x.IsCompleted == false).OrderBy(x => x.CreatedTime).ToList();
        }


        /// <summary>
        /// If checkbox is checked change isCompleted to true
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPostIsChecked()
        {

            foreach (var item in IsChecked)
            {
                ToDo to = _doRepository.FindToDo(item);
                to.IsCompleted = true;
                _doRepository.UpdateToDo(to);
            }

            ToDos = _doRepository.ReadAllToDo();
            ToDos = ToDos.Where(x => x.IsCompleted == false).OrderBy(x => x.CreatedTime).ToList();

            return RedirectToPage("/Index");
        }


        /// <summary>
        /// Add Todo
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPostAdd()
        {
            ToDo todo = new ToDo { ContactId = Todo.ContactId, PriorityId = Todo.PriorityId ,TaskDescription = TextDescrip };

            _doRepository.CreateToDo(todo);

            ToDos = _doRepository.ReadAllToDo();
            ToDos = ToDos.Where(x => x.IsCompleted == false).OrderByDescending(x => x.CreatedTime).ToList();

            return RedirectToPage("/Index");
        }

        /// <summary>
        /// Delete Todo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult OnPostDelete(int id)
        {
            ToDo todo = _doRepository.FindToDo(id);
            _doRepository.DeleteToDo(todo);

            ToDos = _doRepository.ReadAllToDo();

            ToDos = ToDos.Where(x => x.IsCompleted == false).OrderBy(x => x.CreatedTime).ToList();

            return RedirectToPage("/Index");
        }

        public IActionResult OnPostLoad()
        {
            ToDos = _doRepository.ReadAllToDo();
            ToDos = ToDos.Where(x => x.IsCompleted == true).OrderBy(x => x.CreatedTime).ToList();

            return Page();
        }

        public IActionResult OnPostBack()
        {
            return RedirectToPage("/Index");
        }

    }
}
