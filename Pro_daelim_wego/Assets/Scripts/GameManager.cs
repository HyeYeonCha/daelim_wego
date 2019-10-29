using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    private int score = 0;

    [SerializeField]
    private Text timeText;
    private float time = 5.0f;

    private int randomLevel = 4;
    private int checkStart = 4;
    private bool gameOver = false;

    private ArrayList CheckBlock = new ArrayList();
    private ArrayList PlayrBlock = new ArrayList();

    [SerializeField]
    private GameObject[] ArrowBlock;
    private int randomIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            randomLevel = Random.Range(3, 6);
            randomIndex = Random.Range(0, 3);
            SetBlock();
            CheckArrow();
            time -= Time.deltaTime;

            //timeText.text = "" + Mathf.Round(time);
            timeText.text = time.ToString("N0");
        }

        if(time <= 0)
        {
            gameOver = true;
            time = 60;
        }
    }

    public void CheckArrow()
    {
        
        //if()
        //{
        //    score += 100;
        //    scoreText.text = "Score : " + score;
        //}
        //else
        //{
        //    score -= 100;
        //    scoreText.text = "Score : " + score;
        //}
    }

    // 이거 작성중 아직 오류남..
    // ArrowBlock[]에서 랜덤 인덱스의 블록을 가져온뒤, 클론으로 만들어 CheckBlock ArrayList안에 넣는 걸 하는중..
    public void SetBlock()
    {
        for (int i=0; i < randomLevel+1; i++)
        {
            GameObject cloneArrow = Instantiate(ArrowBlock[randomIndex], new Vector3(-120, 0, 0), Quaternion.identity);
            CheckBlock.Add(cloneArrow);
            randomIndex = Random.Range(0, 3);
        }
    }

}
