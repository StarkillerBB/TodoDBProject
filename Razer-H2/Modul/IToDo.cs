using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Razer_H2.Modul
{
    public interface IToDo
    {
        int ID { get; set; }

        int ContactId { get; set; }

        public int PriorityId { get; set; }

        DateTime CreatedTime { get; set; }

        string TaskDescription { get; set; }

        string Priority { get; set; }

        bool IsCompleted { get; set; }

    }
}
