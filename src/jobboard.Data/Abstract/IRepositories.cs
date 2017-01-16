using jobboard.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jobboard.Data.Abstract
{
    public interface ISkillRepository : IEntityBaseRepository<Skill>
    {
        IEnumerable<Skill> GetSkills(int skip, int count);
        IEnumerable<Skill> GetTop10();
        Skill GetSkill(int id);
    }
    public interface IJobRepository : IEntityBaseRepository<Job>
    {
        IEnumerable<Job> GetPageWithSkills(int skip, int count);
        int CleanIrrevelant();
        Job GetJobWithDetail(int id);
    }


}
