using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private Vector3 _startScale;

    public void SetValue()
    {
        _startScale = transform.localScale;
    }

    public void OnTouchedBonusScale(float offsetScaleX, float time)
    {
        transform.DOScaleX(_startScale.x + offsetScaleX, 0.1f);

        StartCoroutine(CountdownNormolizeScale(time));
    }

    private IEnumerator CountdownNormolizeScale(float time)
    {
        yield return new WaitForSeconds(time);

        transform.DOScaleX(_startScale.x, 0.1f);
    }
}