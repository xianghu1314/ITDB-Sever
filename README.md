# 夺宝系统服务端
+ 数据库迁移
```
https://docs.microsoft.com/zh-cn/ef/core/managing-schemas/migrations/?view=aspnetcore-1.0
```
# docker
```
docker build -t itdb .
docker run -it --rm -p 5001:80 --env "MySqlConnection=Server=47.104.175.65:30336;database=itdb;uid=root;pwd=12345678;SslMode=None;TreatTinyAsBoolean=true"  --name itdb_sample itdb 
```