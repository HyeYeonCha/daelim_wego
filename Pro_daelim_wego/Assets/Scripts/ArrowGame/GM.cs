using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using static UnityEngine.Mathf;

public class GM : MonoBehaviour
{
    [SerializeField]
    private GameObject player; // 플레이어 캐릭터

    [SerializeField]
    private Text timeText; // 타이머 UI
    [SerializeField]
    private Text startText; // 게임시작을 알려주는 텍스트 UI

    private float time; // 타이머 text

    [SerializeField]
    private GameObject lightning; // 번개모양 장애물 
    [SerializeField]
    private GameObject lightningClones; // 장애물들을 하이어라키뷰에서 정리해줄 부모오브젝트

    public bool isGameOver = true; // 게임오버 체크 플래그
    
    public GameObject gameOverText; // 게임오버 화면

 

    [SerializeField]
    private float r; // 원의 반지름
    private float degree; // Field의 원주에서 랜덤하게 생성할 장애물의 각도

    [SerializeField]
    private float term; // 몇 초에 한 번씩 장애물을 생성할지 결정할 변수 (기준점)
    private float currentTime; // 현재 시간을 받아올 변수

    // Start is called before the first frame update
    void Start()
    {
        time = 60;
        currentTime = 0;

        gameOverText.SetActive(false);
        isGameOver = true;

        startText.text = "Click !!";
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            time -= Time.deltaTime;
            timeText.text = time.ToString("N0");

            degree = Random.Range(0, 360f);

            currentTime += Time.deltaTime;

            if (currentTime > term)
            {
                currentTime = 0.0f;
                GenerateLightning();
            } 
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                startText.text = "GameStart !!";
                StartCoroutine(GameStartText());
            }

        }
        if (time <= 0)
        {
            time = 60;
            gameOverText.SetActive(true);
            isGameOver = true;
        }
    }

    // 시작 텍스트 코루틴
    IEnumerator GameStartText ()
    {
        yield return new WaitForSeconds(1.5f);
        startText.enabled = false;
        isGameOver = false;
    }
    // 회전값 수정하기 >> 좀 이상함.
    // 장애물 instantiate
    private void GenerateLightning ()
    {
        GameObject go = Instantiate(lightning, new Vector3(r * Cos(degree), r * Sin(degree)), Quaternion.FromToRotation(Vector3.up, go.transform - player.transform).eulerAngles.z);
        float rad = Atan2(player.transform.position.x, player.transform.position.y);
        float rotate = rad * Rad2Deg; // Mathf.Rad2Deg = 360 / 2 * PI
        
       // go.transform.localEulerAngles = new Vector3(0, 0, (-rotate -90));
        go.transform.SetParent(lightningClones.transform);
    }


    // 리플레이 함수
    public void ReStart ()
    {
        SceneManager.LoadScene("ArrowGame");
    }

}
