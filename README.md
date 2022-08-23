# hex-converter
[![nuget](https://img.shields.io/badge/nuget-v0.1.1-blue)](https://www.nuget.org/packages/CrypticWizard.HexConverter)
## Description
* A hex converter utility using dictionaries

## Tests
[![.NET 6.0](https://github.com/cryptic-wizard/hex-converter/actions/workflows/dotnet.yml/badge.svg)](https://github.com/cryptic-wizard/hex-converter/actions/workflows/dotnet.yml)

[![.NET Core 3.1](https://github.com/cryptic-wizard/hex-converter/actions/workflows/dotnetcore.yml/badge.svg)](https://github.com/cryptic-wizard/hex-converter/actions/workflows/dotnetcore.yml)

[![Nuget Publish](https://github.com/cryptic-wizard/hex-converter/actions/workflows/nuget.yml/badge.svg)](https://github.com/cryptic-wizard/hex-converter/actions/workflows/nuget.yml)

## Usage
### Install Package
```Text
dotnet add package CrypticWizard.HexConverter
```
```xml
<PackageReference Include="CrypticWizard.HexConverter" Version="0.1.1"/>
```

### Include Package:
```C#
using CrypticWizard.HexConverter;
```

### GetHex( byte[] )
```C#
byte[] byteArray = { 42, 84, 255, 0 };
string hex = HexConverter.GetHex(byteArray);
```
```Text
"2A54FF00"
```

### GetHexArray( byte[] )
```C#
byte[] byteArray = { 42, 84, 255, 0 };
string[] hexArray = HexConverter.GetHexArray(byteArray);
```
```Text
{ "2A", "54", "FF", "00" }
```

### GetHexList( byte[] )
```C#
byte[] byteArray = { 42, 84, 255, 0 };
List<string> hexList = HexConverter.GetHexList(byteArray);
```
```Text
{ "2A", "54", "FF", "00" }
```

### GetBytes ( string )
```C#
string hex = { "2A54FF00" };
byte[] bytes = HexConverter.GetBytes(hex);
```
```Text
{ 2A, 54, FF, 00 }
```

### GetBytes ( string[] )
```C#
string[] hexArray = { "2A", "54", "FF", "00" };
byte[] bytes = HexConverter.GetBytes(hexArray);
```
```Text
{ 2A, 54, FF, 00 }
```

### GetBytes ( List\<string> )
```C#
List<string> hexList = new List<string> { "2A", "54", "FF", "00" };
byte[] bytes = HexConverter.GetBytes(hexList);
```
```Text
{ 2A, 54, FF, 00 }
```

## Features
### Recently Added
v0.1.1
```C#
GetHex( byte[] )
GetHexArray( byte[] )
GetHexList( byte[] )
GetBytes( string )
GetBytes( string[] )
GetBytes( List<string> )
```
### Planned Features

## Tools
* [Visual Studio](https://visualstudio.microsoft.com/vs/)
* [NUnit 3](https://nunit.org/)
* [SpecFlow](https://specflow.org/tools/specflow/)
* [SpecFlow+ LivingDoc](https://specflow.org/tools/living-doc/)
* [Run SpecFlow Tests](https://github.com/marketplace/actions/run-specflow-tests)

## License
* [MIT License](https://github.com/cryptic-wizard/hex-converter/blob/main/LICENSE.md)