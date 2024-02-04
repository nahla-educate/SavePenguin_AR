using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSize : MonoBehaviour
{
    public PaintingController paintingController; 
    public Slider brushSizeSlider;

    private void Start()
    {
        brushSizeSlider.onValueChanged.AddListener(UpdateBrushSize);
    }

    public void UpdateBrushSize(float newSize)
    {
        paintingController.UpdateBrushSize(newSize);
    }
}
