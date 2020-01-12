dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:Exclude="[xunit.*]*%2c[XptoPortalApi.EFMigrate*]*"
# Integration
./tools/reportgenerator -reports:./tests/Integration/coverage.cobertura.xml -targetdir:./tests/Integration/CodeCoverage/ -reporttypes:"HtmlInline_AzurePipelines;Cobertura"
open ./tests/Integration/CodeCoverage/index.htm
