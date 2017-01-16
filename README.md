# jobboard.backend
A REST data service built with ASP.NET Core + EntityFramework Core + MySql

------
##Source Code Structure  
```
|-- src
    |-- jobboard.backend
    |   |-- appsettings.json
    |   |-- fabfile.py
    |   |-- jobboard.backend.xproj
    |   |-- Program.cs
    |   |-- project.json
    |   |-- Startup.cs
    |   |-- web.config
    |   |-- Controllers
    |   |   |-- JobsController.cs
    |   |   |-- SkillsController.cs
    |   |-- Core
    |   |   |-- Extensions.cs
    |   |   |-- PaginationHeader.cs
    |   |   |-- Services
    |   |       |-- IWorkerService.cs
    |   |       |-- WorkerService.cs
    |   |-- Properties
    |   |   |-- launchSettings.json
    |   |-- ViewModels
    |   |   |-- ContentDto.cs
    |   |   |-- JobDetailDto.cs
    |   |   |-- JobDto.cs
    |   |   |-- JobSkillDto.cs
    |   |   |-- SkillDto.cs
    |   |   |-- SkillDtoValidator.cs
    |   |   |-- Mappings
    |   |       |-- AutoMappingConfiguration.cs
    |   |       |-- DomainToDtoMappingProfile.cs
    |   |       |-- DtoToDomainModelMappingProfile.cs
    |-- jobboard.Data
    |   |-- jobboard.Data.xproj
    |   |-- JobBoardContext.cs
    |   |-- project.json
    |   |-- Abstract
    |   |   |-- IEntityBaseRepository.cs
    |   |   |-- IRepositories.cs
    |   |-- Properties
    |   |   |-- AssemblyInfo.cs
    |   |-- Repositories
    |       |-- EntityBaseRepository1.cs
    |       |-- JobRepository.cs
    |       |-- SkillRepository.cs
    |-- jobboard.Model
        |-- IEntityBase.cs
        |-- jobboard.Model.xproj
        |-- jobboard.Model.xproj.user
        |-- project.json
        |-- Entities
        |   |-- Content.cs
        |   |-- Job.cs
        |   |-- JobSkill.cs
        |   |-- Skill.cs
        |-- Properties
            |-- AssemblyInfo.cs
```

