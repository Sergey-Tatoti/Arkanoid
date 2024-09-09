using UnityEngine;
using UnityEngine.Events;

public class PlayerTouchTracker : MonoBehaviour
{
    public event UnityAction<float, float> TouchedBonusScale;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<GameBonus>(out GameBonus gameBonus))
        {
            switch(gameBonus.Type)
            {
                case Bonus.TypeBonus.Scale:
                    TouchedBonusScale?.Invoke(gameBonus.OffsetScaleX, gameBonus.TimeChangeScale);
                    Destroy(gameBonus.gameObject);
                    break;
            }
        }
    }
}