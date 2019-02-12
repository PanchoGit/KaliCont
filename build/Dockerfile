FROM microsoft/dotnet:2.2-aspnetcore-runtime-nanoserver-1803

WORKDIR /app

EXPOSE 80
EXPOSE 443

COPY . .

ENTRYPOINT ["dotnet", "Kali.WebApi.dll"]