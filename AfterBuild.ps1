dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
cd ../ExtensionMethods.UnitTests
dotnet coveralls --open-cover coverage.opencover.xml