FROM node:lts-slim AS build
WORKDIR /src
ARG CONFIG
RUN npm install -g @angular/cli

COPY package*.json ./
RUN npm ci

COPY . ./
RUN ng build --configuration=${CONFIG}

ENV APP_BASE_URL=${APP_BASE_URL}
ENV APP_NAME=${APP_NAME}

FROM nginx:stable AS final
EXPOSE 80

COPY --from=build src/dist/frontend-spa-app/browser  /usr/share/nginx/html
CMD ["/bin/sh",  "-c",  "envsubst < /usr/share/nginx/html/assets/config.template.json > /usr/share/nginx/html/assets/config.json && exec nginx -g 'daemon off;'"]