using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LWMediaCard : MonoBehaviour
{
    [SerializeField] private Image _mediaImage;
    [SerializeField] private TextMeshProUGUI _mediaTitle;
    [SerializeField] private TextMeshProUGUI _mediaDate;
    [SerializeField] private SimpleButton _button;

    public void SetMediaCard(LWResourceManager.VideoItem item)
    {
        _mediaImage.sprite = item.Image;
        _mediaTitle.text = item.Title;
        _mediaDate.text = item.Date;
        if (_button)
        {
            _button.Evt_BasicEvent_Click += () => { Application.OpenURL(item.Url); };
        }
    }
}
