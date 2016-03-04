# Project Neptune

## Introduction

**Project Neptune** is the code name for an online robot configurator project developed for [Clearpath Robotics](http://www.clearpathrobotics.com/).  The primary deliverable for the project is a web application that allows Clearpath's customers to drag and drop sensors on a robot base.  After customizing the robot, the web app will output the configuration in [Unified Robot Description Format (URDF)](http://wiki.ros.org/urdf) that Clearpath will use as the basis for building the robot.

## Features

The Neptune project includes three notable features:

* Neptune client
* Database maintainence tool
* URDF/Unity converter API

### Neptune Client

The **Neptune Client** is a WebGL application built in Unity that provides the UI for

### DB Maintainance

In addition to the Neptune Client, the **DB Maintainance** subproject is a utility tool for maintaining the centralized database of robot components that is read by the client app.

### URDF/Unity Converter

The **URDF/Unity Converter** subproject is a class library developed in C# that provides an API for converting between URDF files and Unity game objects.

## Development Environment

* Unity 5.3.3
* Unity WebGL plugin
* Unity Tools for Visual Studio
* Visual Studio 2015

## API Documentation

Coming soon!

## Contribute

Currently, contribution for Project Neptune is closed to members of [Mango Mango Development](Mango-Mango).  Upon delivery of their capstone project, the team intends to open the project to external contributors.

## Deployment

Currently, deployment is handled internally by Mango Mango.

The latest deployment for development purposes is available [here](http://ec2-54-148-182-29.us-west-2.compute.amazonaws.com/).