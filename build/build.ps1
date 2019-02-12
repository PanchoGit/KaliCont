$DeployApp = "./etc/deploys/app"
$ClientWebpath = "./src/Kali.ClientWeb"
$WebApipath = "./src/Kali.WebApi"

function PublishClientWeb(){
    Write-Host "Building ClientWeb" -foregroundcolor "green"
    cd $ClientWebpath
    npm install
    ng build --prod
    cd ../..
    Write-Host "(ClientWeb) Build Finished" -foregroundcolor "green"
}

function DeleteWwwRoot(){
    cd $WebApipath
    Remove-Item wwwroot\* -Force -Recurse
    cd ../..
}

function CopyClientWebToWwwRoot(){
    Write-Host "Copy ClientWeb to wwwroot" -foregroundcolor "green"
    Copy-Item -Path $ClientWebpath"/dist/*" -Destination $WebApipath"/wwwroot" -recurse -Force
    Write-Host "Copy finished" -foregroundcolor "green"
}

function CleanDeploy(){
    Write-Host "Clean previous deploys" -foregroundcolor "green"
    if (test-path $DeployApp)
    {
        Remove-Item -LiteralPath $DeployApp -Force -Recurse
    }
    Write-Host "Clean finished" -foregroundcolor "green"    
}

function PublishWebApi(){
    CleanDeploy
    Write-Host "Publish dotnet project" -foregroundcolor "DarkMagenta"
    cd $WebApipath
    dotnet restore
    Invoke-Expression "dotnet publish Kali.WebApi.csproj -c Release -o ./../../$DeployApp"
    cd ../..
    Write-Host "Publish finished" -foregroundcolor "DarkMagenta"    
}

function DockerBuildImage(){
    Write-Host "Dockerize" -foregroundcolor "Magenta"
    Copy-Item build/Dockerfile $DeployApp
    cd $DeployApp
    docker image build -t kali-app .
    cd ../../..
    Write-Host "Dockerize finished" -foregroundcolor "Magenta"
    Write-Host "Run >> docker run --rm -it -p 80:80 kali-app" -foregroundcolor "DarkCyan"
}

cd ..
PublishClientWeb
DeleteWwwRoot
CopyClientWebToWwwRoot
PublishWebApi
DockerBuildImage