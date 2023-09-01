using BepInEx;
using CharacterAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using UnityEngine;

namespace BRC_CharacterLoader
{
	[BepInPlugin(ModGuid, ModName, ModVer)]
	[BepInDependency("com.Viliger.CharacterAPI")]

	public class Plugin : BaseUnityPlugin
	{
		public const string ModGuid = "com.MandM.BRC-CharacterLoader";
		public const string ModName = "BRC-CharacterLoader";
		public const string ModVer = "0.9.1";

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
					List<Metadata> ModCharacters = new List<Metadata>();

					try
					{
						ModCharacters = JsonSerializer.Deserialize<List<Metadata>>(jsonText);
					}
					catch (Exception ex)
					{
						Logger.LogInfo("Exception: " + ex.Message);
						continue;
					}

					if (ModCharacters == null)
					{
						Logger.LogInfo($"JSON data in {new DirectoryInfo(folder).Name} either doesn't exist or is invalid! Skipping...");
						continue;
					}

					foreach (var charaData in ModCharacters)
					{
						AssetBundle bundle = AssetBundle.LoadFromFile(Path.Combine(folder, charaData.bundleName));

						ModdedCharacterConstructor chara = new ModdedCharacterConstructor();
						chara.characterName = charaData.charaName;
						chara.characterPrefab = bundle.LoadAsset<GameObject>(charaData.prefabName);
						chara.defaultOutfit = Math.Abs(charaData.defaultOutfit - 1) % 4;
						chara.defaultMoveStyle = EnumString.ParseMoveStyle(charaData.moveStyle);
						chara.tempAudioBase = EnumString.ParseCharacter(charaData.voice);
						chara.freestyleType = (ModdedCharacterConstructor.FreestyleType)(Math.Abs(charaData.freeStyle - 1) % 19);
						chara.bounceType = (ModdedCharacterConstructor.BounceType)(Math.Abs(charaData.bounce - 1) % 16);

						if (charaData.outfits != null)
						{
							foreach (Outfit outfitData in charaData.outfits)
								chara.AddOutfit(bundle.LoadAsset<Material>(outfitData.outfitMaterial), outfitData.outfitName);
						}
						else
						{
							Logger.LogInfo($"{charaData.charaName} is missing outfit data! Skipping...");
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
