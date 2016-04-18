#
# Installs the ROS Xacro package and its ROS module dependencies
#

packages='
    ros_comm-1.12.0/tools/roslaunch
    ros_comm-1.12.0/tools/rosgraph
    ros-1.13.1/core/roslib
    catkin-0.7.1
    ros_comm-1.12.0/tools/rosmaster
    ros-1.13.1/tools/rosclean
    genpy-0.5.8
    genmsg-0.5.7
    xacro-1.11.0
    '

# Install dependencies from pip
pip install -U catkin_pkg
pip install -U rospkg

# Install dependency packages
root=`pwd`
for pkg in $packages; do
    cd $pkg
    python setup.py install
    cd $root
done

# Generate messages as Python classes before inspythtalling as packages
cd std_msgs-0.5.10
echo std_msgs-0.5.10
../genpy-0.5.8/scripts/genmsg_py.py --initpy -p std_msgs -Istd_msgs:./msg -o gen ./msg/Bool.msg ./msg/Byte.msg ./msg/ByteMultiArray.msg  ./msg/Char.msg ./msg/ColorRGBA.msg ./msg/Duration.msg ./msg/Empty.msg ./msg/Float32.msg ./msg/Float32MultiArray.msg ./msg/Float64.msg ./msg/Float64MultiArray.msg ./msg/Header.msg ./msg/Int8.msg ./msg/Int8MultiArray.msg ./msg/Int16.msg ./msg/Int16MultiArray.msg ./msg/Int32.msg ./msg/Int32MultiArray.msg ./msg/Int64.msg ./msg/Int64MultiArray.msg ./msg/MultiArrayDimension.msg ./msg/MultiArrayLayout.msg ./msg/String.msg ./msg/Time.msg ./msg/UInt8.msg ./msg/UInt8MultiArray.msg ./msg/UInt16.msg ./msg/UInt16MultiArray.msg ./msg/UInt32.msg ./msg/UInt32MultiArray.msg ./msg/UInt64.msg ./msg/UInt64MultiArray.msg
python setup.py install
cd $root

cd ros_comm_msgs-1.11.2/rosgraph_msgs
echo ros_comm_msgs-1.11.2/rosgraph_msgs
../../genpy-0.5.8/scripts/genmsg_py.py --initpy -p rosgraph_msgs -Istd_msgs:../../std_msgs-0.5.10/msg -o gen ../msg/Clock.msg ../msg/Log.msg ../msg/TopicStatistics.msg
python setup.py install
cd $root

echo "Finished installing ROS module dependencies for Xacro"
