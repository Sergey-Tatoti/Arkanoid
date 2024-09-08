using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    public event UnityAction TouchedObject;
    public event UnityAction TouchedPlatform;
    public event UnityAction DestroyedBlock;
}
