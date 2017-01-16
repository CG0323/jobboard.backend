using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jobboard.backend.Core.Services
{
    public interface IWorkerService
    {
        void RegisterTask(string type, int id);
    }
}
