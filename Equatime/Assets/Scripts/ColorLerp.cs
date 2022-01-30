using UnityEngine;
using UnityEngine.UI;

public class ColorLerp : MonoBehaviour
{
    private Image _image;
    private float _hue;
    [Range(0,1)] [SerializeField]
    private float saturation = 1f;
    [Range(0,1)] [SerializeField]
    private float brightness = 0.5f;
    [Range(0,1)] [SerializeField]
    private float speed = 0.5f;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    void Update()
    {
        _hue = (Time.time * speed % 10) / 10;
        _image.color = Color.HSVToRGB(_hue, saturation, brightness);
    }
}