using UnityEngine;
using UnityEngine.EventSystems;
public class ClickItem : MonoBehaviour
{

    [SerializeField]
    private GameObject countMessage;

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
    }

    public void ItemClick()
    {
        Vector3 mousePos = Input.mousePosition;


        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
            }

        }
    }

    private void PurchaseItem()
    {
        if(Input.GetMouseButtonDown(1))
        {
            // 구매 메서드
            Debug.Log("구매");
            if (Input.GetKey(KeyCode.LeftShift))
            {
                // 다중 구매 메서드
                countMessage.SetActive(true);
                Debug.Log("다중 구매");
            }
        }
        if (Input.GetKey(KeyCode.Escape)) ;
        
    }



   
}
