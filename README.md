<img src="./hypar_logo.svg" width="300px" style="display: block;margin-left: auto;margin-right: auto;width: 50%;">

## THE HYPAR SDK IS CURRENTLY IN BETA. DO NOT USE IT FOR PRODUCTION WORK.

# Hypar SDK
[![Build Status](https://travis-ci.org/hypar-io/sdk.svg?branch=master)](https://travis-ci.org/hypar-io/sdk)

The Hypar SDK is a library for creating functions that execute on Hypar. In short, a function is a piece of code that is executed in the cloud to build stuff. You create the function logic and publish the function to Hypar, then we execute it for you and store the results. This repository contains the Hypar SDK, which provides object types that are useful for generating the built environment.

- `Hypar.Elements` provides abstractions for building elements like beams and slabs.
- `Hypar.Geometry` provides a minimal geometry library that supports points, lines, curves, and extrusions.

The Hypar SDK also reads and writes data using several open standards like [GeoJson](http://geojson.org/) and [glTF](https://www.khronos.org/gltf/).

## Getting Started
- The Hypar SDK is currently in beta. Contact beta@hypar.io to have an account created. Functions can be authored and executed locally. **A login is only required when publishing your function to the world!**
- Install [.NET](https://www.microsoft.com/net/)
- Install the [Hypar CLI](https://github.com/hypar-io/sdk/tree/master/csharp/src/cli/).
- Install an IDE. Here's a couple of options:
  - The Hypar team has enjoyed developing and testing the SDK using [Visual Studio Code](https://code.visualstudio.com/). It's a free IDE with great support for .net and python and it looks and acts the same on every platform (Mac, Linux, Windows), so it makes our job of supporting you slightly easier.
  - [Visual Studio Community Edition](https://visualstudio.microsoft.com/vs/community/) is great too, but it doesn't exist for Linux and is still learning to walk on the Mac.
- Make a function:
```
hypar new <function name>
cd <function name>
```
- Edit the `hypar.json` file to describe your function.
```
hypar publish
```
- Preview `.glb` models generated by Hypar locally using the [glTF Extension for Visual Studio Code](https://github.com/AnalyticalGraphicsInc/gltf-vscode), or [Don McCurdy's online glTF Viewer](https://gltf-viewer.donmccurdy.com/).

## Configuration
The `hypar new` command will create a `hypar.json` file in your function's directory. This file is used to provide configuration information to Hypar. Here's an example of a `hypar.json` file.
```json
{
  "email": "info@hypar.io",
  "description": "A function which makes boxes."
  "function": "box.box",
  "name": "Box",
  "parameters": {
    "height": {
      "description": "The height of the box.",
      "max": 11,
      "min": 1,
      "step": 5,
      "type": "number"
    },
    "length": {
      "description": "The length of the box.",
      "max": 11,
      "min": 1,
      "step": 5,
      "type": "number"
    },
    "width": {
      "description": "The width of the box.",
      "max": 11,
      "min": 1,
      "step": 5,
      "type": "number"
    },
    "location": {
      "description": "The location of the box.",
      "type": "location"
    },
  "respository_url": "https://github.com/hypar-io/sdk",
  "returns": {
    "volume": {
      "description": "The volume of the box."
    }
  },
  "runtime": "dotnetcore2.0",
  "version": "0.0.1"
}
```
|Property|Description
|:--|:--
|`description`|A description of the function.
|`email`|The email address of the function's author.
|`function`|The fully qualified name of the function. For python functions this will be the `module.function`. For .net functions this will be `namespace.class.method`.
|`name`|A name for the function.
|`repository_url`|(OPTIONAL) The url of the repository which contains this function's implementation.
|`runtime`|At this time only `dotnetcore2.0` is supported.
|`parameters`|An object containing data about each parameter.
|`description`|A description of the parameter. This description will show up in the Hypar web application.
|`max`|The maximum value for a parameter.
|`min`|The minimum value for a parameter.
|`step`|The value by which the parameter will be incremented when multiple executions are requested.
|`type`|The type of parameter. Supported values are `number`, and `location`.
|`version`|The version of the function. Versions should adhere to [semantic versioning](https://semver.org/).

## Examples
The best examples are those provided in the [tests](https://github.com/hypar-io/elements/tree/master/test), where we demonstrate usage of almost every function in the library.

## Build
`dotnet build`

## Test
`dotnet test`

## Third Party Libraries

- [LibTessDotNet](https://github.com/speps/LibTessDotNet)  
- [Verb](https://github.com/pboyer/verb)
