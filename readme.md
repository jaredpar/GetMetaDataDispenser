# Getting `IMetaDataDispenser`

Demonstrates a universal way to acquire a `IMetaDataDispenser` instance on .NET Framework 4.7.2 up to the latest .NET version, 7 at the time of writing.

## Miniumum requirements

- CMake 3.6.2
- .NET 7 (Managed project targets .NET 7, but can be retargeted to .NET 6)

## Initialize submodules

`git submodule init`

`git submodule update`

## Build

1) Build native

    `cmake -S . -B artifacts`

    `cmake --build artifacts --target install`

2) Build managed project

    `dotnet build Host`

## Run

Pass native binary to managed `Host`.

**.NET 7.0**

Windows:

`dotnet run --project Host --framework net7.0 artifacts\bin\getmddisp.dll`

Linux:

`dotnet run --project Host --framework net7.0 artifacts/lib/libgetmddisp.so`

**.NET Framework 4.7.2**

`dotnet run --project Host --framework net472 artifacts\bin\getmddisp.dll`