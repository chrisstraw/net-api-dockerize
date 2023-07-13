# Example Container Example for .net core api


### Docker Build (from root directory)

```
docker build -t example-container-webapi -f src/Host/Example.Container.Host.WebApi/Dockerfile .
```

### To Run 
```
docker run -p 9696:80 -p 9697:443 -d example-container-webapi:latest
```

### Open the browser to
```
http://localhost:9696/swagger
```
