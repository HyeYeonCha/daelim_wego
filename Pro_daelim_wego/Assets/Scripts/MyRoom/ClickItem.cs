using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickItem : MonoBehaviour
{
    [SerializeField]
    private GameObject countMessage; // 대량 구매시 개수 선택하는 창

    [SerializeField]
    private Text Ruby; // 코인 UI
    [SerializeField]
    private int rubyCoin; // 코인 갯수
    Ray ray;
    RaycastHit2D hit;
    Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        countMessage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ItemClick();
        PurchaseItem();

        Ruby.text = " : " + rubyCoin;

        mousePos = Input.mousePosition;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
    }

    public void ItemClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
            }
        }
    }

    private void PurchaseItem()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (hit.collider != null)
            {
                // 구매 메서드
                Debug.Log(hit.collider.gameObject.name + "구매");
                rubyCoin -= hit.collider.gameObject.GetComponent<Item>().ItemInfo.itemCost;
            }
           
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (hit.collider != null)
                {
                    // 다중 구매 메서드
                    countMessage.SetActive(true);
                    Debug.Log("다중 구매");
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            countMessage.SetActive(false);
            Debug.Log("다중 구매 취소");
        }

    }

   
 
}
