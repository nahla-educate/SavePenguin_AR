using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnRotate : MonoBehaviour
{
    public void rotatePenguin()
    {
        transform.Rotate(0f, 3f, 0f);
    }
}
