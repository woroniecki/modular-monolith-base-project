FROM node:lts-slim AS build
WORKDIR /src
ARG CONFIG
COPY package*.json ./
RUN npm ci
COPY . .
RUN npm install -g @angular/cli && ng build --configuration=${CONFIG}

FROM caddy:alpine
RUN apk add --no-cache gettext

COPY --from=build /src/dist/frontend-spa-app/browser /usr/share/caddy
COPY Caddyfile /etc/caddy/Caddyfile

CMD ["/bin/sh", "-c", "envsubst < /usr/share/caddy/assets/config.template.json > /usr/share/caddy/assets/config.json && caddy run --config /etc/caddy/Caddyfile"]