using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace MetaRTS_Legacy
{
	public class GameManager : Singleton<GameManager>
	{
		public bool enableInfo;
		
		public float heightLimit = 10;
		public float flyHeightLimit = 150;
		public float diveHeightLimit = -30;
		
		public Orb orbPrefab;
		
		public FractionSetting[] fractions;
		
		public void Init()
		{
			
		}
		
		public Orb CreateOrb(Orb parent, OrbAbilities abilities, OrbSettings settings)
		{
			//parent.fraction
			return null;
		}
	}
}