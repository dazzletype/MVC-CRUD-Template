## ASP.NET MVC3 CRUD Template

This application serves as a template for creating a base ASP.NET MVC3 CRUD (Create/Read/Update/Database) app.

The code was initially based on [Ivan Loire's excellent ACME Invoicing App] (https://github.com/iloire/ASP.NET-MVC-ACME-Invoicing--App) and extensively customized to provide support for the en-US culture model, conversion to more generic domain objects, translated comments from Spanish to English, and disabling of some tools (i.e. NuGet for automatic package updates and forced usage of the x86 version of EF 4.1 to remove issues) for further simplicity. A big thanks to him for posting his work on GitHub.

## Overview
 * This application shows **how to create an ASP.NET MVC application from scratch.** and how to use some of the cool features of ASP.NET MVC3 like:
   * **Code First**
   * **Entity Framework** and **LINQ**
   * **Razor** view engine 
   * **Custom Membership Provider** pointing to your own database users table.
   * **Partial views** and **partial actions** (with independent OutputCache for high concurrency page rendering) 
   * **Html Helpers**
   * **Data Annotation** validation
   * **AJAX** partial rendering
   * Custom **T4 templates** for customized scaffolding
   * **NUnit** for unit testing and **Moq for object mocking**.
 * Every major development on this invoicing app has been tagged (0.1, 0.2, etc...)
 
## Installation

 * Download the code and open with Visual Studio 2010 Express or above.
 * By using Code First and EF 4.1 the database will be recreated when you first run the project.

### Altering connectionStrings section 

Based on convention, EF will look for a connection strign named as the DBContext (in this case "InvoiceDB"), and will use it, so feel free to set the data provider you want:

     <!-- 
         By default (convention over configuration, the connection string with the same name as your DBContext will be used 
         You can select then wherever you will use SQL CE, SQL Serer Express Edition, etc, here. 
     -->
     <add name="InvoiceDB" connectionString="Data Source=|DataDirectory|InvoiceDB.sdf" providerName="System.Data.SqlServerCe.4.0" />
     <!--
     <add name="InvoiceDB" connectionString="Data Source=.\SQLEXPRESS; Integrated Security=True; MultipleActiveResultSets=True" providerName="System.Data.SqlClient" />
     <add name="InvoiceDB" connectionString="metadata=res://*;provider=System.Data.SqlClient;provider connection string=&quot;Data Source=mssql2005a.active-ns.com;Initial Catalog=xxxxxxxxxx.org;user id=xxxxxxxxxxxx;password=xxxxxxxxxxx;MultipleActiveResultSets=True&quot;" providerName="System.Data.EntityClient" />
     -->




## LICENSE

Copyright (c) 2011 Iván Loire Mallén -  www.iloire.com

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.

