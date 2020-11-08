using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace BananaLoader
{
    internal static class AssemblyGenerator
    {
        internal static bool HasGeneratedAssembly = false;

        internal static void Check()
        {
            if (!Imports.IsIl2CppGame() || Initialize())
                HasGeneratedAssembly = true;
            else
                BananaLoaderBase.UNLOAD(false);
        }

        private static bool Initialize()
        {
            string GeneratorProcessPath = Path.Combine(Path.Combine(Path.Combine(Path.Combine(Imports.GetGameDirectory(), "BananaLoader"), "Dependencies"), "AssemblyGenerator"), "BananaLoader.AssemblyGenerator.exe");
            if (!File.Exists(GeneratorProcessPath))
            {
                BananaLogger.LogError("BananaLoader.AssemblyGenerator.exe does not Exist!");
                return false;
            }
            var generatorProcessInfo = new ProcessStartInfo(GeneratorProcessPath);
            generatorProcessInfo.Arguments = $"\"{BananaLoaderBase.UnityVersion}\" {"\"" + Regex.Replace(Imports.GetGameDirectory(), @"(\\+)$", @"$1$1") + "\""} {"\"" + Regex.Replace(Imports.GetGameDataDirectory(), @"(\\+)$", @"$1$1") + "\""} {(Force_Regenerate() ? "true" : "false")} {(string.IsNullOrEmpty(Force_Version_Unhollower()) ? "" : Force_Version_Unhollower())}";
            generatorProcessInfo.UseShellExecute = false;
            generatorProcessInfo.RedirectStandardOutput = true;
            generatorProcessInfo.CreateNoWindow = true;
            Process process = null;
            try { process = Process.Start(generatorProcessInfo); } catch (Exception e) { BananaLogger.LogError(e.ToString()); BananaLogger.LogError("Unable to Start Assembly Generator!"); return false; }
            var stdout = process.StandardOutput;
            while (!stdout.EndOfStream)
                BananaLogger.Log(stdout.ReadLine());
            while (!process.HasExited)
                Thread.Sleep(100);
            if (process.ExitCode != 0)
            {
                BananaLogger.Native_ThrowInternalError($"Assembly Generator exited with code {process.ExitCode}");
                return false;
            }
            if (Imports.IsDebugMode())
                BananaLogger.Log($"Assembly Generator ran Successfully!");
            return true;
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern static bool Force_Regenerate();
        [MethodImpl(MethodImplOptions.InternalCall)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        private extern static string Force_Version_Unhollower();
    }
}
