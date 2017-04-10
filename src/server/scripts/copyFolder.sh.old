if [ "$#" -ne 2 ]; then
	echo "Usage: copyFolder.sh <local_dir> <remote_dir>"
else
	scp -i "Conestoga.pem" -r $1 ubuntu@ec2-54-148-182-29.us-west-2.compute.amazonaws.com:/home/ubuntu/dropbox/$2
fi