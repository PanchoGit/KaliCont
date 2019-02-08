#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.2-aspnetcore-runtime-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk-nanoserver-1803 AS build
WORKDIR /src

COPY ["Kali.WebApi/Kali.WebApi.csproj", "Kali.WebApi/"]
COPY ["Kali.Common/Kali.Common.csproj", "Kali.Common/"]
COPY ["Kali.Domain/Kali.Domain.csproj", "Kali.Domain/"]
COPY ["Kali.Model/Kali.Model.csproj", "Kali.Model/"]
COPY ["Kali.Security/Kali.Security.csproj", "Kali.Security/"]
COPY ["Kali.Workflow/Kali.Workflow.csproj", "Kali.Workflow/"]

RUN dotnet restore "Kali.WebApi/Kali.WebApi.csproj"
COPY . .
WORKDIR "/src/Kali.WebApi"
RUN dotnet build "Kali.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Kali.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
COPY Kali.ClientWeb/dist c:/app/wwwroot
ENTRYPOINT ["dotnet", "Kali.WebApi.dll"]