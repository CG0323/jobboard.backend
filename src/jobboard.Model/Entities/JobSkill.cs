using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jobboard.Model.Entities
{
    public class JobSkill : IEntityBase
    {
        public int Id { set; get; }
        public int JobId { set; get; }
        public Job Job { set; get; }
        public int SkillId {set;get;}
        public Skill Skill { set; get; }
        public int Level { set; get; }
    }
}
