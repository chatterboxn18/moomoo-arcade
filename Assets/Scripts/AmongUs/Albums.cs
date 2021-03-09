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
		[TextArea] public List<string> TrackList;
		
	}
	
}
