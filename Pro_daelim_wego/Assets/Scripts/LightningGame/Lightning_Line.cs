using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning_Line : MonoBehaviour
{
    private float randomSpeed; // 배경 이동속도 (랜덤)

    [SerializeField]
    private float staretPos; // 배경 시작지점
    [SerializeField]
    private float endPos; // 배경 끝지점

    // Start is called before the first frame update
    void Start()
    {
        randomSpeed = Random.Range(2.0f, 6.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -1 * randomSpeed * Time.deltaTime, 0);
        if (transform.position.y <= endPos)
        {
            transform.Translate(0, -1 * (endPos - staretPos), 0);

            if (gameObject.tag == "_Lightning")
            {
                randomSpeed = Random.Range(2.0f, 6.0f);
            }
        }
    }
}
