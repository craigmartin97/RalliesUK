Api Migration Commands
dotnet ef migrations add InitialIdentity --project RalliesUK.Infrastructure --startup-project RalliesUK.API
dotnet ef database update --project RalliesUK.Infrastructure --startup-project RalliesUK.API