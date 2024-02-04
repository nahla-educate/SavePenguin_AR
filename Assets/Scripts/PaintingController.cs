using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingController : MonoBehaviour
{
    public Camera mainCamera;
    public float brushSize = 50f;
    //public Color brushColor;
    
    public ColorPaletteController colorPaletteController;

    private Texture2D brushTexture;
    private Texture2D canvasTexture;
    private RaycastHit hitInfo;

    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        canvasTexture = new Texture2D(1024, 1024);
        rend.material.mainTexture = canvasTexture;
        Color[] clearColor = new Color[canvasTexture.width * canvasTexture.height];
        for (int i = 0; i < clearColor.Length; i++)
        {
            clearColor[i] = Color.white;
        }
        canvasTexture.SetPixels(clearColor);
        canvasTexture.Apply();

        brushTexture = CreateBrushTexture((int)brushSize, colorPaletteController.SelectedColor);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hitInfo))
            {
                if (hitInfo.textureCoord2 != Vector2.zero)
                {
                    Paint(hitInfo.textureCoord2);
                }
               /* else
                {
                    Debug.LogError("UV coordinates are not available.");
                }*/
            }
        }
    }
    public void UpdateBrushSize(float newSize)
    {
        brushSize = newSize;

    }
    Texture2D CreateBrushTexture(int size, Color color)
    {
        Texture2D tex = new Texture2D(size, size);
        Color[] colors = new Color[size * size];

        float center = size / 2f;
        float radius = size / 2f;

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                float distance = Vector2.Distance(new Vector2(i, j), new Vector2(center, center));
                float alpha = Mathf.Clamp01(1 - distance / radius);
                colors[i * size + j] = color * new Color(1, 1, 1, alpha);
            }
        }

        tex.SetPixels(colors);
        tex.Apply();
        return tex;
    }

    void Paint(Vector2 uv)
    {
        if (canvasTexture == null)
        {
            Debug.LogError("Canvas texture is not assigned.");
            return;
        }

        int width = canvasTexture.width;
        int height = canvasTexture.height;

        int x = (int)(uv.x * width);
        int y = (int)(uv.y * height);

        int brushWidth = brushTexture.width;
        int brushHeight = brushTexture.height;

        int textureX, textureY;
        for (int i = 0; i < brushWidth; i++)
        {
            for (int j = 0; j < brushHeight; j++)
            {
                textureX = x + i - brushWidth / 2;
                textureY = y + j - brushHeight / 2;

                if (textureX >= 0 && textureX < width && textureY >= 0 && textureY < height)
                {
                    Color canvasColor = canvasTexture.GetPixel(textureX, textureY);
                    Color brushColored = colorPaletteController.SelectedColor;
                    float dist = Vector2.Distance(new Vector2(i, j), new Vector2(brushWidth / 2, brushHeight / 2));
                    float strength = 1 - Mathf.Clamp01(dist / (brushWidth / 2));
                    Color finalColor = Color.Lerp(canvasColor, brushColored, brushColored.a * strength);
                    canvasTexture.SetPixel(textureX, textureY, finalColor);
                }
            }
        }
        canvasTexture.Apply();
    }

}
