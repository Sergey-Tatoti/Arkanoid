using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerTouchTracker))]
[RequireComponent(typeof(PlayerState))]

public class Player : MonoBehaviour
{
    [Header("Компоненты")]
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerTouchTracker _playerTouchTracker;
    [SerializeField] private PlayerState _playerState;
    [Header("Скорость движения")]
    [SerializeField] private float _speedMove;

    private void OnEnable()
    {
        _playerTouchTracker.TouchedBonusScale += _playerState.OnTouchedBonusScale;
    }

    private void OnDisable()
    {
        _playerTouchTracker.TouchedBonusScale -= _playerState.OnTouchedBonusScale;
    }

    public void SetValue()
    {
        _playerMovement.SetValue();
        _playerState.SetValue();
    }

    public void MovePlatform() => _playerMovement.Move(_speedMove);
}