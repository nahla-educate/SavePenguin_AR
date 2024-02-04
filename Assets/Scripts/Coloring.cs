using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coloring : MonoBehaviour
{ 
    public Camera arCamera;
    public GameObject catModel; // Your 3D cat model
    public LayerMask hitLayerMask;

    private Color selectedColor;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = arCamera.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, float.MaxValue, hitLayerMask))
            {
                Vector2 uv = hit.textureCoord;
                ColorTexture(catModel, uv, selectedColor);
            }
        }
    }

    private void ColorTexture(GameObject target, Vector2 uv, Color color)
    {
        Renderer renderer = target.GetComponent<Renderer>();
        Material material = renderer.material;

        Texture2D texture = (Texture2D)material.mainTexture;
        texture.SetPixel((int)(uv.x * texture.width), (int)(uv.y * texture.height), color);
        texture.Apply();
    }

}
