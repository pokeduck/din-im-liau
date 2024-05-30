#https://learn.microsoft.com/zh-tw/ef/core/cli/dotnet
STARTUP_PATH = ./din-im-liau/
PROJECT_PATH = ./Libraries/Models

dbupdate:
	dotnet ef database update -s $(STARTUP_PATH) -p $(PROJECT_PATH)

.PHONY: dbupdate


migration:
	dotnet ef migrations add $(add) -s $(STARTUP_PATH) -p $(PROJECT_PATH) 

.PHONY: migration
