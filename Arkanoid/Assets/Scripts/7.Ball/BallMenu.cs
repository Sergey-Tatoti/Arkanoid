using DG.Tweening;
using UnityEngine;

public class BallMenu : MonoBehaviour
{
    public void StartRotate(float duration, float offsetRotationZ)
    {
        Vector3 startRotation = transform.localEulerAngles;
        Sequence quence = DOTween.Sequence();

        quence.Append(transform.DOLocalRotate(new Vector3(startRotation.x, startRotation.y, startRotation.z + offsetRotationZ), duration));
        quence.Append(transform.DOLocalRotate(new Vector3(startRotation.x, startRotation.y, startRotation.z - offsetRotationZ), duration *2));
        quence.Append(transform.DOLocalRotate(startRotation, duration));

        quence.SetLoops(-1);
    }
}