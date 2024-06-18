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

- Need **Framework solution code** to do the modification and generate published files for `SetupApp` folder
- **IIS**: Version 10 or later, install using the **Windows feature on/off** option with administrative permissions
- **SQL Server**: Version 19, 22, or latest

## On-Prem Setup Guide Using Batch File

### Instructions to Set Up GhostQA Using a .bat File

#### Step 1: Install IIS (If not already installed, please proceed to Step 2 if IIS is already installed)

##### To install Internet Information Services (IIS) using Windows Edition, follow these steps:

1. Open the launch section: Press the **Windows + R** key to open the launch section.  
   ![Windows + R](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/winkey-R.png)

2. Type `appwiz.cpl` in the Run UI and hit Enter to open Programs and Features in Windows 10.  
   ![Programs and Features](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/open-programs-and-features-by-run-420x218.png)

3. Click on `Turn Windows features on or off`. The Windows Features window will appear.  
   ![Windows Features](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/2_.png)

4. Ensure all features under Internet Information Services and Microsoft .NET Framework are selected.  
   ![IIS Features](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/1.png)

5. Click `OK` to install the selected Windows components, including IIS. Once IIS installation is done, **Restart** your machine.

6. To access IIS, click the Windows Start button, start typing `Internet Information Services Manager` in the search field, and click `Internet Information Services (IIS) Manager` once it appears.

7. Install the URL Rewrite module 2.1 from the following link: [URL Rewrite Module 2.1](https://www.iis.net/downloads/microsoft/url-rewrite).

8. Install the .NET Core Hosting Bundle by searching for it in any web browser and installing it from the following link: [Install .NET Core Hosting Bundle](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/hosting-bundle?view=aspnetcore-8.0#direct-download).

9. Download the `FFmpeg` file from the following link: [Download FFmpeg](https://ffmpeg.org/download.html). Extract the FFmpeg file in the `C:` directory for recording functionality.

##### To install Internet Information Services (IIS) using Windows Server Edition, follow these steps:

1. Open the launch section: Press the **Windows + R** key to open the launch section.  
   ![Windows + R](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/winkey-R.png)

2. Type `ServerManager` in the launch box and click OK. This opens the Programs and Features box on Windows Server 2012 (or any other version you’re using).  
   ![Server Manager](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/How-to-install-ISS-on-Windows-10-1.png)

3. Add roles and features: Click on the `Add roles and features` in the newly opened window.  
   ![Add Roles and Features](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/how-to-install-iis-on-windows-10-6.png)

4. Installation Wizard: Now, the Installation Wizard will appear. Click `Next`.  
   ![Installation Wizard](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/how-to-install-iis-on-windows-10-7.png)

5. Select `Role-based or feature-based installation` and click Next.  
   ![Role-based Installation](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/how-to-install-iis-on-windows-10-7-1.jpg)

6. Select `Select a server from the server pool` from the window, then `select Server` and click `Next`.  
   ![Select Server](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/how-to-install-iis-on-windows-10-8.png)

7. Check the Web Server (IIS) section: When you get to the Series Roles section, scroll down the list, then check the `Web Server (IIS)` section and hit `Next`.  
   ![Web Server (IIS)](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/how-to-install-iis-on-windows-10-9.png)

8. Click on `Add Features`, ensuring the checkbox `Install management tool (if applicable)` is selected.  
   ![Add Features](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/how-to-install-iis-on-windows-10-10.png)

9. Keep the default features selection as they already are, then click `Next`.  
   ![Default Features](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/how-to-install-iis-on-windows-10-11.png)

10. Click `Next` when you’re done with the Web Server Roles (IIS) text.  
    ![Web Server Roles](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/how-to-install-iis-on-windows-10-12.png)

11. Click `Next` after selecting the `Web Server roles` as shown in the following image.  
    ![Select Web Server Roles](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/how-to-install-iis-on-windows-10-13.png)

12. Click `Install`.  
    ![Install](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/how-to-install-iis-on-windows-10-14.png)

13. To access IIS, click the Windows Start button. Start typing `Internet Information Services Manager` in the search field and click `Internet Information Services (IIS) Manager` once it appears.

14. Install the URL Rewrite module 2.1 from the following link: [URL Rewrite Module 2.1](https://www.iis.net/downloads/microsoft/url-rewrite).

15. Install the .NET Core Hosting Bundle by searching for it in any web browser and installing it from the following link: [Install .NET Core Hosting Bundle](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/hosting-bundle?view=aspnetcore-8.0#direct-download).

16. Download the `FFmpeg` file from the following link: [Download FFmpeg](https://ffmpeg.org/download.html). Extract the FFmpeg file in the `C:` directory for recording functionality.

#### Step 2: Install SQL Server (If not already installed, please proceed to Step 3 if SQL Server is already installed)

    To Install SQL Server follow below steps
**Step 1.** Download installation media from this [link](https://go.microsoft.com/fwlink/p/?linkid=2215158&clcid=0x409&culture=en-us&country=us).

**Step 2.** Run the downloaded file and you will see the below screen. Now select the third option – `Download Media`.
[img](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/image-20210925153402-1.png)

**Step 3.** Now you will see the below screen. Please select the language you prefer and select the `ISO` radio button to download the ISO file. In addition, select the download location of your choice. I will go with the default location. Now press the `Download` button.
[img](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/image-20210925153402-2.png)

**Step 4.** Now it will start downloading SQL Server installation media. It will take some time based on your internet connection speed.
[img](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/image-20210925153402-3.png)

**Step 5.** After successful download of installation media, you will see the below screen. Click the `Close` button.
[img](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/image-20210925153402-4.png)

**Step 6.** Run install media file `(ISO file)` downloaded in above section by double-clicking on it. It will extract/mount all the contents in a new `temporary drive`.
[img](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/image-20210925153402-6.png)

**Step 7.** Once extraction is completed, double click on the `setup.exe` file and you will see the below screen. Click on the Installation option in the left panel and then click on `New SQL Server stand-alone installation or add features to an existing installation` option from the right panel.
[img](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/image-20210925153402-7.png)

**Step 8.** Now you will see the `Product Key window`. Select the `Developer option` from the dropdown and click on the `Next` button.
[img](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/image-20210925153402-8.png)

**Step 9.** Now you will see the `License Terms window`. Just select the `checkbox` and click on the `Next` button.
[img](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/image-20210925153402-9.png)

**Step 10.** Now you will see the `Microsoft Update window`. It is not compulsory to check for the latest updates but it is recommended. So, select the `checkbox` and click the `Next` button.
[img](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/image-20210925153402-10.png)

**Step 11.** Now it will check for updates and install them if any.
[img](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/image-20210925153402-11.png)

**Step 12.** After that, it will check some rules or prerequisites for the installation of SQL Server. Once all the rules passed, click on the `Next` button. Sometimes you may face an error at this stage. You need to check those on google to resolve on runtime.
[img](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/image-20210925153402-12.png)

**Step 13.** On the `Feature Selection window`, select features as shown in the below `screenshot`. You can also change the location for SQL Server instance installation but I will go with the default location. After feature selection please click the `Next` button.
[img](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/image-20210925153402-13.png)
[img](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/image-20210925153402-14.png)

**Step 14.** It will check some feature rules/prerequisites and then you will see the `Instance Configuration` screen. Here you can choose between `Default Instance` and `Named Instance`. Here I will go with `Named Instance`.
[img](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/image-20210925153402-15.png)


**Step 15.** Next, you will see the `Server Configuration` window. In `Service Accounts` tab, select `Automatic` in `Startup Type` for `SQL Server Agent`, `SQL Server Database Engine`, and `SQL Server Browser` services.
[img](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/image-20210925153402-16.png)

In the `Collation tab`, select collation as per your preference.
“Collations in SQL Server provide sorting rules, case, and accent sensitivity properties for your data. Collations that are used with character data types, such as `char` and `varchar`, dictate the code page and corresponding characters that can be represented for that data type.” – Microsoft.
[img](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/image-20210925153402-17.png)

**Step 16.** Next, you will see the `Database Engine Configuration` window. In the `Server Configuration` tab, choose `Mixed Mode` in the `authentication mode` section and enter a `strong password`. In Specify `SQL Server administrators` section, your current windows user should already be added automatically. If not, click on `Add Current User` button.
[img](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/image-20210925153402-18.png)

In the `Data Directories` tab, specify locations for database files and backup files. By default, it saves all the files on a C drive but it is not recommended to store database files on an OS drive because if any OS-related issue occurs then we may lose our data. Therefore, I choose D drive on my local machine.
[img](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/image-20210925153402-19.png)

In the `TempDB` tab, there are configurations for the temporary database file(s). There are some best practices on how to configure temporary database files locations, the number of files, and their file sizes. Ideally, the number of the TempDB data files should match the number of logical processors. So I have a number of files to 2.
[img](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/image-20210925153402-20.png)

Next, in the `MaxDOP` tab, the maximum degree of parallelism (MAXDOP) is a server configuration option for running SQL Server on `multiple CPUs`. It controls the number of processors used to run a single statement in `parallel plan execution`. By default, the setup will suggest value based on the system configuration.
[img](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/image-20210925153402-21.png)

Next, in the `Memory` tab, we can configure how much memory SQL Server instance can consume. By default, the installation process will recommend you min and `max memory allocation` based on the system configuration on which it is going install. However, you can change it.
[img](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/image-20210925153402-22.png)

In the `FILESTREAM` tab, leave the checkbox unchecked because we are not going to enable this feature. `FILESTREAM`, in SQL Server, `allows storing these large documents, images, or files` onto the file system itself.
Just Click on the `Next` button.
[img](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/image-20210925153402-23.png)

**Step 17.** Next, the setup will check some feature configuration rules, and then the `Ready to Install` window will appear. This window shows the summary of all the features and configurations which we have done in the above steps. Once review the summary and click on the `Install` button.
[img](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/image-20210925153402-24.png)

**Step 18.** Now, the installation will start and it may take some time based on our configurations.
[img](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/image-20210925153402-25.png)

**Step 19.** After installation, it will show you the list of features and their installation status. If any error occurred, it will show here.
[img](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/image-20210925153402-26.png)

#### `Congratulations! We have successfully installed SQL Server 2019 Developer edition on Windows machine. Next, you can install SQL Server Management Studio to connect SQL Server and query SQL databases`

#### Install SQL Server Management Studio by following below steps

**Step 1.** Download installation media from this [link](https://aka.ms/ssmsfullsetup)

**Step 2.** Run the `downloaded file` and you will see below screen. Just click on the `Install` button.
[img](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/image-20211022172644-2.png)

**Step 3.** It will start installing management studio. It will take some time.
[img](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/image-20211022172644-3.png)

**Step 4.** Once `installation` finished, `close` the installation wizard and open `start` menu and search for `SQL Server Management Studio`. click on it to open the application.
[img](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/image-20211022172644-4.png)

**Step 5.** Next, you will see below screen. In Connect to Server window, you can see the SQL instance name, which we have just installed. Also you can find your server name by selecting `Browse for more` option from `dropdown`, and now from opened pop-up window select `local server` by expanding `database engine`.
[img](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/image-20211022172644-5.png)
[img](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/Sql_server_browse_server.jpg)

**Step 6.** After selecting Server name select `Sql Server Authentication` from `Authentication` dropdown put `sa` as `user name` and `password` is what you have entered during installation of `SQL server Mixed mode` also click on `checkbox` for `Remember me` and `Trust Server Cirtificate` options.
After all above setup click on `Connect` button
[img](https://github.com/Ghost-QA/GhostQA/blob/main/Assets/SQL_Server_Auth_Login_Screen.jpg)

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
