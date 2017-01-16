using AutoMapper;
using jobboard.backend.Core;
using jobboard.backend.Core.Services;
using jobboard.backend.ViewModels;
using jobboard.Data.Abstract;
using jobboard.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jobboard.backend.Controllers
{
    [Route("api/[controller]")]
    public class JobsController : Controller
    {
        private IJobRepository _jobRepository;
        private IWorkerService _workerService;
        int offset = 0;
        int pageSize = 10000;
        public JobsController(IJobRepository jobRepository, IWorkerService workerService)
        {
            _jobRepository = jobRepository;
            _workerService = workerService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var pagination = Request.Headers["Pagination"];

            if (!string.IsNullOrEmpty(pagination))
            {
                string[] vals = pagination.ToString().Split(',');
                int.TryParse(vals[0], out offset);
                int.TryParse(vals[1], out pageSize);
            }

            int currentOffset = offset;
            int currentPageSize = pageSize;
            var totalJobs = _jobRepository.Count();

            var jobs = _jobRepository.GetPageWithSkills(offset, pageSize).ToList();

            IEnumerable<JobDto> jobsVM = Mapper.Map<IEnumerable<Job>, IEnumerable<JobDto>>(jobs);

            Response.AddPagination(totalJobs);

            return new OkObjectResult(jobsVM);
        }

        [HttpPost]
        public IActionResult Create([FromBody]JobDto jobVM)
        {
            Console.Write(jobVM);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var job = Mapper.Map<JobDto, Job>(jobVM);
            _jobRepository.Add(job);
            _jobRepository.Commit();
            _workerService.RegisterTask("job", job.Id);

            return new OkResult();
        }

        [HttpDelete("clean")]
        public IActionResult Clean()
        {
            var removedCount = _jobRepository.CleanIrrevelant();
            _jobRepository.Commit();
            return new OkObjectResult(removedCount);
        }

        [HttpGet("{id}", Name = "GetJob")]
        public IActionResult Get(int id)
        {
            var job = _jobRepository.GetJobWithDetail(id);
            if (job == null)
                return NotFound();
            var jobDetailVM = Mapper.Map<Job, JobDetailDto>(job);
            return new OkObjectResult(jobDetailVM);
        }

        [HttpDelete("all")]
        public IActionResult DeleteAll()
        {
            _jobRepository.DeleteWhere(x => true);
            _jobRepository.Commit();
            return NoContent();
        }
    }
}
