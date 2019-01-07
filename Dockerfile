FROM microsoft/dotnet:2.1-sdk
WORKDIR /srvchkr

# Copy a whole project
COPY . ./

# RUN dotnet restore

RUN dotnet publish -c Release -o out

WORKDIR /srvchkr/ServicesChecker/out

ENTRYPOINT ["dotnet", "ServicesChecker.dll"]