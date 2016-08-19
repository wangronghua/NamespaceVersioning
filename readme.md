[![Stable Nuget](https://img.shields.io/nuget/v/Stubble.Core.svg?style=flat-square)](https://www.nuget.org/packages/versionrouting)


#Namespace Versioning#
##Create routes for .NET Core Web API Controllers based on version number in the namespace##

Set up controllers inside of version numbered folders like the example below (v1, v100, v10000, etc)

![example](http://i.imgur.com/RhrxF5N.png)

Setup:

```cs
  var apiPrefix = "api";
  services.AddMvc(options => {
    options.Conventions.Add(new NameSpaceVersionRoutingConvention(apiPrefix));
  });

```
Route structure : 
```
/api/{versionnumber}/{controller}
```
for the above example image the route would be
```
/api/v1/BaseConfiguration
```
Use action routes in controllers to have version specific action routes 
```
/api/v1/BaseConfiguration/GetAll
```

Thanks to @rynowak for the assistance in getting this working.