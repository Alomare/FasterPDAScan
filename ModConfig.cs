using Nautilus.Json;
using Nautilus.Options.Attributes;

namespace Alomare.FasterPDAScan
{
    [Menu("Faster PDA Scan Mod")]
    public class ModOptions : ConfigFile
    {
        [Slider("Scan Speed Multiplier", 0.1f, 10.0f, DefaultValue = 2.0f, Format = "{0:F2}")]
        public float ScanSpeedMultiplier = 2.0f;
    }
}