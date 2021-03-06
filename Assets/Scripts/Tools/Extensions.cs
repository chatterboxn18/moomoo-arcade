using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
	public static Vector3 AddVector3(this Vector3 vec, Vector3 other)
	{
		vec = new Vector3(vec.x + other.x, vec.y + other.y, vec.z + other.z);
		return vec;
	}

	public static Vector3 AddVector3To2(this Vector2 vec, Vector3 other)
	{
		vec = new Vector3(vec.x + other.x, vec.y + other.y, other.z);
		return vec;
	}

	public static Vector3 MultiplyVector3(this Vector3 vec, float number)
	{
		vec = new Vector3(vec.x * number, vec.y * number, vec.z * number);
		return vec;
	}
	
	public static Vector2 MultiplyVector2(this Vector2 vec, float number)
	{
		vec = new Vector2(vec.x * number, vec.y * number);
		return vec;
	}

	public static Vector2 SetX(this Vector2 vec, float number)
	{
		vec = new Vector2(number, vec.y);
		return vec;
	}

	public static Vector3 SetX(this Vector3 vec, float number)
	{
		vec = new Vector3(number, vec.y, vec.z);
		return vec;
	}

	public static Vector3 SetY(this Vector3 vec, float number)
	{
		vec = new Vector3(vec.x, number, vec.z);
		return vec;
	}

	public static Vector2 AddY(this Vector2 vec, float number)
	{
		vec = new Vector2(vec.x, vec.y + number);
		return vec;
	}

	public static Quaternion RotateZ(this Quaternion quat, float number)
	{
		quat = Quaternion.Euler(0,0,number);
		return quat;
	}
	
	public static List<int> Randomize(this List<int> finalList)
	{
		var length = finalList.Count;
		finalList = new List<int>();
		var tempList = new List<int>();
		for (var i = 0; i < length; i++)
		{
			tempList.Add(i);
		}
		for (var i = 0; i < length; i++)
		{
			var ranNum = tempList[Random.Range(0, tempList.Count)];
			finalList.Add(ranNum);
			tempList.Remove(ranNum);
		}

		return finalList;
	}

	public static Color SetAlpha(this Color color, float alpha)
	{
		color = new Color(color.r, color.g, color.b, alpha);
		return color;
	}

	public static Sprite Texture2DToSprite(Texture2D texture)
	{
		return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
	}
}
