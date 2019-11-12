using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    GM gm; // GM 스크립트를 이용하기 위한 GM형 변수
    Vector3 currentPosition; // 플레이어의 현재 좌표
    [SerializeField]
    private float playerSpeed; // 플레이어 스피드

    [SerializeField]
    private Text scoreText; // score text UI
    private float score; // score 점수

    [SerializeField]
    private Image hp;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GM>();
        score = 0;
        hp.fillAmount = 1;
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
            hp.fillAmount -= 0.05f;
            Debug.Log("접촉");

            if (hp.fillAmount <= 0)
            {
                gm.isGameOver = true;
                gm.gameOverText.SetActive(true);
            }
        }

        if (collision.gameObject.tag == "Ruby")
        {
            score++;
            scoreText.text = " : " + score;

            for (int i = 0; i < gm.lightning_list.Count; i++)
            {
                Destroy(gm.lightning_list[i]);
            }

            gm.lightning_list.Clear();
            Destroy(collision.gameObject);
        }

    }

}
