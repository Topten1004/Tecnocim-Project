21/10/2022 JOAQUIN SOSA MARTIN

1.- Pasos para ejecutar el scaffolding de Entity Framework Core 6 usando la "Database First" aproximación:

1.1- Primero se han creado dos proyectos de librería de clases, una para las entidades de dominio y otra para la infraestructura de Entity Framework:
- Tecnocim.Alia.Intermedia.Domain
- Tecnocim.Alia.Intermedia.DataInfrastructure

1.2.- Importar los nugets relativos a Entity Framework Core 6 en el proyecto de infraestructura y además:
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="6.0.8" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="6.0.8" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="6.0.8">

1.3.- Referenciar el proyecto de dominio desde el de infraestructura.

1.4.- En el proyecto principal de la API incluir en el appsettings la cadena de conexión de la nueva base de datos, en nuestro caso se llama "IntermediaConnection":
 "ConnectionStrings": {
    "DefaultConnection": "Server=38.242.155.155;Database=smartdebt-ef;User Id=XXXX;Password=XXXX;Trusted_Connection=False;MultipleActiveResultSets=true",
    "IntermediaConnection": "Server=38.242.155.155;Database=smartdebt-intermedia;User Id=XXXX;Password=XXXX;Trusted_Connection=False;MultipleActiveResultSets=true"
  },

1.5.- Abrir dentro de Visual Studio la Package Manager Console, seleccionar como Default Project el "Tecnocim.Alia.Intermedia.DataInfrastructure" y ejecutar el siguiente comando:

Scaffold-DbContext "Name=ConnectionStrings:IntermediaConnection" -Provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir ../../Domain/Tecnocim.Alia.Intermedia.Domain -ContextDir . -Context SmartdebtIntermediaContext -Namespace Tecnocim.Alia.Intermedia.Domain -ContextNamespace Tecnocim.Alia.Intermedia.DataInfrastructure

- Aquí le estamos indicando el nombre de la cadena de conexión a utilizar
- El proveedor de base de datos (Sql Server)
- En qué ruta queremos que genere las clases de las entidades
- En qué ruta queremos que genere la clase del contexto (en nuestro caso . porque hemos indicado nuestro default project el de infraestructura, y es aquí donde queremos que esté)
- Nombre del contexto
- Namespace para las clases generadas del dominio
- Namespace para la clase generada del contexto

Indicar que cuando se haya modificado la base de datos y se quiera generar de nuevo el modelo, usar el parametro -Force para sobreescribir los ficheros generados previamente.