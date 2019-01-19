FROM microsoft/dotnet:2.1-sdk
WORKDIR /testwebapp

COPY . ./

RUN dotnet publish -c Release -o out

WORKDIR /testwebapp/out

ENTRYPOINT ["dotnet", "TestWebApp.dll"]
