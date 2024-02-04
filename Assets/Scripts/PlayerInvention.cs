using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInvention : MonoBehaviour
{
   public int NumberOfPoissons { get; private set; }

    public UnityEvent<PlayerInvention> OnPoissonCollected;
    public void PoissonCollected()
    {
        NumberOfPoissons++;
        OnPoissonCollected.Invoke(this);
    }
}
