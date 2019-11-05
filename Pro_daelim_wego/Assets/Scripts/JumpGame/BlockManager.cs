using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField]
    private float maxHeight; // Ground 최대 높이
    [SerializeField]
    private float minHeight; // Ground 최소 높이
    [SerializeField]
    private float minWidth; // Ground 랜덤 최소 x 좌표
    [SerializeField]
    private float maxWidth; // Ground 랜덤 최대 x 좌표

    // Start is called before the first frame update
    void Start()
    {
        ChangeLocation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangeLocation()
    {
        float randomWidth = Random.Range(minWidth, maxWidth);
        float height = Random.Range(minHeight, maxHeight);
        transform.localPosition = new Vector3(randomWidth, height, 0.0f);
    }
}
