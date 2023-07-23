using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetaRTS_Legacy
{
	public static class WorldGenUtils
	{
		public static Texture2D Bake(
			GenNode node,
			int           width,
			int           height,
			float         xSize = 1,
			float         ySize = 1)
		{
			var texture = new Texture2D(width, height, TextureFormat.ARGB32, false);
			var pixels  = texture.GetPixels();
			for (int y = 0; y < height; y++)
			for (int x = 0; x < width; x++)
			{
				var value = node.Calculate(
					xSize * x / width,
					ySize * y / height);
				pixels[x + y * width] = Color.Lerp(Color.black, Color.white, value);
			}
			texture.SetPixels(pixels);
			texture.Apply();
			return texture;
		}
	}
}