PROJ_PATH=FSharp.Data.GraphQL.ProviderTests.NetFramework/FSharp.Data.GraphQL.ProviderTests.NetFramework.fsproj
EXE_PATH=FSharp.Data.GraphQL.ProviderTests.NetFramework/bin/Debug/net461/FSharp.Data.GraphQL.ProviderTests.NetFramework.exe

export FrameworkPathOverride=/usr/lib/mono/4.6.1-api/

if ! [ -e "$EXE_PATH" ]
then
    dotnet build PROJ_PATH
fi

mono "$EXE_PATH" $1