using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TestUnits
{
	[ExecuteAlways]
	public class BugsUpdater : MonoBehaviour
	{
		public List<Bug> bugs;

		public bool findAllBugs;
		public bool expandAllBugs;

		public void Update()
		{
			if (!Application.isPlaying)
			{
#if UNITY_EDITOR
				HandleEditorActions();
#endif
			}
			else
			{
				int count = bugs.Count;
				for (int i = 0; i < count; i++)
				{
					bugs[i].Tick();
				}
			}
		}

#if UNITY_EDITOR
		private void HandleEditorActions()
		{
			if (findAllBugs)
			{
				findAllBugs = false;

				bugs = FindObjectsOfType<Bug>().ToList();
			}

			if (expandAllBugs)
			{
				expandAllBugs = false;

				foreach (var bug in bugs)
				{
					PrefabUtility.UnpackPrefabInstance(bug.transform.parent.gameObject, PrefabUnpackMode.Completely,
						InteractionMode.AutomatedAction);
					bug.transform.SetParent(null);
					foreach (var leg in bug.legs)
					{
						leg.transform.SetParent(null);
					}
				}
			}
		}
#endif
	}
}