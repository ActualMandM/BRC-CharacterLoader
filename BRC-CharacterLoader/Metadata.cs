using System.Collections.Generic;

namespace BRC_CharacterLoader
{
    public class Metadata
    {
        public string bundleName { get; set; } = string.Empty;
        public string prefabName { get; set; } = string.Empty;
        public string charaName { get; set; } = string.Empty;
        public int defaultOutfit { get; set; } = 0;
        public bool canBlink { get; set; } = false;
        public string moveStyle { get; set; } = string.Empty;
        public string voiceBase { get; set; } = string.Empty;
        public int freeStyle { get; set; } = 0;
        public int bounce { get; set; } = 0;
        public List<Outfit>? outfits { get; set; }
        public Graffiti? graffiti { get; set; }
    }

    public class Outfit
    {
        public string outfitMaterial { get; set; } = string.Empty;
        public string outfitName { get; set; } = string.Empty;
    }

    public class Graffiti
    {
        public string graffitiBase { get; set; } = string.Empty;
        public string graffitiName { get; set; } = string.Empty;
        public string graffitiArtist { get; set; } = string.Empty;
        public string graffitiMaterial { get; set; } = string.Empty;
        public string graffitiTexture { get; set; } = string.Empty;
    }
}
