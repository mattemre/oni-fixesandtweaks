using HarmonyLib;

// Horizontal scrolling is way too slow (annoying e.g. in Consumables view).
// Make it 4x faster.
namespace FixesAndTweaks
{
    [HarmonyPatch(typeof(KScrollRect))]
    public class KScreenRect_Patch
    {
        [HarmonyPostfix]
        [HarmonyPatch(MethodType.Constructor)]
        public static void ctor(ref float ___horizontalScrollInertiaScale)
        {
            if( ___horizontalScrollInertiaScale == 5 )
                ___horizontalScrollInertiaScale = 20;
        }
    }
}
