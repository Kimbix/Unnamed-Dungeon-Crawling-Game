build:
	dotnet publish -c Release -r linux-x64 /p:PublishReadyToRun=false /p:TieredCompilation=false --self-contained
run:
	dotnet run
content:
	dotnet mgcb-editor
debug:
	dotnet publish -c Debug -r linux-x64 --self-contained
publish-run:
	./bin/Release/net6.0/linux-x64/publish/Unnamed-Dungeon-Crawling-Game
