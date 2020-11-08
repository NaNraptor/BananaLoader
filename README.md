### GENERAL INFORMATION:

- Normal Console is for Debugging and Displaying the Output of Plugins and Mods.
- Debug Mode/Console is for Debugging BananaLoader Internals.
- All Plugins get placed in the created Plugins folder inside the Game's Install Folder.
- All Mods get placed in the created Mods folder inside the Game's Install Folder.
- All Logs are made in the created Logs folder inside the Game's Install Folder.
- In order to properly use dnSpy's Debugger run the game with the Debug Mode/Console launch option.

---

### REQUIREMENTS:

- [.NET Framework 4.8 Runtime](https://dotnet.microsoft.com/download/dotnet-framework/thank-you/net48-web-installer)
- [Microsoft Visual C++ 2015-2019 Re-distributable](https://aka.ms/vs/16/release/vc_redist.x64.exe)

---

### AUTOMATED INSTALLATION:

1. Make sure the Game you are installing to is Closed and Not Running.
2. Download [BananaLoader.Installer.exe](https://github.com/NaNraptor/BananaLoader/releases/latest/download/BananaLoader.Installer.exe).
3. Run BananaLoader.Installer.exe.
4. Click the SELECT button.
5. Select and Open the Game's EXE in your Game's Installation Folder.
6. Select which Version of BananaLoader to install using the Drop-Down List.  (Or leave it as-is for the Latest Version.)
7. Click the INSTALL or RE-INSTALL or UPDATE button.

---
	
### MANUAL INSTALLATION:

1. Make sure the Game you are installing BananaLoader to is Closed and Not Running.
2. Download [BananaLoader.zip](https://github.com/NaNraptor/BananaLoader/releases/latest/download/BananaLoader.zip).
3. Extract the BananaLoader folder from BananaLoader.zip to the Game's Installation Folder.
4. Extract version.dll from BananaLoader.zip to the Game's Installation Folder.

---

### AUTOMATED UNINSTALL:

1. Make sure the Game you are uninstalling from is Closed and Not Running.
2. Download [BananaLoader.Installer.exe](https://github.com/NaNraptor/BananaLoader/releases/latest/download/BananaLoader.Installer.exe).
3. Run BananaLoader.Installer.exe.
4. Click the SELECT button.
5. Select and Open the Game's EXE in your Game's Installation Folder.
7. Click the UN-INSTALL button.

---

### MANUAL UNINSTALL:

1. Make sure the Game you are installing BananaLoader to is Closed and Not Running.
2. Remove the version.dll file from the Game's Installation Folder.
3. Remove the BananaLoader folder from the Game's Installation Folder.
4. Remove the Plugins folder from the Game's Installation Folder.
5. Remove the Mods folder from the Game's Installation Folder.
6. Remove the Logs folder from the Game's Installation Folder.

---

### LAUNCH OPTIONS:

| Argument | Description |
| - | - |
| --no-mods | Launch game without loading Mods |
| --quitfix | Fixes the Hanging Process Issue with some Games |
| --Bananaloader.hideconsole | Hides the Normal Console |
| --Bananaloader.hidewarnings | Hides Warnings from Displaying |
| --Bananaloader.debug | Debug Mode/Console |
| --Bananaloader.magenta | Magenta Console Color |
| --Bananaloader.rainbow | Rainbow Console Color |
| --Bananaloader.randomrainbow | Random Rainbow Console Color |
| --Bananaloader.maxlogs | Max Log Files [ Default: 10 ] [ Disable: 0 ] |
| --Bananaloader.maxwarnings | Max Warnings per Log File [ Default: 100 ] [ Disable: 0 ] |
| --Bananaloader.maxerrors | Max Errors per Log File [ Default: 100 ] [ Disable: 0 ] |
| --Bananaloader.loadmodeplugins | Load Mode for Plugins [ Default: 0 ] |
| --Bananaloader.loadmodemods  | Load Mode for Mods [ Default: 0 ] |
| --Bananaloader.agregenerate | Forces Assembly to be Regenerated on Il2Cpp Games |
| --Bananaloader.agfvunhollower | Forces use a Specified Version of Il2CppAssemblyUnhollower |
| --Bananaloader.consoleontop | Forces the Console over all other Applications |

---

### LOAD MODES:

- Load Mode launch options are a way to dictate how you want Mods or Plugins to Load.

| Value | Action |
| - | - |
| 0 | NORMAL - Load them only if they don't have the "-dev" Name Extension |
| 1 | DEV - Load them only if they have the "-dev" Name Extension |
| 2 | BOTH - Load All |

---

### LICENSING & CREDITS:

BananaLoader is licensed under the Apache License, Version 2.0. See [LICENSE](https://github.com/NaNraptor/BananaLoader/blob/master/LICENSE.md) for the full License.

Third-party libraries used as source code or bundled in binary form:
- [Research Detours Package](https://github.com/microsoft/Detours) is licensed under the MIT License. See [LICENSE](https://github.com/NaNraptor/BananaLoader/blob/master/Detours/LICENSE.md) for the full License.
- [Mono](https://github.com/Unity-Technologies/mono) is licensed under multiple licenses. See [LICENSE](https://github.com/Unity-Technologies/mono/blob/unity-master/LICENSE) for full details.
- [Harmony](https://github.com/pardeike/Harmony) is licensed under the MIT License. See [LICENSE](https://github.com/NaNraptor/BananaLoader/blob/master/BananaLoader.ModHandler/Harmony/LICENSE) for the full License.
- [TinyJSON](https://github.com/pbhogan/TinyJSON) is licensed under the MIT License. See [LICENSE](https://github.com/NaNraptor/BananaLoader/blob/master/BananaLoader.AssemblyGenerator/TinyJSON/LICENSE.md) for the full License.
- [LightJson](https://github.com/MarcosLopezC/LightJson) is licensed under the MIT License. See [LICENSE](https://github.com/NaNraptor/BananaLoader/blob/master/BananaLoader.Installer/LightJson/LICENSE.txt) for the full License.
- [Tomlyn](https://github.com/xoofx/Tomlyn) is licensed under the MIT License. See [LICENSE](https://github.com/NaNraptor/BananaLoader/blob/master/BananaLoader.ModHandler/Tomlyn/license.txt) for the full License.
- [SharpZipLib](https://github.com/icsharpcode/SharpZipLib) is licensed under the MIT License. See [LICENSE](https://github.com/NaNraptor/BananaLoader/blob/master/BananaLoader.ModHandler/SharpZipLib/LICENSE.txt) for the full License.
- [AtiRoNya](https://github.com/AtiLion/AtiRoNya) is licensed under the MIT License. See [LICENSE](https://github.com/AtiLion/AtiRoNya/blob/e20e4a8fc47b37834c8284f9e6e937f04a84c510/LICENSE) for the full License.
- Unity Runtime libraries (found in [Unity Dependencies](BaseLibs/Unity%20Dependencies)) are part of Unity Software.  
Their usage is subject to [Unity Terms of Service](https://unity3d.com/legal/terms-of-service), including [Unity Software Additional Terms](https://unity3d.com/legal/terms-of-service/software).

External tools downloaded and used at runtime:
- [Il2CppDumper](https://github.com/Perfare/Il2CppDumper) is licensed under the MIT License. See [LICENSE](https://github.com/Perfare/Il2CppDumper/blob/master/LICENSE) for the full License.
- [Il2CppAssemblyUnhollower](https://github.com/knah/Il2CppAssemblyUnhollower) is licensed under the GNU Lesser General Public License v3.0. See [LICENSE](https://github.com/knah/Il2CppAssemblyUnhollower/blob/master/LICENSE) for the full License.

See [BananaLoader Wiki](https://Bananawiki.xyz/#/credits) for the full Credits.

BananaLoader is not sponsored by, affiliated with or endorsed by Unity Technologies or its affiliates.  
"Unity" is a trademark or a registered trademark of Unity Technologies or its affiliates in the U.S. and elsewhere.
