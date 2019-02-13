FROM stefanscherer/node-windows AS build-node
WORKDIR /ClientApp
COPY ./Kali.ClientWeb/ .
RUN npm install -g @angular/cli
RUN npm install
RUN ng build --prod

FROM microsoft/dotnet:2.2-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "Kali.WebApi/Kali.WebApi.csproj"
COPY --from=build-node /ClientApp/dist ./Kali.WebApi/wwwroot
RUN dotnet publish "Kali.WebApi/Kali.WebApi.csproj" -c Release -o /deploy

FROM microsoft/dotnet:2.2-aspnetcore-runtime-nanoserver-1803
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY --from=build ./deploy .
ENTRYPOINT ["dotnet", "Kali.WebApi.dll"]