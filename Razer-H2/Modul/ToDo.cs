using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Razer_H2.Modul
{
    public class ToDo : IToDo
    {
        //ToDo class felt
        public int ID { get; set; }
        public int ContactId { get; set; }
        public int PriorityId { get; set; }
        public DateTime CreatedTime { get; set; }
        public string TaskDescription { get; set; }
        public string Priority { get; set; }
        public bool IsCompleted { get; set; }

    }
}
