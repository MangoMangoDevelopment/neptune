#!/usr/bin/env python

#
# Written by MangoMango to install the rosgraph_msgs package after generating
# the Python classes for its .msg definitions, and copying the generated classes
# to ./std_msgs/msgs (relative to setup.py's location)
#

from distutils.core import setup
from catkin_pkg.python_setup import generate_distutils_setup

d = generate_distutils_setup(
    packages=['std_msgs'],
    requires=['message_generation']
)

setup(**d)
