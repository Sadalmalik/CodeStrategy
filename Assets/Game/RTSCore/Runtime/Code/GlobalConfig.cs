using System.Collections;
using System.Collections.Generic;
using RTSCore;
using UnityEngine;

namespace RTSCore
{
	[CreateAssetMenu(
		order = 0,
		menuName = "RTS/Global Config",
		fileName = "GlobalConfig"
	)]
	public class GlobalConfig : ScriptableObject
	{
	}
}

public static class Test
{
	public static void Do(OrbInteraction inter)
	{
		inter.Init();
	}
}