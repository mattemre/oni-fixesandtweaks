using HarmonyLib;

// https://forums.kleientertainment.com/klei-bug-tracker/oni/destructive-duplicants-may-target-invincible-buildings-such-as-rocket-engines-r40223/
// Some building such as rocket engines are invincible, but they are also breakable.
// Destructive dupes throwing a tantrum will target them and end up just standing
// there, doing nothing.
namespace FixesAndTweaks
{
    [HarmonyPatch(typeof(BuildingTemplates))]
    public class BuildingTemplates_Patch
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(CreateRocketBuildingDef))]
        public static void CreateRocketBuildingDef(BuildingDef def)
        {
            if( def.Invincible )
                def.Breakable = false;
        }
        [HarmonyPostfix]
        [HarmonyPatch(nameof(CreateMonumentBuildingDef))]
        public static void CreateMonumentBuildingDef(BuildingDef def)
        {
            if( def.Invincible )
                def.Breakable = false;
        }
    }
}
