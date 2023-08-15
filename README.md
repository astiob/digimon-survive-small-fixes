Small fix plugin for Digimon Survive (PC/Steam)
===============================================

This plugin resolves several game bugs:

  * At screen resolutions other than 1920x1080 & 1920x1200,
    video cutscenes appear pixelated.

  * At 16:10 screen resolutions, if the Profile menu is opened
    between meeting Miu and meeting Kaito, it glitches and
    becomes mostly unusable until Digimon Survive is relaunched.

  * In many languages, in the Help menu, the "For default control
    settings" footnote is invisible when the menu is opened for
    the first time and appears only when the menu is reopened.

  * The first version of the game froze/crashed on some computers
    when a video cutscene ended or was skipped. This has been
    fixed in an official update, but this plugin includes
    an independent (but similar) fix as a precaution, which
    works equally well on old and new versions of the game.


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

4. Put SmallFixPlugin.dll in the plugins folder inside the BepInEx folder.

5. Simply start the game and enjoy.


Combining with other mods
-------------------------

This can be combined with other mods, including other BepInEx plugins.
Just put all your BepInEx plugins in the same plugins folder.