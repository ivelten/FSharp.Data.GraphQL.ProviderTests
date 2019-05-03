PROJ_PATH=FSharp.Data.GraphQL.ProviderTests.NetCoreApp/FSharp.Data.GraphQL.ProviderTests.NetCoreApp.fsproj
EXE_PATH=FSharp.Data.GraphQL.ProviderTests.NetCoreApp/bin/Debug/netcoreapp2.1/FSharp.Data.GraphQL.ProviderTests.NetCoreApp.dll

if ! [ -e "$EXE_PATH" ]
then
    dotnet build PROJ_PATH
fi

dotnet "$EXE_PATH" $1