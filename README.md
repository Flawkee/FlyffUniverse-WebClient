# Flyff Universe Web Client
 Smart Webclient for Flyff Universe

Flyff Universe Web Client allows you to play Flyff Universe natively
with dedicated client.
It eliminates the need for using a browser like Google Chrome that takes additional resources and impact the game performance:



- Better FPS & Latency
- Flexible for your needs
- Ease of use for multipe characters in parallel

## Features

- Cache is saved per profile meaning different settings and auto login for multiple characters is possible with one click!
- Set Window Mode / Full Screen.
- Set custom resolution.
- Launch the web client anywhere in your screen you'd like.

## Tech

Flyff Universe Web Client is writted in C# on .Net 4.5.2 using CefSharp - Open Source Wrapper for chromium framework.

## Installation

.Net 4.5.2 is required.

1. [Download the latest release from the project's GitHub.](https://github.com/Flawkee/FlyffUniverse-WebClient/releases)
2. Extract it in a convinient location.
3. Create a shortcut of _"Flyff Universe Webclient.exe"_ and send it to your desktop (or any other location).
4. Configure flags and run!

## Configuration Flags

You can launch the web client with default setting as follows:
1. DefaultPorfile will be created in %appdata%\FlyffUniverse\ saving your character settings and cache.
2. Window mode is selected.
3. Resolution is set to 1920x1080.

However it is recommended to set and customize the flags per character needs using multiple shortcuts.
1. Create a shortcut for the web client .exe
2. Right click the shortuct and select properties
3. On **Target** add a space after the execution file path and add required flags

| flag | example | description |
| ------ | ------ | ------ |
| /ProfileName=<ProfileName> | /ProfileName=Flawkee | Creates a profile for the character saving it's settings & allow auto login. Use on per-character basis.
| /Resolution=<WidthxHeight> | /Resolution=1920x1080 | Set resolution for Window Mode (default: 1920x1080). Does not have effect in Fullscreen mode.
| /Fullscreen | /Fullscreen | Set Fullscreen mode.
| /PixelLocation=<x,y> | /PixelLocation=3850,-80 | Launch the web client in a specific pixel location on your Windows envrionment. Override /DisplayID flag.
| /DisplayID=<ID>| /DisplayID=2 | Launch the web client in a specific display. Use your display ID shown in Window Display Settings. Override /PixelLocation flag.

**Examples:**

Shortcut 1: _"C:\SomeFolderPath\FlyffUniverse-WebClient\Flyff Universe Webclient.exe" /profilename=Flawkee /fullscreen /displayID=2_

Shortcut 2: _"C:\SomeFolderPath\FlyffUniverse-WebClient\Flyff Universe Webclient.exe" /profilename=FlawkeeLeech /resolution=800x1000 /PixelLocation=3850,-80