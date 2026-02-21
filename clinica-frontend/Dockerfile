# Etapa 1: Build do Angular
FROM node:20 AS build
WORKDIR /app

# Instala dependências
COPY package*.json ./
RUN npm install

# Copia o código e gera o build de produção
COPY . .
RUN npm run build -- --configuration production

# Etapa 2: Servidor Nginx
FROM nginx:alpine

# Remove a configuração padrão do Nginx
RUN rm /etc/nginx/conf.d/default.conf

# Copia nossa configuração personalizada (vamos criar abaixo)
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Copia os arquivos gerados pelo Angular para a pasta do Nginx
# ATENÇÃO: Verifique se o nome da pasta em 'dist' é 'clinica-front' ou 'clinica-frontend'
# Se tiver dúvida, rode 'npm run build' e veja o nome da pasta gerada em /dist
COPY --from=build /app/dist/clinica-front/browser /usr/share/nginx/html

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]