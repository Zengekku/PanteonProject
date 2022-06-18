using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DrawSystem : MonoBehaviour
{
    [SerializeField] Texture2D targetTexture;
    [SerializeField] Texture2D dirtyTexture;
    [SerializeField] Material baseMaterial;
    [SerializeField] AnimationCurve curve;
    [SerializeField] int brushRadius = 25;

    Texture2D fakeTexture;
    int maxPaintedCount = 260000;
    [SerializeField] int paintedCount;
    [SerializeField] TextMeshPro paintedPercantage;
    Vector2 previousMousePos;
    void Start()
    {
        InitFakeTexture();
    }
    void InitFakeTexture()
    {
        fakeTexture = new Texture2D(512, 512, TextureFormat.RGBA32, true);
        Color[] colors = dirtyTexture.GetPixels(0, 0, 512, 512);

        fakeTexture.SetPixels(0, 0, 512, 512, colors);
        fakeTexture.Apply();
        baseMaterial.mainTexture = fakeTexture;
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (CheckIfPaintable(ref hit)) return;
                int x, y;
                hit = GetMousePositionOnWall(hit, out x, out y);
                Color colors2;

                for (int i = -brushRadius; i < brushRadius; i++)
                {
                    for (int j = -brushRadius; j < brushRadius; j++)
                    {
                        if (CheckIfInBoundsOfImage(x, y, i, j)) continue;
                        colors2 = targetTexture.GetPixel(x + i, y + j);
                        var preColor = fakeTexture.GetPixel(x + i, y + j);
                        if (AlreadyPainted(colors2, preColor)) continue;
                        fakeTexture.SetPixel(x + i, y + j, colors2);
                        paintedCount++;
                    }
                }
                fakeTexture.Apply();
                baseMaterial.mainTexture = fakeTexture;
                paintedPercantage.text = "%" + Mathf.RoundToInt((float)paintedCount / maxPaintedCount * 100).ToString();
            }
            previousMousePos = hit.point;
        }
    }
    bool AlreadyPainted(Color colors2, Color preColor) => colors2 == preColor;
    bool CheckIfInBoundsOfImage(int x, int y, int i, int j) => x + i <= -512 || y + j <= -512 || x + i >= 0 || y + j >= 0;
    RaycastHit GetMousePositionOnWall(RaycastHit hit, out int x, out int y)
    {
        var pos = hit.point - hit.transform.position;
        x = -Mathf.FloorToInt(curve.Evaluate(pos.x));
        y = -Mathf.FloorToInt(curve.Evaluate(pos.y));
        return hit;
    }
    bool CheckIfPaintable(ref RaycastHit hit) => Vector2.Distance(hit.point, previousMousePos) < 0.25f || !hit.transform.CompareTag("Wall");
}
