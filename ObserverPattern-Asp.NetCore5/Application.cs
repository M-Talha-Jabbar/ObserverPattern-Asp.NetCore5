using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPattern_Asp.NetCore5
{
    public class Application
    {
        public int JobId { get; set; }
        public string ApplicantName { get; set; }

        public Application(int jobId, string applicantName)
        {
            JobId = jobId;
            ApplicantName = applicantName;
        }
    }
}
