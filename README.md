# FSharp.Data.GraphQL.ProviderTests

This repository was created to test package installation of FSharp.Data.GraphQL packages on Windows, Linux and OSX.

Since TP provider installation is somewhat complicated to test, this repository was built to help testing it.

## How to configure

Testing of the provider is done by using a local NuGet Server. The implementation used to build this project is [this one](https://github.com/NuGet/NuGet.Server).

After installing and configuring the server, you need to build the packages from the official repository and publishing them to the server.

After publishing to the server, change the source line of the [paket.dependencies](paket.dependencies) file to match the url of the local server.

Be sure about the version that is being published.