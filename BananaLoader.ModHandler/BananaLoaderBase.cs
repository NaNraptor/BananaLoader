﻿using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Harmony;

namespace BananaLoader
{
    public static class BananaLoaderBase
    {
        internal static BananaGameAttribute CurrentGameAttribute = null;
        internal static bool _IsVRChat = false;
        public static bool IsVRChat { get => _IsVRChat; }
        internal static bool _IsBoneworks = false;
        public static bool IsBoneworks { get => _IsBoneworks; }

        private static void Initialize()
        {
            Setup();
            BananaConsole.Check();
            AssemblyGenerator.Check();
            if (!AssemblyGenerator.HasGeneratedAssembly)
                return;
            BananaHandler.LoadAll(true);
            if (!BananaHandler.HasBananas)
                return;
            BananaPrefs.Setup();
            BananaHandler.OnPreInitialization();
        }

        private static void Startup()
        {
            if (!AssemblyGenerator.HasGeneratedAssembly)
                return;
            SetupSupport();
            WelcomeLog();
            BananaHandler.LoadAll();
            BananaHandler.LogAndPrune();
            if (!BananaHandler.HasBananas)
                return;
            BananaPrefs.Setup();
            AddUnityDebugLog();
            BananaHandler.OnApplicationStart();
            if (!BananaHandler.HasBananas)
                SupportModule.Destroy();
        }

        internal static void Quit()
        {
            if (BananaHandler.HasBananas)
                BananaPrefs.SaveConfig();
            HarmonyInstance.UnpatchAllInstances();
            UNLOAD();
            if (IsQuitFix()) Process.GetCurrentProcess().Kill();
        }

        private static void Setup()
        {
            FixCurrentBaseDirectory();
            AppDomain.CurrentDomain.UnhandledException += ExceptionHandler;
            CurrentGameAttribute = new BananaGameAttribute(Imports.GetCompanyName(), Imports.GetProductName());
            if (Imports.IsIl2CppGame())
            {
                _IsVRChat = CurrentGameAttribute.IsGame("VRChat", "VRChat");
                _IsBoneworks = CurrentGameAttribute.IsGame("Stress Level Zero", "BONEWORKS");
            }
        }

        private static void SetupSupport()
        {
            if (Imports.IsIl2CppGame())
            {
                if (_IsVRChat)
                    BananaHandler.Assembly_CSharp = Assembly.Load("Assembly-CSharp");
                UnhollowerSupport.Initialize();
            }
            SupportModule.Initialize();
        }

        private static void WelcomeLog()
        {
            BananaLogger.Log("------------------------------");
            BananaLogger.Log("Unity " + _UnityVersion);
            BananaLogger.Log("OS: " + Environment.OSVersion.ToString());
            BananaLogger.Log("------------------------------");
            BananaLogger.Log("Name: " + CurrentGameAttribute.GameName);
            BananaLogger.Log("Developer: " + CurrentGameAttribute.Developer);
            BananaLogger.Log("Type: " + (Imports.IsIl2CppGame() ? "Il2Cpp" : (IsOldMono() ? "Mono" : "MonoBleedingEdge")));
            BananaLogger.Log("------------------------------");
            BananaLogger.Log("Using v" + BuildInfo.Version + " Open-Beta");
            BananaLogger.Log("------------------------------");
        }

        private static void AddUnityDebugLog()
        {
            SupportModule.UnityDebugLog("--------------------------------------------------------------------------------------------------");
            SupportModule.UnityDebugLog("~   This Game has been MODIFIED using BananaLoader. DO NOT report any issues to the Developers!   ~");
            SupportModule.UnityDebugLog("--------------------------------------------------------------------------------------------------");
        }

        public static void FixCurrentBaseDirectory()
        {
            ((AppDomainSetup)typeof(AppDomain).GetProperty("SetupInformationNoCopy", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(AppDomain.CurrentDomain, new object[0])).ApplicationBase = Imports.GetGameDirectory();
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        }

        private static void ExceptionHandler(object sender, UnhandledExceptionEventArgs e) => BananaLogger.LogError((e.ExceptionObject as Exception).ToString());

        private static string _UserDataPath = null;
        public static string UserDataPath
        {
            get
            {
                if (_UserDataPath == null)
                {
                    _UserDataPath = Path.Combine(Imports.GetGameDirectory(), "UserData");
                    if (!Directory.Exists(_UserDataPath))
                        Directory.CreateDirectory(_UserDataPath);
                }
                return _UserDataPath;
            }
        }

        private static string _UnityVersion = null;
        public static string UnityVersion
        {
            get
            {
                if (_UnityVersion != null)
                    return _UnityVersion;
                string exepath = Imports.GetExePath();
                string ggm_path = Path.Combine(Imports.GetGameDataDirectory(), "globalgamemanagers");
                if (!File.Exists(ggm_path))
                {
                    FileVersionInfo versioninfo = FileVersionInfo.GetVersionInfo(exepath);
                    if ((versioninfo == null) || string.IsNullOrEmpty(versioninfo.FileVersion))
                        return "UNKNOWN";
                    return versioninfo.FileVersion.Substring(0, versioninfo.FileVersion.LastIndexOf('.'));
                }
                byte[] ggm_bytes = File.ReadAllBytes(ggm_path);
                if ((ggm_bytes == null) || (ggm_bytes.Length <= 0))
                    return "UNKNOWN";
                int start_position = 0;
                for (int i = 10; i < ggm_bytes.Length; i++)
                {
                    byte pos_byte = ggm_bytes[i];
                    if ((pos_byte <= 0x39) && (pos_byte >= 0x30))
                    {
                        start_position = i;
                        break;
                    }
                }
                if (start_position == 0)
                    return "UNKNOWN";
                int end_position = 0;
                for (int i = start_position; i < ggm_bytes.Length; i++)
                {
                    byte pos_byte = ggm_bytes[i];
                    if ((pos_byte != 0x2E) && ((pos_byte > 0x39) || (pos_byte < 0x30)))
                    {
                        end_position = (i - 1);
                        break;
                    }
                }
                if (end_position == 0)
                    return "UNKNOWN";
                int verstr_byte_pos = 0;
                byte[] verstr_byte = new byte[((end_position - start_position) + 1)];
                for (int i = start_position; i <= end_position; i++)
                {
                    verstr_byte[verstr_byte_pos] = ggm_bytes[i];
                    verstr_byte_pos++;
                }
                return _UnityVersion = Encoding.UTF8.GetString(verstr_byte, 0, verstr_byte.Length);
            }
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static bool IsOldMono();
        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern static bool IsQuitFix();
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void UNLOAD(bool doquitfix = true);
    }
}