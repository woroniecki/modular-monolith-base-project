FROM node:lts-slim AS build
WORKDIR /src
ARG CONFIG
ARG APP_BASE_URL
RUN npm install -g @angular/cli

COPY package*.json ./
RUN npm ci

COPY . ./
RUN echo "APP_BASE_URL is set to: ${APP_BASE_URL}"
RUN sed -i "s|\$APP_BASE_URL|${APP_BASE_URL}|g" src/environments/environment.${CONFIG}.ts
RUN ng build --configuration=${CONFIG}

FROM nginx:stable AS final
EXPOSE 80

COPY --from=build src/dist/frontend-spa-app/browser  /usr/share/nginx/html