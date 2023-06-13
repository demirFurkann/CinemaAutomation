using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Report : BaseEntity
    {
        public DateTime StartedDate { get; set; }
        public DateTime EndDate { get; set; }
        public ReportType Type { get; set; }

        public Report()
        {
            ReportItems = new List<ReportItem>();
        }

        //Relational Properties
        public virtual List<ReportItem> ReportItems { get; set; }

    }
}
