# Levantar Proyecto User.Api

1. La base de datos utilizada para el proyecto es SQL SERVER
2. Solo habria el conection string en el archivo appsettings.json por defecto el nombre de la base de datos para almacenar toda la informacion se llama **DbUsers**
3. Despues correr la migracion
    1. Package manager: **Update-Database**
    2. Línea de comandos: **dotnet ef database update**
4. Con toda estas revisiones no deberia tener problemas