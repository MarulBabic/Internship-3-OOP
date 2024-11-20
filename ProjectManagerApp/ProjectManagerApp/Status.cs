using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerApp
{
    public enum ProjectStatus
    {
        Active,
        Pending,
        Finished
    }

    public enum TaskStatus
    {
        Active,
        Finished,
        Delayed
    }

    public enum Priority
    {
        High,
        Medium,
        Low
    }
}
