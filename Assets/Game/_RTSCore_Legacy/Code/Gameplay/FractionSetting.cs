using UnityEngine;

namespace MetaRTS_Legacy
{
	[CreateAssetMenu(
		fileName = "FractionSetting",
		menuName = "RTS/FractionSetting",
		order    = 0)]
	public class FractionSetting : ScriptableObject
	{
		public string name;
		public string author;
		[Space]
		public FractionController controllerPrefab;
		[Space]
		public ResourceType[] resources;
		
	}
}