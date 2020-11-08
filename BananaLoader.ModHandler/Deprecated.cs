using System;
using System.Collections.Generic;
using System.Linq;
#pragma warning disable 0108

namespace BananaLoader
{
    [Obsolete("Main is obsolete. Please use BananaLoaderBase or BananaHandler instead.")]
    public static class Main
    {
        public static List<BananaMod> Mods = null;
        public static List<BananaPlugin> Plugins = null;
        public static bool IsVRChat = false;
        public static bool IsBoneworks = false;
        public static string GetUnityVersion() => BananaLoaderBase.UnityVersion;
        public static string GetUserDataPath() => BananaLoaderBase.UserDataPath;
        internal static void LegacySupport(List<BananaMod> mods, List<BananaPlugin> plugins, bool isVRChat, bool isBoneworks)
        {
            Mods = mods;
            Plugins = plugins;
            IsVRChat = isVRChat;
            IsBoneworks = isBoneworks;
        }
    }
    [Obsolete("BananaModGame is obsolete. Please use BananaGame instead.")]
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class BananaModGameAttribute : Attribute
    {
        public string Developer { get; }
        public string GameName { get; }
        public BananaModGameAttribute(string developer = null, string gameName = null)
        {
            Developer = developer;
            GameName = gameName;
        }
        internal BananaGameAttribute Convert() => new BananaGameAttribute(Developer, GameName);
    }
    [Obsolete("BananaModInfo is obsolete. Please use BananaInfo instead.")]
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class BananaModInfoAttribute : Attribute
    {
        public Type SystemType { get; }
        public string Name { get; }
        public string Version { get; }
        public string Author { get; }
        public string DownloadLink { get; }

        public BananaModInfoAttribute(Type type, string name, string version, string author, string downloadLink = null)
        {
            SystemType = type;
            Name = name;
            Version = version;
            Author = author;
            DownloadLink = downloadLink;
        }
        internal BananaInfoAttribute Convert() => new BananaInfoAttribute(SystemType, Name, Version, Author, DownloadLink);
    }
    [Obsolete("BananaPluginGame is obsolete. Please use BananaGame instead.")]
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class BananaPluginGameAttribute : Attribute
    {
        public string Developer { get; }
        public string GameName { get; }
        public BananaPluginGameAttribute(string developer = null, string gameName = null)
        {
            Developer = developer;
            GameName = gameName;
        }
        public BananaGameAttribute Convert() => new BananaGameAttribute(Developer, GameName);
    }
    [Obsolete("BananaPluginInfo is obsolete. Please use BananaInfo instead.")]
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class BananaPluginInfoAttribute : Attribute
    {
        public Type SystemType { get; }
        public string Name { get; }
        public string Version { get; }
        public string Author { get; }
        public string DownloadLink { get; }

        public BananaPluginInfoAttribute(Type type, string name, string version, string author, string downloadLink = null)
        {
            SystemType = type;
            Name = name;
            Version = version;
            Author = author;
            DownloadLink = downloadLink;
        }
        public BananaInfoAttribute Convert() => new BananaInfoAttribute(SystemType, Name, Version, Author, DownloadLink);
    }
    [Obsolete("BananaModLogger is obsolete. Please use BananaLogger instead.")]
    public class BananaModLogger : BananaLogger {}
    [Obsolete("ModPrefs is obsolete. Please use BananaPrefs instead.")]
    public class ModPrefs : BananaPrefs
    {
        public static Dictionary<string, Dictionary<string, PrefDesc>> GetPrefs()
        {
            Dictionary<string, Dictionary<string, PrefDesc>> output = new Dictionary<string, Dictionary<string, PrefDesc>>();
            Dictionary<string, Dictionary<string, BananaPreference>> prefs = GetPreferences();
            for (int i = 0; i < prefs.Values.Count; i++)
            {
                Dictionary<string, BananaPreference> prefsdict = prefs.Values.ElementAt(i);
                Dictionary<string, PrefDesc> newprefsdict = new Dictionary<string, PrefDesc>();
                for (int j = 0; j < prefsdict.Values.Count; j++)
                {
                    BananaPreference pref = prefsdict.Values.ElementAt(j);
                    PrefDesc newpref = new PrefDesc(pref.Value, (PrefType)pref.Type, pref.Hidden, pref.DisplayText);
                    newpref.ValueEdited = pref.ValueEdited;
                    newprefsdict.Add(prefsdict.Keys.ElementAt(j), newpref);
                }
                output.Add(prefs.Keys.ElementAt(i), newprefsdict);
            }
            return output;
        }
        public static void RegisterPrefString(string section, string name, string defaultValue, string displayText = null, bool hideFromList = false) => RegisterString(section, name, defaultValue, displayText, hideFromList);
        public static void RegisterPrefBool(string section, string name, bool defaultValue, string displayText = null, bool hideFromList = false) => RegisterBool(section, name, defaultValue, displayText, hideFromList);
        public static void RegisterPrefInt(string section, string name, int defaultValue, string displayText = null, bool hideFromList = false) => RegisterInt(section, name, defaultValue, displayText, hideFromList);
        public static void RegisterPrefFloat(string section, string name, float defaultValue, string displayText = null, bool hideFromList = false) => RegisterFloat(section, name, defaultValue, displayText, hideFromList);
        public enum PrefType
        {
            STRING,
            BOOL,
            INT,
            FLOAT
        }
        public class PrefDesc : BananaPreference
        {
            public PrefType Type { get => (PrefType)base.Type; }
            public PrefDesc(string value, PrefType type, bool hidden, string displayText) : base(value, type, hidden, displayText)
            {
                Value = value;
                ValueEdited = value;
                base.Type = (BananaPreferenceType)type;
                Hidden = hidden;
                DisplayText = displayText;
            }
        }
    }
 }