using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 _screenBounds;
    private bool _canMove;

    public void SetValue()
    {
        _canMove = true;
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
    }

    public void Move(float speed)
    {
        float moveInput = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(moveInput * speed * Time.deltaTime, 0, 0);

        transform.position += movement;

        SetBoundaries();
    }

    private void SetBoundaries()
    {
        float halfWidth = transform.GetComponent<Renderer>().bounds.size.x / 2;

        if (transform.position.x > _screenBounds.x - halfWidth)
            transform.position = new Vector2(_screenBounds.x - halfWidth, transform.position.y);

        if (transform.position.x < -_screenBounds.x + halfWidth)
            transform.position = new Vector2(-_screenBounds.x + halfWidth, transform.position.y);
    }
}