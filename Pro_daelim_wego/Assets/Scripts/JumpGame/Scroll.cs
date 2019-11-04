using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f; // 배경 이동속도

    [SerializeField]
    private float staretPos; // 배경 시작지점
    [SerializeField]
    private float endPos; // 배경 끝지점
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-1 * speed * Time.deltaTime, 0, 0);
        if(transform.position.x <= endPos)
        {
            transform.Translate(-1 * (endPos - staretPos), 0, 0);
            if(gameObject.tag == "Blcok")
            {
                gameObject.SendMessage("OnScrollEnd", SendMessageOptions.RequireReceiver);
            }
        }
    }
}
