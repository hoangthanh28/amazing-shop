## Purpose
This repository shows the way to use identityserver4 against oauth2 protocol and openid connect.

For the slide of the presentation, please download at [link](/docs/HNVN_IDP_Presentation.pdf)
## Architecture
![Architecture](/docs/images/architecture.png "Architecture")
## Setup
### Prerequisites
- [vs code](https://code.visualstudio.com/download)
- [docker](https://docs.docker.com/get-docker/)
- [window terminal](https://github.com/microsoft/terminal)
- [dotnet core 3.1 sdk](https://dotnet.microsoft.com/download)
- [node js](https://nodejs.org/en/download/)

### Run the sample
```cs
docker-compose -f docker-compose.yml -f development.yml up
```
### Database migration
```ps
build.ps1 --target=Migrate
```