using UnityEngine;


public class ARColoring : MonoBehaviour
{
    public GameObject catModel; // Your 3D cat model
    public Material catMaterial; // Material of the cat model
    public Color[] colors; // Array of colors in your palette
    private int selectedColorIndex = 0;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                PaintAtTouchPoint(touch.position);
            }
        }
    }

    void PaintAtTouchPoint(Vector2 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            MeshRenderer renderer = hit.transform.GetComponent<MeshRenderer>();

            if (renderer != null)
            {
                Material material = Instantiate(catMaterial); // Create a copy of the cat's material
                material.color = colors[selectedColorIndex]; // Set the selected color

                Texture2D tex = material.mainTexture as Texture2D;
                Vector2 uv = hit.textureCoord;
                tex.SetPixel((int)(uv.x * tex.width), (int)(uv.y * tex.height), colors[selectedColorIndex]);
                tex.Apply();

                renderer.material = material; // Apply the modified material to the model
            }
        }
    }

    public void SelectColor(int colorIndex)
    {
        selectedColorIndex = colorIndex;
    }
}
