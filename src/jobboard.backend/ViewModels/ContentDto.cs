using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jobboard.backend.ViewModels
{
    public class ContentDto
    {
        public int Id { set; get; }
        public string Text { set; get; }
        public int JobId { set; get; }
        public JobDto Job { set; get; }
    }
}
