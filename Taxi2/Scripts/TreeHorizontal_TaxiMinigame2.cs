using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHorizontal_TaxiMinigame2 : MonoBehaviour
{
    public bool isOnLaneTree1, isOnLaneTree2, isOnLaneTree3;

    private void Start()
    {
        isOnLaneTree1 = false;
        isOnLaneTree2 = false;
        isOnLaneTree3 = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PointWindow") && collision.GetComponent<WayPoint_TaxiMinigame2>().isWayPointTree1)
        {
            isOnLaneTree1 = true;
        }

        if (collision.gameObject.CompareTag("PointWindow") && collision.GetComponent<WayPoint_TaxiMinigame2>().isWayPointTree2)
        {
            isOnLaneTree2 = true;
        }

        if (collision.gameObject.CompareTag("PointWindow") && collision.GetComponent<WayPoint_TaxiMinigame2>().isWayPointTree3)
        {
            isOnLaneTree3 = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PointWindow") && collision.GetComponent<WayPoint_TaxiMinigame2>().isWayPointTree1)
        {
            isOnLaneTree1 = false;
        }

        if (collision.gameObject.CompareTag("PointWindow") && collision.GetComponent<WayPoint_TaxiMinigame2>().isWayPointTree2)
        {
            isOnLaneTree2 = false;
        }

        if (collision.gameObject.CompareTag("PointWindow") && collision.GetComponent<WayPoint_TaxiMinigame2>().isWayPointTree3)
        {
            isOnLaneTree3 = false;
        }
    }
}
