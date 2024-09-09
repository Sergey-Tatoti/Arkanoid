using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]

public class Ball : MonoBehaviour
{
    [SerializeField] private float _force;

    private int _powerHit = 1;
    private bool _isActivate;
    private Rigidbody2D _rigidBody2D;

    public event UnityAction TouchedObject;
    public event UnityAction TouchedGameDestroyer;

    public void SetPosition(Player player, float offsetY)
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _rigidBody2D.bodyType = RigidbodyType2D.Kinematic;

        transform.SetParent(player.transform);
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + offsetY, 0);
    }

    public void TryActivate() { if (!_isActivate) { Activate(); } }

    private void Activate()
    {
        _isActivate = true;
        transform.SetParent(null);
        _rigidBody2D.bodyType = RigidbodyType2D.Dynamic;
        _rigidBody2D.AddForce(new Vector2(0, -_force));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Block>(out Block block))
            block.TryChangeType(_powerHit);

        if (collision.gameObject.TryGetComponent<Player>(out Player player))
            ChangeDirection(collision, player);

        if (collision.gameObject.GetComponent<GameDestroyer>())
        {
            _isActivate = false;
            TouchedGameDestroyer?.Invoke();
        }
        else
        {
            TouchedObject?.Invoke();
        }
    }

    private void ChangeDirection(Collision2D collision, Player player)
    {
        Vector3 hitPoint = collision.contacts[0].point;
        Vector3 paddleCenter = new Vector3(player.transform.position.x, player.transform.position.y);
        float difference = paddleCenter.x - hitPoint.x;

        _rigidBody2D.velocity = Vector2.zero;

        if (hitPoint.x < paddleCenter.x)
            _rigidBody2D.AddForce(new Vector2(-Mathf.Abs(difference * _force), -_force));
        else
            _rigidBody2D.AddForce(new Vector2(Mathf.Abs(difference * _force), -_force));
    }
}