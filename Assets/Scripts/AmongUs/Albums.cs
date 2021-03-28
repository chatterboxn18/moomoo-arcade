using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Albums", menuName = "MMMAU/Albums")]
public class Albums : ScriptableObject
{
	public List<Album> AlbumList;
	
	[Serializable]
	public struct Album
	{
		public string Title;
		public Color OutlineColor;
		public Color AlbumColor; 
		public List<Track> TrackList;
		
	}
	
	[Serializable]
	public struct Track
	{
		[TextArea] public string Title;
		[TextArea] public string Subtitle;
		[TextArea] public string Description;
		public string Time;
		public List<Link> Links;
	}

	[Serializable]
	public struct Link
	{
		public string URL;
		public Sprite Icon;
	}
	
}
