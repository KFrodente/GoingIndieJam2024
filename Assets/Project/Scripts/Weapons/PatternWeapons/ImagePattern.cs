using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Image_Pattern", menuName = "Patterns/Image")]
public class ImagePattern : Pattern
{
	[Header("Image")]
	[SerializeField] Texture2D image;
	[SerializeField] Vector2 scale;

	public override Vector3[] SpawnBullets(Vector3 direction)
	{
		List<Vector3> positions = new List<Vector3>();

		for (int y = 0; y < image.height; y++)
		{
			for (int x = 0; x < image.width; x++)
			{
				if (image.GetPixel(x, y) == Color.black)
				{
					Vector3 toadd = new Vector3((x * scale.x) - (image.width * scale.x / 2), (y * scale.y) - (image.height * scale.y / 2));
					positions.Add(toadd);
				}
			}
		}

		return positions.ToArray();
	}
}
