if [ "$#" -ne 2 ]; then
	echo "Usage: copy.sh <file_dir> <file_name>"
else
	scp -i "Neptune-1.pem" $1$2 ubuntu@ec2-50-112-139-235.us-west-2.compute.amazonaws.com:/home/ubuntu/dropbox/$2
fi
