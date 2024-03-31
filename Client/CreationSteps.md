# Creation Steps
The steps I took to changed this **File New** Blazor WebAssembly Standalone App

## 1. Creation
- Project Name: Client
- Solution Name: VirtualFileSystemWASM

## Root Files
- Constants
 
## Create Root Folders

- Components: AddressCard, BreadCrumbs, LoadingComponent, PageHeader, TableTemplate, ToggleButton
- Enums: MediaQuery, Navs
- Helpers


## Create Features Folders
- Home
- Sitemap

## `_Imports.razor`

Append...
```
@using Client.Components
@using Blazored.Toast
@using Blazored.Toast.Services
```

## Nuget

```json
    <PackageReference Include="Ardalis.SmartEnum" Version="8.0.0" />
    <PackageReference Include="Blazored.LocalStorage" Version="4.4.0" />
    <PackageReference Include="Blazored.Toast" Version="4.2.0" />
    <PackageReference Include="Blazored.Typeahead" Version="4.7.0" />
```

## Program.cs

### Before
```csharp
using Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();

```

### After
```csharp
```

## wwwroot

### css/app.css

```css
.btn-group-xs > .btn, .btn-xs {
  padding: .25rem .4rem;
  font-size: .875rem;
  line-height: .5;
  border-radius: .2rem;
}
```

#### Could add
```css

html, body {
    font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif;
}

hr.warning {
  border-bottom: 3px solid #ece1c1;
}

.hebrew16 {
  font-size: 16.0pt;
  font-family: david;
  direction: rtl;
}

.hebrew {
  font-size: 22.0pt;
  font-family: david;
  direction: rtl;
}

```



## VirtualFileSystem Specific
    <PackageReference Include="System.IO.Pipelines" Version="8.0.0" />

# Delete


# Other
Stuff not included but wanted to make a note of

#nullable disable
code {///}



## LoadingStatusEnum
```csharp
namespace Client.Enums;

public enum LoadingStatusEnum
{
	Loading,
	Loaded,
	ListHasData,
	EmptyList,
	Error
}
```

## NavSocialMedia
public abstract class NavSocialMedia : SmartEnum<NavSocialMedia>
{

	#region Id's
	private static class Id
	{
		internal const int YouTube = 1;
		internal const int GitHub = 2;
		internal const int Twitter = 3;
		//<a href="https://www.facebook.com/LivingMessiahMinistries"
	}

#### libman.json
```json
{
  "version": "1.0",
  "defaultProvider": "cdnjs",
  {
    "provider": "unpkg",
    "library": "bootstrap@5.3.2",
    "destination": "wwwroot/lib/bootstrap/"
  },
  "libraries": [
    {
      "library": "font-awesome@5.11.2",
      "destination": "wwwroot/lib/font-awesome/"
    }
  ]
}


```


Uncaught (in promise) TypeError: 
Failed to register a ServiceWorker for scope ('https://localhost:7081/') 
with script ('https://localhost:7081/service-worker.js'): 
A bad HTTP response code (404) was received when fetching the script.

