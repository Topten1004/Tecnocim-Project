FROM node:16-alpine as build
LABEL org.opencontainers.image.source https://github.com/josuedotpro/MiniApp

COPY ./ /app
WORKDIR /app

RUN npm install && \
    npm run build

FROM nginx
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx","-g","daemon off;"]
