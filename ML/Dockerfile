FROM ubuntu:18.04 AS workerbaseimage
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
ENV TZ=Europe/London
ENV URL_API_NET "http://1da2-13-70-205-20.ngrok.io/"

RUN apt update && apt install -y python-pip python-dev ssh python-boto3
RUN pip  install ansible==2.4.3.0

# Install python3
RUN apt-get update
RUN apt-get install -y python3.7 python3-pip
RUN apt-get install language-pack-en-base -y
RUN apt-get install python3-dev
RUN apt-get install gcc -y
RUN apt-get install python3-pip -y 
RUN apt-get install libxslt1-dev -y
RUN apt-get install zlib1g-dev -y
RUN apt-get install g++ -y
RUN apt-get install unixodbc-dev -y


RUN mkdir -p /code
COPY . /code
WORKDIR /code

RUN pip3 install -r requirements.txt

FROM workerbaseimage

ARG FLASK_ENV="development"
ENV FLASK_ENV="${FLASK_ENV}" \
    PYTHONUNBUFFERED="true"
ENV LC_ALL=C.UTF-8
ENV LANG=C.UTF-8

CMD ["python3", "./app.py" ]