using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuBackGround : MonoBehaviour
{
    private RawImage _rawImage;
    private float _speed;

    private void OnEnable()
    {
        if (_rawImage != null)
            StartCoroutine(UseMove());
    }

    public void SetValue(float speed)
    {
        _rawImage = GetComponent<RawImage>();
        _speed = speed;

        StartCoroutine(UseMove());
    }

    private IEnumerator UseMove()
    {
        float imagePositionX = 0;

        while(true)
        {
            imagePositionX += Time.deltaTime * _speed;

            _rawImage.uvRect = new Rect(imagePositionX, 0, _rawImage.uvRect.width, _rawImage.uvRect.height);

            yield return null;
        }
    }
}