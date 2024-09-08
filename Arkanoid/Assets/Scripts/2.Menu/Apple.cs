using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class Apple : MonoBehaviour, IPointerDownHandler
{
    private int _timeReload;
    private Image _image;

    public event UnityAction ClickedApple;

    public void SetValue(int timeReload)
    {
        _image = GetComponent<Image>();
        _timeReload = timeReload;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ShowImage(false);
        Invoke(nameof(UseCountDownFood), _timeReload);

        ClickedApple?.Invoke();
    }

    private void UseCountDownFood() => ShowImage(true);

    private void ShowImage(bool isShow) => _image.enabled = isShow;
}