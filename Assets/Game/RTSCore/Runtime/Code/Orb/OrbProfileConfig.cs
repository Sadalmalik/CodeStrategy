using UnityEngine;

namespace RTSCore
{
	[CreateAssetMenu(
		order = 0,
		menuName = "RTS/Orb Profile",
		fileName = "OrbProfile"
	)]
	public class OrbProfileConfig : ScriptableObject
	{
		public OrbProfile Profile;
	}
}