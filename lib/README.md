# Setup for ROS Python Packages

To convert Xacro files to URDF, the ROS [Xacro](http://wiki.ros.org/xacro) python is used and requires configuration as described below.

## System Requirements

* Python version: 2.7.11

Note: The ROS Xacro package requires Python 2.7.x due to using the pre-built [file](https://docs.python.org/2/library/functions.html#file) function that was removed in later versions.

## Xacro Dependencies

To install the ROS Xacro package, the following dependency packages need to be installed.

Installed from [pip](https://pip.pypa.io/en/stable/):

* catkin_pkg
* rosdep

Installed from `setup.py`:

* roslaunch
* rosgraph
* roslib
* catkin
* rosmaster
* rosclean
* xacro

### Message Dependencies

Additionally, the following message modules need to be installed:

* std_msgs
* rosgraph_msgs

For messages, ROS uses a simplified description language to define the data types stored in the message in a `.msg` file.  ROS tools then generate the classes from the `.msg` files.  See http://wiki.ros.org/msg for more information.

To generate python classes from the required message types, the following dependency packages need to be installed via `setup.py`:

* genpy
* genmsg

The generated message classes are output to a `gen` directory.  For simplicity of python module installation, the generate messages have been checked into the source code repository.  The generated classes have been copied into `[package_name]/msg` as per the standard python file structure, along with required `__init__.py` files. Within the message package directories, a `setup.py` script has been added that installs the generated messages as a module that can be imported.

## Installing the Dependencies

A script has been written to automate the installation of Xacro and its dependencies.

Usage:
```bash
./install.sh [--generate_msgs]
```

The `--generate_msgs` flag will generate the python classes required from their .msg file definitions.  However, the generated python classes for messages are committed to to the source repository and don't need to be generated for subsequent installations on different machines.

## Configuration for the UrdfUnity Project

In `UrdfUnity/Config`, there is a configuration file called `xacro.config` where you must specify the path to python.exe and the Xacro script on your local machine.  For example:

```
python=C:\Python27\python.exe
xacro=C:\Dev\Code\Capstone\neptune\lib\xacro-1.11.0\scripts\xacro
```