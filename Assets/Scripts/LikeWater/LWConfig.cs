using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public static class LWConfig 
{
	public const string Timer = "PreferredTimer";
	public const string HasNotification = "Timer_HasNotification";
	public const string HasSound = "Timer_HasSound";
	public const string AttributeWaterKey = "water";
	public const string DataSaveName = "likewater";
	public const string PageIndexName = "PageIndex";
	public const string NotificationTitle = "Like Water";
	public const string WaterNotificationDescription = "Don't forget to stay hydrated. Drink some water <3";
	public const string StreamNotificationDescription = "Need water? Stream like water.";
	public const string NotificationChannel = "like_water";
	public const float FadeTime = 0.2f;

	public static Color SelectedColor = new Color(39,134,197, 1);
	public static Color MainColor = new Color(105,169,229, 1);
}
