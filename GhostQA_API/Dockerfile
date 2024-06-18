# Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
# For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

#installing chrome
RUN apt-get update \
    && apt-get install -y wget gnupg2 unzip\
    && wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | apt-key add - \
    && sh -c 'echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google-chrome.list' \
    && apt-get update \
    && apt-get install -y google-chrome-stable
# Install Chromedriver

# Change working directory
RUN mkdir -p /usr/local/bin
WORKDIR /usr/local/bin/

# Download ChromeDriver
RUN wget https://storage.googleapis.com/chrome-for-testing-public/125.0.6422.141/linux64/chromedriver-linux64.zip

# Unzip ChromeDriver
RUN apt-get install -y unzip && \
    unzip chromedriver-linux64.zip 
 
# Set execute permissions for ChromeDriver
RUN chmod +x /usr/local/bin/chromedriver-linux64/chromedriver

# Verify installation
CMD ["chromedriver", "--version"]

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GhostQA_API/GhostQA_API.csproj", "GhostQA_API/"]
COPY ["GhostQA/GhostQA_FrameworkTests/GhostQA_FrameworkTests.csproj", "GhostQA/GhostQA_FrameworkTests/"]
COPY ["GhostQA/GhostQA_Framework/GhostQA_Framework.csproj", "GhostQA/GhostQA_Framework/"]
RUN dotnet restore "GhostQA_API/GhostQA_API.csproj"
RUN dotnet restore "GhostQA/GhostQA_FrameworkTests/GhostQA_FrameworkTests.csproj"
RUN dotnet restore "GhostQA/GhostQA_Framework/GhostQA_Framework.csproj"
COPY . .
WORKDIR "/src/GhostQA_API"
RUN dotnet build "GhostQA_API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GhostQA_API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Install mssql-tools for sqlcmd
RUN apt-get update && \
    apt-get install -y curl gnupg2 && \
    curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && \
    curl https://packages.microsoft.com/config/debian/11/prod.list > /etc/apt/sources.list.d/mssql-release.list && \
    apt-get update && \
    ACCEPT_EULA=Y apt-get install -y mssql-tools unixodbc-dev

# Copy the SQL script
COPY ["GhostQA_API/SqlScript/AllGhostQA_SP.sql", "./SqlScript/AllGhostQA_SP.sql"]
COPY ["GhostQA_API/SqlScript/Insert_FirstUser.sql", "./SqlScript/Insert_FirstUser.sql"]

# Copy and set permissions for the entrypoint script
COPY ["GhostQA_API/entrypoint.sh", "/app/"]
RUN apt install ffmpeg -y
# Use the entrypoint script to start the application
RUN sed -i 's/\r//' /app/entrypoint.sh \
	&& chmod +x /app/entrypoint.sh
ENTRYPOINT ["/bin/bash","/app/entrypoint.sh" ,"dotnet","GhostQA_API.dll"]
# ENTRYPOINT ["dotnet" ,"GhostQA_API.dll"]
