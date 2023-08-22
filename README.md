Small fix plugin for Digimon Survive (PC/Steam)
===============================================

This plugin resolves several game bugs:

  * With Turkish regional settings in Windows, the game gets stuck
    during startup: it shows only a black screen and is unplayable.

  * At 16:10 screen resolutions, if the Profile menu is opened
    between meeting Miu and meeting Kaito, it glitches and
    becomes mostly unusable until Digimon Survive is relaunched.

  * In many languages, in the Help menu, the “For default control
    settings” footnote is invisible when the menu is opened for
    the first time and appears only when the menu is reopened.

  * At screen resolutions other than 1920×1080 & 1920×1200,
    video cutscenes appear pixelated.

  * The first version of the game froze/crashed on some computers
    when a video cutscene ended or was skipped. This has been
    fixed in an official update, but this plugin includes
    an independent (but similar) fix as a precaution, which
    works equally well on old and new versions of the game.


Installing
----------

 1. In your Steam Library, right-click Digimon Survive (or click the gear
    button for Digimon Survive) and select Manage → Browse Local Files
    or Properties → Local Files → Browse. This will open the game’s
    file folder.

 2. Download BepInEx 5 x64 from https://github.com/BepInEx/BepInEx/releases

 3. Copy/extract BepInEx into the game’s folder,
    so that the folder looks like this:

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

 4. Create a “plugins” folder inside the BepInEx folder.

 5. Copy/extract SmallFixPlugin.dll into the plugins folder.

 6. If you’re using Linux/Proton (including Steam Deck), open Digimon Survive
    in your Steam Library, press the gear button, select Properties, and put
    this in Launch Options:

        WINEDLLOVERRIDES="winhttp=n,b" %command%

That’s it. The mod will be active whenever you launch the game.


Upgrading from VideoFixPlugin.dll
---------------------------------

In an earlier version, this plugin was known as the “video fix plugin”.
If you have VideoFixPlugin.dll installed, you can delete it now
(but it is equally safe to keep it).


Combining with other mods
-------------------------

This can be combined with other mods, including other BepInEx plugins.
Just put all your BepInEx plugins in the same plugins folder.


Updates
-------

Find the latest version at:
  * https://gamebanana.com/mods/393721
  * https://github.com/astiob/digimon-survive-small-fixes
