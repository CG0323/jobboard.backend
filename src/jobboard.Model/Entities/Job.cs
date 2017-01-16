using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jobboard.Model.Entities
{
    public class Job : IEntityBase
    {
        public int Id{ set; get; }
        public DateTime PostAt { set; get; }
        public DateTime ReadAt { set; get; }
        public string Title { set; get; }
        public string Employer { set; get; }
        public string Province { set; get; }
        public string City { set; get; }
        public string Url { set; get; }
        public Content Content { set; get; }
        public bool Analyzed { set; get; }
        public ICollection<JobSkill> JobSkills { set; get; }
    }
}
