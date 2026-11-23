# FabbierBot
this is proyect for my school.
# Trabajar con el registro de NuGet

Puede configurar la dotnet interfaz de línea de comandos (CLI) para publicar paquetes NuGet en GitHub Packages y usar paquetes almacenados en GitHub Packages como dependencias en un proyecto de .NET.

<!-- 2148AF7B-5FF8-4B28-A808-D692FEE2225A -->

## Autenticación en GitHub Packages

> [!NOTE]
> GitHub Packages solo admite la autenticación usando un personal access token (classic). Para más información, consulta [Administración de tokens de acceso personal](/es/authentication/keeping-your-account-and-data-secure/managing-your-personal-access-tokens).

Necesitas un token de acceso para publicar, instalar y eliminar paquetes privados, internos y públicos.

Puedes utilizar un personal access token (classic) para autenticarte en el GitHub Packages o en la API de GitHub. Cuando creas un personal access token (classic), puedes asignar al token diferentes ámbitos en función de tus necesidades. Para más información sobre los ámbitos relacionados con paquetes para un personal access token (classic), consulta [Acerca de los permisos para los Paquetes de GitHub](/es/packages/learn-github-packages/about-permissions-for-github-packages#about-scopes-and-permissions-for-package-registries).

Para autenticarte en un registro del GitHub Packages dentro de un flujo de trabajo de GitHub Actions, puedes utilizar:

* `GITHUB_TOKEN` para publicar los paquetes asociados con el repositorio del flujo de trabajo.
* Un personal access token (classic) con al menos alcance `read:packages` para instalar los paquetes asociados con otros repositorios privados (`GITHUB_TOKEN` puede utilizarse si el repositorio tiene acceso de lectura al paquete. Consulta [Configurar la visibilidad y el control de accesos de un paquete](/es/packages/learn-github-packages/configuring-a-packages-access-control-and-visibility)).

### Autenticación en un GitHub Actions flujo de trabajo

Este registro admite permisos granulares.
En los registros que admiten permisos granulares, si tu flujo de trabajo GitHub Actions usa un personal access token para autenticarse en un registro, te recomendamos encarecidamente que actualices tu flujo de trabajo para usar `GITHUB_TOKEN`. Para obtener instrucciones sobre cómo actualizar los flujos de trabajo que se autentican en un registro con personal access token, consulte [Publicar e instalar un paquete con Acciones de GitHub](/es/packages/managing-github-packages-using-github-actions-workflows/publishing-and-installing-a-package-with-github-actions#upgrading-a-workflow-that-accesses-a-registry-using-a-personal-access-token).

> [!NOTE]
> La capacidad de que los flujos de trabajo de GitHub Actions eliminen y restauren paquetes mediante la API de REST se encuentra actualmente en versión preliminar pública y está sujeta a cambios.

Puede usar un `GITHUB_TOKEN` en un flujo de trabajo de GitHub Actions para eliminar o restaurar un paquete mediante la API de REST, si el token tiene permiso de `admin` para el paquete. A los repositorios que publican paquetes mediante un flujo de trabajo y a los repositorios que se han conectado explícitamente a los paquetes se les concede automáticamente el permiso `admin` para los paquetes del repositorio.

Para obtener más información sobre `GITHUB_TOKEN`, consulta [Uso de GITHUB\_TOKEN para la autenticación en flujos de trabajo](/es/actions/tutorials/authenticate-with-github_token#using-the-github_token-in-a-workflow). Para obtener más información sobre los procedimientos recomendados al usar un registro en acciones, consulta [Ejecutores en peligro](/es/actions/concepts/security/compromised-runners#cross-repository-access).

Use el siguiente comando para autenticarse en GitHub Packages en un flujo de trabajo de GitHub Actions usando `GITHUB_TOKEN` en lugar de codificar de forma rígida un elemento personal access token en un archivo nuget.config del repositorio:

```shell
dotnet nuget add source --username USERNAME --password ${{ secrets.GITHUB_TOKEN }} --store-password-in-clear-text --name github "https://nuget.pkg.github.com/NAMESPACE/index.json"
```

Reemplace por `NAMESPACE` el nombre de la cuenta personal u organización a la que se los paquetes.

Reemplace `USERNAME` con el nombre de usuario que se usará al conectarse a un origen autenticado.

También puede optar por conceder permisos de acceso a los paquetes de forma independiente paraGitHub Codespaces yGitHub Actions . Para más información, consulta [Configurar la visibilidad y el control de accesos de un paquete](/es/packages/learn-github-packages/configuring-a-packages-access-control-and-visibility#ensuring-github-codespaces-access-to-your-package) y [Configurar la visibilidad y el control de accesos de un paquete](/es/packages/learn-github-packages/configuring-a-packages-access-control-and-visibility#ensuring-workflow-access-to-your-package).

### Autenticación con personal access token

> [!NOTE]
> GitHub Packages solo admite la autenticación usando un personal access token (classic). Para más información, consulta [Administración de tokens de acceso personal](/es/authentication/keeping-your-account-and-data-secure/managing-your-personal-access-tokens).

Necesitas un token de acceso para publicar, instalar y eliminar paquetes privados, internos y públicos.

Puedes utilizar un personal access token (classic) para autenticarte en el GitHub Packages o en la API de GitHub. Cuando creas un personal access token (classic), puedes asignar al token diferentes ámbitos en función de tus necesidades. Para más información sobre los ámbitos relacionados con paquetes para un personal access token (classic), consulta [Acerca de los permisos para los Paquetes de GitHub](/es/packages/learn-github-packages/about-permissions-for-github-packages#about-scopes-and-permissions-for-package-registries).

Para autenticarte en un registro del GitHub Packages dentro de un flujo de trabajo de GitHub Actions, puedes utilizar:

* `GITHUB_TOKEN` para publicar los paquetes asociados con el repositorio del flujo de trabajo.
* Un personal access token (classic) con al menos alcance `read:packages` para instalar los paquetes asociados con otros repositorios privados (`GITHUB_TOKEN` puede utilizarse si el repositorio tiene acceso de lectura al paquete. Consulta [Configurar la visibilidad y el control de accesos de un paquete](/es/packages/learn-github-packages/configuring-a-packages-access-control-and-visibility)).

Debes utilizar un personal access token (classic) con los ámbitos adecuados para publicar e instalar paquetes en GitHub Packages. Para más información, consulta [Introducción a los paquetes de GitHub](/es/packages/learn-github-packages/introduction-to-github-packages#authenticating-to-github-packages).

Para autenticarse en GitHub Packages mediante la `dotnet` interfaz de línea de comandos (CLI), cree un archivo *nuget.config* en el directorio del proyecto en el que se especifique GitHub Packages como una fuente en `packageSources` para el cliente `dotnet` CLI.

Debes reemplazar:

* `USERNAME` con el nombre de su cuenta personal en GitHub.
* `TOKEN` con tu personal access token (classic).
* `NAMESPACE` con el nombre de la cuenta personal u organización a la que pertenecen tus paquetes.

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
    <packageSources>
        <clear />
        <add key="github" value="https://nuget.pkg.github.com/NAMESPACE/index.json" />
    </packageSources>
    <packageSourceCredentials>
        <github>
            <add key="Username" value="USERNAME" />
            <add key="ClearTextPassword" value="TOKEN" />
        </github>
    </packageSourceCredentials>
</configuration>
```

## Publicación de un paquete

> [!NOTE]
> El archivo `nupkg` para una versión del paquete NuGet debe tener un tamaño inferior a 2,147 GB.

Puede publicar un paquete en GitHub Packages autenticándose con un archivo *nuget.config*, usando la opción de línea de comandos `--api-key` con su GitHubpersonal access token (classic), o mediante un comando que puede ejecutarse directamente desde la línea de comandos con la interfaz de línea de comandos (CLI) `dotnet`.

Sustituya `OWNER` por su nombre de usuario o el nombre de su empresa, y `YOUR_GITHUB_PAT` por su personal access token.

```shell
dotnet nuget add source --username OWNER --password YOUR_GITHUB_PAT --store-password-in-clear-text --name github "https://nuget.pkg.github.com/OWNER/index.json"
```

El registro de NuGet almacena paquetes en tu cuenta personal o de la organización y te permite asociar los paquetes a un repositorio. Puedes elegir si quieres heredar permisos desde un repositorio o si quieres configurar permisos granulares independientemente de un repositorio.

Cuando publicas un paquete por primera vez, la visibilidad predeterminada es privada. Para cambiar la visibilidad o establecer permisos de acceso, consulta [Configurar la visibilidad y el control de accesos de un paquete](/es/packages/learn-github-packages/configuring-a-packages-access-control-and-visibility). Para obtener más información sobre cómo vincular un paquete publicado con un repositorio, consulte [Conectar un repositorio a un paquete](/es/packages/learn-github-packages/connecting-a-repository-to-a-package).

Si especifica un `RepositoryURL` en el archivo *.csproj* del project, el paquete publicado se conectará automáticamente al repositorio especificado. Para obtener más información, vea [Trabajar con el registro de NuGet](/es/packages/working-with-a-github-packages-registry/working-with-the-nuget-registry#publishing-a-package-using-a-nugetconfig-file). Para obtener información sobre cómo vincular un paquete ya publicado a un repositorio, consulte [Conectar un repositorio a un paquete](/es/packages/learn-github-packages/connecting-a-repository-to-a-package).

### Publicar un paquete usando GitHub personal access token como tu clave de API

Si aún no dispones de un personal access token para utilizarlo con tu cuenta en GitHub, consulta [Administración de tokens de acceso personal](/es/authentication/keeping-your-account-and-data-secure/managing-your-personal-access-tokens).

1. Cree un nuevo proyecto. Reemplace `PROJECT_NAME` por el nombre que desea asignar al project.

   ```shell
   dotnet new console --name PROJECT_NAME
   ```

2. Empaqueta el proyecto.

   ```shell
   dotnet pack --configuration Release
   ```

3. Publique el paquete mediante personal access token como clave de API. Reemplace `PROJECT_NAME` por el nombre del proyecto, `1.0.0` por el número de versión del paquete y `YOUR_GITHUB_PAT` por personal access token.

   ```shell
   dotnet nuget push "bin/Release/PROJECT_NAME.1.0.0.nupkg" --api-key YOUR_GITHUB_PAT --source "github"
   ```

Después de que publiques un paquete, puedes verlo en GitHub. Para más información, consulta [Visualizar paquetes](/es/packages/learn-github-packages/viewing-packages).

### Publicación de un paquete mediante un archivo *nuget.config*

Al publicar, si vinculas tu paquete a un repositorio, el `OWNER` del repositorio especificado en tu archivo *.csproj* debe coincidir con el `NAMESPACE` que utilizas en tu archivo de autenticación *nuget.config*. Especifique o incremente el número de versión en el archivo *.csproj* y, a continuación, use el comando `dotnet pack` para crear un archivo *.nuspec* para esa versión. Para obtener más información sobre cómo crear el paquete, consulta [Creación y publicación de un paquete](https://docs.microsoft.com/nuget/quickstart/create-and-publish-a-package-using-the-dotnet-cli) en la documentación de Microsoft.

> [!NOTE]
> Si publicas un paquete vinculado a un repositorio, el paquete hereda automáticamente los permisos de acceso del repositorio vinculado y los flujos de trabajo de GitHub Actions en el repositorio vinculado automáticamente obtienen acceso al paquete, a menos que la organización haya deshabilitado la herencia automática de los permisos de acceso. Para más información, consulta [Configurar la visibilidad y el control de accesos de un paquete](/es/packages/learn-github-packages/configuring-a-packages-access-control-and-visibility#about-inheritance-of-access-permissions).

1. Autentícate en GitHub Packages. Para obtener más información, consulta [Autenticación en GitHub Packages](#authenticating-to-github-packages).

2. Cree un nuevo proyecto. Reemplace `PROJECT_NAME` por el nombre que desea asignar al project.

   ```shell
   dotnet new console --name PROJECT_NAME
   ```

3. Agregue la información específica de su proyecto al archivo del proyecto, que termina en *.csproj*. Asegúrese de reemplazar:

   * `1.0.0` con el número de versión del paquete.
   * `OWNER`con el nombre de la cuenta personal o la organización que posee el repositorio al que desea vincular el paquete para publicar el paquete.
   * `REPOSITORY` con el nombre del repositorio al que desea conectar el paquete.

   ```xml
   <Project Sdk="Microsoft.NET.Sdk">

     <PropertyGroup>
       <OutputType>Exe</OutputType>
       <TargetFramework>netcoreapp3.0</TargetFramework>
       <PackageId>PROJECT_NAME</PackageId>
       <Version>1.0.0</Version>
       <Authors>AUTHORS</Authors>
       <Company>COMPANY_NAME</Company>
       <PackageDescription>PACKAGE_DESCRIPTION</PackageDescription>
       <RepositoryUrl>https://github.com/OWNER/REPOSITORY</RepositoryUrl>
     </PropertyGroup>

   </Project>
   ```

4. Empaqueta el proyecto.

   ```shell
   dotnet pack --configuration Release
   ```

5. Publique el paquete con el `key` que especificó en el archivo *nuget.config*. Reemplace `PROJECT_NAME` por el nombre del project y reemplace `1.0.0` por el número de versión del paquete.

   ```shell
   dotnet nuget push "bin/Release/PROJECT_NAME.1.0.0.nupkg" --source "github"
   ```

Después de que publiques un paquete, puedes verlo en GitHub. Para más información, consulta [Visualizar paquetes](/es/packages/learn-github-packages/viewing-packages).

## Publicar múltiples paquetes en el mismo repositorio

Para conectar varios paquetes al mismo repositorio, use la misma GitHub dirección URL del repositorio en los `RepositoryURL` campos de todos los archivos *de proyecto .csproj* .
GitHub compara el repositorio basándose en ese campo.

En el ejemplo siguiente se publican los proyectos MY\_APP y MY\_OTHER\_APP en el mismo repositorio:

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>netcoreapp3.0</TargetFramework>
    <PackageId>MY_APP</PackageId>
    <Version>1.0.0</Version>
    <Authors>Octocat</Authors>
    <Company>GitHub</Company>
    <PackageDescription>This package adds a singing Octocat!</PackageDescription>
    <RepositoryUrl>https://github.com/my-org/my-repo</RepositoryUrl>
  </PropertyGroup>

</Project>
```

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>netcoreapp3.0</TargetFramework>
    <PackageId>MY_OTHER_APP</PackageId>
    <Version>1.0.0</Version>
    <Authors>Octocat</Authors>
    <Company>GitHub</Company>
    <PackageDescription>This package adds a dancing Octocat!</PackageDescription>
    <RepositoryUrl>https://github.com/my-org/my-repo</RepositoryUrl>
  </PropertyGroup>

</Project>
```

## Instalación de un paquete

El uso de paquetes de GitHub en el proyecto es similar al uso de paquetes de *nuget.org*. Agregue las dependencias del paquete al archivo *.csproj* y especifique el nombre y la versión del paquete. Para obtener más información sobre el uso de un archivo *.csproj* en la project, consulte [Working with NuGet packages](https://docs.microsoft.com/nuget/consume-packages/overview-and-workflow) en la documentación de Microsoft.

1. Autentícate en GitHub Packages. Para obtener más información, consulta [Autenticación en GitHub Packages](#authenticating-to-github-packages).

2. Para usar un paquete, agregue `ItemGroup` y configure el campo `PackageReference` en el archivo *.csproj*. Reemplace el valor `PACKAGE_NAME` de `Include="PACKAGE_NAME"` por la dependencia de su paquete, y sustituya el valor `X.X.X` de `Version="X.X.X"` por la versión del paquete que desea utilizar:

   ```xml
   <Project Sdk="Microsoft.NET.Sdk">

     <PropertyGroup>
       <OutputType>Exe</OutputType>
       <TargetFramework>netcoreapp3.0</TargetFramework>
       <PackageId>My-app</PackageId>
       <Version>1.0.0</Version>
      <Authors>Octocat</Authors>
       <Company>GitHub</Company>
      <PackageDescription>This package adds an Octocat!</PackageDescription>
       <RepositoryUrl>https://github.com/OWNER/REPOSITORY</RepositoryUrl>
     </PropertyGroup>

     <ItemGroup>
       <PackageReference Include="PACKAGE_NAME" Version="X.X.X" />
     </ItemGroup>

   </Project>
   ```

3. Instale los paquetes con el comando `restore`.

   ```shell
   dotnet restore
   ```

## Solución de problemas

Si utiliza un `GITHUB_TOKEN` para autenticarse en un registro de GitHub Packages dentro de un flujo de trabajo de GitHub Actions, el token no puede acceder a paquetes privados basados en repositorios de otro repositorio distinto de aquel en el que se ejecuta el flujo de trabajo. Para acceder a los paquetes asociados a otros repositorios, genere un personal access token (classic) con el ámbito `read:packages` y páselo como secreto.

### Errores intermitentes 403 al restaurar paquetes públicos

Si usa GitHub Packages junto con *nuget.org* y experimenta errores intermitentes de 403 Prohibido al restaurar paquetes públicos estándar (como `Microsoft.Extensions.*`), esto puede ocurrir porque NuGet consulta todos los orígenes de paquetes configurados para cada paquete. Si GitHub Packages se produce un error de autenticación temporalmente, puede bloquear toda la restauración, incluso para los paquetes que no existen en GitHub Packages.

Para evitar esto, use la [asignación de origen del paquete NuGet](https://learn.microsoft.com/nuget/consume-packages/package-source-mapping) para enrutar paquetes a orígenes específicos.

Reemplazar:

* `NAMESPACE` con el nombre de la cuenta personal o la organización que es propietaria de tu GitHub Packages feed de NuGet.
* `PACKAGE-ID-PREFIX` con el prefijo de identificador de paquete de NuGet que se usa para los paquetes alojados en GitHub Packages. Si usa varios prefijos, agregue entradas adicionales `<package>` para cada prefijo.

```xml
<configuration>
    <packageSources>
        <add key="nuget.org" value="https://api.nuget.org/v3/index.json" />
        <add key="github" value="https://nuget.pkg.github.com/NAMESPACE/index.json" />
    </packageSources>
    <packageSourceMapping>
        <packageSource key="nuget.org">
            <package pattern="*" />
        </packageSource>
        <packageSource key="github">
            <package pattern="PACKAGE-ID-PREFIX.*" />
        </packageSource>
    </packageSourceMapping>
</configuration>
```

NuGet usa el [patrón de coincidencia más específico](https://learn.microsoft.com/nuget/consume-packages/package-source-mapping#package-pattern-precedence), por lo que los paquetes coincidentes `PACKAGE-ID-PREFIX.*` solo se capturan de GitHub Packages, mientras que todos los demás paquetes se capturan de *nuget.org*. Esto también ayuda a evitar ataques de confusión de dependencias asegurándose de que los paquetes privados solo pueden provenir de la GitHub Packages fuente.

## Información adicional

* [Borrar y restablecer un paquete](/es/packages/learn-github-packages/deleting-and-restoring-a-package)
