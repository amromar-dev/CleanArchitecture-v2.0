
param ([string] $name = "SampleMigration")

dotnet ef migrations add $name --project src\Infrastructure --startup-project src\API --output-dir Persistence\Migrations

Add-Migration {} -Project EntityFramework -StartupProject API -OutputDir Migrations

