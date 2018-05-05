# Redis Asp.Net Core (Sample)

Allow PubSub connect console app with asp.net core app.

### Run
```
docker build -t redis-mvc .
docker run -d -p 80:80 -e REDIS__CONNECTION=redis --name app redis-mvc 
```