# MyersAndStauffer_GhostQA




# Open Prem Deployment

**Deployment Guide: Deploying GhostQA Web Application**

**Prerequisites:**
- Docker must be installed on the target system.

**Deployment Steps:**

1. **Open PowerShell as User:**
    - Open PowerShell on your system with user privileges.
    - DO NOT OPEN AS ADMIN

2. **Download and Execute Deployment Script:**
    - Run the following command in PowerShell:
     - WINDOWS 
        ```powershell
        powershell -ExecutionPolicy Bypass -Command "(New-Object System.Net.WebClient).DownloadFile('https://raw.githubusercontent.com/Ghost-QA/GhostQA/main/deploy.ps1', '.\deploy.ps1'); .\deploy.ps1"
        ```
        Ubuntu
        ```sh
        wget -O - https://raw.githubusercontent.com/Ghost-QA/GhostQA/main/deploy.sh | bash
        ```

    This command will download the deployment script from the specified URL and execute it.

3. **Accessing the Deployed Web Application:**
    - After successful deployment, open the URL [http://127.0.0.1:30001](http://127.0.0.1:30001) in your web browser.
    This URL will allow you to access the documentation for the deployed GhostQA web application.

**Note:** 
- Ensure that you have internet connectivity during the deployment process to download the required resources.
- Make sure that no other services are running on port 8011 to avoid conflicts with the deployed GhostQA web application.
  
