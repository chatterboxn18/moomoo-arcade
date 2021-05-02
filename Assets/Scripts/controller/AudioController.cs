using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    private AudioSource _audioSource;

    public AudioSource Source => _audioSource;
    
    private bool isUp = false;

    private float _volume;
    private float _time = 0.3f;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void FadeAudio(bool isUp, float time)
    {
        StartCoroutine(FadeIn(isUp, time));
    }

    private IEnumerator FadeIn(bool on, float time)
    {
        var timer = 0f;
        if (on)
        {
            _audioSource.Play();
            while (timer < _time)
            {
                _audioSource.volume = Mathf.Lerp(0,1, timer/_time);
                timer += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            while (timer < _time)
            {
                _audioSource.volume = Mathf.Lerp(1,0, timer/_time);
                timer += Time.deltaTime;
                yield return null;
            }
            _audioSource.Stop();
        }
    }
}
