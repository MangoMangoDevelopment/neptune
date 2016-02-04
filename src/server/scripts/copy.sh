if [ "$#" -ne 2 ]; then
	echo "Usage: copy.sh <file_dir> <file_name>"
else
	scp -i "Conestoga.pem" $1$2 ubuntu@ec2-54-148-182-29.us-west-2.compute.amazonaws.com:/home/ubuntu/dropbox/$2
fi