using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace jobboard.Model.Entities
{
    public class Skill : IEntityBase
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string KeyWords { set; get; }
        public bool IsReg { set; get; }
        public ICollection<JobSkill> JobSkills { set; get; }
    }
}
