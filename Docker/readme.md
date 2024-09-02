# Markdown File

### 初次啟動或在你需要重新啟動服務時使用以下命令

```bash
docker-compose up -d
```

## 以下是一些常用的 Docker Compose 命令，用於管理你的容器：

### 啟動容器

```bash
docker-compose start
```

### 停止容器

```bash
docker-compose stop
```

### 重啟容器

```bash
docker-compose restart
```

### 關閉容器

```bash
docker-compose down
```

### 查看容器日誌

```bash
docker-compose logs
```

### 查看容器狀態

```bash
docker-compose ps
```

### 進入容器

```bash
docker exec -it xxxxxx bash

psql -h localhost -U user -d clean_db
```