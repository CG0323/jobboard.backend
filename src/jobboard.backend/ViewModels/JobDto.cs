using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jobboard.backend.ViewModels
{
    public class JobDto
    {
        public int Id { set; get; }
        public DateTime PostAt { set; get; }
        public DateTime ReadAt { set; get; }
        public string Title { set; get; }
        public string Employer { set; get; }
        public string Province { set; get; }
        public string City { set; get; }
        public string Url { set; get; }
        public bool Analyzed { set; get; }
        public ContentDto Content { set; get; }
        public ICollection<JobSkillDto> Skills { set; get; }

    }
}
