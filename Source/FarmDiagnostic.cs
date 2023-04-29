using HarmonyLib;
using System.Collections.Generic;
using STRINGS;

// The 'Crops' diagnostic group has two problematic items:
// - 'Check colony has farms' does not seem to make much sense, that's not a state that is likely to change
//   without the player noticing, and it is also rather pointless (there are other ways to make food).
// - 'Check farms are planted' triggers even if there are no farm plots.
// These two together mean that e.g. unhabitated planetoids get a permanent yellow 'Crops' diagnostic
// that is useless. Make it possible to disable the first one and fix the latter one.
namespace FixesAndTweaks
{
    [HarmonyPatch(typeof(FarmDiagnostic))]
    public class FarmDiagnostic_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(CheckHasFarms))]
        public static bool CheckHasFarms( ref ColonyDiagnostic.DiagnosticResult __result )
        {
            if( !Options.Instance.BlockHasFarmsDiagnostic )
                return true;
            __result = new ColonyDiagnostic.DiagnosticResult(
                ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS);
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(CheckPlanted))]
        public static bool CheckPlanted( ref ColonyDiagnostic.DiagnosticResult __result, List<PlantablePlot> ___plots )
        {
            if( !Options.Instance.PlantedDiagnosticOnlyIfFarms )
                return true;
            if( ___plots.Count != 0 )
                return true;
            __result = new ColonyDiagnostic.DiagnosticResult(
                ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS);
            return false;
        }
    }
}
