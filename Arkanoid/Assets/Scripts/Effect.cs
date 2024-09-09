using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private float _timeDestroy = 1;

    private void OnEnable()
    {
        _particleSystem.Play();
        Destroy(gameObject, _timeDestroy);
    }
}