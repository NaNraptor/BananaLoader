using BananaLoader;
using System;
using System.Reflection;
using UnityEngine;
using HarmonyLib;
using System.IO;
using System.Threading;
using Photon.Pun;
using System.Runtime.CompilerServices;

namespace TestMod
{
    public static class BuildInfo
    {
        public const string Name = "TestMod"; // Name of the Mod.  (MUST BE SET)
        public const string Description = "Mod for Testing"; // Description for the Mod.  (Set as null if none)
        public const string Author = null; // Author of the Mod.  (Set as null if none)
        public const string Company = null; // Company that made the Mod.  (Set as null if none)
        public const string Version = "1.0.0"; // Version of the Mod.  (MUST BE SET)
        public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
    }

    public class TestMod : BananaMod
    {
        public bool doOnce1 = false;
        public GameObject privateCopy;
        public override void OnApplicationStart() // Runs after Game Initialization.
        {
            BananaLogger.Log("OnApplicationStart");
            //testPatch.DoPatching();
            //myPatch.DoPatching(typeof(Door), "SpawnHandPrintEvidence", new Type[] { });
            //myPatch.DoPatching(typeof(Door), "NetworkedPlayClosedSound", new Type[] { });
            //myPatch.DoPatching(typeof(Door), "NetworkedPlayLockSound", new Type[] { });
            //myPatch.DoPatching(typeof(Door), "NetworkedPlayUnlockSound", new Type[] { });
            //myPatch.DoPatching(typeof(Door), "Update", new Type[] { });

            new Thread((ThreadStart)(() =>
            {
                while (true)
                {
                    Thread.Sleep(30000);
                    var doors = UnityEngine.Object.FindObjectsOfType<Door>();
                    if (SetupPhaseController.field_Public_Static_SetupPhaseController_0 != null)
                    {
                        Console.WriteLine(SetupPhaseController.field_Public_Static_SetupPhaseController_0.field_Public_Boolean_0 + " " +
                                SetupPhaseController.field_Public_Static_SetupPhaseController_0.field_Public_Boolean_1 + " " +
                                SetupPhaseController.field_Public_Static_SetupPhaseController_0.field_Public_Boolean_2);
                    }
                    else continue;
                    if (doors.Length == 0 || doOnce1) continue;
                    doOnce1 = true;

                    foreach (var door in doors)
                    {
                        if (door.field_Public_Boolean_0 == true)
                        {
                            door.UnlockDoor();
                        }
                        door.SpawnHandPrintEvidence();
                        //GameObject.Destroy(door.GetComponent<BoxCollider>());
                        //GameObject.Destroy(door.GetComponent<Rigidbody>());
                        //GameObject.Destroy(door.GetComponent<Renderer>());
                        //PhotonNetwork.Destroy((GameObject)door);
                    }
                }
            })).Start();

            new Thread((ThreadStart)(() =>
            {
                while (true)
                {
                    //do some shit here
                    Thread.Sleep(5000);
                }
            })).Start();
        }

        public override void OnLevelIsLoading() // Runs when a Scene is Loading or when a Loading Screen is Shown. Currently only runs if the Mod is used in BONEWORKS.
        {
            BananaLogger.Log("OnLevelIsLoading");
        }

        public override void OnLevelWasLoaded(int level) // Runs when a Scene has Loaded.
        {
            BananaLogger.Log("OnLevelWasLoaded: " + level.ToString());
        }

        public override void OnLevelWasInitialized(int level) // Runs when a Scene has Initialized.
        {
            BananaLogger.Log("OnLevelWasInitialized: " + level.ToString());
        }

        public override void OnUpdate() // Runs once per frame.
        {
            //BananaLogger.Log("OnUpdate");
        }

        public override void OnFixedUpdate() // Can run multiple times per frame. Mostly used for Physics.
        {
            //BananaLogger.Log("OnFixedUpdate");
        }

        public override void OnLateUpdate() // Runs once per frame after OnUpdate and OnFixedUpdate have finished.
        {
            //BananaLogger.Log("OnLateUpdate");
        }
        public override void OnGUI() // Can run multiple times per frame. Mostly used for Unity's IMGUI.
        {
            //BananaLogger.Log("OnGUI");
            GUI.color = Color.white;
            
            GUI.Label(new Rect(100f, 100f, 100f, 75f), "TEST MOD LOADED");
        }

        public override void OnApplicationQuit() // Runs when the Game is told to Close.
        {
            BananaLogger.Log("OnApplicationQuit");
        }

        public override void OnModSettingsApplied() // Runs when Mod Preferences get saved to UserData/modprefs.ini.
        {
            BananaLogger.Log("OnModSettingsApplied");
        }
    }

    public class testPatch
    {
        public static void DoPatching()
        {
            HarmonyLib.Harmony harmony = new HarmonyLib.Harmony("testPatch");

            Type T = typeof(UnityEngine.Object);
            string mName = "DestroyObject";
            Type[] args = { typeof(UnityEngine.Object), typeof(float) };

            MethodInfo mOriginal = T.GetMethod(mName, args );

            var mPrefix = typeof(testPatch).GetMethod("MyPrefix", BindingFlags.Static | BindingFlags.Public);
            var mPostfix = typeof(testPatch).GetMethod("MyPostfix", BindingFlags.Static | BindingFlags.Public);
            if (mOriginal == null)
            {
                BananaLogger.LogError("harmony patch didnt work - the function '" + mName + "()' that was to be patched is null...");
                return;
            }
            if (mPrefix == null)
            {
                BananaLogger.LogError("harmony patch didnt work - prefix function is null...");
                return;
            }
            if (mPostfix == null)
            {
                BananaLogger.LogError("harmony patch didnt work - postfix function is null...");
                return;
            }

            harmony.Patch(mOriginal, new HarmonyMethod(mPrefix), new HarmonyMethod(mPostfix));
            BananaLogger.Log("Patched " + T.Name + "." + mName + "(number of args: " + args.Length + ")");
        }

        public static void MyPrefix(UnityEngine.Object obj, float t)
        {
            System.Console.WriteLine("obj " + obj.name + " destroyed " + t);
            //BananaLogger.Log(obj.name + " destroyed");

            //GameObject ho = GameObject.Find("NaNraptor");
            //if (!ho)
            //{
            //    Console.WriteLine("couldnt find hack obj");
            //    return;
            //}

            //ho.SetActive(true);
        }

        public static void MyPostfix()
        {
        }
    }
}