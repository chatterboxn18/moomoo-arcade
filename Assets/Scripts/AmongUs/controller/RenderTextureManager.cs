using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class RenderTextureManager : MonoBehaviour
{
    [SerializeField] private RawImage _image;
    [SerializeField] private Camera _camera;

    private void Awake()
    {
        var rt = new RenderTexture(_camera.pixelWidth, _camera.pixelHeight, 24, GraphicsFormat.R32G32B32A32_SFloat);
        rt.Create();
        _camera.targetTexture = rt;
        _image.texture = rt;
    }
}
