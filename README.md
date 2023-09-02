# BRC-CharacterLoader
A BepInEx plugin for [Bomb Rush Cyberfunk](https://store.steampowered.com/app/1353230) that allows people to easily add custom characters via [CharacterAPI](https://github.com/viliger2/BRC_CharacterAPI).

## Usage

Download and install [this plugin](https://github.com/ActualMandM/BRC-CharacterLoader/releases/latest) and [CharacterAPI](https://thunderstore.io/c/bomb-rush-cyberfunk/p/viliger/CharacterAPI/). Follow [CharacterAPI's tutorial](https://github.com/viliger2/BRC_CharacterAPI/wiki/Creating-new-character-via-plugin) up until creating a plugin.

Then, in the `Characters` folder of this plugin, create a new folder. Then create a file called `metadata.json` based off the [example JSON](#example-json).

You then need to modify the json to fit your character. Certain variables will take in certain values which are noted below:

- `voice`, `graffitiBase`: Character names, either what they're called in-game or their internal name.
  - Defaults to Red if invalid
  - `graffitiBase` must be left blank if you're using custom graffiti, otherwise it'll override
  - DOT.EXE's boss can be selected by either using the internal name (`eightballBoss`) or by specifying boss (`dotexeboss`)
  - Ritvield can also be selected with `irene`
- `moveStyle`: Move style, internal name and in-game names pretty much match up
  - Defaults to Skateboard if invalid
  - Inline skates can also be selected with `skates`

Afterwards, boot up the game and go to a Cypher. Your character should show up. If not, check the BepInEx console for any errors and [make sure your JSON is valid](https://jsonlint.com/).

You can also have several character entries in the metadata, in case you want to make a character pack.

### Example JSON

```json
[
	{
		"bundleName": "beat",
		"prefabName": "beat_no_blades",
		"charaName": "Beat",
		"defaultOutfit": 0,
		"moveStyle": "inline",
		"voice": "felix",
		"freeStyle": 15,
		"bounce": 3,
		"outfits": [
			{
				"outfitMaterial": "beatDefault",
				"outfitName": "Jet Set"
			},
			{
				"outfitMaterial": "beatFuture",
				"outfitName": "Future"
			},
			{
				"outfitMaterial": "beatCombo",
				"outfitName": "Combo"
			},
			{
				"outfitMaterial": "beatCorn",
				"outfitName": "Corn"
			}
		],
		"graffiti": {
			"graffitiBase": "",
			"graffitiName": "Beat",
			"graffitiArtist": "Beat",
			"graffitiMaterial": "beatGraffiti",
			"graffitiTexture": "graffitiTexture"
		}
	}
]
```
