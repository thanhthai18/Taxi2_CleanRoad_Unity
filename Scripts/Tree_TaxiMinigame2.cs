using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree_TaxiMinigame2 : MonoBehaviour
{
    public bool isOnRoad = false;
    public bool isOnLaneTreeNgang = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Path"))
        {
            if (!isOnRoad)
            {
                isOnRoad = true;
            }
        }
        if (collision.gameObject.CompareTag("PointWindow"))
        {
            if (!isOnLaneTreeNgang)
            {
                isOnLaneTreeNgang = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Path"))
        {
            if (isOnRoad)
            {
                isOnRoad = false;

            }
        }
        if (collision.gameObject.CompareTag("PointWindow"))
        {
            if (isOnLaneTreeNgang)
            {
                isOnLaneTreeNgang = false;

            }
        }

    }


}
