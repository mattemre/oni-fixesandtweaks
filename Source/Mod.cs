using HarmonyLib;
using PeterHan.PLib.Core;

namespace FixesAndTweaks
{
    public class Mod : KMod.UserMod2
    {
        public override void OnLoad( Harmony harmony )
        {
            base.OnLoad( harmony );
            PUtil.InitLibrary( false );
        }
    }
}
