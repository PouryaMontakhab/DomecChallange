The project is composed of two controllers.
  ShoppingController which is responsible for Create,Update,Delete,Get and also getting all of the products.
  AuthController which is reponsible for authentication. By default there are two initialized users 
  ({username: mirco, password: mirco123},{username: simone, password: simone123}) Which you can login by them. Shopping controlle is protected and you must be 
  authenticate first. You can test it by Swagger or Postman.
  
  
  You can run project with docker run command or docker-compose : 
    
    docker run => docker run --rm -it -p 44311:44311 -e ASPNETCORE_URLS="https://+:44311;http://+:80" -e ASPNETCORE_ENVIRONMENT=Development -e ASPNETCORE_HTTPS_PORT=44311 -e ASPNETCORE_Kestrel__Certificates__Default__Password="admin@123" -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx -v %USERPROFILE%\.aspnet\https:/https/ domec-challange
    
    docker compose => docker-compose -f "docker-compose" up -d
    
