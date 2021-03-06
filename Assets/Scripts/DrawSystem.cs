using TMPro;
using UnityEngine;

public class DrawSystem : MonoBehaviour
{
    [SerializeField] Texture2D targetTexture;
    [SerializeField] Texture2D dirtyTexture;
    [SerializeField] Material baseMaterial;
    [SerializeField] AnimationCurve curve;
    [SerializeField] int brushRadius = 25;
    [SerializeField] ParticleSystem[] particleEffects;

    Texture2D fakeTexture;
    int maxPaintedCount = 262144;
    [SerializeField] int paintedCount;
    [SerializeField] TextMeshPro paintedPercantage;
    [SerializeField] GameObject restartButton;

    Vector2 previousMousePos;

    void Start()
    {
        InitFakeTexture();
    }
    void InitFakeTexture()
    {
        fakeTexture = new Texture2D(512, 512, TextureFormat.RGBA32, true);
        //Color[] colors = dirtyTexture.GetPixels(0, 0, 512, 512);
        Color[] colors = new Color[262144];
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
                Color color;

                for (int i = -brushRadius; i < brushRadius; i++)
                {
                    for (int j = -brushRadius; j < brushRadius; j++)
                    {
                        if (CheckIfInBoundsOfImage(x, y, i, j)) continue;
                        color = Color.red;//targetTexture.GetPixel(x + i, y + j)
                        var currentColor = fakeTexture.GetPixel(x + i, y + j);

                        if (AlreadyPainted(color, currentColor)) continue;
                        fakeTexture.SetPixel(x + i, y + j, color);
                        paintedCount++;
                    }
                }
                fakeTexture.Apply();

                var percantage = Mathf.RoundToInt((float)paintedCount / maxPaintedCount * 100);

                if (percantage == 100)
                    GameOver();

                else
                    baseMaterial.mainTexture = fakeTexture;
                paintedPercantage.text = "%" + percantage.ToString();
            }
            previousMousePos = hit.point;
        }
    }

    void GameOver()
    {
        baseMaterial.mainTexture = targetTexture;
        paintedPercantage.color = Color.black;
        foreach (var effect in particleEffects)
        {
            effect.Play();
        }
        restartButton.SetActive(true);
        enabled = false;
    }

    bool AlreadyPainted(Color colors2, Color preColor) => colors2 == preColor;
    bool CheckIfInBoundsOfImage(int x, int y, int i, int j) => x + i >= 512 || y + j >= 512 || x + i <= 0 || y + j <= 0;
    RaycastHit GetMousePositionOnWall(RaycastHit hit, out int x, out int y)
    {
        var pos = hit.point - hit.transform.position;
        x = Mathf.FloorToInt(curve.Evaluate(pos.x));
        y = Mathf.FloorToInt(curve.Evaluate(pos.y));
        return hit;
    }
    bool CheckIfPaintable(ref RaycastHit hit) => Vector2.Distance(hit.point, previousMousePos) < 0.25f || !hit.transform.CompareTag("Wall");

    private void OnDestroy()
    {
        //baseMaterial.mainTexture = dirtyTexture;
    }
}
