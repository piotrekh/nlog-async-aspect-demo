#NLog async aspect demo using Ninject interception

How to create an aspect that will log (using NLog) parameters and returned value of a method called on object (intercepted by Castle DynamicProxy and Ninject)

1. Install required NuGet packages:

	- NLog
	- NLog.Schema
	- NLog.Config
	- Castle.Core
	- Microsoft.Web.Infrastructure
	- WebActivatorEx
	- Ninject
	- Ninject.Web.Common
	- Ninject.Web.Common.WebHost
	- Ninject.MVC5
	- Ninject.Extensions.Interception
	- Ninject.Extensions.Interception.DynamicProxy
	- Newtonsoft.Json

Some other packages may be needed if you wish to e.g. save logs to database, use different proxy library for Ninject or use this aspect with Web Api

2. Configure NLog - https://github.com/NLog/NLog/wiki/Tutorial#configuration

3. Bind your Ninject dependencies in NinjectWebCommon.cs and inject them to MVC Controllers

4. Include files from NLogAsyncAspectDemo.Logging namespace in your project

5. Decorate methods in your Ninject dependencies that you wish to log with "Log" attribute 

6. Implement ILoggable interface in your models (passed as parameters to methods marked with "Log" attribute) for custom serializatinog
