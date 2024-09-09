using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bonus", menuName = "Create/Bonus", order = 51)]

public class Bonus : ScriptableObject
{
    public enum TypeBonus { Scale }

    [SerializeField] private Sprite _spriteBonus;
    [SerializeField] private TypeBonus _typeBonus;
    [SerializeField] private float _offsetScaleX;
    [SerializeField] private float _timeChangeScale;
    
    public TypeBonus Type => _typeBonus;
    public Sprite Sprite => _spriteBonus;
    public float OffsetScaleX => _offsetScaleX;
    public float TimeChangeScale => _timeChangeScale;
}
