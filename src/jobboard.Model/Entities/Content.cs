using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jobboard.Model.Entities
{
    public class Content : IEntityBase
    {
        public int Id { set; get; }
        public string Text { set; get; }
        public int JobId { set; get; }
        public Job Job { set; get; }
    }
}
