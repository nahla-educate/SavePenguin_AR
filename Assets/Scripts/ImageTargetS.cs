using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageTargetS : MonoBehaviour
{
    public GameObject cat;

    public void ActiveObj()
    {
        cat.SetActive(true);
    }

    public void DesactiveObj()
    {
        cat.SetActive(false);
    }
}
