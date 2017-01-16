using jobboard.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jobboard.Data
{
    public class JobBoardDbInitializer
    {
        private static JobBoardContext _context;
        public static void Initialize(IServiceProvider serviceProvider)
        {
            _context = (JobBoardContext)serviceProvider.GetService(typeof(JobBoardContext));

            InitializeJobBoard();
        }

        private static void InitializeJobBoard()
        {
            if (!_context.Skills.Any())
            {
                var skill = new Skill() { Name = "Linux", IsReg = false, KeyWords = "linux,centos,red hat,redhat,rhel,ubuntu,fedora" };
                _context.Skills.Add(skill);
                _context.SaveChanges();
            }
        }
    }
}
