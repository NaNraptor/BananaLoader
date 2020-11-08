using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace BananaLoader
{
    public class BananaLogger
    {
        public static void Log(string s)
        {
            string namesection = GetNameSection();
            Native_Log(namesection, s);
            BananaConsole.RunLogCallbacks(namesection, s);
        }

        public static void Log(ConsoleColor color, string s)
        {
            string namesection = GetNameSection();
            Native_LogColor(namesection, s, color);
            BananaConsole.RunLogCallbacks(namesection, s);
        }

        public static void Log(string s, params object[] args)
        {
            string namesection = GetNameSection();
            string fmt = string.Format(s, args);
            Native_Log(namesection, fmt);
            BananaConsole.RunLogCallbacks(namesection, fmt);
        }

        public static void Log(ConsoleColor color, string s, params object[] args)
        {
            string namesection = GetNameSection();
            string fmt = string.Format(s, args);
            Native_LogColor(namesection, fmt, color);
            BananaConsole.RunLogCallbacks(namesection, fmt);
        }

        public static void Log(object o)
        {
            Log(o.ToString());
        }

        public static void Log(ConsoleColor color, object o)
        {
            Log(color, o.ToString());
        }

        public static void LogWarning(string s)
        {
            string namesection = GetNameSection();
            Native_LogWarning(namesection, s);
            BananaConsole.RunWarningCallbacks(namesection, s);
        }

        public static void LogWarning(string s, params object[] args)
        {
            string namesection = GetNameSection();
            string fmt = string.Format(s, args);
            Native_LogWarning(namesection, fmt);
            BananaConsole.RunWarningCallbacks(namesection, fmt);
            Native_LogWarning(GetNameSection(), fmt);
        }

        public static void LogError(string s)
        {
            string namesection = GetNameSection();
            Native_LogError(namesection, s);
            BananaConsole.RunErrorCallbacks(namesection, s);
        }
        public static void LogError(string s, params object[] args)
        {
            string namesection = GetNameSection();
            string fmt = string.Format(s, args);
            Native_LogError(namesection, fmt);
            BananaConsole.RunErrorCallbacks(namesection, fmt);
        }

        internal static void LogBananaError(string msg, string modname)
        {
            string namesection = (string.IsNullOrEmpty(modname) ? "" : ("[" + modname.Replace(" ", "_") + "] "));
            Native_LogBananaError(namesection, msg);
            BananaConsole.RunErrorCallbacks(namesection, msg);
        }

        internal static void LogBananaCompatibility(BananaBase.BananaCompatibility comp) => Native_LogBananaCompatibility(comp);

        internal static string GetNameSection()
        {
            StackTrace st = new StackTrace(2, true);
            StackFrame sf = st.GetFrame(0);
            if (sf != null)
            {
                MethodBase method = sf.GetMethod();
                if (!method.Equals(null))
                {
                    Type methodClassType = method.DeclaringType;
                    if (!methodClassType.Equals(null))
                    {
                        Assembly asm = methodClassType.Assembly;
                        if (!asm.Equals(null))
                        {
                            BananaPlugin plugin = BananaHandler.Plugins.Find(x => (x.Assembly == asm));
                            if (plugin != null)
                            {
                                if (!string.IsNullOrEmpty(plugin.Info.Name))
                                    return "[" + plugin.Info.Name.Replace(" ", "_") + "] ";
                            }
                            else
                            {
                                BananaMod mod = BananaHandler.Mods.Find(x => (x.Assembly == asm));
                                if (mod != null)
                                {
                                    if (!string.IsNullOrEmpty(mod.Info.Name))
                                        return "[" + mod.Info.Name.Replace(" ", "_") + "] ";
                                }
                            }
                        }
                    }
                }
            }
            return "";
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Native_Log(string namesection, string txt);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Native_LogColor(string namesection, string txt, ConsoleColor color);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Native_LogWarning(string namesection, string txt);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Native_LogError(string namesection, string txt);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Native_LogBananaError(string namesection, string txt);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Native_LogBananaCompatibility(BananaBase.BananaCompatibility comp);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Native_ThrowInternalError(string txt);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern static IntPtr Native_GetConsoleOutputHandle();
    }
}