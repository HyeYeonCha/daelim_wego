using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{

    public float itemCount;
    public GameObject itemCountImage;
    public Text itemCountText;

    public bool removeFlag;

    ClickItem gm;

    private void Start()
    {
        gm = FindObjectOfType<ClickItem>();
    }

    public static Slot instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(this);
    }

    public void ItemAdd(string itemName, float _count = 1)
    {
        itemCount = _count;
        Debug.Log("item count : " + _count);

        if(itemCount >= 2)
        {
            itemCountImage.SetActive(true);
            itemCountText.text = "" + itemCount;
        } else
        {
            itemCountImage.SetActive(false);
        }

        if(gm.itemList.Count <= 0)
        {
            gm.itemList.Add(itemName);
        }
        else
        {
            for (int i = 0; i < gm.itemList.Count; i++)
            {
                Debug.Log("도는중");
                if (gm.itemList[i] == itemName)
                {
                    Debug.Log("찾았");
                    itemCountImage.SetActive(true);
                    itemCount++;
                    itemCountText.text = "" + itemCount;
                    //Destroy(gameObject);
                    removeFlag = true;
//                    return;
                }
                else
                {
                    if (itemCount == 1)
                    {
                        itemCountImage.SetActive(false);
                        gm.itemList.Add(itemName);
                        removeFlag = false;
                        return;
                    }
                    else
                    {
                        itemCount = _count;
                        itemCountImage.SetActive(true);
                        itemCountText.text = "" + itemCount;
                        gm.itemList.Add(itemName);
                        removeFlag = false;
                        return;
                    }
                }
            }
        }

        itemCountText.text = "" + itemCount;
    }



}
