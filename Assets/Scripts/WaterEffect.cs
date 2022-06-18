using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEffect : MonoBehaviour
{
    [SerializeField] Material waterMaterial;
    [SerializeField] float speed;

    void Update() => waterMaterial.mainTextureOffset += speed * Time.deltaTime * Vector2.right;
    private void OnDestroy() => waterMaterial.mainTextureOffset = Vector2.zero;
}
