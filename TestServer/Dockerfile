FROM microsoft/dotnet:2.1-sdk
WORKDIR /testserver

# Copy project files
COPY *.cs ./
COPY *.csproj ./

RUN dotnet publish -c Release -o out

WORKDIR /testserver/out

ENTRYPOINT ["dotnet", "TestServer.dll", "9430"]
