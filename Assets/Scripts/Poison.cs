using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour
{
    public int scorewhenKilled;
    private void OnTriggerEnter(Collider other)
    {/*
        PlayerInvention playerInvention = other.GetComponent<PlayerInvention>();
        if (playerInvention != null)
        {
            playerInvention.PoissonCollected();
            gameObject.SetActive(false);
        }*/
        if(other.tag == "Penguin")
        {
            FishDetect.OnPoissonCollected?.Invoke(this);
            Debug.Log("hello");
            Destroy(gameObject);
        }
    }
}
