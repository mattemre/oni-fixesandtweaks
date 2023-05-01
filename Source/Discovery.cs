#if false
using HarmonyLib;

namespace FixesAndTweaks
{
    [HarmonyPatch(typeof(WorldContainer))]
    public class WorldContainer_Patch
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(OnSpawn))]
        public static void OnSpawn(WorldContainer __instance, ref float ___discoveryTimestamp)
        {
            if (!__instance.IsDiscovered || __instance.IsModuleInterior)
                return;
            Debug.Log( "XXX:" + __instance.worldName );
            if( __instance.worldName == "expansion1::worlds/WaterMoonlet" )
            {
                Debug.Log( "YYY:" + ___discoveryTimestamp + " -> " + GameUtil.GetCurrentTimeInCycles());
                ___discoveryTimestamp = GameUtil.GetCurrentTimeInCycles();
            }
        }
    }
}
#endif
