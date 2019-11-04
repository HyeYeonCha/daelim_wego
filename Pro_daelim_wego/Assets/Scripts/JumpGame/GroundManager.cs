using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    Vector3 pos; //현재위치

    [SerializeField]
    private float x = 2.0f; // 좌우로 이동가능한 x좌표 최대값
    [SerializeField]
    private float speed = 3.0f; // 이동속도

    private bool isGround;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        isGround = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 groundPos = pos;

        if (isGround)
        {
            groundPos.x = pos.x;
        }
        else
        {
            groundPos.x += x * Mathf.Sin(Time.time * speed);

            transform.position = groundPos;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;
    }

}
