using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonDrag : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private float _scale = 0.1f;
    private float _timeScale = 0.5f;
    private Vector2 _baseScale;
    private Vector2 _changedScale;

    private void Awake()
    {
        _baseScale = transform.localScale;
        _changedScale = new Vector3(_baseScale.x + _scale, _baseScale.y + _scale);
    }

    public void OnPointerEnter(PointerEventData eventData) => transform.DOScale(_changedScale, _timeScale);

    public void OnPointerExit(PointerEventData eventData) => transform.DOScale(_baseScale, _timeScale);

    public void OnPointerClick(PointerEventData eventData) => transform.DOScale(_baseScale, _timeScale);
}