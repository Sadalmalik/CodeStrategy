using System.Collections;
using System.Collections.Generic;

namespace MetaRTS_Legacy
{
	public class Fraction
	{
		public FractionSetting settings;
		
		public HashSet<Orb> orbs { get; private set; }
		
		public Fraction()
		{
			orbs = new HashSet<Orb>();
		}
		
		public HashSet<Orb> GetAllVisibleEnemies(HashSet<Orb>set=null)
		{
			if (set==null)
				set=new HashSet<Orb>();
			foreach (var orb in orbs)
				orb.GetVisibleEnemies(set);
			return set;
		}
		
		public HashSet<ResourceEntity> GetAllVisibleResources(HashSet<ResourceEntity>set=null)
		{
			if (set==null)
				set=new HashSet<ResourceEntity>();
			foreach (var orb in orbs)
				orb.GetVisibleSources(set);
			return set;
		}
	}
}