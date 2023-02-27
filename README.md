# Minimal-API-Platinum-dev

from: <https://www.youtube.com/playlist?list=PLEtg-LdqEKXb9RjcrqAHGPlQTBw0uISy7>

## Write simple minimal web api

1. install project: *dotnet new web --name PlatinumDev.HotelsWebAPI*

2. run project: *dotnet run*
put attention to line: *Now listening on:*

3. install httpRepl:
open terminal
*dotnet tool install -g Microsoft.dotnet-httprepl*
For change default nuget server, go to file *%appdata% \NuGet\NuGet.Config* and change server to:
add key="nuget.org" value="https://api.nuget.org/v3/index.json" protocolVersion="3"

4. run in terminal: *httprepl <http://localhost:5003>*

5. write: *get*
console output:
HTTP/1.1 200 OK
Content-Type: text/plain; charset=utf-8
Date: Fri, 24 Feb 2023 13:41:54 GMT
Server: Kestrel
Transfer-Encoding: chunked

Hello World!

6. After add post method, in repl write:
post hotels

{
"Id": 3,
"Name": "Cleo",
"Latitude": 23,
"Longitude": 32
}

## Add SQLite, EF core

1. install EF core:
open terminal
*dotnet add package Microsoft.EntityFrameworkCore*

2. install sqlite:
open terminal
*dotnet add package Microsoft.EntityFrameworkCore.Sqlite*

## Add Swagger

1. install Swagger:
open terminal
*dotnet add package Swashbuckle.AspNetCore*
