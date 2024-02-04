using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSelectionManager : MonoBehaviour
{
    public static ColorSelectionManager Instance;

    private Color selectedColor;

    private void Awake()
    {
        Instance = this;
    }

    public void SetSelectedColor(Color color)
    {
        selectedColor = color;
    }

    public Color GetSelectedColor()
    {
        return selectedColor;
    }

}
