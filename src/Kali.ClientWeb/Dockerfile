FROM microsoft/iis:nanoserver

RUN powershell -Command Remove-Item -Recurse C:\inetpub\wwwroot\*

COPY ./dist c:/inetpub/wwwroot
