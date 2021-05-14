using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LWShopController : LWBaseController
{
    [SerializeField] private Transform _shopContainer;
    [SerializeField] private LWShopItem _shopItem;
    [SerializeField] private TextMeshProUGUI _coinText;

    private new IEnumerator Start()
    {
        while (!LWResourceManager.IsLoaded)
            yield return null;
        var flowers = LWResourceManager.Flowers;
        var sprites = LWResourceManager.Sprites;
        foreach (var flower in flowers)
        {
            var item = Instantiate(_shopItem, _shopContainer);
            //_shopItem.MainImage.sprite = sprites[key][0];
            item.SetItem(sprites[flower.Index][0], flower);
            item.Evt_BoughtFlower += (text) => { _coinText.text = text; };
        }
        
    }
}
