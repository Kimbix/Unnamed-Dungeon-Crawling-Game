build:
	dotnet publish -c Release -r linux-x64 /p:PublishReadyToRun=false /p:TieredCompilation=false --self-contained
content:
	dotnet mgcb-editor
debug:
	./bin/Debug/net6.0/Unnamed-Dungeon-Crawling-Game
run:
	./bin/Release/net6.0/linux-x64/Unnamed-Dungeon-Crawling-Game
