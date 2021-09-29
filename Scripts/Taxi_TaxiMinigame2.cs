using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taxi_TaxiMinigame2 : MonoBehaviour
{
    public static Taxi_TaxiMinigame2 instance;

    public bool isOnLaneTree1, isOnLaneTree2, isOnLaneTree3;
    public int num;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(instance);
    }

    private void Start()
    {
        num = 1;
        isOnLaneTree1 = false;
        isOnLaneTree2 = false;
        isOnLaneTree3 = false;
    }

    void DelayTransitionEnd()
    {
        GameController_TaxiMinigame2.instance.TransitionLevelEnd();
    }

    void DelayNextLevel()
    {
        num++;
        GameController_TaxiMinigame2.instance.SetUpMap(num);
        GameController_TaxiMinigame2.instance.txtLevel.transform.DOPunchScale(new Vector3(1.3f, 1.3f, 1.3f), 1);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Path") && collision.GetComponent<WayPoint_TaxiMinigame2>().isWayPointTree1)
        {
            isOnLaneTree1 = true;
        }

        if (collision.gameObject.CompareTag("Path") && collision.GetComponent<WayPoint_TaxiMinigame2>().isWayPointTree2)
        {
            isOnLaneTree2 = true;
        }

        if (collision.gameObject.CompareTag("Path") && collision.GetComponent<WayPoint_TaxiMinigame2>().isWayPointTree3)
        {
            isOnLaneTree3 = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Path") && collision.GetComponent<WayPoint_TaxiMinigame2>().isWayPointTree1)
        {
            isOnLaneTree1 = false;
        }

        if (collision.gameObject.CompareTag("Path") && collision.GetComponent<WayPoint_TaxiMinigame2>().isWayPointTree2)
        {
            isOnLaneTree2 = false;
        }

        if (collision.gameObject.CompareTag("Path") && collision.GetComponent<WayPoint_TaxiMinigame2>().isWayPointTree3)
        {
            isOnLaneTree3 = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            GameController_TaxiMinigame2.instance.isNextLv = true;
            GameController_TaxiMinigame2.instance.isHoldTaxi = false;
            GameController_TaxiMinigame2.instance.finish.GetComponent<SpriteRenderer>().DOFade(0, 0.3f).OnComplete(() =>
            {
                GameController_TaxiMinigame2.instance.finish.GetComponent<SpriteRenderer>().DOFade(1, 0.3f).OnComplete(() =>
                {
                    if(num < 5)
                    {
                        GameController_TaxiMinigame2.instance.TransitionLevelStart();
                        Invoke(nameof(DelayTransitionEnd), 0.5f);
                        Invoke(nameof(DelayNextLevel), 0.5f);
                    }
                    if(num == 5)
                    {
                        GameController_TaxiMinigame2.instance.isWin = true;
                        Debug.Log("Win");
                        //anim phao hoa
                    }
                });
            });
        }
    }

}
