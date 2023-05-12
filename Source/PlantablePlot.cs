using HarmonyLib;

// https://forums.kleientertainment.com/klei-bug-tracker/oni/copy-settings-does-not-work-with-grubfruit-plant-r40474/
// When "Copy settings" is used from a farm plot with a Gruibfruit plant (the rubbed variant),
// and then applied to an empty farm plot, there will be no seed delivered. This is because
// the copying sets it to require the rubbed variant of the plant, but no such seed exists.
// Convert to the (un-rubbed) seed variant.
namespace FixesAndTweaks
{
    [HarmonyPatch(typeof(PlantablePlot))]
    public class PlantablePlot_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(CreateOrder))]
        public static void CreateOrder(Tag entityTag, ref Tag additionalFilterTag)
        {
            // Doing this as strings instead of tags is probably a bit hackish, but I don't see any API
            // to do the conversion based on tags. Since tags are just a string with a hash of the string,
            // this should work reliably too.
            if(entityTag == "WormPlantSeed" && additionalFilterTag.ToString().StartsWith("SuperWormPlant_"))
                additionalFilterTag = additionalFilterTag.ToString().Replace("SuperWormPlant_", "WormPlant_");
        }
    }
}
