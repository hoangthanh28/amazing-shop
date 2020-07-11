//#tool "nuget:https://www.nuget.org/api/v2?package=JetBrains.ReSharper.CommandLineTools&version=2018.1.0"
//#tool "nuget:https://www.nuget.org/api/v2?package=coveralls.io&version=1.4.2"
//#addin "nuget:https://www.nuget.org/api/v2?package=Cake.Coveralls&version=0.8.0"
//#addin "nuget:https://www.nuget.org/api/v2?package=Cake.Incubator&version=2.0.1"
#tool "nuget:?package=roundhouse"
///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument<string>("target", "Default");
var configuration = Argument<string>("configuration", "Release");
var output = Argument<string>("output", "output");
var sqlFolder = Argument<string>("sql", "sql");
var projectFiles = Argument<string>("projects", "./**/*.csproj").Split(',');
var connectionString = EnvironmentVariable("ConnectionString__Default")?? "Server=127.0.0.1;Database=Default;User=sa;Password=Pass1234$;TrustServerCertificate=True";
var masterConnectionString = EnvironmentVariable("ConnectionString__Master")?? "Server=127.0.0.1;Database=master;User=sa;Password=Pass1234$;TrustServerCertificate=True";

///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////

var projects = projectFiles.SelectMany( x=> GetFiles(x));
var projectPaths = projects.Select(project => project.GetDirectory().ToString());
var artifactsDir = "Artifacts";

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(context =>
{
    Information("Running tasks...");
    if (DirectoryExists(output))
    {
        var settings = new DeleteDirectorySettings {
            Recursive = true,
            Force = true
        };
        Information($"Cleaning path {output} ...");
        DeleteDirectory(output, settings);
    }
    CreateDirectory(output);
});

Teardown(context =>
{
    Information("Finished running tasks.");
    var settings = new DeleteDirectorySettings {
        Recursive = true,
        Force = true
    };
    // Delete artifact output too
    foreach(var path in projectPaths)
    {
        Information($"Cleaning path {path} ...");
        var dir = Directory($"{path}/{artifactsDir}");
        if (DirectoryExists(dir))
        {
            Information($"Cleaning path {dir} ...");
            DeleteDirectory(dir, settings);
        }
    }
});

///////////////////////////////////////////////////////////////////////////////
// TASK DEFINITIONS
///////////////////////////////////////////////////////////////////////////////

Task("Clean")
    .Description("Cleans all directories that are used during the build process.")
    .Does(() =>
{
    var settings = new DeleteDirectorySettings {
        Recursive = true,
        Force = true
    };
    // Clean solution directories.
    foreach(var path in projectPaths)
    {
        Information($"Cleaning path {path} ...");
        var directoriesToDelete = new DirectoryPath[]{
            Directory($"{path}/obj"),
            Directory($"{path}/bin"),
            Directory($"{path}/{artifactsDir}"),
        };
        foreach(var dir in directoriesToDelete)
        {
            if (DirectoryExists(dir))
            {
                DeleteDirectory(dir, settings);
            }
        }
    }
});

Task("Restore")
    .Description("Restores all the NuGet packages that are used by the specified solution.")
    .Does(() =>
{
    // Restore all NuGet packages.
    foreach(var path in projectPaths)
    {
        Information($"Restoring {path}...");
        DotNetCoreRestore(path);
    }
});

Task("Build")
    .Description("Builds all the different parts of the project.")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .DoesForEach(projectPaths, (project) => 
{
    var outDirectory = $"{project}/{artifactsDir}";
    var settings = new DotNetCoreBuildSettings
     {
         Configuration = configuration,
         OutputDirectory = outDirectory
     };

     DotNetCoreBuild(project, settings);
});

Task("Publish")
    .Description("Publish function")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .DoesForEach(projects, (p) => 
{
    var outDirectory = $"{p.GetDirectory()}/{artifactsDir}";
    var project = p.ToString();
    Information($"Publish to directory: {outDirectory}");
    var fileOutputName=  $"{outDirectory}/{p.GetFilename().ToString().Replace(".csproj", ".zip")}";
    Information($"Publish fileName: {fileOutputName}");
    var settings = new DotNetCorePublishSettings 
     {
         Configuration = configuration,
         OutputDirectory = outDirectory,
         NoRestore = true,
         //NoBuild = true
     };
    DotNetCorePublish(project, settings);
    Zip(outDirectory, fileOutputName);   
    MoveFileToDirectory(fileOutputName, output);
});


///////////////////////////////////////////////////////////////////////////////
// Migrate DB
///////////////////////////////////////////////////////////////////////////////

Task("MigrateDB")
    .Description("Migrate database")
    .Does(() =>
{
    // Create the migration database
    var settings = new RoundhouseSettings {
        ConnectionString = connectionString,
        ConnectionStringAdmin = masterConnectionString,
        DoNotCreateDatabase=false,
        Silent=true,
        Drop=false,
        Debug=false,
        WithTransaction=true,
        SqlFilesDirectory = sqlFolder
    };
    RoundhouseMigrate(settings);
});

///////////////////////////////////////////////////////////////////////////////
// Unit Tests
///////////////////////////////////////////////////////////////////////////////

Task("Test")
    .Description("Run all unit tests within the project.")
    .DoesForEach(projects, (p) => 
{
    if(p.GetFilename().ToString().ToLower().Contains("test")){
        Information($"Test project: {p.GetFilename()}");
        // Calculate code coverage
        var settings = new DotNetCoreTestSettings
        {
            Logger = "trx",
            ArgumentCustomization = args => args.Append("/p:CollectCoverage=true")
                                                .Append("/p:CoverletOutputFormat=cobertura")
                                                .Append($"/p:CoverletOutput={output}/tests")
                                                .Append("/p:Exclude=\"[Dapper.*]*\"")
                                                .Append("/p:Exclude=\"[Pipelines.Sockets.Unofficial.*]*\"")
                                                .Append("/p:Exclude=\"[StackExchange.Redis.*]*\"")
                                                .Append("/p:Exclude=\"[System.Interactive.Async]*\"")
                                                .Append("/p:ThresholdType=line")
        };
        DotNetCoreTest(p.ToString(), settings);
    }
});

///////////////////////////////////////////////////////////////////////////////
// CI
///////////////////////////////////////////////////////////////////////////////

// Task("CI")
//     .Description("Build the code, test and validate")
//     .IsDependentOn("Build");

// Task("CI-UNIX")
//     .Description("Build the code, test and validate")
//     .IsDependentOn("Build");

///////////////////////////////////////////////////////////////////////////////
// TARGETS
///////////////////////////////////////////////////////////////////////////////

Task("Default")
    .Description("This is the default task which will be ran if no specific target is passed in.")
    .IsDependentOn("Build");

///////////////////////////////////////////////////////////////////////////////
// EXECUTION
///////////////////////////////////////////////////////////////////////////////

RunTarget(target);