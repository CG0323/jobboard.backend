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
This application has the following data models:  
![Data Model](img/datamodel.png)  
* Job: Represent the core information of a job post.  
* Content: Represent the raw text information of a job post (its job description and requirements)  
* Skill: Represent a skill, such as `.NET`,`Java`, and its matching config (keywords based / regex based)  
* JobSkill: Skill required by a specific job, and the required level of experience  

User add __Skill__ along with the matching keywords/regex via frontend app.    
The Jobboard.Scraper retrieve info from recruitment websites, post the __Job__ and __Content__ to this backend, 
then the backend trigger the Jobboard.Analyzer to extract __JobSkill__ from the Content and write to the database. 

####Handle mayny to many relation
Many-to-many relationships without an entity class to represent the join table are not yet supported by EF Core. So a joining table entity __JobSkill__
is required as a bridge :   
In both __Job__ and __Skill__, there is a navigation property:
```C#
public ICollection<JobSkill> JobSkills { set; get; }
```
In __JobSkill__
```C#
public class JobSkill : IEntityBase
{
    public int Id { set; get; }
    public int JobId { set; get; }
    public Job Job { set; get; }
    public int SkillId {set;get;}
    public Skill Skill { set; get; }
    public int Level { set; get; }
}
```
The relations need to be specified in DbContext OnModelCreating():
```C#
modelBuilder.Entity<Job>()
            .HasMany(j => j.JobSkills)
            .WithOne(js => js.Job)
            .HasForeignKey(js => js.JobId);

modelBuilder.Entity<Skill>()
            .HasMany(s => s.JobSkills)
            .WithOne(js => js.Skill)
            .HasForeignKey(js => js.SkillId);
```
####Handle very long string datatable column  
The `Text` column in __Content__ is a very long string (detailed description of a job and its requirement), by default the `string` 
in EF will be mapped to nvarchar in MySql, which is not enough. Strangely, even if I add `.HasMaxLength(100000)` it still does not map to MySql `text`.
It works only after explicitely specify the column type:
```C#
modelBuilder.Entity<Content>()
            .Property(c => c.Text)
            .HasColumnType("text")
            .HasMaxLength(100000)
            .IsRequired();
```
###Startup configuration  
In ASP.NET Core, essential configurations are in Startup.cs file, basically it does 2 important things there: 1. Register services for dependency injection 
2. Configure HTTP pipeline  
Below is some lesson learned / good practices  
####Enable CORS
Due to the separation of frontend and backend in this project, CORS must be enabled. To enable CORS in ASP.NET Core, 2 steps are needed:  
```C#
public void ConfigureServices(IServiceCollection services)
{
    ......
    // Enable Cors
    services.AddCors();
    ......
}
```
```C#
public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
{
    ......        
    app.UseCors(builder =>
                builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
    ......
}
```
####Use a global exception handler
Instead of polluting the code with try/catch blocks everywhere, in ASP.NET Core we could add a global Exception Handler into the pipeline.
```C#
app.UseExceptionHandler(
    builder =>
    {
        builder.Run(
            async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                var error = context.Features.Get<IExceptionHandlerFeature>();
                if (error != null)
                {
                    context.Response.AddApplicationError(error.Error.Message); 
                    await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                }
            });
    });
```
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

