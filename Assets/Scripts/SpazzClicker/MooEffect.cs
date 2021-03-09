using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MooEffect : MonoBehaviour
{
    private float _timeToDie = 0.75f;
    private float _timer = 0;

    [SerializeField] private Text _text;
    [SerializeField] private RectTransform _transform;

    private bool _isActive;
    public RectTransform RectTransform => _transform;

    public void Setup(string number)
    {
        _text.text = number;
        _isActive = true;
    }
    
    void Update()
    {
        if (_isActive)
        {
            if (_timer > _timeToDie)
            {
                Destroy(gameObject);
            }
            _transform.anchoredPosition = _transform.anchoredPosition.AddY(1);
            _timer += Time.deltaTime;
        }
    }
}
