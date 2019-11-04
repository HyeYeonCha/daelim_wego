using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText; // 게임점수 반환
    private int score = 0; // 게임점수

    [SerializeField]
    private Text timeText; // 제한시간 반환
    private float time = 60.0f; // 제한시간

    private bool gameOver; // 게임오버(타임오버) 체크

    private int randomindex; // 매 라운드 레시피의 종류를 랜덤 생성할 인덱스
    private int randomLevel; // 매 라운드 레시피 개수 랜덤 생성할 인덱스

    [SerializeField]
    private List<GameObject> PlayerBuger = new List<GameObject>(); // 플레이어가 입력한 버거 재료들 저장 
    [SerializeField]
    private List<GameObject> CompleteBuger = new List<GameObject>(); // 완성된 버거 저장
    [SerializeField]
    private GameObject[] PBugerIng; // 버거 재료들의 부모객체

    private float x, y = 0; // 커서의 좌표

    private bool scoreflag; // 스코어 플래그 생성

    [SerializeField]
    private GameObject[,] BugerIngredients = new GameObject[4,4]; // 버거 재료들의 고정 배열
    
    [SerializeField]
    private GameObject CompleteBugerClones; // 하이어라키뷰에 완성된 버거를 정리하기위한 부모오브젝트
    [SerializeField]
    private GameObject PlayerBugerClones; // 하이어라키뷰에 생성된 버거를 정리하기 위한 부모오브젝트

    [SerializeField]
    private GameObject Cursor;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        scoreflag = false;

        SetBuger();
        BugerIngredients.Initialize();
        for (int i = 1; i < 4; i++)
        {
            for (int j = 1; j < 4; j++)
            {
                BugerIngredients[i, j] = PBugerIng[ (i-1)*3 + j-1];
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            PlayerCursor();
            time -= Time.deltaTime;

            //timeText.text = "" + Mathf.Round(time);
            timeText.text = time.ToString("N0");
        }

        if (time <= 0)
        {
            gameOver = true;
            time = 60;
        }
    }

    // 플레이어가 생성한 버거 재료배열과 완성되어있던 버거 재료배열 비교후 점수 반환
    public void ScorePlus()
    {
        for (int i = 0; i < CompleteBuger.Count; i++)
        {
            if (CompleteBuger[i].name == PlayerBuger[i].name)
            {
                scoreflag = true;
            }
            else break;
        }

        if (scoreflag)
        {
            score += 100;
            scoreText.text = "Score : " + score;
            DestroyBuger();
        }
        else
        {
            scoreflag = false;
            score -= 100;
            scoreText.text = "Score : " + score;
            DestroyBuger();
        }

    }

    // 매 라운드 랜덤한 버거 레시피 생성
    public void SetBuger()
    {
        randomLevel = Random.Range(3, 6);

        CompleteBuger.Add((Instantiate(PBugerIng[0], new Vector3(4, 3, 0), Quaternion.identity) as GameObject));
        CompleteBuger[0].transform.SetParent(CompleteBugerClones.transform);

        for (int i = 0; i < randomLevel + 1; i++)
        {
            randomindex = Random.Range(2, 8);
            CompleteBuger.Add((Instantiate(PBugerIng[randomindex], new Vector3(4, 5, 0+ (i * -0.1f)), Quaternion.identity) as GameObject));
            CompleteBuger[i].transform.SetParent(CompleteBugerClones.transform);
        }
        CompleteBuger.Last().transform.SetParent(CompleteBugerClones.transform);
        StartCoroutine(LastBugerIngredient());
    }

    // 마지막 뚜껑 재료 닫기 코루틴
    IEnumerator LastBugerIngredient ()
    {
        yield return new WaitForSeconds(1.2f);
        CompleteBuger.Add((Instantiate(PBugerIng[1], new Vector3(4, 6, -2), Quaternion.identity) as GameObject));
        CompleteBuger.Last().transform.SetParent(CompleteBugerClones.transform);
    }

    // 매 라운드가 끝날때마다 생성된 Arrow GameObject Clones을 없애줌.
    public void DestroyBuger()
    {
        for (int i = 0; i < PlayerBuger.Count; i++)
        {
            Destroy(PlayerBuger[i]);
        }

        for (int i = 0; i < CompleteBuger.Count; i++)
        {
            Destroy(CompleteBuger[i]);
        }

        CompleteBuger.Clear();
        PlayerBuger.Clear();
        scoreflag = false;
        SetBuger();
    }

    // 플레이어의 입력값 반환하여 ArrayList에 저장
    public void PlayerCursor()
    {
        x = Cursor.transform.position.x;
        y = Cursor.transform.position.y;

        // 이동 범위 제한
        if(Cursor.transform.position.x > 6)
        {
            Cursor.transform.position = new Vector3(6, y);
        }
        if (Cursor.transform.position.x < 2)
        {
            Cursor.transform.position = new Vector3(2, y);
        }
        if (Cursor.transform.position.y > -1)
        {
            Cursor.transform.position = new Vector3(x, -1f);
        }
        if (Cursor.transform.position.y < -2)
        {
            Cursor.transform.position = new Vector3(x, -3f);
        }

        // 플레이어 커서 이동
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Cursor.transform.position = new Vector3(x - 2, y);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Cursor.transform.position = new Vector3(x + 2, y);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Cursor.transform.position = new Vector3(x, y + 1f);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Cursor.transform.position = new Vector3(x, y - 1f);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StackBuger(x, y);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ScorePlus();
        }
    }

    // 플레이어 커서의 x,y 좌표를 받아 해당 위치에 일치하는 버거 재료들 생성
    private void StackBuger(float _x, float _y)
    {
        int x = (int)Mathf.Round(_x);
        int y = (int)Mathf.Round(_y);

        PlayerBuger.Add(Instantiate(BugerIngredients[(x / 2), y * (-1)], new Vector3(-4, 4, 0), Quaternion.identity) as GameObject);
        PlayerBuger.Last().transform.SetParent(PlayerBugerClones.transform);

        // 버거의 최대개수 초과시 점수 감소후 새로운 레시피 불러오기
        if(PlayerBuger.Count > 11)
        {
            score -= 200;
            scoreText.text = "Score : " + score;
            DestroyBuger();
        }

    }
}
