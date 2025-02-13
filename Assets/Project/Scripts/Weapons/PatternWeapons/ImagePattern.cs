using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Image_Pattern", menuName = "Patterns/Image")]
public class ImagePattern : Pattern
{
	[Header("Image")]
	[Tooltip("The image to take the pattern from")]
	[SerializeField] Texture2D image;
	[Tooltip("Scale is applied as 1/inverseScale.\r\ntype bigger numbers rather than decimals to make it smaller.")]
	[SerializeField] Vector2 inverseScale;
	[Tooltip("Offset on the x and y axis applied to the pattern")]
	[SerializeField] Vector2 offset;

	[Tooltip("If the image should be read in Top to Down")]
	[SerializeField] bool topToDown;
	[Tooltip("If the image should be read in Right to Left")]
	[SerializeField] bool rightToLeft;

	public override Vector3[] SpawnBullets(Vector3 direction, Vector2 scalar)
	{
		List<Vector3> positions = new List<Vector3>();

		for (int y = 0; y < image.height; y++)
		{
			for (int x = 0; x < image.width; x++)
			{
				int xtoadd = x;
				int ytoadd = y;

				if (rightToLeft) { xtoadd = image.width - x - 1; }
				if (topToDown) { ytoadd = image.height - y - 1; }

				if (image.GetPixel(xtoadd, ytoadd) == Color.black)
				{
					float xforvec = (xtoadd * 1 / inverseScale.x) - (image.width * (1 / inverseScale.x) / 2);
					float yforvec = (ytoadd * 1 / inverseScale.y) - (image.height * (1 / inverseScale.y) / 2);

					Vector3 vectoadd = new Vector3(xforvec + offset.x, yforvec + offset.y) * scalar;
					vectoadd = Quaternion.Euler(0, 0, (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90) * vectoadd;

					positions.Add(vectoadd);
				}
			}
		}

		bulletAmount = positions.Count;

		return positions.ToArray();
	}
}
