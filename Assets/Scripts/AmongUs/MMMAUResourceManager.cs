using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public enum NewsType
{
	LeftPhoto = 0, 
	FourPhotos = 1
}

public class MMMAUResourceManager : MonoBehaviour
{
	public static Dictionary<string, AlbumPerformances> PerformanceVideos => _albumPerformances;

	public static Dictionary<string, List<NewsData>> NewsList => _newsData;
	public static Dictionary<string, Albums.Album> Albums => _albums;

	public static bool IsDataReady;

	[SerializeField] private Albums _albumData;
	
	private static Dictionary<string, AlbumPerformances> _albumPerformances;
	private static Dictionary<string, List<NewsData>> _newsData;
	private static Dictionary<string, Albums.Album> _albums;

	[SerializeField] private Sprite _defaultSprite;
	
	public class NewsData
	{
		public NewsType Type;
		public List<News> NewsItems;
	}

	public class News
	{
		public string Title;
		public string Body;
		public string Link;
		public Sprite Photo;
		public Sprite Thumbnail;
		public string Subtitle;
	}
	
	public class AlbumPerformances
	{
		public string AlbumName;
		public Dictionary<string, List<Performance>> Performances;
	}
	
	[Serializable]
	public class Performance
	{
		public string Title;
		public string Date;
		public Sprite Image;
		public string VideoURL;
		public bool Win;
	}

	private IEnumerator Start()
	{
		yield return GetAlbums();
		yield return GetNews();
		yield return GetAlbumPerformances();
		IsDataReady = true;
	}

	private IEnumerator GetAlbums()
	{
		_albums = new Dictionary<string, Albums.Album>();
		foreach (var album in _albumData.AlbumList)
		{
			_albums[album.Title] = album;
		}
		yield return null;
	}
	
	private IEnumerator GetNews()
	{
		var request = UnityWebRequest.Get(Application.streamingAssetsPath + "/mmmau-news.json");
		request.SendWebRequest();
		while (!request.isDone)
		{
			yield return null;
		}

		var data = request.downloadHandler.text;
		var newsData = JSON.Parse(data);
		_newsData = new Dictionary<string, List<NewsData>>();
		foreach (var newData in newsData["News"].AsArray)
		{
			foreach (var key in newData.Value.Keys)
			{
				string albumKey = key;
				var items = newData.Value[albumKey].AsArray;
				_newsData[albumKey] = new List<NewsData>();
				foreach (var item in items)
				{
					var testValues = item.Value;
					var counter = 0;
					foreach (var keyThing in testValues.Keys)
					{
						string check = keyThing;
						var newInfo = new NewsData()
						{
							Type = (NewsType) Enum.Parse(typeof(NewsType), check),
							NewsItems = new List<News>()
						};
						foreach (var listItem in testValues[check].AsArray)
						{
							var someValue = listItem.Value;
							var info = new News()
							{
								Title = someValue["Header"],
								Link = someValue["Link"],
								Body = someValue["Body"],
								Subtitle = someValue["Type"]
							};
							yield return GetImage(someValue["Photo-URL"], sprite => info.Thumbnail = sprite);
							newInfo.NewsItems.Add(info);
						}
						_newsData[albumKey].Add(newInfo);

						counter++;
					}
				}
			}
		}
	}
	
	private IEnumerator GetAlbumPerformances()
	{
		var request = UnityWebRequest.Get(Application.streamingAssetsPath + "/mmmau-video.json");
		request.SendWebRequest();
		while (!request.isDone)
		{
			yield return null;
		}

		var data = request.downloadHandler.text;
		//Debug.Log(data);
		var videoData = JSON.Parse(data);
		_albumPerformances = new Dictionary<string, AlbumPerformances>();
		foreach (var perf in videoData["Videos"].AsArray)
		{
			foreach (var key in perf.Value.Keys)
			{
				string albumKey = key;
				_albumPerformances[albumKey] = new AlbumPerformances
				{
					Performances = new Dictionary<string, List<Performance>>(), AlbumName = albumKey
				};

				var videos = perf.Value[albumKey].AsArray;
				foreach (var item in videos)
				{
					var values = item.Value;
					_albumPerformances[albumKey].Performances[values["Header"]] = new List<Performance>();
					//Debug.Log(values["Header"]);
					foreach (var val in values["Performances"].AsArray)
					{
						var value = val.Value;
						var performance = new Performance();
						performance.Title = value["title"];
						performance.Date = value["date"];
						performance.VideoURL = value["video-url"];
						yield return GetImage(value["image-url"], sprite => performance.Image = sprite);
						performance.Win = value["win"].ToString().ToLower() == "true";
						_albumPerformances[albumKey].Performances[values["Header"]].Add(performance);
					}
				}
			}
		}
	}

	private IEnumerator GetImage(string url, Action<Sprite> onComplete)
	{
		var sprite = _defaultSprite;
#if !UNITY_WEBGL
		request = UnityWebRequestTexture.GetTexture(url);
		request.SetRequestHeader("Access-Control-Allow-Credentials", "true");
		request.SetRequestHeader("Access-Control-Allow-Headers",
			"Accept, Content-Type, X-Access-Token, X-Application-Name, X-Request-Sent-Time");
		request.SetRequestHeader("Access-Control-Allow-Methods", "GET, POST, PUT, OPTIONS");
		request.SetRequestHeader("Access-Control-Allow-Origin", "*");
		yield return request.SendWebRequest();
			
		var texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
		sprite = Extensions.Texture2DToSprite(texture);
#endif
		
		onComplete(sprite);
		yield return null;
	}

}
