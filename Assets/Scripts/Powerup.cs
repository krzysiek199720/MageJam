using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Powerup
{
    [SerializeField]
    public PowerupType type;

    [SerializeField]
    public int rarity = 1; // Higher is more likely

    [SerializeField]
    public float duration;

    // used to apply the Powerup of the Powerup
    [SerializeField]
    public UnityEvent startAction;

    // used to remove the Powerup of the Powerup
    [SerializeField]
    public UnityEvent endAction;

    public void Start()
    {
        if (startAction != null)
            startAction.Invoke();
    }

    public void End()
    {
        if (endAction != null)
            endAction.Invoke();
    }
}
