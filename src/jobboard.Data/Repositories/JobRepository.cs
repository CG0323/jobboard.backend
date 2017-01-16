using jobboard.Data.Abstract;
using jobboard.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace jobboard.Data.Repositories
{
    public class JobRepository : EntityBaseRepository<Job>, IJobRepository
    {
        public JobRepository(JobBoardContext context) : base(context)
        {

        }

        public IEnumerable<Job> GetPageWithSkills(int skip, int count)
        {
            return _context.Jobs.Include(j => j.JobSkills).OrderByDescending(j => j.PostAt).Skip(skip).Take(count).ToList();
        }

        public Job GetJobWithDetail(int id)
        {
            return _context.Jobs.Include(j => j.JobSkills).Include(j => j.Content).FirstOrDefault(j => j.Id == id);
        }

        public int CleanIrrevelant()
        {
            var irrevelantJobs = _context.Jobs.Where(j => j.JobSkills.Count < 3).ToList();
            var count = irrevelantJobs.Count;
            _context.RemoveRange(irrevelantJobs);
            return count;
        }
    }
}
