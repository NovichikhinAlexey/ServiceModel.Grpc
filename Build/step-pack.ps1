. (Join-Path $PSScriptRoot ".\step-pack-scripts.ps1")

$sourceDir = [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot "..\Sources"))
$binDir = [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot "..\build-out"))
$repositoryCommitId = $env:GITHUB_SHA

# ServiceModel.Grpc
$projectFile = Join-Path $sourceDir "ServiceModel.Grpc\ServiceModel.Grpc.csproj"
dotnet pack `
    -c Release `
    --no-build `
    -p:RepositoryCommit=$repositoryCommitId `
    -o $binDir `
    $projectFile

# ServiceModel.Grpc.AspNetCore
$projectFile = Join-Path $sourceDir "ServiceModel.Grpc.AspNetCore\ServiceModel.Grpc.AspNetCore.csproj"
dotnet pack `
    -c Release `
    --no-build `
    -p:RepositoryCommit=$repositoryCommitId `
    -o $binDir `
    $projectFile

# ServiceModel.Grpc.DesignTime
$projectFile = Join-Path $sourceDir "ServiceModel.Grpc.DesignTime\ServiceModel.Grpc.DesignTime.csproj"
dotnet pack `
    -c Release `
    --no-build `
    -p:RepositoryCommit=$repositoryCommitId `
    -o $binDir `
    $projectFile

# ServiceModel.Grpc.SelfHost
$projectFile = Join-Path $sourceDir "ServiceModel.Grpc.SelfHost\ServiceModel.Grpc.SelfHost.csproj"
dotnet pack `
    -c Release `
    --no-build `
    -p:RepositoryCommit=$repositoryCommitId `
    -o $binDir `
    $projectFile

# ServiceModel.Grpc.ProtoBufMarshaller
$projectFile = Join-Path $sourceDir "ServiceModel.Grpc.ProtoBufMarshaller\ServiceModel.Grpc.ProtoBufMarshaller.csproj"
dotnet pack `
    -c Release `
    --no-build `
    -p:RepositoryCommit=$repositoryCommitId `
    -o $binDir `
    $projectFile