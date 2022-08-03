Video fix plugin for Digimon Survive (PC/Steam)
===============================================

This plugin resolves problems with the game's video cutscenes:

  * On some computers, the game freezes/crashes when the video ends or is skipped.

  * At screen resolutions other than 1920x1080 & 1920x1200, the video appears pixelated.

Tested with Digimon Survive build 8976793 (the current public version as of 2/3 August 2022, released on 29 July 2022).


Installing
----------

1. Download BepInEx 5 x64 from https://github.com/BepInEx/BepInEx/releases

2. Unpack BepInEx into the root folder of the game, so that the folder looks like this:

        BepInEx
        DigimonSurvive_Data
        MonoBleedingEdge
        Support
        changelog.txt
        DigimonSurvive.exe
        doorstop_config.ini
        UnityCrashHandler64.exe
        UnityPlayer.dll
        winhttp.dll

3. Create a "plugins" folder inside the BepInEx folder.

4. Put VideoFixPlugin.dll in the plugins folder inside the BepInEx folder.

5. Simply start the game and enjoy.


Combining with other mods
-------------------------

This can be combined with other mods, including other BepInEx plugins.
Just put all your BepInEx plugins in the same plugins folder.