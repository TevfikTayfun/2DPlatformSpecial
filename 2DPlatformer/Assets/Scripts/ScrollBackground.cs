using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    MeshRenderer _mr;
    Material _mat;

    // Start is called before the first frame update
    void Start()
    {
        _mr = GetComponent<MeshRenderer>();
        _mat = _mr.material;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = _mat.mainTextureOffset;
        offset.x += Time.deltaTime / 10;
        _mat.mainTextureOffset = offset;
    }
}
