#https://learn.microsoft.com/zh-tw/ef/core/cli/dotnet
STARTUP_PATH = ./din-im-liau/
PROJECT_PATH = ./Libraries/Models
MAIN_PROJECT_PATH = ./din-im-liau/din-im-liau.csproj
dbupdate:
	dotnet ef database update -s $(STARTUP_PATH) -p $(PROJECT_PATH)

.PHONY: dbupdate


migration:
	dotnet ef migrations add $(add) -s $(STARTUP_PATH) -p $(PROJECT_PATH) 

.PHONY: migration


mlist:
	dotnet ef migrations list -s $(STARTUP_PATH) -p $(PROJECT_PATH) 

.PHONY: migration

rollback:
	dotnet ef database update $(target) -s $(STARTUP_PATH) -p $(PROJECT_PATH) 

m-remove:
	dotnet ef migrations remove -s $(STARTUP_PATH) -p $(PROJECT_PATH) 


.PHONY: migration


page:
	dotnet aspnet-codegenerator razorpage -p $(MAIN_PROJECT_PATH) -n $(STARTUP_PATH) -m Account -dc Models.DataModels.DataContext  -udl -outDir Pages/Account --referenceScriptLibraries --databaseProvider mysql