using UnityEngine;
using UnityEngine.Events;

public class Block : MonoBehaviour
{
    public enum Type { Easy, Normal, NotDestroy }

    [SerializeField] private Type _type;

    private Color _easyColor;
    private Color _normalColor;
    private Color _notDestroyColor;
    private Effect _effectDestroy;
    private GameBonus _bonus;

    public Type TypeBlock => _type;

    public event UnityAction Destroyed;

    public void SetValue(Color easy, Color normal, Color notDestroy, Effect effectDestroy, GameBonus bonus)
    {
        _easyColor = easy;
        _normalColor = normal;
        _notDestroyColor = notDestroy;
        _effectDestroy = effectDestroy;
        _bonus = bonus;

        SetColor();
    }

    public void TryChangeType(int count)
    {
        int numberType = (int)_type - count;

        if (numberType >= 0 && _type != Type.NotDestroy)
            ChangeType(numberType);
        else if(_type != Type.NotDestroy)
            DestroyBlock();
    }

    private void ChangeType(int numberType)
    {
        _type = (Type)numberType;

        SetColor();
    }

    private void DestroyBlock()
    {
        Instantiate(_effectDestroy, transform.position, Quaternion.identity);
        TryCreateBonus();
        Destroy(gameObject);

        Destroyed?.Invoke();
    }

    private void TryCreateBonus()
    {
        if (_bonus != null)
            Instantiate(_bonus, transform.position, Quaternion.identity);
    }

    private void SetColor()
    {
        Color color = Color.white;

        switch (_type)
        {
            case Type.Easy:
                color = _easyColor;
                break;
            case Type.Normal:
                color = _normalColor;
                break;
            case Type.NotDestroy:
                color = _notDestroyColor;
                break;
        }

        GetComponent<SpriteRenderer>().color = color;
    }
}