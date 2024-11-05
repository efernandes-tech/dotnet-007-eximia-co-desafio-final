# Notes:

- Run database:

```
cd dependencies-compose
sed -i 's/\r$//' mssql/entrypoint.sh
sed -i 's/\r$//' mssql/run-initialization.sh
sed -i 's/\r$//' mssql/create-database.sql
docker-compose up --build -d
```

# Links:

- https://github.com/eximiaco/csharp-course/tree/main
