using System;

namespace MetaRTS
{
	[Flags]
	public enum OrbAbilities
	{
		None = 0,

		// Base
		Move    = 0b0000_0001,
		Attack  = 0b0000_0010,
		Heal    = 0b0000_0100,
		Create  = 0b0000_1000,
		Harvest = 0b0001_0000,

		// Additional
		Fly  = 0b0010_0000,
		Dive = 0b0100_0000,

		// Bitmasks
		MaskBase       = Move | Attack | Heal | Create | Harvest,
		MaskAdditional = Fly | Dive
	}

	public static class OrbAbilitiesExtensions
	{
		public static bool Contains(this ref OrbAbilities abilities, OrbAbilities flags)
		{
			return (int)(abilities & flags) == (int)flags;
		}
	
		public static bool IsValid(this ref OrbAbilities abilities, int resources)
		{
			// 1 ресурс  - 1 основная способность
			// 2 ресурса - 2 или 1+1 дополнительная
			// 3 ресурса - 3 или 2+1
			// 4 ресурса - 4 или 3+1 или 2+2
			var bits = BitUtils.CountBits((int) abilities);
			if (bits > resources)
				return false;
			var additional = BitUtils.CountBits((int) (abilities & OrbAbilities.MaskAdditional));
			switch (bits)
			{
				case 1: return additional == 0;
				case 2: return additional <= 1;
				case 3: return additional <= 1;
				case 4: return additional <= 2;
			}
			return false;
		}
	}
}