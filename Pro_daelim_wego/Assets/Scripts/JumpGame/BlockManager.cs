using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField]
    private float maxHeight; // 장애물 최대 높이
    [SerializeField]
    private float minHeight; // 장애물 최소 높이

    // Start is called before the first frame update
    void Start()
    {
        ChangeHeight();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangeHeight()
    {
        float height = Random.Range(minHeight, maxHeight);
        transform.localPosition = new Vector3(0.0f, height, 0.0f);
    }

    void OnScrollEnd()
    {
        ChangeHeight();
        Debug.Log("Send massage");
    }

}
