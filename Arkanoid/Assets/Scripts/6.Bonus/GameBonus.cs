using System.Collections;
using UnityEngine;

public class GameBonus : MonoBehaviour
{
    [SerializeField] private Bonus _bonus;
    [SerializeField] private SpriteRenderer _imageBonus;
    [SerializeField] private float _speedMove;

    public Bonus.TypeBonus Type => _bonus.Type;
    public float OffsetScaleX => _bonus.OffsetScaleX;
    public float TimeChangeScale => _bonus.TimeChangeScale;

    private void OnEnable()
    {
        _imageBonus.sprite = _bonus.Sprite;
        StartCoroutine(MoveDown());
    }

    private IEnumerator MoveDown()
    {
        while (true)
        {
            transform.Translate(Vector3.down * _speedMove * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<GameDestroyer>())
            Destroy(gameObject);
    }
}
