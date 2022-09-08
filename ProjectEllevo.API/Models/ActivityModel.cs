using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectEllevo.API.Models
{
    public class ActivityModel
    {
        public string TaskId { get; set; }
        public string ActivityId { get; set; }
        public string ActivityTitle { get; set; }
        public string ActivityDescription { get; set; }

    }
}
