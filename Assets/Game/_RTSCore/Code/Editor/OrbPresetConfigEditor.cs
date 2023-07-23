using UnityEditor;
using UnityEngine;

namespace RTSCore
{
	[CustomEditor(typeof(OrbProfileConfig))]
	public class OrbPresetConfigEditor : Editor
	{
		private OrbProfileConfig _config;

		private static GUIStyle ToggleButtonStyleNormal  = null;
		private static GUIStyle ToggleButtonStyleToggled = null;

		void OnEnable()
		{
			_config = target as OrbProfileConfig;
		}

		public override void OnInspectorGUI()
		{
			Init();
			
			EditorGUILayout.BeginHorizontal();
			{
				EditorGUILayout.BeginVertical();
				DrawResourcesUI();
				EditorGUILayout.EndVertical();
			}
			{
				EditorGUILayout.BeginVertical();
				DrawAbilitiesUI();
				EditorGUILayout.EndVertical();
			}
			{
				EditorGUILayout.BeginVertical();
				DrawParametersUI();
				EditorGUILayout.EndVertical();
			}
			EditorGUILayout.EndHorizontal();
		}

		private void Init()
		{
			if (ToggleButtonStyleNormal == null)
			{
				ToggleButtonStyleNormal  = "Button";
				ToggleButtonStyleToggled = new GUIStyle(ToggleButtonStyleNormal);

				ToggleButtonStyleToggled.normal.background = ToggleButtonStyleToggled.active.background;
			}

			if (_config.Profile == null)
			{
				_config.Profile = new OrbProfile();
			}
		}

		private void ToggleButton(ref bool flag, string label, bool enabled)
		{
			EditorGUI.BeginDisabledGroup(!enabled);
			flag = GUILayout.Toggle(flag, label, "Button", GUILayout.Width(120));
			/*
			if ( GUILayout.Button( label , flag ? ToggleButtonStyleToggled : ToggleButtonStyleNormal ) )
			{
				flag = !flag;
			}
			*/
			EditorGUI.EndDisabledGroup();
		}

		private void DrawResourcesUI()
		{
			GUILayout.Label("DrawResourcesUI");
		}
		
		private void ToggleButtonSimple(string label, ref bool flag)
		{
			flag = GUILayout.Toggle(flag, label, "Button");
		}

		private void DrawAbilitiesUI()
		{
			GUILayout.Label("DrawAbilitiesUI");

			ToggleButtonSimple("See", ref _config.Profile.AbilitySee);
			ToggleButtonSimple("Move", ref _config.Profile.AbilityMove);
			ToggleButtonSimple("Attack", ref _config.Profile.AbilityAttack);
			ToggleButtonSimple("Heal", ref _config.Profile.AbilityHeal);
			ToggleButtonSimple("Store", ref _config.Profile.AbilityStore);
			ToggleButtonSimple("Harvest", ref _config.Profile.AbilityHarvester);
			ToggleButtonSimple("Create", ref _config.Profile.AbilityCreate);
			ToggleButtonSimple("Dispose", ref _config.Profile.AbilityDispose);
			ToggleButtonSimple("Fly", ref _config.Profile.AbilityFly);

			GUILayout.Label($"Abilities: {_config.Profile.AbilityCount}");
		}

		private void DrawParametersUI()
		{
			GUILayout.Label("DrawParametersUI");
		}
	}
}