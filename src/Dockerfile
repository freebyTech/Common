FROM microsoft/dotnet:2.1-sdk
WORKDIR /lib

# copy csproj and restore as distinct layers so we break cache as little as possible
COPY ./freebyTech.Common/*.csproj ./freebyTech.Common/
WORKDIR /lib/freebyTech.Common
RUN dotnet restore

COPY ./freebyTech.Common.Tests/*.csproj ./freebyTech.Common.Tests/
WORKDIR /lib/freebyTech.Common.Tests
RUN dotnet restore

# copy everything else and build library
WORKDIR /lib/freebyTech.Common
COPY ./freebyTech.Common/. .
RUN dotnet build -o

# copy unit test project and run tests
WORKDIR /lib/freebyTech.Common.Tests
COPY ./freebyTech.Common.Tests/. .
RUN dotnet build
RUN dotnet test

WORKDIR /lib/freebyTech.Common
RUN dotnet build -c Debug -o /lib/debug
RUN dotnet pack -c Debug --include-symbols --include-source -o /lib/nuget_d 
RUN dotnet build -c Release -o /lib/release
RUN dotnet pack -c Release -o /lib/nuget