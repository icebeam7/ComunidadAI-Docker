FROM python:3.7

ENV TZ=Europe/London
ENV URL_API_NET "http://1da2-13-70-205-20.ngrok.io/"

RUN mkdir -p /code
COPY . /code
WORKDIR /code

RUN pip install -r requirements.txt

ARG FLASK_ENV="development"
ENV FLASK_ENV="${FLASK_ENV}" \
    PYTHONUNBUFFERED="true"
ENV LC_ALL=C.UTF-8
ENV LANG=C.UTF-8

CMD gunicorn --bind 0.0.0.0:6002 main:app

