using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stage : MonoBehaviour
{
    [SerializeField] private List<Block> _blocks = new List<Block>();

    public event UnityAction DestroyedBlock;

    private void OnDisable()
    {
        for (int i = 0; i < _blocks.Count; i++)
        {
            _blocks[i].Destroyed -= OnDestroyed;
        }
    }

    public void SetValue(BlockManager blockManager)
    {
        for (int i = 0; i < _blocks.Count; i++)
        {
            _blocks[i].SetValue(blockManager.EasyColor, blockManager.NormalColor, blockManager.NotDestroyColor,
                                blockManager.EffectDestroy, blockManager.GetGameBonus());

            _blocks[i].Destroyed += OnDestroyed;
        }
    }

    public int GetCountWorkBlocks()
    {
        int count = 0;

        for (int i = 0; i < _blocks.Count; i++)
        {
            if (_blocks[i].TypeBlock != Block.Type.NotDestroy)
                count++;
        }

        return count;
    }
    
    private void OnDestroyed() => DestroyedBlock?.Invoke();
}