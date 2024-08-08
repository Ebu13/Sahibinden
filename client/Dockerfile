# Temel image olarak Node.js kullanıyoruz
FROM node:14

# Çalışma dizinini oluştur ve ayarla
WORKDIR /app

# package.json ve package-lock.json dosyalarını kopyala
COPY package*.json ./

# Bağımlılıkları yükle
RUN npm install

# Uygulama dosyalarını kopyala
COPY . .

# Uygulamayı derle
RUN npm run build

# Uygulamanın çalışacağı portu belirt
EXPOSE 3000

# Uygulamayı başlat
CMD ["npm", "start"]
