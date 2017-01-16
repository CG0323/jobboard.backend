# jobboard.backend
A REST data service built with ASP.NET Core + EntityFramework Core + MySql

------
##Source code structure  
```
|-- src
    |-- jobboard.backend
    |   |-- appsettings.json          // contains configuration string such as db connection
    |   |-- fabfile.py                // python script for auto deploy with fabric
    |   |-- jobboard.backend.xproj
    |   |-- Program.cs
    |   |-- project.json
    |   |-- Startup.cs   // configure http pipeline, register services
    |   |-- web.config
    |   |-- Controllers  // do not need content and skill controllers
    |   |   |-- JobsController.cs
    |   |   |-- SkillsController.cs
    |   |-- Core
    |   |   |-- Extensions.cs
    |   |   |-- PaginationHeader.cs
    |   |   |-- Services
    |   |       |-- IWorkerService.cs
    |   |       |-- WorkerService.cs  // the service to trigger analyzer task (async call to flask)
    |   |-- Properties
    |   |   |-- launchSettings.json
    |   |-- ViewModels      // data transfer objects
    |   |   |-- ContentDto.cs
    |   |   |-- JobDetailDto.cs
    |   |   |-- JobDto.cs
    |   |   |-- JobSkillDto.cs
    |   |   |-- SkillDto.cs
    |   |   |-- SkillDtoValidator.cs
    |   |   |-- Mappings    // configuration for AutoMapper
    |   |       |-- AutoMappingConfiguration.cs
    |   |       |-- DomainToDtoMappingProfile.cs
    |   |       |-- DtoToDomainModelMappingProfile.cs
    |-- jobboard.Data
    |   |-- jobboard.Data.xproj
    |   |-- JobBoardContext.cs  // ef context for the whole project
    |   |-- project.json
    |   |-- Abstract
    |   |   |-- IEntityBaseRepository.cs
    |   |   |-- IRepositories.cs
    |   |-- Properties
    |   |   |-- AssemblyInfo.cs
    |   |-- Repositories
    |       |-- EntityBaseRepository.cs
    |       |-- JobRepository.cs
    |       |-- SkillRepository.cs
    |-- jobboard.Model          //data model project
        |-- IEntityBase.cs
        |-- jobboard.Model.xproj
        |-- project.json
        |-- Entities
        |   |-- Content.cs      // data model for the raw text of a job post
        |   |-- Job.cs          // data model for a job post
        |   |-- JobSkill.cs     // data model for the multi-multi relation between Job and Skill
        |   |-- Skill.cs        // data model for a skill, it has name and keywords
        |-- Properties
            |-- AssemblyInfo.cs
```
##Implementation key points
This section records some key points (or lesson learned) in the implementation, for future reference~

-----------
###Code first database design
TODO
####Handle multi to multi relation
TODO
####Handle very long string datatable column
TODO
####Generic repository pros & cons
TODO
###Startup configuration
TODO
####Enable CORS
TODO
####Use a global exception handler
TODO
####Specify EntityBaseRepository migration assembly
TODO
####Configure Automapper mapping strategy
TODO
##Deployment key points
This section records some key points for deploying asp.net core on linux, for future reference~

-----------
###Install .NET Core runtime
TODO
###Start .Net Core Application
TODO
###Configure Nginx as reverse proxy
TODO
###Use supervisord to daemon the app server
TODO
###Use Python Fabric to automate the deployment
TODO

