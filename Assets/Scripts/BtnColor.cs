using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnColor : MonoBehaviour
{
    public Color selectedColor;

    public void OnClick()
    {
        // When this button is clicked, set the selected color for painting.
        ColorSelectionManager.Instance.SetSelectedColor(selectedColor);
    }

}
