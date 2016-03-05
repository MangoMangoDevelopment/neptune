# Project Neptune

## Introduction

**Project Neptune** is the code name for an online robot configurator project developed by [Mango Mango](https://github.com/MangoMangoDevelopment/neptune/wiki/Mango-Mango) for [Clearpath Robotics](http://www.clearpathrobotics.com/).  The primary deliverable for the project is a web application that allows Clearpath's customers to drag and drop sensors on a robot base.  After customizing the robot, the web app will output the configuration in [Unified Robot Description Format (URDF)](http://wiki.ros.org/urdf) that Clearpath will use as the basis for building the robot.

## Subprojects

The Neptune project includes three notable features:

* Neptune client
* Database maintenance tool
* URDF/Unity converter API

### Neptune Client

The **Neptune Client** is a WebGL application built in Unity that provides the UI for configuring a robot with customized sensors.  The Neptune Client provides a drag and drop UI, extensive support for sensor position manipulation, and can also provide updated power usage, labour time and cost ratings for the configuration.

Feature overviews:

* [Object Manipulation in 3D Space](https://github.com/MangoMangoDevelopment/neptune/wiki/FEATURE:-Object-Manipulation-in-3D-Space)

### DB Maintenance

In addition to the Neptune Client, the **DB Maintainance** subproject is a utility tool for maintaining the centralized database of robot components that is read by the client app.

Feature overviews:

* [Uploading and Displaying URDF file](https://github.com/MangoMangoDevelopment/neptune/wiki/Uploading-and-Displaying-URDF-file)

### URDF/Unity Converter

The **URDF/Unity Converter** subproject is a class library developed in C# that provides an API for converting between URDF files and Unity game objects.

Feature overviews:

* [URDF/Unity Conversion](https://github.com/MangoMangoDevelopment/neptune/wiki/FEATURE:-URDF-Unity-Conversion)

Developer References:

* [URDF](https://github.com/MangoMangoDevelopment/neptune/wiki/URDF)
* [XML parsing](https://github.com/MangoMangoDevelopment/neptune/wiki/XML-Parsing)

## Development Environment

* Unity 5.3.3
* Unity WebGL plugin
* Unity Tools for Visual Studio
* Visual Studio 2015

## API Documentation

Coming soon!

## Contribute

Currently, contribution for Project Neptune is closed to members of [Mango Mango](https://github.com/MangoMangoDevelopment/neptune/wiki/Mango-Mango).  Upon delivery of their capstone project, the team intends to open the project to external contributors.

### Resources for Contributors

* Mango Mango's [scrum board](https://trello.com/b/hpg9uP95/neptune) on Trello
* Mango Mango's [scrum process](https://github.com/MangoMangoDevelopment/neptune/wiki/Scrum-Process)
* [Using Git](https://github.com/MangoMangoDevelopment/neptune/wiki/Git-Workflow)
* [Code Style for URDF/Unity Converter](https://github.com/MangoMangoDevelopment/neptune/wiki/Code-Style)

## Deployment

Currently, deployment is handled internally by Mango Mango.

The latest deployment for development purposes is available [here](http://ec2-54-148-182-29.us-west-2.compute.amazonaws.com/).