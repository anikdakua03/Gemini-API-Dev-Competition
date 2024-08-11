FROM node:21 AS build
WORKDIR /app
COPY package*.json ./
RUN npm install --legacy-peer-deps
COPY . .
RUN npm run build
# removing extra node module s, no more needed
# RUN rm -rf node_modules
# Stage 2: Serve the angular app with Nginx
FROM nginx:latest
WORKDIR /usr/share/nginx/html
# when ng build can see it is under browser also , so ..
# this was for without SSR
COPY --from=build /app/dist/my-gemini-ui/browser /app/dist/my-gemini-ui/server /usr/share/nginx/html/
# this is for with SSR, so we need both browser and server folder
# COPY --from=build /app/dist/my-gemini-ui /usr/share/nginx/html
COPY default.conf /etc/nginx/conf.d/default.conf
EXPOSE 80