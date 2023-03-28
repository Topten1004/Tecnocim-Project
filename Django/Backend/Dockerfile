FROM python:3.10.4-slim-bullseye
LABEL org.opencontainers.image.source https://github.com/pbarcelog/smartdebt

ENV PYTHONUNBUFFERED 1

COPY ./requirements.txt /tmp/requirements.txt
COPY ./requirements.dev.txt /tmp/requirements.dev.txt

COPY ./app /app
WORKDIR /app
EXPOSE 8000

ARG DEV=false
RUN python -m venv /py && \
    /py/bin/pip install --upgrade pip && \
    /py/bin/pip install psycopg2-binary && \
    /py/bin/pip install sphinxcontrib-applehelp && \
    apt-get update && apt-get -y install gcc && \
    apt-get install libffi-dev && \
    apt-get install -y --no-install-recommends unixodbc-dev unixodbc libpq-dev && \
    /py/bin/pip install -r /tmp/requirements.txt && \
    rm -rf /tmp && \
    adduser \
        --disabled-password \
        --no-create-home \
        django-user

#FROM postgres
#COPY 20230501174400_staging.sql /docker-entrypoint-initdb.d/

ENV PATH="/py/bin:$PATH"

USER django-user