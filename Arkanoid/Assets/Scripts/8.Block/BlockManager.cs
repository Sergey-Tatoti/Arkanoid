using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] private Color _easyColor;
    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _notDestroyColor;
    [SerializeField] private Effect _effectDestroy;
    [SerializeField] private List<GameBonus> _gameBonuses;
    [SerializeField] private int _chanceUseBonus;
    [SerializeField] private int _maxChanceUseBonus;

    public Color EasyColor => _easyColor;
    public Color NormalColor => _normalColor;
    public Color NotDestroyColor => _notDestroyColor;
    public Effect EffectDestroy => _effectDestroy;

    public GameBonus GetGameBonus()
    {
        if (Random.Range(0, _maxChanceUseBonus) <= _chanceUseBonus)
            return _gameBonuses[Random.Range(0, _gameBonuses.Count - 1)];
        else
            return null;
    }
}