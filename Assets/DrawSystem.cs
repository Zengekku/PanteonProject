using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSystem : MonoBehaviour
{
    [SerializeField] Texture2D targetTexture;
    [SerializeField] Texture2D dirtyTexture;
    [SerializeField] Material baseMaterial;
    [SerializeField] AnimationCurve curve;
    [SerializeField] AnimationCurve curveY;
    [SerializeField] MeshRenderer mesh;
    [SerializeField] int brushRadius = 25;

    Texture2D fakeTexture;
    [SerializeField] int count;
    Vector2 previousMousePos;
    void Start()
    {
        fakeTexture = new Texture2D(512, 512, TextureFormat.RGBA32, true);
        Color[] colors = dirtyTexture.GetPixels(0, 0, 512, 512);

        fakeTexture.SetPixels(0, 0, 512, 512, colors);
        fakeTexture.Apply();
        baseMaterial.mainTexture = fakeTexture;
    }
    private void Update()
    {
        //var mousePos = Input.mousePosition;

        //mousePos.z= 17.5f;

        // transform.position = Camera.main.ScreenToWorldPoint(mousePos); ;
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (Vector2.Distance(hit.point, previousMousePos) < 0.5f) return;
                var pos = hit.point - transform.position-hit.point;
                int x = -Mathf.FloorToInt(curve.Evaluate(pos.x));
                int y = -Mathf.FloorToInt(curve.Evaluate(pos.y));
                Color colors2;

                for (int i = -brushRadius; i < brushRadius; i++)
                {
                    for (int j = -brushRadius; j < brushRadius; j++)
                    {
                        if (x + i <= -512 || y + j <= -512 || x + i >= 0 || y + j >= 0) continue;
                        colors2 = targetTexture.GetPixel(x + i, y + j);
                        var preColor = fakeTexture.GetPixel(x + i, y + j);
                        if (colors2 == preColor) continue;
                        fakeTexture.SetPixel(x + i, y + j, colors2);
                        count++;
                    }
                }
                fakeTexture.Apply();
                baseMaterial.mainTexture = fakeTexture;
            }
            previousMousePos = hit.point;
        }        
    }
}
