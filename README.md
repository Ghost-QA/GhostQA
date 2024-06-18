# GhostQA

## System Requirements

Before setting up GhostQA, ensure your system meets the following hardware and software requirements:

### Hardware Requirements

- **Windows OS**: 10 or later, or Windows Server 2016 or later
- **RAM**: Minimum 16 GB
- **SSD**: At least 250 GB with a minimum of 150 GB of available space
- **Admin Access**: Required for the machine, SQL Server, and installation of software with admin rights

### Software Requirements

To successfully set up and run GhostQA, ensure the following software is installed on your machine:

- **IIS**: Version 10 or later, install using the **Windows feature on/off** option with administrative permissions
- **SQL Server**: Version 18, 22, or latest

## On-Prem Setup Guide Using Batch File

### Instructions to Set Up GhostQA Using a .bat File

#### Step 1: Install IIS (If not already installed, please proceed to Step 2 if IIS is already installed)

##### To install Internet Information Services (IIS) using Windows Edition, follow these steps:

1. Open the launch section: Press the **Windows + R** key to open the launch section.  
   ![Windows + R](https://cloudzy.com/wp-content/uploads/winkey-R.png)

2. Type `appwiz.cpl` in the Run UI and hit Enter to open Programs and Features in Windows 10.  
   ![Programs and Features](https://cloudzy.com/wp-content/uploads/open-programs-and-features-by-run-420x218.png)

3. Click on `Turn Windows features on or off`. The Windows Features window will appear.  
   ![Windows Features](https://qvlyvrthdkqyff8ep3lxa.shekhartarare.com/SetupIIS/2_.png)

4. Ensure all features under Internet Information Services and Microsoft .NET Framework are selected.  
   ![IIS Features](https://www.c-sharpcorner.com/article/how-to-deploy-and-publish-a-net-7-app-in-iis/Images/1.png)

5. Click `OK` to install the selected Windows components, including IIS. Once IIS installation is done, **Restart** your machine.

6. To access IIS, click the Windows Start button, start typing `Internet Information Services Manager` in the search field, and click `Internet Information Services (IIS) Manager` once it appears.

7. Install the URL Rewrite module 2.1 from the following link: [URL Rewrite Module 2.1](https://www.iis.net/downloads/microsoft/url-rewrite).

8. Install the .NET Core Hosting Bundle by searching for it in any web browser and installing it from the following link: [Install .NET Core Hosting Bundle](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/hosting-bundle?view=aspnetcore-8.0#direct-download).

9. Download the `FFmpeg` file from the following link: [Download FFmpeg](https://ffmpeg.org/download.html). Extract the FFmpeg file in the `C:` directory for recording functionality.

##### To install Internet Information Services (IIS) using Windows Server Edition, follow these steps:

1. Open the launch section: Press the **Windows + R** key to open the launch section.  
   ![Windows + R](https://cloudzy.com/wp-content/uploads/winkey-R.png)

2. Type `ServerManager` in the launch box and click OK. This opens the Programs and Features box on Windows Server 2012 (or any other version you’re using).  
   ![Server Manager](https://cloudzy.com/wp-content/uploads/How-to-install-ISS-on-Windows-10-1.webp)

3. Add roles and features: Click on the `Add roles and features` in the newly opened window.  
   ![Add Roles and Features](https://cloudzy.com/wp-content/uploads/how-to-install-iis-on-windows-10-6.webp)

4. Installation Wizard: Now, the Installation Wizard will appear. Click `Next`.  
   ![Installation Wizard](https://cloudzy.com/wp-content/uploads/how-to-install-iis-on-windows-10-7.webp)

5. Select `Role-based or feature-based installation` and click Next.  
   ![Role-based Installation](https://cloudzy.com/wp-content/uploads/how-to-install-iis-on-windows-10-7-1.jpg)

6. Select `Select a server from the server pool` from the window, then `select Server` and click `Next`.  
   ![Select Server](https://cloudzy.com/wp-content/uploads/how-to-install-iis-on-windows-10-8.webp)

7. Check the Web Server (IIS) section: When you get to the Series Roles section, scroll down the list, then check the `Web Server (IIS)` section and hit `Next`.  
   ![Web Server (IIS)](https://cloudzy.com/wp-content/uploads/how-to-install-iis-on-windows-10-9.webp)

8. Click on `Add Features`, ensuring the checkbox `Install management tool (if available)` is selected.  
   ![Add Features](https://cloudzy.com/wp-content/uploads/how-to-install-iis-on-windows-10-10.webp)

9. Keep the default features selection as they already are, then click `Next`.  
   ![Default Features](https://cloudzy.com/wp-content/uploads/how-to-install-iis-on-windows-10-11.webp)

10. Click `Next` when you’re done with the Web Server Roles (IIS) text.  
    ![Web Server Roles](https://cloudzy.com/wp-content/uploads/how-to-install-iis-on-windows-10-12.webp)

11. Click `Next` after selecting the `Web Server roles` as shown in the following image.  
    ![Select Web Server Roles](https://cloudzy.com/wp-content/uploads/how-to-install-iis-on-windows-10-13.webp)

12. Click `Install`.  
    ![Install](https://cloudzy.com/wp-content/uploads/how-to-install-iis-on-windows-10-14.webp)

13. To access IIS, click the Windows Start button. Start typing `Internet Information Services Manager` in the search field and click `Internet Information Services (IIS) Manager` once it appears.

14. Install the URL Rewrite module 2.1 from the following link: [URL Rewrite Module 2.1](https://www.iis.net/downloads/microsoft/url-rewrite).

15. Install the .NET Core Hosting Bundle by searching for it in any web browser and installing it from the following link: [Install .NET Core Hosting Bundle](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/hosting-bundle?view=aspnetcore-8.0#direct-download).

16. Download the `FFmpeg` file from the following link: [Download FFmpeg](https://ffmpeg.org/download.html). Extract the FFmpeg file in the `C:` directory for recording functionality.

#### Step 2: Install SQL Server (If not already installed, please proceed to Step 3 if SQL Server is already installed)

Follow this guide to install SQL Server with Mixed Mode Authentication: [SQL Server 2019 Installation Guide](https://www.bu.edu/csmet/files/2021/02/SQL-Server-2019-Installation-Guide.pdf).

After installing SQL Server, perform the following tasks:

1. Create the database and provide a database name.
2. Run the script to create tables: [Table Script](https://github.com/MechlinTech/MyersAndStauffer_GhostQA/blob/main/SeleniumReportAPI/SqlScript/TableScript.sql).
3. Run the script to create procedures: [SP Script](https://github.com/MechlinTech/MyersAndStauffer_GhostQA/blob/main/SeleniumReportAPI/SqlScript/AllGhostQA_SP.sql).
4. Run the script for the initial user setup: [Initial User Script](https://github.com/MechlinTech/MyersAndStauffer_GhostQA/blob/main/SeleniumReportAPI/SqlScript/Insert_FirstUser.sql).

   **Note**: Ensure the scripts are run in the order mentioned above.

#### Step 3: Download the Provided Zip File

Download the zip file [Download Zip](https://github.com/MechlinTech/MyersAndStauffer_GhostQA/blob/main/SeleniumReportAPI/wwwroot/LatestSetupApp.zip) for the GhostQA application setup and perform the following steps:

1. Create a folder named `Published Project`.
   **Note**: Ensure all permissions are granted for `Published Project` for users `IUser` and `IISUser`.

   - Right-click on the folder and in `properties`, check the `security` tab.
   - Click on the `Edit` option.
   - Click on the `Add` option to add users.
   - Click on the `Advanced` option to search for `IUser` and `IISUser`.
   - Click on the `Find Now` option to search for the above users and select them one by one.
   - Once you find the user and they are visible in the list, click on `OK` until you return to the `security` page.
   - Click on the users one by one and select `Allow All` permissions.
   - Click on `OK` and save these changes.

2. Extract the zip file into the created `Published Project` folder. It should contain the following files/folders:

   - `GhostQA_API`
   - `GhostQA_UI`
   - `SetupApp.bat`

   **Note**: Ensure the `GhostQA_API` and `GhostQA_UI` folders are directly placed in the `Published Project` folder.

3. Right-click on the `SetupApp.bat` file and run it as an Administrator. It will create two sites on IIS:
   A. API
   B. UI

   - On completion of the BAT installation process, copy the URL provided in the `command prompt` and open it.
   - After launching the URL, the default username and password will be: `admin@gmail.com` and `Admin@123`.

#### Step 4: Set Up the Application

1. Under the `GhostQA_API` folder, modify the `appsettings.json` file to update the connection string with the appropriate server name, database name, username, and password.
