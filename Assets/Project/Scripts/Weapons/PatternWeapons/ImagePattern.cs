using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Image_Pattern", menuName = "Patterns/Image")]
public class ImagePattern : Pattern
{
	[Header("Image")]
	[SerializeField] Texture2D image;
	[SerializeField] Vector2 inverseScale;
	[SerializeField] Vector2 offset;

	[SerializeField] bool topToDown;
	[SerializeField] bool rightToLeft;

	public override Vector3[] SpawnBullets(Vector3 direction)
	{
		List<Vector3> positions = new List<Vector3>();

		for (int y = 0; y < image.height; y++)
		{
			for (int x = 0; x < image.width; x++)
			{
				int xtoadd = x;
				int ytoadd = y;

				if (rightToLeft) { xtoadd = image.width - x - 1; }
				if ( topToDown) { ytoadd = image.height - y - 1; }

				if (image.GetPixel(xtoadd, ytoadd) == Color.black)
				{
					float xforvec = (xtoadd * 1 / inverseScale.x) - (image.width * (1 / inverseScale.x) / 2);
					float yforvec = (ytoadd * 1 / inverseScale.y) - (image.height * (1 / inverseScale.y) / 2);

					Vector3 vectoadd = new Vector3(xforvec + offset.x, yforvec + offset.y);
					vectoadd = Quaternion.Euler(0,0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) * vectoadd;
					positions.Add(vectoadd);
				}
			}
		}

		return positions.ToArray();
	}
}
