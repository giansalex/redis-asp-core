# Redis Asp.Net Core (Sample)

### Run
```
docker build -t redis-mvc .
docker run -d -p 80:80 -e REDIS_CONNECTION=redis --name app redis-mvc 
```
