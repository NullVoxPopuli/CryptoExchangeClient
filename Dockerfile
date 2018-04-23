# TODO: publish this dev image
FROM microsoft/dotnet:2.0.7-sdk-2.1.105 AS dev-env
WORKDIR /app

RUN echo \
  && apt update \
  && apt install -y apt-transport-https

# MONO

# https://unix.stackexchange.com/a/253476/45041
RUN echo \
  && export CODENAME="$(dpkg --status tzdata|grep Provides|cut -f2 -d'-')" \
  && apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF \
  && echo "deb https://download.mono-project.com/repo/ubuntu stable-$CODENAME main" | tee /etc/apt/sources.list.d/mono-official-stable.list \
  && apt update \
  && apt install -y mono-complete

# PAKET

ENV PAKET_VERSION 5.156.0
ENV PAKET "mono /app/.paket/paket.exe"

COPY . ./
RUN echo \
  && cd /app/.paket \
  && wget https://github.com/fsprojects/Paket/releases/download/$PAKET_VERSION/paket.bootstrapper.exe \
  && chmod +x ./paket.bootstrapper.exe \
  && mono ./paket.bootstrapper.exe \
  && chmod +x ./paket.exe \
  && ${PAKET} restore


FROM microsoft/dotnet:2.0.7-sdk-2.1.105 AS build-env
# TODO..


# Build runtime image
FROM microsoft/aspnetcore:2.0 AS release
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "aspnetapp.dll"]
