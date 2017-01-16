using jobboard.Data.Abstract;
using jobboard.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jobboard.Data.Repositories
{
    public class SkillRepository : EntityBaseRepository<Skill>, ISkillRepository
    {
        public SkillRepository(JobBoardContext context) : base(context)
        {
        }

        public IEnumerable<Skill> GetTop10()
        {
            return _context.Skills.Include(s => s.JobSkills).OrderByDescending(s => s.JobSkills.Count).Take(10);
        }

        public IEnumerable<Skill> GetSkills(int skip, int count)
        {
            return _context.Skills.Skip(skip).Take(count).ToList();
        }

        public Skill GetSkill(int id)
        {
            return _context.Skills.FirstOrDefault(s => s.Id == id);
        }
    }
}
