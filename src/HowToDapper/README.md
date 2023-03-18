# How to Dapper

Simple console application to showcase how to use basic [Dapper](https://github.com/DapperLib/Dapper) extensions together with [Npgsql](https://github.com/npgsql/npgsql). More info here.

Once running, the application will:
- create a Payments table
- insert a couple of payments
- fetch and display some payments
- [optional] clean-up the database

To run the app, you need a running PostgreSQL server with the following connection properties:

```JSON
{
   "Host": "localhost",
   "Port": 5432,
   "Username": "postgres",
   "Password": "password"
}
```

If you use docker, just run the following command in a shell/prompt:

```shell
$ docker run -d -p 5432:5432 postgres
```
