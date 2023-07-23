using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace MetaRTS_Legacy
{
	public abstract class GenNode : Node
	{
#region Main

		public bool showPreview = false;
		
		[Output] public GenNode value;

		public void Reset()
		{
			value = this;
		}

		public override object GetValue(NodePort port)
		{
			if (port.fieldName == "value")
				return value;
			return null;
		}
		
		public abstract void SetRandom(GenRandom random);
		
		public abstract float Calculate(float x, float y);

#endregion


#region Generator preview

		private Texture2D priview;

		public Texture2D Preview
		{
			get
			{
				if (priview == null)
					priview = WorldGenUtils.Bake(value, 180, 180);
				return priview;
			}
		}

		public void ClearPreview()
		{
			priview = null;
			foreach (var port in Ports)
			{
				if (!port.IsOutput)
					continue;

				var connections = port.GetConnections();
				foreach (var connect in connections)
				{
					if (connect.node is GenNode node)
						node.ClearPreview();
				}
			}
		}

#endregion
	}
}