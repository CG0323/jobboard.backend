using System;
using System.Collections.Generic;
using System.Linq;
using jobboard.Data.Abstract;
using jobboard.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using jobboard.backend.ViewModels;
using jobboard.backend.Core;
using jobboard.backend.Core.Services;

namespace jobboard.backend.Controllers
{
    [Route("api/[controller]")]
    public class SkillsController : Controller
    {
        private ISkillRepository _skillRepository;
        private IWorkerService _workerService;
        int offset = 0;
        int pageSize = 1000;
        public SkillsController(ISkillRepository skillRepository, IWorkerService workerService)
        {
            _skillRepository = skillRepository;
            _workerService = workerService;
        }

        // GET all skills
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
            var totalSkills= _skillRepository.Count();

            IEnumerable<Skill> skills = _skillRepository.GetSkills(currentOffset, currentPageSize);
            IEnumerable<SkillDto> skillsVM = Mapper.Map<IEnumerable<Skill>, IEnumerable<SkillDto>>(skills);
            Response.AddPagination(totalSkills);

            return new OkObjectResult(skillsVM);
        }

        [HttpGet("top10")]
        public IActionResult GetTop10()
        {
            IEnumerable<Skill> skills = _skillRepository
                .GetTop10().ToList();

            IEnumerable<SkillDto> skillsVM = Mapper.Map<IEnumerable<Skill>, IEnumerable<SkillDto>>(skills);

            return new OkObjectResult(skillsVM);
        }

        [HttpGet("{id}", Name = "GetSkill")]
        public IActionResult Get(int id)
        {
            var skill = _skillRepository.GetSkill(id);
            if (skill == null)
                return NotFound();
            var skillVM = Mapper.Map<Skill, SkillDto>(skill);
            return new OkObjectResult(skillVM);
        }

        [HttpPost]
        public IActionResult Create([FromBody]SkillDto skillVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var skill = new Skill { Name = skillVM.Name, IsReg = skillVM.IsReg, KeyWords = skillVM.KeyWords};
            _skillRepository.Add(skill);
            _skillRepository.Commit();
            _workerService.RegisterTask("skill", skill.Id);
            skillVM = Mapper.Map<Skill, SkillDto>(skill);
            CreatedAtRouteResult result = CreatedAtRoute("GetSkill", new { controller = "Skills", id = skillVM.Id }, skillVM);
            return result;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]SkillDto skillVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Skill skill = _skillRepository.GetSkill(id);

            if (skill == null)
            {
                return NotFound();
            }
            else
            {
                skill.Name = skillVM.Name;
                skill.KeyWords = skillVM.KeyWords;
                _skillRepository.Commit();
            }
            _workerService.RegisterTask("skill", skill.Id);

            return NoContent();
        }

        // DELETE api/skill/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var skill = _skillRepository.GetSkill(id);
            if (skill == null)
                return new NotFoundResult();
            var skillId = skill.Id;
            _skillRepository.Delete(skill);
            _skillRepository.Commit();
            _workerService.RegisterTask("clean", skillId);
            return NoContent();
        }

        // DELETE api/skills/all
        [HttpDelete("all")]
        public IActionResult DeleteAll()
        {
            _skillRepository.DeleteWhere(x => true);
            _skillRepository.Commit();
            return NoContent();
        }
    }
}
