using BepInEx;
using CharacterAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;
using UnityEngine;

namespace BRC_CharacterLoader
{
	[BepInPlugin("com.MandM.BRC-CharacterLoader", "BRC-CharacterLoader", "0.9.7")]
	[BepInDependency("com.Viliger.CharacterAPI", "0.6.0")]

	public class Plugin : BaseUnityPlugin
	{
		[DllImport("User32.dll", CharSet = CharSet.Unicode)]
		private static extern int MessageBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);

		public void Awake()
		{
			string charaPath = Path.Combine(Path.GetDirectoryName(Info.Location), "Characters");

			if (!Directory.Exists(charaPath))
				Directory.CreateDirectory(charaPath);

			foreach (string folder in Directory.GetDirectories(charaPath))
			{
				string folderName = new DirectoryInfo(folder).Name;
				bool isPlugin = false;

				try
				{
					// Detect if user has installed a BepInEx plugin
					foreach (string file in Directory.GetFiles(folder))
					{
						if (Path.GetExtension(file) == ".dll")
						{
							Logger.LogError($"[{folderName}] BepInEx plugin detected.");
							MessageBox(IntPtr.Zero, $"{folderName} is a BepInEx plugin, it will not load through BRC-CharacterLoader.\n\nPlease install it in the proper location: \"BombRushCyberfunk\\BepInEx\\plugins\"", "BRC-CharacterLoader", 0);
							isPlugin = true;
						}
					}

					if (isPlugin)
						continue;

					string jsonText = File.ReadAllText(folder + "/metadata.json");
					List<Metadata>? modCharacters = JsonSerializer.Deserialize<List<Metadata>>(jsonText);

					if (modCharacters == null)
					{
						Logger.LogError($"[{folderName}] JSON data either doesn't exist or is invalid.");
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
						chara.freestyleType = (ModdedCharacterConstructor.FreestyleType)(Math.Abs(charaData.freeStyle) % 19);
						chara.bounceType = (ModdedCharacterConstructor.BounceType)(Math.Abs(charaData.bounce) % 16);

						if (!String.IsNullOrEmpty(charaData.voiceBase))
							chara.tempAudioBase = EnumString.ParseCharacter(charaData.voiceBase);
						else
							chara.audioClips = bundle.LoadAllAssets<AudioClip>().ToList();

						if (charaData.outfits != null)
						{
							foreach (Outfit outfitData in charaData.outfits)
								chara.AddOutfit(bundle.LoadAsset<Material>(outfitData.outfitMaterial), outfitData.outfitName);
						}
						else
						{
							Logger.LogError($"[{folderName}] {charaData.charaName} is missing outfit data.");
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
				catch (Exception ex)
				{
					Logger.LogError($"[{folderName}] Exception: " + ex.Message);
				}
			}
		}
	}
}
