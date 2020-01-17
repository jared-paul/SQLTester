This program is a parallel task application that currently monitors system information and establishes a connection with a socket listening on a specified port.
It recieves status updates from the paired application to start, reset, or kill the monitor. It currently resets on every query execution as specified in the paired application.
Using Microsofts PerformanceCounter library to monitor system statistics and using design patterns: observer, aggregator, and builder/composer

To Build:
You must open it in VS 2019, 2015 doesn't work as microsoft changed the format of solutions.
Once opened, you must install all dependencies (it should auto install with NuGet) and "publish" the solution.

To Publish:
Right click the c# solution and click "publish", change the folder to wherever you'd like it to build to.
Once published you can now run the Tester.dll file

To Run:
You must install .NET core runtime version 2.2.5 found here: https://dotnet.microsoft.com/download/thank-you/dotnet-runtime-2.2.5-windows-hosting-bundle-installer
Once installed you can open cmd and run the program from the root directory by doing "dotnet Tester.dll"
It should now run in the cmd you opened.

You must have the paired application running before starting this one as it immediately tries to connect to it.
Paired Application: https://code.maruhub.com/projects/MB/repos/tester_sql_executor/