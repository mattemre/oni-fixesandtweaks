using HarmonyLib;
using System;
using System.Reflection;

// The 'Main Menu' and 'Quit To Desktop' options in the paused menu always
// warn about losing unsaved progress and want confirmation, even if
// the game has just been saved. Avoid the warning in those cases.
namespace FixesAndTweaks
{
    [HarmonyPatch(typeof(PauseScreen))]
    public class PauseScreen_Patch
    {
        public static bool hasSaved = false;

        [HarmonyPrefix]
        [HarmonyPatch(nameof(OnShow))]
        public static void OnShow(bool show)
        {   // 'show' doesn't really matter, reset flag on both show and hide
            hasSaved = false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(OnQuit))]
        public static bool OnQuit( PauseScreen __instance )
        {
            if( hasSaved )
            {
                MethodInfo onQuitConfirm = AccessTools.Method( typeof( PauseScreen ), "OnQuitConfirm" );
                onQuitConfirm.Invoke( __instance, null );
                return false;
            }
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(OnDesktopQuit))]
        public static bool OnDesktopQuit( PauseScreen __instance )
        {
            if( hasSaved )
            {
                MethodInfo onQuitConfirm = AccessTools.Method( typeof( PauseScreen ), "OnDesktopQuitConfirm" );
                onQuitConfirm.Invoke( __instance, null );
                return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(SaveLoader))]
    public class SaveLoader_Patch
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(Save), new Type[] { typeof(string), typeof(bool), typeof(bool) })]
        public static void Save(bool isAutoSave)
        {
            // This throws an exception if not successful, so any normal return is a successful save.
            if( !isAutoSave )
                PauseScreen_Patch.hasSaved = true;
        }
    }
}
