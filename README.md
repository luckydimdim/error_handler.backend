Создание NuGet пакета:
* dotnet restore
* dotnet pack --configuration Release
* nuget push -source http://cm-ylng-msk-04/nuget/nuget d:\Dev\cmas.backend\error_handler.backend\111\error_handler.backend.1.0.0.nupkg