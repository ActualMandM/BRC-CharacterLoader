using Reptile;

namespace BRC_CharacterLoader
{
	public class EnumString
	{
		public static MoveStyle ParseMoveStyle(string enumString)
		{
			var cleanString = enumString.ToLowerInvariant().Replace(" ", "").Trim();

            return cleanString switch
            {
                "bmx" => MoveStyle.BMX,
                "inline" or "skates" or "inlineskates" => MoveStyle.INLINE,
                _ => MoveStyle.SKATEBOARD,
            };
        }

		public static Characters ParseCharacter(string enumString)
		{
			var cleanString = enumString.ToLowerInvariant().Replace(" ", "").Replace(".", "").Trim();

            return cleanString switch
            {
                "girl1" or "vinyl" => Characters.girl1,
                "frank" => Characters.frank,
                "ringdude" or "coil" => Characters.ringdude,
                "blockguy" or "tryce" => Characters.blockGuy,
                "spacegirl" or "bel" => Characters.spaceGirl,
                "angel" or "rave" => Characters.angel,
                "eightball" or "dotexe" => Characters.eightBall,
                "dummy" or "solace" => Characters.dummy,
                "dj" or "djcyber" => Characters.dj,
                "medusa" or "eclipse" => Characters.medusa,
                "boarder" or "deviltheory" => Characters.boarder,
                "headman" or "faux" => Characters.headMan,
                "prince" or "fleshprince" => Characters.prince,
                "jetpackbossplayer" or "jetpackboss" or "rietveld" or "irene" => Characters.jetpackBossPlayer,
                "legendface" or "felix" => Characters.legendFace,
                "oldheadplayer" or "oldhead" => Characters.oldheadPlayer,
                "robot" or "base" => Characters.robot,
                "skate" or "jet" => Characters.skate,
                "widekid" or "mesh" => Characters.wideKid,
                "futuregirl" or "futurism" => Characters.futureGirl,
                "puffergirl" or "rise" => Characters.pufferGirl,
                "bungirl" or "shine" => Characters.bunGirl,
                "eightballboss" or "dotexeboss" => Characters.eightBallBoss,
                "legendmetalhead" or "redfelix" => Characters.legendMetalHead,
                _ => Characters.metalHead,
            };
        }
	}
}
