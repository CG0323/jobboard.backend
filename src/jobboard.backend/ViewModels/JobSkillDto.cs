using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jobboard.backend.ViewModels
{
    public class JobSkillDto
    {
        public int Id { set; get; }
        public int JobId { set; get; }
        public int SkillId { set; get; }
        public int Level { set; get; }
        public SkillDto Skill { set; get; }
    }
}
