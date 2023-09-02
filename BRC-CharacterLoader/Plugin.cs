using BepInEx;
using CharacterAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using UnityEngine;

namespace BRC_CharacterLoader
{
	[BepInPlugin("com.MandM.BRC-CharacterLoader", "BRC-CharacterLoader", "0.9.2")]
	[BepInDependency("com.Viliger.CharacterAPI")]

	public class Plugin : BaseUnityPlugin
	{
		public void Awake()
		{
			string charaPath = Path.GetDirectoryName(Info.Location) + "/Characters";

			if (!Directory.Exists(charaPath))
				Directory.CreateDirectory(charaPath);

			try
			{
				foreach (string folder in Directory.GetDirectories(charaPath))
				{
					string jsonText = File.ReadAllText(folder + "/metadata.json");
					List<Metadata>? modCharacters = JsonSerializer.Deserialize<List<Metadata>>(jsonText);

					if (modCharacters == null)
					{
						Logger.LogInfo($"JSON data in {new DirectoryInfo(folder).Name} either doesn't exist or is invalid! Skipping...");
						continue;
					}

					foreach (Metadata charaData in modCharacters)
					{
						AssetBundle bundle = AssetBundle.LoadFromFile(Path.Combine(folder, charaData.bundleName));

						ModdedCharacterConstructor chara = new ModdedCharacterConstructor();
						chara.characterName = charaData.charaName;
						chara.characterPrefab = bundle.LoadAsset<GameObject>(charaData.prefabName);
						chara.defaultOutfit = Math.Abs(charaData.defaultOutfit) % 4;
						chara.defaultMoveStyle = EnumString.ParseMoveStyle(charaData.moveStyle);
						chara.tempAudioBase = EnumString.ParseCharacter(charaData.voice);
						chara.freestyleType = (ModdedCharacterConstructor.FreestyleType)(Math.Abs(charaData.freeStyle) % 19);
						chara.bounceType = (ModdedCharacterConstructor.BounceType)(Math.Abs(charaData.bounce) % 16);

						if (charaData.outfits != null)
						{
							foreach (Outfit outfitData in charaData.outfits)
								chara.AddOutfit(bundle.LoadAsset<Material>(outfitData.outfitMaterial), outfitData.outfitName);
						}
						else
						{
							Logger.LogInfo(charaData.charaName + " is missing outfit data! Skipping...");
							continue;
						}

						if (charaData.graffiti != null)
						{
							if (!String.IsNullOrEmpty(charaData.graffiti.graffitiBase))
							{
								chara.personalGraffitiBase = EnumString.ParseCharacter(charaData.graffiti.graffitiBase);
							}
							else
							{
								chara.AddPersonalGraffiti(
									charaData.graffiti.graffitiName,
									charaData.graffiti.graffitiArtist,
									bundle.LoadAsset<Material>(charaData.graffiti.graffitiMaterial),
									bundle.LoadAsset<Texture>(charaData.graffiti.graffitiTexture)
								);
							}
						}

						chara.CreateModdedCharacter();
					}
				}
			}
			catch (Exception ex)
			{
				Logger.LogInfo("Exception: " + ex.Message);
			}
		}
	}
}
