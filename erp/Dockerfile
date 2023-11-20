FROM node:19

# 设置工作目录
WORKDIR /app

# 复制package.json和package-lock.json到工作目录
COPY package*.json ./

# 安装依赖
RUN cnpm install

# 复制所有文件到工作目录
COPY . .

# 运行构建命令
RUN npm run build

# 暴露容器的端口（如有需要）
EXPOSE 80

# 启动应用
CMD ["npm", "run", "serve"]
