FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS base

ARG debug='1'
ENV DEBUG=${debug}
WORKDIR /src
COPY . .

RUN dotnet restore --ignore-failed-sources || :
RUN (test $DEBUG -eq 1 && dotnet publish -c Debug -o /app) || (test $DEBUG -eq 0 && dotnet publish -c Release -o /app)

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic
RUN apt update && apt install -y telnet vim tcpdump iputils-ping && apt-get install -y locales && sed -i -e 's/# pt_BR.UTF-8 UTF-8/pt_BR.UTF-8 UTF-8/' /etc/locale.gen

ARG deploy_env='dev'
ARG port='8080'
ARG debug='1'
ARG tracer_version='1.15.0'
ENV DEBUG ${debug}

ENV ASPNETCORE_ENVIRONMENT ${deploy_env}
ENV ASPNETCORE_URLS http://0.0.0.0:${port}
ENV TZ=America/Sao_Paulo
ENV LANG pt_BR.UTF-8
ENV LANGUAGE pt_BR.UTF-8
ENV LC_ALL pt_BR.UTF-8

RUN apt-get update && \
    apt-get install -y tzdata && \
    ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone && \
    dpkg-reconfigure -f noninteractive tzdata

WORKDIR /app
COPY --from=0 /app .
CMD ["/bin/bash", "-c", "dotnet Anima.Student.Adapter.Api.dll"]
EXPOSE ${port}