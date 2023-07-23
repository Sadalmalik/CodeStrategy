using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace MetaRTS_Legacy
{
	[CustomNodeEditor(typeof(GenNode))]
	public class GenerationNodeEditor : NodeEditor
	{
		private GenNode _node;
		
		public override void OnBodyGUI()
		{
			EditorGUI.BeginChangeCheck();
			base.OnBodyGUI();
			var dirty = EditorGUI.EndChangeCheck();

			if (_node == null) _node = target as GenNode;
			
			if (dirty) _node.ClearPreview();
			if (!_node.showPreview) return;
			
			EditorGUILayout.LabelField(
				new GUIContent(_node.Preview),
				GUILayout.Width(180),
				GUILayout.Height(180));
		}
	}
}