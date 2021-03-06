# SharpDevelopWebApi Boilerplate + Vue JS

A light-weight solid starting point for developing ASP.NET Web API application in portable [SharpDevelop](https://portable.info.pl/sharpdevelop-portable/). Great for student learning and small medium size projects!

## Features and Libraries

- EntityFramework.SharpDevelop (SQL Server)
- Dapper (MS Access support)
- Swagger / Swashbuckle
- Simple JWT Authentication
- AutoMapper
- Hangfire Core / Hangfire.MemoryStorage
- FluentValidation
- ~~TuesPechkin PDF~~
- SimpleExcelImport
- Image/File Upload
- Email / SMTP
- SimpleLogger

### Why it's lite?

- No OWIN
- No ASP.NET Identity

### Web.config missing?

Just copy Web.github.config then rename to Web.config

### Build Error? Could not resolve reference... Could not locate assembly... The underlying connection was closed...

- Run (Merge) [nugetfix.reg](https://stackoverflow.com/a/53677845/1281209)
- or run nuget.bat
- or execute this command `nuget restore`
- or download the offline [nuget packages here](https://drive.google.com/file/d/1_BPJqxucppNr5WX337RRxpl8jv7YB8Kd/view?usp=sharing). Extract and add as a local source, Tools > Option > Package Management > Package Sources

### Running in IIS Express

1. Click Project Menu > Project Options
2. In **Web** tab, choose **[Use IIS Express Web Server]**
3. Enter a port number higher than `8001`
4. Click **[Create application/virtual directory]** button. Done! You can now run/debug the web app.
   > \*\*\* Error indicating duplicate entry of type 'site' with unique key attributes...
5. Goto `Documents\IISExpress\config` folder
6. In `applicationhost.config`, delete `<site name=...>` entries in `<sites>...</sites>`
7. Repeat step 1.

### Support for Github Actions FTP Deploy

- Automate deploying websites and more
- Simply update `/.github/workflows/main.yml` with your FTP credentials
- Secure your FTP password, go to **Settings** tab then select **Secrets**

### Portable Asp.Net Web Server

Run the project without SharpDevelop in this tiny web server!

### Database Browsers

You can browse the database using SQL Server Management Studio (SSMS) or portable [Database.NET](https://bit.ly/30tqqxU). To enable (LocalDB)\MSSQLLocalDB, install [SQL Server Express LocalDB](https://bit.ly/2Mlijj1).

### Free ASP.NET Hosting

- [Somee.com](https://somee.com/FreeAspNetHosting.aspx)
- [Smarterasp.net](https://www.smarterasp.net/secured_signup?plantype=FREE)
- [myasp.net](https://www.myasp.net/freeaspnethosting)

### Learning Slides

- JSON
- ASP.NET Web API
- [Entity Framework 6 Code-First Tutorial](https://bernardgabon.com/blog/entity-framework-tutorial/)

### Vue JS Example

- Step by step Code Tutorial
- JWT Account Login
- Bootstrap with Vue
- Single File Component with httpVueLoader
- Vuetify

### Warning

- Do not load the project in Visual Studio
- Do not add Nuget packages which supports .NET Standard

## Contributors

- [Bernard Gabon](https://bernardgabon.com)
