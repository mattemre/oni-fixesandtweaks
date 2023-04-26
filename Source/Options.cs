using PeterHan.PLib.Options;
using Newtonsoft.Json;

namespace FixesAndTweaks
{
    [JsonObject(MemberSerialization.OptIn)]
    [ModInfo("https://github.com/llunak/oni-fixesandtweaks")]
    [ConfigFile(SharedConfigLocation: true)]
    public sealed class Options : SingletonOptions< Options >
    {
        [Option("Faster Horizontal Scrolling", "Makes horizontal scrolling in views such as the 'Consumables' one faster.")]
        [JsonProperty]
        public bool FasterHorizontalScrolling { get; set; } = true;

        public override string ToString()
        {
            return string.Format("DeliveryTemperatureLimit.Options[fasterhorizontalscrolling={0}]",
                FasterHorizontalScrolling);
        }
    }
}
