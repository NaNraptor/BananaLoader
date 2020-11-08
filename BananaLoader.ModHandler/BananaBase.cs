using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BananaLoader
{
    public abstract class BananaBase
    {
        /// <summary>
        /// Gets the Assembly of the Mod or Plugin.
        /// </summary>
        public Assembly Assembly { get; internal set; }

        /// <summary>
        /// Gets the File Location of the Mod or Plugin.
        /// </summary>
        public string Location { get; internal set; }

        /// <summary>
        /// Enum for Banana Compatibility
        /// </summary>
        public enum BananaCompatibility
        {
            UNIVERSAL = 0,
            COMPATIBLE = 1,
            NOATTRIBUTE = 2,
            INCOMPATIBLE = 3,
        }

        /// <summary>
        /// Gets the Compatibility of the Mod or Plugin.
        /// </summary>
        public BananaCompatibility Compatibility { get; internal set; }

        /// <summary>
        /// Gets the Info Attribute of the Mod or Plugin.
        /// </summary>
        public BananaInfoAttribute Info { get; internal set; }

        [Obsolete()]
        internal BananaModInfoAttribute LegacyModInfo { get; set; }
        [Obsolete()]
        internal BananaPluginInfoAttribute LegacyPluginInfo { get; set; }

        /// <summary>
        /// Gets the Game Attributes of the Mod or Plugin.
        /// </summary>
        public BananaOptionalDependenciesAttribute OptionalDependenciesAttribute { get; internal set; }

        /// <summary>
        /// Gets the Game Attributes of the Mod or Plugin.
        /// </summary>
        public BananaGameAttribute[] Games { get; internal set; }

        /// <summary>
        /// Gets the Auto-Created Harmony Instance of the Mod or Plugin.
        /// </summary>
        public Harmony.HarmonyInstance harmonyInstance { get; internal set; }

        [Obsolete()]
        internal BananaModGameAttribute[] LegacyModGames { get; set; }
        [Obsolete()]
        internal BananaPluginGameAttribute[] LegacyPluginGames { get; set; }

        public virtual void OnApplicationStart() { }
        public virtual void OnUpdate() { }
        public virtual void OnLateUpdate() { }
        public virtual void OnGUI() { }
        public virtual void OnApplicationQuit() { }
        public virtual void OnModSettingsApplied() { }
        public virtual void VRChat_OnUiManagerInit() { }
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class BananaOptionalDependenciesAttribute : Attribute
    {
        /// <summary>
        /// The (simple) assembly names of the dependencies that should be regarded as optional.
        /// </summary>
        public string[] AssemblyNames { get; internal set; }

        public BananaOptionalDependenciesAttribute(params string[] assemblyNames)
        {
            AssemblyNames = assemblyNames;
        }
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class BananaInfoAttribute : Attribute
    {
        /// <summary>
        /// Gets the System.Type of the Mod.
        /// </summary>
        public Type SystemType { get; internal set; }

        /// <summary>
        /// Gets the Name of the Mod.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Gets the Version of the Mod.
        /// </summary>
        public string Version { get; internal set; }

        /// <summary>
        /// Gets the Author of the Mod.
        /// </summary>
        public string Author { get; internal set; }

        /// <summary>
        /// Gets the Download Link of the Mod.
        /// </summary>
        public string DownloadLink { get; internal set; }

        public BananaInfoAttribute(Type type, string name, string version, string author, string downloadLink = null)
        {
            SystemType = type;
            Name = name;
            Version = version;
            Author = author;
            DownloadLink = downloadLink;
        }

        [Obsolete()]
        internal BananaModInfoAttribute ConvertLegacy_Mod() => new BananaModInfoAttribute(SystemType, Name, Version, Author, DownloadLink);
        [Obsolete()]
        internal BananaPluginInfoAttribute ConvertLegacy_Plugin() => new BananaPluginInfoAttribute(SystemType, Name, Version, Author, DownloadLink);
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class BananaGameAttribute : Attribute
    {
        /// <summary>
        /// Gets the target Developer
        /// </summary>
        public string Developer { get; internal set; }

        /// <summary>
        /// Gets target Game Name
        /// </summary>
        public string GameName { get; internal set; }

        /// <summary>
        /// Gets whether this Mod can target any Game.
        /// </summary>
        public bool Universal { get => string.IsNullOrEmpty(Developer) || string.IsNullOrEmpty(GameName); }

        /// <summary>
        /// Mark this Mod as Universal or Compatible with specific Games.
        /// </summary>
        public BananaGameAttribute(string developer = null, string gameName = null)
        {
            Developer = developer;
            GameName = gameName;
        }

        [Obsolete()]
        internal BananaModGameAttribute ConvertLegacy_Mod() => new BananaModGameAttribute(Developer, GameName);
        [Obsolete()]
        internal BananaPluginGameAttribute ConvertLegacy_Plugin() => new BananaPluginGameAttribute(Developer, GameName);

        public bool IsGame(string developer, string gameName) => (Universal || ((developer != null) && (gameName != null) && Developer.Equals(developer) && GameName.Equals(gameName)));
        public bool IsCompatible(BananaGameAttribute att) => ((att == null) || IsCompatibleBecauseUniversal(att) || (att.Developer.Equals(Developer) && att.GameName.Equals(GameName)));
        public bool IsCompatibleBecauseUniversal(BananaGameAttribute att) => ((att == null) || Universal || att.Universal);
    }

    [Obsolete()]
    internal class BananaLegacyAttributeSupport
    {
        internal class Response_Info
        {
            internal BananaInfoAttribute Default;
            [Obsolete()]
            internal BananaModInfoAttribute Legacy_Mod;
            [Obsolete()]
            internal BananaPluginInfoAttribute Legacy_Plugin;
            internal Response_Info(BananaInfoAttribute def, BananaModInfoAttribute legacy_mod, BananaPluginInfoAttribute legacy_plugin)
            {
                Default = def;
                Legacy_Mod = legacy_mod;
                Legacy_Plugin = legacy_plugin;
            }
            internal void SetupBanana(BananaBase baseInstance)
            {
                if (Default != null)
                    baseInstance.Info = Default;
                if (Legacy_Mod != null)
                    baseInstance.LegacyModInfo = Legacy_Mod;
                if (Legacy_Plugin != null)
                    baseInstance.LegacyPluginInfo = Legacy_Plugin;
            }
        }

        internal class Response_Game
        {
            internal BananaGameAttribute[] Default;
            internal BananaModGameAttribute[] Legacy_Mod;
            internal BananaPluginGameAttribute[] Legacy_Plugin;
            internal Response_Game(BananaGameAttribute[] def, BananaModGameAttribute[] legacy_mod, BananaPluginGameAttribute[] legacy_plugin)
            {
                Default = def;
                Legacy_Mod = legacy_mod;
                Legacy_Plugin = legacy_plugin;
            }
            internal void SetupBanana(BananaBase baseInstance)
            {
                if (Default.Length > 0)
                    baseInstance.Games = Default;
                if (Legacy_Mod.Length > 0)
                    baseInstance.LegacyModGames = Legacy_Mod;
                if (Legacy_Plugin.Length > 0)
                    baseInstance.LegacyPluginGames = Legacy_Plugin;
            }
        }

        internal static Response_Info GetBananaInfoAttribute(Assembly asm, bool isPlugin = false)
        {
            BananaInfoAttribute def = asm.GetCustomAttributes(false).FirstOrDefault(x => (x.GetType() == typeof(BananaInfoAttribute))) as BananaInfoAttribute;
            BananaModInfoAttribute legacy_mod = null;
            BananaPluginInfoAttribute legacy_plugin = null;
            if (def == null)
            {
                if (isPlugin)
                {
                    legacy_plugin = asm.GetCustomAttributes(false).FirstOrDefault(x => (x.GetType() == typeof(BananaPluginInfoAttribute))) as BananaPluginInfoAttribute;
                    if ((legacy_plugin != null) && (def == null))
                        def = legacy_plugin.Convert();
                }
                else
                {
                    legacy_mod = asm.GetCustomAttributes(false).FirstOrDefault(x => (x.GetType() == typeof(BananaModInfoAttribute))) as BananaModInfoAttribute;
                    if ((legacy_mod != null) && (def == null))
                        def = legacy_mod.Convert();
                }
            }
            else
            {
                if (isPlugin)
                    legacy_plugin = def.ConvertLegacy_Plugin();
                else
                    legacy_mod = def.ConvertLegacy_Mod();
            }
            return new Response_Info(def, legacy_mod, legacy_plugin);
        }

        internal static Response_Game GetBananaGameAttributes(Assembly asm, bool isPlugin = false)
        {
            BananaGameAttribute[] def = asm.GetCustomAttributes(typeof(BananaGameAttribute), true) as BananaGameAttribute[];
            BananaModGameAttribute[] legacy_mod = new BananaModGameAttribute[0];
            BananaPluginGameAttribute[] legacy_plugin = new BananaPluginGameAttribute[0];
            if (def.Length <= 0)
            {
                if (isPlugin)
                {
                    legacy_plugin = asm.GetCustomAttributes(typeof(BananaPluginGameAttribute), true) as BananaPluginGameAttribute[];
                    if (legacy_plugin.Length > 0)
                    {
                        List<BananaGameAttribute> deflist = new List<BananaGameAttribute>();
                        foreach (BananaPluginGameAttribute att in legacy_plugin)
                            deflist.Add(att.Convert());
                        def = deflist.ToArray();
                    }
                }
                else
                {
                    legacy_mod = asm.GetCustomAttributes(typeof(BananaModGameAttribute), true) as BananaModGameAttribute[];
                    if (legacy_mod.Length > 0)
                    {
                        List<BananaGameAttribute> deflist = new List<BananaGameAttribute>();
                        foreach (BananaModGameAttribute att in legacy_mod)
                            deflist.Add(att.Convert());
                        def = deflist.ToArray();
                    }
                }
            }
            else
            {
                if (isPlugin)
                {
                    List<BananaPluginGameAttribute> legacy_pluginlist = new List<BananaPluginGameAttribute>();
                    foreach (BananaGameAttribute att in def)
                        legacy_pluginlist.Add(att.ConvertLegacy_Plugin());
                    legacy_plugin = legacy_pluginlist.ToArray();
                }
                else
                {
                    List<BananaModGameAttribute> legacy_modlist = new List<BananaModGameAttribute>();
                    foreach (BananaGameAttribute att in def)
                        legacy_modlist.Add(att.ConvertLegacy_Mod());
                    legacy_mod = legacy_modlist.ToArray();
                }
            }
            return new Response_Game(def, legacy_mod, legacy_plugin);
        }
    }
}
