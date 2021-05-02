using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;

public class LWResourceManager : MonoBehaviour
{
	public static List<Music> MusicList => _musicList;

	private static List<VideoItem> _videoList = new List<VideoItem>();
	private static List<Music> _musicList = new List<Music>();

	public static bool IsLoaded => _isLoaded;

	private static bool _isLoaded;

	public static Dictionary<int, List<Sprite>> Sprites = new Dictionary<int, List<Sprite>>();
	private int _spriteWidth = 400;
	private int _spriteHeight = 600;

	private static List<Sprite> _drinkIcons = new List<Sprite>();
	public static List<Sprite> DrinkIcons => _drinkIcons;

	private static List<Flower> _flowers = new List<Flower>();

	public static List<Flower> Flowers => _flowers;
	
	public class Flower
	{
		public string Name;
		public string Type;
		public int Index;
		public int Cost;
		public int Earns;
	}
	
	public class VideoItem
	{
		public Sprite Image;
		public string Title;
		public string Date;
		public string Url;
	}

	public class LinkItem
	{
		public Sprite Icon;
		public string Link;
	}

	public class Music
	{
		public string Title;
		public List<LinkItem> Links;
		public List<VideoItem> Videos;
	}

	private IEnumerator Start()
	{
		
		LWData.current.Setup((LWData) SerializationManager.Load(Application.persistentDataPath + "/saves/likewater.rv"));
		PrepareData();
		yield return GetVideos();
		yield return GetFlowers();
		yield return GetSprites();
		yield return GetDrinkIcons();
		_isLoaded = true;
	}

	private void PrepareData()
	{
		var date = DateTime.Today;
		var key = date.Month + "/" + date.Year;
		var dict = LWData.current.FlowerDictionary;
		if (string.IsNullOrEmpty(LWData.current.CurrentFlower.Date))
		{
			if (!dict.ContainsKey(key))
				dict.Add(key, new List<LWData.FlowerMonth>());
			var days = DateTime.DaysInMonth(date.Year, date.Month);
			for (var day = 0; day < days; day++)
			{
				var flower = new LWData.FlowerMonth {Date = date.Month + "/" + (day + 1) + "/" + date.Year};
				dict[key].Add(flower);
			}

			LWData.current.FlowerDictionary = dict;
		}
	}
	
	private IEnumerator GetFlowers()
	{
		var request = UnityWebRequest.Get(Application.streamingAssetsPath + "/likewater-pots.json");
		request.SendWebRequest();
		while (!request.isDone)
		{
			yield return null;
		}
		var data = request.downloadHandler.text;
		var flowerItems = JSON.Parse(data);
		foreach (var flower in flowerItems["Flowers"].AsArray)
		{
			var value = flower.Value;
			var item = new Flower();
			item.Name = value["Name"];
			item.Type = value["Type"];
			if (!int.TryParse(value["Index"], out item.Index))
				Debug.LogError("Failed to parse: " + value["Index"]);
			if (!int.TryParse(value["Cost"], out item.Cost))
				Debug.LogError("Failed to parse: " + value["Cost"]);
			if (!int.TryParse(value["Earns"], out item.Earns))
				Debug.LogError("Failed to parse: " + value["Earns"]);
			_flowers.Add(item);
			
		}
	}
	
	private IEnumerator GetVideos()
	{
		
		var request = UnityWebRequest.Get(Application.streamingAssetsPath + "/likewater-music.json");
		request.SendWebRequest();
		while (!request.isDone)
		{
			yield return null;
		}

		var data = request.downloadHandler.text;
		var videoItems = JSON.Parse(data);
		foreach (var videoItem in videoItems["Music"].AsArray)
		{
			var music = new Music();
			var value = videoItem.Value;

			var links = value["Links"].AsArray;
			var videos = value["Videos"].AsArray;
			music.Title = value["Title"];
			music.Links = new List<LinkItem>();
			foreach (var linkItem in links)
			{
				var linkValue = linkItem.Value;
				var item = new LinkItem();
				item.Link = linkValue["link"];
				music.Links.Add(item);

			}

			music.Videos = new List<VideoItem>();
			Debug.Log(value);
			foreach (var vidItem in videos)
			{
				var videoValue = vidItem.Value;
				var video = new VideoItem();
				video.Title = videoValue["title"];
				video.Date = videoValue["date"];
				video.Url = videoValue["video-url"];
				yield return GetImage(videoValue["image-url"], sprite => { video.Image = sprite;});
				music.Videos.Add(video);
			}
			_musicList.Add(music);
		}
	}

	private IEnumerator GetDrinkIcons()
	{
		var url = Application.streamingAssetsPath + "/likewater-drinks.png";
		var request = UnityWebRequestTexture.GetTexture(url);
		yield return request.SendWebRequest();

		var texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
		if (texture == null)
		{
			Debug.LogError("Nothing was downloaded at: " + url);
			yield break;
		}

		var width = 300;
		var height = 300;
		var spriteCount = Mathf.RoundToInt((float) texture.height / _spriteHeight);
		var counter = 0;
		var spriteList = new List<Sprite>();
		for (var i = 0; i < spriteCount; i++)
		{
			for (var j = 0; j < 3; j++)
			{
				var sprite = Sprite.Create(texture,
					new Rect(j * width, i * height, width, height),
					new Vector2(0.5f, 0.5f));
				spriteList.Add(sprite);
			}
		}

		_drinkIcons = spriteList;
	}
	
	private IEnumerator GetSprites()
	{
		var url = Application.streamingAssetsPath + "/likewater-flowers.png";
		var request = UnityWebRequestTexture.GetTexture(url);
		yield return request.SendWebRequest();

		var texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
		if (texture == null)
		{
			Debug.LogError("Nothing was downloaded at: " + url);
			yield break;
		}

		var spriteCount = Mathf.RoundToInt((float) texture.height / _spriteHeight);
		var spriteWidth = Mathf.RoundToInt((float) texture.width / _spriteWidth);
		for (var i = 0; i < spriteCount; i++)
		{
			var spriteList = new List<Sprite>();
			for (var j = 0; j < spriteWidth; j++)
			{
				var sprite = Sprite.Create(texture,
					new Rect(j * _spriteWidth, i * _spriteHeight, _spriteWidth, _spriteHeight),
					new Vector2(0.5f, 0.5f));
				spriteList.Add(sprite);
			}

			Sprites[i] = spriteList;
		}

	}
	
	private IEnumerator GetImage(string url, Action<Sprite> onComplete)
	{
		var request = UnityWebRequestTexture.GetTexture(url);
		yield return request.SendWebRequest();
			
		var texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
		var sprite = Extensions.Texture2DToSprite(texture);
		
		onComplete(sprite);
		yield return null;
	}
}
