using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GM gm;
    Vector3 currentPosition;
    [SerializeField]
    private float playerSpeed; // 플레이어 스피드

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GM>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gm.isGameOver)
        {
            PlayerController();
            currentPosition = transform.position;
        }
        
    }

    // 플레이어 이동 함수 및 이동 제한 범위 설정
    private void PlayerController()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 nextPosition = currentPosition + new Vector3(horizontal * playerSpeed * Time.deltaTime, vertical * playerSpeed * Time.deltaTime);

        float distance = Vector3.Distance(new Vector3(0, 0, 0), nextPosition);

        if (distance < 3.5)
        {
            transform.position += new Vector3(horizontal * playerSpeed * Time.deltaTime, vertical * playerSpeed * Time.deltaTime);
        }

    }

    // 플레이어 충돌체크
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Lightning")
        {
            gm.isGameOver = true;
            gm.gameOverText.SetActive(true);
        }
    }

}
