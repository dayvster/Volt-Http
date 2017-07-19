# Volt-Http
 Volt is a simplified HttpClient for the dotnet core Framework.
 
# Installation
```` PM> Install-Package Volt.DayviSchuster ````
# Usage
The client itself is relatively easy to use.

Below you will find a simple code example on how to use the library in a console application, of course you are free to use it in any kind of dotnet core application you would like.
#### Example: 
```csharp
namespace Vtest{
    using System;
    using Volt.Http;
    class Program {
        static void Main(string[] args){ 
            // Initialise the client
            VClient Volt = new VClient();

            // Let's just send a very simple GET request
            string result = Volt.Get(new Uri("https://httpbin.org/get")).Content.ReadAsStringAsync().Result;

            // And print it to the console
            Console.WriteLine(result);
        }
    }
}
```

if you did everything right then the output should be something along these lines:
#### Output
```
{
  "args": {},
  "headers": {
    "Accept-Encoding": "gzip, deflate",
    "Connection": "close",
    "Host": "httpbin.org"
  },
  "origin": "90.153.6.52",
  "url": "https://httpbin.org/get"
}
```

#### Post Request
```csharp
    VClient Volt = new VClient();
    Volt.Post(
        new Uri("https://httpbin.org/post"), 
        "supercalifragilisticexpialidocious",
        "text/plain"
    ).Content.ReadAsStringAsync().Result();
```

#### Chainable
```csharp
    VClient Volt = new VClient();
    Volt.SendPost(
        new Uri("https://httpbin.org/post"), 
        "supercalifragilisticexpialidocious", 
        "text/plain"
    ).GetResultAsString();
```

The main difference between the two above methods is the return type, methods prefixed with Send will always return the instance of the `VClient`, which allows you to chain methods, whereas methods like Post, Get, Put will always have a return type of `HttpResponseMessage`

