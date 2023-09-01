using Reptile;

namespace BRC_CharacterLoader
{
	public class EnumString
	{
		public static MoveStyle ParseMoveStyle(string enumString)
		{
			var cleanString = enumString.ToLower().Replace(" ", "");
			switch (cleanString.Trim())
			{
				case "bmx":
					return MoveStyle.BMX;

				default:
				case "skateboard":
					return MoveStyle.SKATEBOARD;

				case "inline":
				case "skates":
				case "inlineskates":
					return MoveStyle.INLINE;
			}
		}

		public static Characters ParseCharacter(string enumString)
		{
			var cleanString = enumString.ToLower().Replace(" ", "").Replace(".", "");
			switch (cleanString.Trim())
			{
				case "girl1":
				case "vinyl":
					return Characters.girl1;

				case "frank":
					return Characters.frank;

				case "ringdude":
				case "coil":
					return Characters.ringdude;

				default:
				case "metalhead":
				case "red":
					return Characters.metalHead;

				case "blockguy":
				case "tryce":
					return Characters.blockGuy;

				case "spacegirl":
				case "bel":
					return Characters.spaceGirl;

				case "angel":
				case "rave":
					return Characters.angel;

				case "eightball":
				case "dotexe":
					return Characters.eightBall;

				case "dummy":
				case "solace":
					return Characters.dummy;

				case "dj":
				case "djcyber":
					return Characters.dj;

				case "medusa":
				case "eclipse":
					return Characters.medusa;

				case "boarder":
				case "deviltheory":
					return Characters.boarder;

				case "headman":
				case "faux":
					return Characters.headMan;

				case "prince":
				case "fleshprince":
					return Characters.prince;

				case "jetpackbossplayer":
				case "jetpackboss":
				case "ritvield":
				case "irene":
					return Characters.jetpackBossPlayer;

				case "legendface":
				case "felix":
					return Characters.legendFace;

				case "oldheadplayer":
				case "oldhead":
					return Characters.oldheadPlayer;

				case "robot":
				case "base":
					return Characters.robot;

				case "skate":
				case "jet":
					return Characters.skate;

				case "widekid":
				case "mesh":
					return Characters.wideKid;

				case "futuregirl":
				case "futurism":
					return Characters.futureGirl;

				case "puffergirl":
				case "rise":
					return Characters.pufferGirl;

				case "bungirl":
				case "shine":
					return Characters.bunGirl;

				case "eightballboss":
				case "dotexeboss":
					return Characters.eightBallBoss;

				case "legendmetalhead":
				case "redfelix":
					return Characters.legendMetalHead;
			}
		}
	}
}
