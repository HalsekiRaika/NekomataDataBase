string buildConfiguration = Argument("configuration", "Release");
DirectoryPath baseBuildDir = DirectoryPath.FromString("./out/NekomataServer/");
DirectoryPath collectorBuildDir = baseBuildDir.Combine("Controller/");

Task("buildLauncher").Does(() => {
    Information("Build Launcher...");
    DotNetCoreBuild("./Launcher/Launcher.csproj", new DotNetCoreBuildSettings(){
        Configuration = buildConfiguration,
        OutputDirectory = baseBuildDir
    });
}).Finally(() => { Information("Build Successfully! : Launcher"); });

Task("buildConfigEditor").IsDependentOn("buildLauncher").Does(() => {
    Information("Build ConfigEditor...");
    DotNetCoreBuild("./ConfigEditor/ConfigEditor.csproj", new DotNetCoreBuildSettings(){
        Configuration = buildConfiguration,
        OutputDirectory = baseBuildDir
    });
}).Finally(() => { Information("Build Successfully! : ConfigEditor"); });

Task("buildYoutubeDataCollector").IsDependentOn("buildConfigEditor").Does(() => {
    Information("Build YtDataCollector...");
    DotNetCoreBuild("./YtDataCollector/YtDataCollector.csproj", new DotNetCoreBuildSettings(){
        Configuration = buildConfiguration,
        OutputDirectory = collectorBuildDir
    });
}).Finally(() => { Information("Build Successfully! : YtDataCollector"); });

Task("createOutputDir").Does(() => {
    CreateDirectory(collectorBuildDir);
});

Task("buildRun").IsDependentOn("createOutputDir")
    .IsDependentOn("buildYoutubeDataCollector")
    .Does(() => {
    Information("Nekomata Server App Built.");
});

RunTarget("buildRun");