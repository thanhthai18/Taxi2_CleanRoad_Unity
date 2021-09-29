using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController_TaxiMinigame2 : MonoBehaviour
{
    public static GameController_TaxiMinigame2 instance;

    public Taxi_TaxiMinigame2 myTaxi;
    public Tree_TaxiMinigame2 tree1, tree2, tree3;
    public TreeHorizontal_TaxiMinigame2 treeNgang;
    public Vector3 mousePos;
    public Camera mainCamera;
    public RaycastHit2D[] hit;
    public bool isHoldTaxi, isHoldTree1, isHoldTree2, isHoldTree3, isHoldTreeNgang;
    public float tmpPosX_Taxi, tmpPosY_Tree1, tmpPosY_Tree2, tmpPosY_Tree3, tmpPosX_TreeNgang;
    public Canvas canvas;
    public List<GameObject> listWayPoint = new List<GameObject>();
    public int level, power, life;
    public Text txtLevel, txtPower, txtLife;
    public List<TreeHorizontal_TaxiMinigame2> listTreeNgang = new List<TreeHorizontal_TaxiMinigame2>();
    private Vector2 ClampMapTreeNgang;
    private float sizeX_TreeNgang;
    public GameObject finish;
    public TransitionLevel_TaxiMinigame1 transition;
    public bool isNextLv, isLose, isWin;
    public GameObject tutorial1, tutorial2, tutorial3;
    public bool isTutorial1, isTutorial2, isTutorial3;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(instance);
        transition.gameObject.SetActive(true);
    }

    private void Start()
    {
        isHoldTree1 = false;
        isHoldTree2 = false;
        isHoldTree3 = false;
        isHoldTreeNgang = false;
        isHoldTaxi = false;
        isNextLv = false;
        isLose = false;
        isWin = false;
        isTutorial1 = true;
        isTutorial2 = true;
        isTutorial3 = true;
        SetSizeCamera();
        SetUpTreeNgang(0);
        life = 3;
        SetUpMap(1);

        tutorial1.SetActive(true);
        tutorial2.SetActive(false);
        tutorial3.SetActive(false);
        ShowTutorial1();
    }

    void ShowTutorial1()
    {
        if (tutorial1.activeSelf)
        {
            tutorial1.transform.DOMoveY(tutorial1.transform.position.y + 5, 1).OnComplete(() =>
            {
                tutorial1.transform.DOMoveY(tutorial1.transform.position.y - 5, 1).OnComplete(() =>
                {
                    ShowTutorial1();
                });
            });
        }      
    }
    void ShowTutorial2()
    {
        if (tutorial2.activeSelf)
        {
            tutorial2.transform.position = new Vector2(myTaxi.transform.position.x, tutorial2.transform.position.y);
            tutorial2.transform.DOMoveX(finish.transform.position.x, 3).OnComplete(() =>
            {
                ShowTutorial2();
            });
        }
    }
    void ShowTutorial3()
    {
        if (tutorial3.activeSelf)
        {
            tutorial3.transform.DOMoveX(tutorial3.transform.position.x + 4, 1).OnComplete(() =>
            {
                tutorial3.transform.DOMoveX(tutorial3.transform.position.x - 4, 1).OnComplete(() =>
                {
                    ShowTutorial3();
                });
            });
        }
    }

    void SetSizeCamera()
    {
        float f1 = 16.0f / 9;
        float f2 = Screen.width * 1.0f / Screen.height;

        mainCamera.orthographicSize *= f1 / f2;
    }

    void SetUpTreeNgang(int index)
    {
        treeNgang = listTreeNgang[index];
        sizeX_TreeNgang = ((treeNgang.transform.localScale.x * treeNgang.GetComponent<BoxCollider2D>().size.x / 2f) - 0.3f);
        if (treeNgang == listTreeNgang[0])
        {
            ClampMapTreeNgang = new Vector2(-6.4f, 7.8f);
            listTreeNgang[1].GetComponent<BoxCollider2D>().enabled = false;
            listTreeNgang[1].GetComponent<SpriteRenderer>().enabled = false;
        }
        if (treeNgang == listTreeNgang[1])
        {
            ClampMapTreeNgang = new Vector2(-4.4f, 6.18f);
            listTreeNgang[0].GetComponent<BoxCollider2D>().enabled = false;
            listTreeNgang[0].GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void SetUpWayPoint()
    {
        for (int i = 0; i < listWayPoint[0].transform.childCount; i++)
        {
            if (System.Math.Round(listWayPoint[0].transform.GetChild(i).transform.position.y, 0) == System.Math.Round(myTaxi.transform.position.y, 0) &&
                System.Math.Round(listWayPoint[0].transform.GetChild(i).transform.position.x, 0) == System.Math.Round(tree1.transform.position.x, 0))
            {
                listWayPoint[0].transform.GetChild(i).GetComponent<Collider2D>().enabled = true;
                listWayPoint[0].transform.GetChild(i).GetComponent<WayPoint_TaxiMinigame2>().isWayPointTree1 = true;

                if (treeNgang != null)
                {
                    listWayPoint[1].transform.GetChild(i).GetComponent<Collider2D>().enabled = true;
                    listWayPoint[1].transform.GetChild(i).GetComponent<WayPoint_TaxiMinigame2>().isWayPointTree1 = true;
                }
            }

            if (System.Math.Round(listWayPoint[0].transform.GetChild(i).transform.position.y, 0) == System.Math.Round(myTaxi.transform.position.y, 0) &&
                System.Math.Round(listWayPoint[0].transform.GetChild(i).transform.position.x, 0) == System.Math.Round(tree2.transform.position.x, 0))
            {
                listWayPoint[0].transform.GetChild(i).GetComponent<Collider2D>().enabled = true;
                listWayPoint[0].transform.GetChild(i).GetComponent<WayPoint_TaxiMinigame2>().isWayPointTree2 = true;

                if (treeNgang != null)
                {
                    listWayPoint[1].transform.GetChild(i).GetComponent<Collider2D>().enabled = true;
                    listWayPoint[1].transform.GetChild(i).GetComponent<WayPoint_TaxiMinigame2>().isWayPointTree2 = true;
                }
            }

            if (System.Math.Round(listWayPoint[0].transform.GetChild(i).transform.position.y, 0) == System.Math.Round(myTaxi.transform.position.y, 0) &&
                System.Math.Round(listWayPoint[0].transform.GetChild(i).transform.position.x, 0) == System.Math.Round(tree3.transform.position.x, 0))
            {
                listWayPoint[0].transform.GetChild(i).GetComponent<Collider2D>().enabled = true;
                listWayPoint[0].transform.GetChild(i).GetComponent<WayPoint_TaxiMinigame2>().isWayPointTree3 = true;

                if (treeNgang != null)
                {
                    listWayPoint[1].transform.GetChild(i).GetComponent<Collider2D>().enabled = true;
                    listWayPoint[1].transform.GetChild(i).GetComponent<WayPoint_TaxiMinigame2>().isWayPointTree3 = true;
                }
            }
        }
    }

    void ResetWayPoint()
    {
        for(int k = 0; k < 2; k++)
        {
            for (int i = 0; i < listWayPoint[k].transform.childCount; i++)
            {
                listWayPoint[k].transform.GetChild(i).GetComponent<Collider2D>().enabled = false;
                listWayPoint[k].transform.GetChild(i).GetComponent<WayPoint_TaxiMinigame2>().isWayPointTree1 = false;
                listWayPoint[k].transform.GetChild(i).GetComponent<WayPoint_TaxiMinigame2>().isWayPointTree2 = false;
                listWayPoint[k].transform.GetChild(i).GetComponent<WayPoint_TaxiMinigame2>().isWayPointTree3 = false;
            }
        }
        
    }

    public void SetUpMap(int levelIndex)
    {
        if (levelIndex == 1)
        {
            isNextLv = false;
            ResetWayPoint();
            level = 1;
            power = 7;
            treeNgang.isOnLaneTree1 = false;
            treeNgang.isOnLaneTree2 = false;
            treeNgang.isOnLaneTree3 = false;
            treeNgang.GetComponent<BoxCollider2D>().enabled = false;
            treeNgang.GetComponent<SpriteRenderer>().enabled = false;
            tree3.isOnRoad = false;
            tree3.isOnRoad = false;
            tree3.isOnRoad = false;
            tree3.isOnLaneTreeNgang = false;
            tree3.isOnLaneTreeNgang = false;
            tree3.isOnLaneTreeNgang = false;
            tree3.GetComponent<BoxCollider2D>().enabled = false;
            tree3.GetComponent<SpriteRenderer>().enabled = false;

            myTaxi.transform.position = new Vector2(-5.94f, 1.65f);
            tree1.transform.position = new Vector2(-0.95f, 3.29f);
            tree2.transform.position = new Vector2(2.6f, 4.95f);
            SetUpWayPoint();

        }

        if (levelIndex == 2)
        {
            if (tutorial2.activeSelf)
            {
                tutorial2.SetActive(false);
                tutorial2.transform.DOKill();
            }
            isNextLv = false;
            ResetWayPoint();
            level = 2;
            power = 4;
            tree3.GetComponent<BoxCollider2D>().enabled = true;
            tree3.GetComponent<SpriteRenderer>().enabled = true;

            myTaxi.transform.position = new Vector2(-5.94f, 1.65f);
            tree1.transform.position = new Vector2(-0.95f, 3.29f);
            tree2.transform.position = new Vector2(5.96f, 4.95f);
            tree3.transform.position = new Vector2(9.31f, -0.12f);
            SetUpWayPoint();

        }
        if (levelIndex == 3)
        {
            if (isTutorial3)
            {
                isTutorial3 = false;
                tutorial3.SetActive(true);
                ShowTutorial3();
            }
            isNextLv = false;
            ResetWayPoint();
            level = 3;
            power = 4;
            SetUpTreeNgang(1);

            treeNgang.GetComponent<BoxCollider2D>().enabled = true;
            treeNgang.GetComponent<SpriteRenderer>().enabled = true;

            tree2.isOnRoad = false;
            tree2.isOnRoad = false;
            tree2.isOnRoad = false;
            tree2.isOnLaneTreeNgang = false;
            tree2.isOnLaneTreeNgang = false;
            tree2.isOnLaneTreeNgang = false;
            tree2.GetComponent<BoxCollider2D>().enabled = false;
            tree2.GetComponent<SpriteRenderer>().enabled = false;
            tree3.isOnRoad = false;
            tree3.isOnRoad = false;
            tree3.isOnRoad = false;
            tree3.isOnLaneTreeNgang = false;
            tree3.isOnLaneTreeNgang = false;
            tree3.isOnLaneTreeNgang = false;
            tree3.GetComponent<BoxCollider2D>().enabled = false;
            tree3.GetComponent<SpriteRenderer>().enabled = false;

            myTaxi.transform.position = new Vector2(-5.94f, 1.65f);
            tree1.transform.position = new Vector2(2.55f, 3.29f);
            treeNgang.transform.position = new Vector2(2.43f, -1.6f);
            SetUpWayPoint();


        }
        if (levelIndex == 4)
        {
            isNextLv = false;
            ResetWayPoint();
            level = 4;
            power = 6;
            SetUpTreeNgang(0);
            tree2.GetComponent<BoxCollider2D>().enabled = true;
            tree2.GetComponent<SpriteRenderer>().enabled = true;
            tree3.GetComponent<BoxCollider2D>().enabled = true;
            tree3.GetComponent<SpriteRenderer>().enabled = true;
            treeNgang.GetComponent<BoxCollider2D>().enabled = true;
            treeNgang.GetComponent<SpriteRenderer>().enabled = true;

            myTaxi.transform.position = new Vector2(-5.94f, 1.65f);
            tree1.transform.position = new Vector2(2.55f, 3.29f);
            tree2.transform.position = new Vector2(5.96f, 4.95f);
            tree3.transform.position = new Vector2(-4.31f, -3.58f);
            treeNgang.transform.position = new Vector2(4.22f, -1.68f);
            SetUpWayPoint();


        }
        if (levelIndex == 5)
        {
            isNextLv = false;
            ResetWayPoint();
            level = 5;
            power = 5;
            SetUpTreeNgang(1);

            tree3.isOnRoad = false;
            tree3.isOnRoad = false;
            tree3.isOnRoad = false;
            tree3.isOnLaneTreeNgang = false;
            tree3.isOnLaneTreeNgang = false;
            tree3.isOnLaneTreeNgang = false;
            tree3.GetComponent<BoxCollider2D>().enabled = false;
            tree3.GetComponent<SpriteRenderer>().enabled = false;
            treeNgang.GetComponent<BoxCollider2D>().enabled = true;
            treeNgang.GetComponent<SpriteRenderer>().enabled = true;

            myTaxi.transform.position = new Vector2(-5.94f, 1.65f);
            tree1.transform.position = new Vector2(2.55f, 3.29f);
            tree2.transform.position = new Vector2(-0.82f, 4.95f);
            treeNgang.transform.position = new Vector2(2.43f, -1.6f);
            SetUpWayPoint();
        }
    }

    public void TransitionLevelStart()
    {
        transition.LoadTransitionStart();

    }

    public void TransitionLevelEnd()
    {
        transition.LoadTransitionEnd();
    }

    private void Update()
    {
        txtLevel.text = level.ToString();
        txtPower.text = power.ToString();
        txtLife.text = life.ToString();

        if (Input.GetMouseButtonDown(0) && !isLose && !isWin && power > 0)
        {
            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.RaycastAll(mousePos, Vector2.zero);
            if (hit.Length != 0)
            {
                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i] && hit[i].collider != null)
                    {
                        if (hit[i].collider.gameObject.CompareTag("Player"))
                        {
                            tmpPosX_Taxi = mousePos.x - myTaxi.transform.position.x;
                            isHoldTaxi = true;       
                        }
                        if (hit[i].collider.gameObject.CompareTag("Tree"))
                        {
                            tmpPosY_Tree1 = mousePos.y - tree1.transform.position.y;
                            isHoldTree1 = true;
                            if (tutorial1.activeSelf)
                            {
                                tutorial1.SetActive(false);
                                tutorial1.transform.DOKill();
                            }
                        }
                        if (hit[i].collider.gameObject.CompareTag("ColorHive"))
                        {
                            tmpPosY_Tree2 = mousePos.y - tree2.transform.position.y;
                            isHoldTree2 = true;
                            if (tutorial1.activeSelf)
                            {
                                tutorial1.SetActive(false);
                                tutorial1.transform.DOKill();
                            }
                        }
                        if (hit[i].collider.gameObject.CompareTag("People"))
                        {
                            tmpPosY_Tree3 = mousePos.y - tree3.transform.position.y;
                            isHoldTree3 = true;
                        }
                        if (hit[i].collider.gameObject.CompareTag("Balloon"))
                        {
                            tmpPosX_TreeNgang = mousePos.x - treeNgang.transform.position.x;
                            isHoldTreeNgang = true;
                            if (tutorial3.activeSelf)
                            {
                                tutorial3.SetActive(false);
                                tutorial3.transform.DOKill();
                            }
                        }
                    }
                }
            }
        }
        if (Input.GetMouseButtonUp(0) && !isLose && !isWin && power > 0)
        {

            if (isHoldTaxi || isHoldTree1 || isHoldTree2 || isHoldTree3 || isHoldTreeNgang)
            {
                power--;
            }


            if (power == 0 && !isNextLv)
            {
                if (life == 1)
                {
                    life--;
                    txtLife.transform.DOPunchScale(new Vector3(1.3f, 1.3f, 1.3f), 1);
                    Debug.Log("Thua");
                    isLose = true;
                }
                if (life > 1)
                {
                    life--;
                    txtLife.transform.DOPunchScale(new Vector3(1.3f, 1.3f, 1.3f), 1);
                    txtPower.GetComponent<Text>().DOFade(0, 0.3f).OnComplete(() =>
                    {
                        txtPower.GetComponent<Text>().DOFade(1, 0.3f).OnComplete(() =>
                        {
                            txtPower.GetComponent<Text>().DOFade(0, 0.3f).OnComplete(() =>
                            {
                                txtPower.GetComponent<Text>().DOFade(1, 0.3f).OnComplete(() =>
                                {
                                    SetUpMap(level);
                                });
                            });
                        });
                    });                    
                }
            }
            isHoldTaxi = false;
            isHoldTree1 = false;
            isHoldTree2 = false;
            isHoldTree3 = false;
            isHoldTreeNgang = false;
        }

        if (isHoldTaxi)
        {
            //*
            if (tree1.isOnRoad && !tree2.isOnRoad)
            {
                if (!tree3.isOnRoad)
                {
                    if (myTaxi.transform.position.x < tree1.transform.position.x)
                    {
                        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                        mousePos = new Vector2(Mathf.Clamp(mousePos.x, -6.6f + tmpPosX_Taxi, tree1.transform.position.x - 2.4f - 2.7f + tmpPosX_Taxi), mousePos.y);
                        myTaxi.transform.position = new Vector2(mousePos.x - tmpPosX_Taxi, Mathf.Clamp(myTaxi.transform.position.y, -0.03f, 3.48f));
                    }
                    if (myTaxi.transform.position.x > tree1.transform.position.x)
                    {
                        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                        mousePos = new Vector2(Mathf.Clamp(mousePos.x, tree1.transform.position.x + 2.4f + 2.7f + tmpPosX_Taxi, 10.25f + tmpPosX_Taxi), mousePos.y);
                        myTaxi.transform.position = new Vector2(mousePos.x - tmpPosX_Taxi, Mathf.Clamp(myTaxi.transform.position.y, -0.03f, 3.48f));
                    }
                }
                if (tree3.isOnRoad)
                {
                    if (tree3.transform.position.x > tree1.transform.position.x)
                    {
                        if (myTaxi.transform.position.x < tree1.transform.position.x)
                        {
                            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                            mousePos = new Vector2(Mathf.Clamp(mousePos.x, -6.6f + tmpPosX_Taxi, tree1.transform.position.x - 2.4f - 2.7f + tmpPosX_Taxi), mousePos.y);
                            myTaxi.transform.position = new Vector2(mousePos.x - tmpPosX_Taxi, Mathf.Clamp(myTaxi.transform.position.y, -0.03f, 3.48f));
                        }
                        if (myTaxi.transform.position.x > tree1.transform.position.x && myTaxi.transform.position.x < tree3.transform.position.x)
                        {
                            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                            mousePos = new Vector2(Mathf.Clamp(mousePos.x, tree1.transform.position.x + 2.4f + 2.7f + tmpPosX_Taxi, tree3.transform.position.x - 2.4f - 2.7f + tmpPosX_Taxi), mousePos.y);
                            myTaxi.transform.position = new Vector2(mousePos.x - tmpPosX_Taxi, Mathf.Clamp(myTaxi.transform.position.y, -0.03f, 3.48f));
                        }
                    }
                    if (tree3.transform.position.x < tree1.transform.position.x)
                    {
                        if (myTaxi.transform.position.x > tree1.transform.position.x)
                        {
                            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                            mousePos = new Vector2(Mathf.Clamp(mousePos.x, tree1.transform.position.x + 2.4f + 2.7f + tmpPosX_Taxi, 10.25f + tmpPosX_Taxi), mousePos.y);
                            myTaxi.transform.position = new Vector2(mousePos.x - tmpPosX_Taxi, Mathf.Clamp(myTaxi.transform.position.y, -0.03f, 3.48f));
                        }
                        if (myTaxi.transform.position.x < tree1.transform.position.x)
                        {
                            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                            mousePos = new Vector2(Mathf.Clamp(mousePos.x, -6.6f + tmpPosX_Taxi, tree1.transform.position.x - 2.4f - 2.7f + tmpPosX_Taxi), mousePos.y);
                            myTaxi.transform.position = new Vector2(mousePos.x - tmpPosX_Taxi, Mathf.Clamp(myTaxi.transform.position.y, -0.03f, 3.48f));
                        }

                    }
                }
            }
            //*
            if (tree2.isOnRoad && !tree1.isOnRoad)
            {
                if (!tree3.isOnRoad)
                {
                    if (myTaxi.transform.position.x < tree2.transform.position.x)
                    {
                        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                        mousePos = new Vector2(Mathf.Clamp(mousePos.x, -6.6f + tmpPosX_Taxi, tree2.transform.position.x - 2.4f - 2.7f + tmpPosX_Taxi), mousePos.y);
                        myTaxi.transform.position = new Vector2(mousePos.x - tmpPosX_Taxi, Mathf.Clamp(myTaxi.transform.position.y, -0.03f, 3.48f));
                    }
                    if (myTaxi.transform.position.x > tree2.transform.position.x)
                    {
                        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                        mousePos = new Vector2(Mathf.Clamp(mousePos.x, tree2.transform.position.x + 2.4f + 2.7f + tmpPosX_Taxi, 10.25f + tmpPosX_Taxi), mousePos.y);
                        myTaxi.transform.position = new Vector2(mousePos.x - tmpPosX_Taxi, Mathf.Clamp(myTaxi.transform.position.y, -0.03f, 3.48f));
                    }
                }
                if (tree3.isOnRoad)
                {
                    if (tree3.transform.position.x > tree2.transform.position.x)
                    {
                        if (myTaxi.transform.position.x < tree2.transform.position.x)
                        {
                            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                            mousePos = new Vector2(Mathf.Clamp(mousePos.x, -6.6f + tmpPosX_Taxi, tree2.transform.position.x - 2.4f - 2.7f + tmpPosX_Taxi), mousePos.y);
                            myTaxi.transform.position = new Vector2(mousePos.x - tmpPosX_Taxi, Mathf.Clamp(myTaxi.transform.position.y, -0.03f, 3.48f));
                        }
                        if (myTaxi.transform.position.x > tree3.transform.position.x)
                        {
                            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                            mousePos = new Vector2(Mathf.Clamp(mousePos.x, tree3.transform.position.x + 2.4f + 2.7f + tmpPosX_Taxi, 10.25f + tmpPosX_Taxi), mousePos.y);
                            myTaxi.transform.position = new Vector2(mousePos.x - tmpPosX_Taxi, Mathf.Clamp(myTaxi.transform.position.y, -0.03f, 3.48f));
                        }
                    }
                    if (tree3.transform.position.x < tree2.transform.position.x)
                    {
                        if (myTaxi.transform.position.x > tree3.transform.position.x && myTaxi.transform.position.x < tree2.transform.position.x)
                        {
                            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                            mousePos = new Vector2(Mathf.Clamp(mousePos.x, tree3.transform.position.x + 2.4f + 2.7f + tmpPosX_Taxi, tree2.transform.position.x - 2.4f - 2.7f + tmpPosX_Taxi), mousePos.y);
                            myTaxi.transform.position = new Vector2(mousePos.x - tmpPosX_Taxi, Mathf.Clamp(myTaxi.transform.position.y, -0.03f, 3.48f));
                        }
                    }
                }

            }
            //*
            if (tree1.isOnRoad && tree2.isOnRoad)
            {
                if (!tree3.isOnRoad)
                {
                    if(tree1.transform.position.x < tree2.transform.position.x)
                    {
                        if (myTaxi.transform.position.x < tree1.transform.position.x)
                        {
                            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                            mousePos = new Vector2(Mathf.Clamp(mousePos.x, -6.6f + tmpPosX_Taxi, tree1.transform.position.x - 2.4f - 2.7f + tmpPosX_Taxi), mousePos.y);
                            myTaxi.transform.position = new Vector2(mousePos.x - tmpPosX_Taxi, Mathf.Clamp(myTaxi.transform.position.y, -0.03f, 3.48f));
                        }
                        if (myTaxi.transform.position.x > tree2.transform.position.x)
                        {
                            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                            mousePos = new Vector2(Mathf.Clamp(mousePos.x, tree2.transform.position.x + 2.4f + 2.7f + tmpPosX_Taxi, 10.25f + tmpPosX_Taxi), mousePos.y);
                            myTaxi.transform.position = new Vector2(mousePos.x - tmpPosX_Taxi, Mathf.Clamp(myTaxi.transform.position.y, -0.03f, 3.48f));
                        }
                        if (myTaxi.transform.position.x > tree1.transform.position.x && myTaxi.transform.position.x < tree2.transform.position.x)
                        {
                            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                            mousePos = new Vector2(Mathf.Clamp(mousePos.x, tree1.transform.position.x + 2.4f + 2.7f + tmpPosX_Taxi, tree2.transform.position.x - 2.4f - 2.7f + tmpPosX_Taxi), mousePos.y);
                            myTaxi.transform.position = new Vector2(mousePos.x - tmpPosX_Taxi, Mathf.Clamp(myTaxi.transform.position.y, -0.03f, 3.48f));
                        }
                    }
                    if(tree1.transform.position.x > tree2.transform.position.x)
                    {
                        if (myTaxi.transform.position.x < tree2.transform.position.x)
                        {
                            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                            mousePos = new Vector2(Mathf.Clamp(mousePos.x, -6.6f + tmpPosX_Taxi, tree2.transform.position.x - 2.4f - 2.7f + tmpPosX_Taxi), mousePos.y);
                            myTaxi.transform.position = new Vector2(mousePos.x - tmpPosX_Taxi, Mathf.Clamp(myTaxi.transform.position.y, -0.03f, 3.48f));
                        }
                        if(myTaxi.transform.position.x > tree1.transform.position.x)
                        {
                            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                            mousePos = new Vector2(Mathf.Clamp(mousePos.x, tree1.transform.position.x + 2.4f + 2.7f + tmpPosX_Taxi, 10.25f + tmpPosX_Taxi), mousePos.y);
                            myTaxi.transform.position = new Vector2(mousePos.x - tmpPosX_Taxi, Mathf.Clamp(myTaxi.transform.position.y, -0.03f, 3.48f));
                        }
                    }
                    
                }
                if (tree3.isOnRoad)
                {
                    if (tree3.transform.position.x > tree2.transform.position.x)
                    {
                        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                        mousePos = new Vector2(Mathf.Clamp(mousePos.x, -6.6f + tmpPosX_Taxi, tree1.transform.position.x - 2.4f - 2.7f + tmpPosX_Taxi), mousePos.y);
                        myTaxi.transform.position = new Vector2(mousePos.x - tmpPosX_Taxi, Mathf.Clamp(myTaxi.transform.position.y, -0.03f, 3.48f));
                    }
                }

            }
            //*
            if (!tree1.isOnRoad && !tree2.isOnRoad)
            {
                if (!tree3.isOnRoad)
                {
                    mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    mousePos = new Vector2(Mathf.Clamp(mousePos.x, -6.6f + tmpPosX_Taxi, 10.25f + tmpPosX_Taxi), mousePos.y);
                    myTaxi.transform.position = new Vector2(mousePos.x - tmpPosX_Taxi, Mathf.Clamp(myTaxi.transform.position.y, -0.03f, 3.48f));
                }
                if (tree3.isOnRoad)
                {
                    if (myTaxi.transform.position.x < tree3.transform.position.x)
                    {
                        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                        mousePos = new Vector2(Mathf.Clamp(mousePos.x, -6.6f + tmpPosX_Taxi, tree3.transform.position.x - 2.4f - 2.7f + tmpPosX_Taxi), mousePos.y);
                        myTaxi.transform.position = new Vector2(mousePos.x - tmpPosX_Taxi, Mathf.Clamp(myTaxi.transform.position.y, -0.03f, 3.48f));
                    }
                    if (myTaxi.transform.position.x > tree3.transform.position.x)
                    {
                        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                        mousePos = new Vector2(Mathf.Clamp(mousePos.x, tree3.transform.position.x + 2.4f + 2.7f + tmpPosX_Taxi, 14.24f + tmpPosX_Taxi), mousePos.y);
                        myTaxi.transform.position = new Vector2(mousePos.x - tmpPosX_Taxi, Mathf.Clamp(myTaxi.transform.position.y, -0.03f, 3.48f));
                    }
                }

            }
        }

        if (isHoldTreeNgang)
        {
            if (tree1.isOnLaneTreeNgang && !tree2.isOnLaneTreeNgang)
            {
                if (!tree3.isOnLaneTreeNgang)
                {
                    if (treeNgang.transform.position.x < tree1.transform.position.x)
                    {
                        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                        mousePos = new Vector2(Mathf.Clamp(mousePos.x, ClampMapTreeNgang.x + tmpPosX_TreeNgang, tree1.transform.position.x - 2.4f - sizeX_TreeNgang + tmpPosX_TreeNgang), mousePos.y);
                        treeNgang.transform.position = new Vector2(mousePos.x - tmpPosX_TreeNgang, treeNgang.transform.position.y);
                    }
                    if (treeNgang.transform.position.x > tree1.transform.position.x)
                    {
                        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                        mousePos = new Vector2(Mathf.Clamp(mousePos.x, tree1.transform.position.x + 2.4f + sizeX_TreeNgang + tmpPosX_TreeNgang, ClampMapTreeNgang.y + tmpPosX_TreeNgang), mousePos.y);
                        treeNgang.transform.position = new Vector2(mousePos.x - tmpPosX_TreeNgang, treeNgang.transform.position.y);
                    }
                }
                if (tree3.isOnLaneTreeNgang)
                {
                    if (tree3.transform.position.x > tree1.transform.position.x)
                    {
                        if (treeNgang.transform.position.x < tree1.transform.position.x)
                        {
                            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                            mousePos = new Vector2(Mathf.Clamp(mousePos.x, ClampMapTreeNgang.x + tmpPosX_TreeNgang, tree1.transform.position.x - 2.4f - sizeX_TreeNgang + tmpPosX_TreeNgang), mousePos.y);
                            treeNgang.transform.position = new Vector2(mousePos.x - tmpPosX_TreeNgang, treeNgang.transform.position.y);
                        }
                        if (treeNgang.transform.position.x > tree1.transform.position.x && treeNgang.transform.position.x < tree3.transform.position.x)
                        {
                            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                            mousePos = new Vector2(Mathf.Clamp(mousePos.x, tree1.transform.position.x + 2.4f + sizeX_TreeNgang + tmpPosX_TreeNgang, tree3.transform.position.x - 2.4f - sizeX_TreeNgang + tmpPosX_TreeNgang), mousePos.y);
                            treeNgang.transform.position = new Vector2(mousePos.x - tmpPosX_TreeNgang, treeNgang.transform.position.y);
                        }
                    }
                    if (tree3.transform.position.x < tree1.transform.position.x)
                    {
                        if (treeNgang.transform.position.x > tree1.transform.position.x)
                        {
                            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                            mousePos = new Vector2(Mathf.Clamp(mousePos.x, tree1.transform.position.x + 2.4f + sizeX_TreeNgang + tmpPosX_TreeNgang, ClampMapTreeNgang.y + tmpPosX_TreeNgang), mousePos.y);
                            treeNgang.transform.position = new Vector2(mousePos.x - tmpPosX_TreeNgang, treeNgang.transform.position.y);
                        }
                        if (treeNgang.transform.position.x < tree1.transform.position.x)
                        {
                            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                            mousePos = new Vector2(Mathf.Clamp(mousePos.x, ClampMapTreeNgang.x + tmpPosX_TreeNgang, tree1.transform.position.x - 2.4f - sizeX_TreeNgang + tmpPosX_TreeNgang), mousePos.y);
                            treeNgang.transform.position = new Vector2(mousePos.x - tmpPosX_TreeNgang, treeNgang.transform.position.y);
                        }

                    }
                }
            }

            if (tree2.isOnLaneTreeNgang && !tree1.isOnLaneTreeNgang)
            {
                if (!tree3.isOnLaneTreeNgang)
                {
                    if (treeNgang.transform.position.x < tree2.transform.position.x)
                    {
                        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                        mousePos = new Vector2(Mathf.Clamp(mousePos.x, ClampMapTreeNgang.x + tmpPosX_TreeNgang, tree2.transform.position.x - 2.4f - sizeX_TreeNgang + tmpPosX_TreeNgang), mousePos.y);
                        treeNgang.transform.position = new Vector2(mousePos.x - tmpPosX_TreeNgang, treeNgang.transform.position.y);
                    }
                    if (treeNgang.transform.position.x > tree2.transform.position.x)
                    {
                        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                        mousePos = new Vector2(Mathf.Clamp(mousePos.x, tree2.transform.position.x + 2.4f + sizeX_TreeNgang + tmpPosX_TreeNgang, ClampMapTreeNgang.y + tmpPosX_TreeNgang), mousePos.y);
                        treeNgang.transform.position = new Vector2(mousePos.x - tmpPosX_TreeNgang, treeNgang.transform.position.y);
                    }
                }

                if (tree3.isOnLaneTreeNgang)
                {
                    if (tree3.transform.position.x > tree2.transform.position.x)
                    {
                        if (treeNgang.transform.position.x < tree2.transform.position.x)
                        {
                            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                            mousePos = new Vector2(Mathf.Clamp(mousePos.x, ClampMapTreeNgang.x + tmpPosX_TreeNgang, tree2.transform.position.x - 2.4f - sizeX_TreeNgang + tmpPosX_TreeNgang), mousePos.y);
                            treeNgang.transform.position = new Vector2(mousePos.x - tmpPosX_TreeNgang, treeNgang.transform.position.y);
                        }
                        if (treeNgang.transform.position.x > tree3.transform.position.x)
                        {
                            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                            mousePos = new Vector2(Mathf.Clamp(mousePos.x, tree3.transform.position.x + 2.4f + sizeX_TreeNgang + tmpPosX_TreeNgang, ClampMapTreeNgang.y + tmpPosX_TreeNgang), mousePos.y);
                            treeNgang.transform.position = new Vector2(mousePos.x - tmpPosX_TreeNgang, treeNgang.transform.position.y);
                        }
                    }
                    if (tree3.transform.position.x < tree2.transform.position.x)
                    {
                        if (treeNgang.transform.position.x > tree3.transform.position.x && treeNgang.transform.position.x < tree2.transform.position.x)
                        {
                            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                            mousePos = new Vector2(Mathf.Clamp(mousePos.x, tree3.transform.position.x + 2.4f + sizeX_TreeNgang + tmpPosX_TreeNgang, tree2.transform.position.x - 2.4f - sizeX_TreeNgang + tmpPosX_TreeNgang), mousePos.y);
                            treeNgang.transform.position = new Vector2(mousePos.x - tmpPosX_TreeNgang, treeNgang.transform.position.y);
                        }
                    }
                }
            }
            //2 cay On
            if (tree1.isOnLaneTreeNgang && tree2.isOnLaneTreeNgang)
            {
                if (!tree3.isOnLaneTreeNgang)
                {
                    if (treeNgang.transform.position.x < tree1.transform.position.x)
                    {
                        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                        mousePos = new Vector2(Mathf.Clamp(mousePos.x, ClampMapTreeNgang.x + tmpPosX_TreeNgang, tree1.transform.position.x - 2.4f - sizeX_TreeNgang + tmpPosX_TreeNgang), mousePos.y);
                        treeNgang.transform.position = new Vector2(mousePos.x - tmpPosX_TreeNgang, treeNgang.transform.position.y);
                    }
                    if (treeNgang.transform.position.x > tree2.transform.position.x)
                    {
                        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                        mousePos = new Vector2(Mathf.Clamp(mousePos.x, tree2.transform.position.x + 2.4f + sizeX_TreeNgang + tmpPosX_TreeNgang, ClampMapTreeNgang.y + tmpPosX_TreeNgang), mousePos.y);
                        treeNgang.transform.position = new Vector2(mousePos.x - tmpPosX_TreeNgang, treeNgang.transform.position.y);
                    }
                    if (treeNgang.transform.position.x > tree1.transform.position.x && treeNgang.transform.position.x < tree2.transform.position.x)
                    {
                        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                        mousePos = new Vector2(Mathf.Clamp(mousePos.x, tree1.transform.position.x + 2.4f + sizeX_TreeNgang + tmpPosX_TreeNgang, tree2.transform.position.x - 2.4f - sizeX_TreeNgang + tmpPosX_TreeNgang), mousePos.y);
                        treeNgang.transform.position = new Vector2(mousePos.x - tmpPosX_TreeNgang, treeNgang.transform.position.y);
                    }
                }
                if (tree3.isOnLaneTreeNgang)
                {
                    if (tree3.transform.position.x > tree2.transform.position.x)
                    {
                        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                        mousePos = new Vector2(Mathf.Clamp(mousePos.x, ClampMapTreeNgang.x + tmpPosX_TreeNgang, tree1.transform.position.x - 2.4f - sizeX_TreeNgang + tmpPosX_TreeNgang), mousePos.y);
                        treeNgang.transform.position = new Vector2(mousePos.x - tmpPosX_TreeNgang, treeNgang.transform.position.y);
                    }
                }

            }
            //2 cay Off
            if (!tree1.isOnLaneTreeNgang && !tree2.isOnLaneTreeNgang)
            {
                if (!tree3.isOnLaneTreeNgang)
                {
                    mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    mousePos = new Vector2(Mathf.Clamp(mousePos.x, ClampMapTreeNgang.x + tmpPosX_TreeNgang, ClampMapTreeNgang.y + tmpPosX_TreeNgang), mousePos.y);
                    treeNgang.transform.position = new Vector2(mousePos.x - tmpPosX_TreeNgang, treeNgang.transform.position.y);
                }
                if (tree3.isOnLaneTreeNgang)
                {
                    if (treeNgang.transform.position.x < tree3.transform.position.x)
                    {
                        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                        mousePos = new Vector2(Mathf.Clamp(mousePos.x, ClampMapTreeNgang.x + tmpPosX_TreeNgang, tree3.transform.position.x - 2.4f - sizeX_TreeNgang + tmpPosX_TreeNgang), mousePos.y);
                        treeNgang.transform.position = new Vector2(mousePos.x - tmpPosX_TreeNgang, treeNgang.transform.position.y);
                    }
                    if (treeNgang.transform.position.x > tree3.transform.position.x)
                    {
                        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                        mousePos = new Vector2(Mathf.Clamp(mousePos.x, tree3.transform.position.x + 2.4f + sizeX_TreeNgang + tmpPosX_TreeNgang, ClampMapTreeNgang.y + tmpPosX_TreeNgang), mousePos.y);
                        treeNgang.transform.position = new Vector2(mousePos.x - tmpPosX_TreeNgang, treeNgang.transform.position.y);
                    }
                }

            }

        }

        if (isHoldTree1)
        {
            if (myTaxi.isOnLaneTree1 && !treeNgang.isOnLaneTree1)
            {
                if (tree1.transform.position.y < myTaxi.transform.position.y)
                {
                    mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    mousePos = new Vector2(mousePos.x, Mathf.Clamp(mousePos.y, -6.87f + tmpPosY_Tree1, myTaxi.transform.position.y - 1.67f - 3.7f + tmpPosY_Tree1));
                    tree1.transform.position = new Vector2(Mathf.Clamp(tree1.transform.position.x, tree1.transform.position.x - 2.63f, tree1.transform.position.x + 2.63f), mousePos.y - tmpPosY_Tree1);
                }
                if (tree1.transform.position.y > myTaxi.transform.position.y)
                {
                    mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    mousePos = new Vector2(mousePos.x, Mathf.Clamp(mousePos.y, myTaxi.transform.position.y + 1.67f + 3.7f + tmpPosY_Tree1, 6.87f + tmpPosY_Tree1));
                    tree1.transform.position = new Vector2(Mathf.Clamp(tree1.transform.position.x, tree1.transform.position.x - 2.63f, tree1.transform.position.x + 2.63f), mousePos.y - tmpPosY_Tree1);
                }

            }
            if (!myTaxi.isOnLaneTree1 && !treeNgang.isOnLaneTree1)
            {
                mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                mousePos = new Vector2(mousePos.x, Mathf.Clamp(mousePos.y, -6.87f + tmpPosY_Tree1, 6.87f + tmpPosY_Tree1));
                tree1.transform.position = new Vector2(Mathf.Clamp(tree1.transform.position.x, tree1.transform.position.x - 2.63f, tree1.transform.position.x + 2.63f), mousePos.y - tmpPosY_Tree1);
            }

            if (treeNgang.isOnLaneTree1 && !myTaxi.isOnLaneTree1)
            {
                if (tree1.transform.position.y < treeNgang.transform.position.y)
                {
                    mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    mousePos = new Vector2(mousePos.x, Mathf.Clamp(mousePos.y, -6.87f + tmpPosY_Tree1, treeNgang.transform.position.y - 1.67f - 3.7f + tmpPosY_Tree1));
                    tree1.transform.position = new Vector2(Mathf.Clamp(tree1.transform.position.x, tree1.transform.position.x - 2.63f, tree1.transform.position.x + 2.63f), mousePos.y - tmpPosY_Tree1);
                }
                if (tree1.transform.position.y > treeNgang.transform.position.y)
                {
                    mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    mousePos = new Vector2(mousePos.x, Mathf.Clamp(mousePos.y, treeNgang.transform.position.y + 1.67f + 3.7f + tmpPosY_Tree1, 6.87f + tmpPosY_Tree1));
                    tree1.transform.position = new Vector2(Mathf.Clamp(tree1.transform.position.x, tree1.transform.position.x - 2.63f, tree1.transform.position.x + 2.63f), mousePos.y - tmpPosY_Tree1);
                }

            }
            if (treeNgang.isOnLaneTree1 && myTaxi.isOnLaneTree1)
            {
                if (tree1.transform.position.y > myTaxi.transform.position.y)
                {
                    mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    mousePos = new Vector2(mousePos.x, Mathf.Clamp(mousePos.y, myTaxi.transform.position.y + 1.67f + 3.7f + tmpPosY_Tree1, 6.87f + tmpPosY_Tree1));
                    tree1.transform.position = new Vector2(Mathf.Clamp(tree1.transform.position.x, tree1.transform.position.x - 2.63f, tree1.transform.position.x + 2.63f), mousePos.y - tmpPosY_Tree1);
                }
                if (tree1.transform.position.y < treeNgang.transform.position.y)
                {
                    mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    mousePos = new Vector2(mousePos.x, Mathf.Clamp(mousePos.y, -6.87f + tmpPosY_Tree1, treeNgang.transform.position.y - 1.67f - 3.7f + tmpPosY_Tree1));
                    tree1.transform.position = new Vector2(Mathf.Clamp(tree1.transform.position.x, tree1.transform.position.x - 2.63f, tree1.transform.position.x + 2.63f), mousePos.y - tmpPosY_Tree1);
                }
            }
        }


        if (isHoldTree2)
        {
            if (myTaxi.isOnLaneTree2 && !treeNgang.isOnLaneTree2)
            {
                if (tree2.transform.position.y < myTaxi.transform.position.y)
                {
                    mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    mousePos = new Vector2(mousePos.x, Mathf.Clamp(mousePos.y, -5.23f + tmpPosY_Tree2, myTaxi.transform.position.y - 1.67f - 4.91f + tmpPosY_Tree2));
                    tree2.transform.position = new Vector2(Mathf.Clamp(tree2.transform.position.x, tree2.transform.position.x - 2.63f, tree2.transform.position.x + 2.63f), mousePos.y - tmpPosY_Tree2);
                }
                if (tree2.transform.position.y > myTaxi.transform.position.y)
                {
                    mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    mousePos = new Vector2(mousePos.x, Mathf.Clamp(mousePos.y, myTaxi.transform.position.y + 1.67f + 4.91f + tmpPosY_Tree2, 5.23f + tmpPosY_Tree2));
                    tree2.transform.position = new Vector2(Mathf.Clamp(tree2.transform.position.x, tree2.transform.position.x - 2.63f, tree2.transform.position.x + 2.63f), mousePos.y - tmpPosY_Tree2);
                }

            }
            if (!myTaxi.isOnLaneTree2 && !treeNgang.isOnLaneTree2)
            {
                mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                mousePos = new Vector2(mousePos.x, Mathf.Clamp(mousePos.y, -5.23f + tmpPosY_Tree2, 5.23f + tmpPosY_Tree2));
                tree2.transform.position = new Vector2(Mathf.Clamp(tree2.transform.position.x, tree2.transform.position.x - 2.63f, tree2.transform.position.x + 2.63f), mousePos.y - tmpPosY_Tree2);
            }

            if (treeNgang.isOnLaneTree2 && !myTaxi.isOnLaneTree2)
            {
                if (tree2.transform.position.y < treeNgang.transform.position.y)
                {
                    mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    mousePos = new Vector2(mousePos.x, Mathf.Clamp(mousePos.y, -5.23f + tmpPosY_Tree2, treeNgang.transform.position.y - 1.67f - 4.91f + tmpPosY_Tree2));
                    tree2.transform.position = new Vector2(Mathf.Clamp(tree2.transform.position.x, tree2.transform.position.x - 2.63f, tree2.transform.position.x + 2.63f), mousePos.y - tmpPosY_Tree2);
                }
                if (tree2.transform.position.y > treeNgang.transform.position.y)
                {
                    mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    mousePos = new Vector2(mousePos.x, Mathf.Clamp(mousePos.y, treeNgang.transform.position.y + 1.67f + 4.91f + tmpPosY_Tree2, 5.23f + tmpPosY_Tree2));
                    tree2.transform.position = new Vector2(Mathf.Clamp(tree2.transform.position.x, tree2.transform.position.x - 2.63f, tree2.transform.position.x + 2.63f), mousePos.y - tmpPosY_Tree2);
                }

            }
        }

        if (isHoldTree3)
        {
            if (myTaxi.isOnLaneTree3 && !treeNgang.isOnLaneTree3)
            {
                if (tree3.transform.position.y < myTaxi.transform.position.y)
                {
                    mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    mousePos = new Vector2(mousePos.x, Mathf.Clamp(mousePos.y, -6.87f + tmpPosY_Tree3, myTaxi.transform.position.y - 1.67f - 3.7f + tmpPosY_Tree3));
                    tree3.transform.position = new Vector2(Mathf.Clamp(tree3.transform.position.x, tree3.transform.position.x - 2.63f, tree3.transform.position.x + 2.63f), mousePos.y - tmpPosY_Tree3);
                }
                if (tree3.transform.position.y > myTaxi.transform.position.y)
                {
                    mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    mousePos = new Vector2(mousePos.x, Mathf.Clamp(mousePos.y, myTaxi.transform.position.y + 1.67f + 3.7f + tmpPosY_Tree3, 6.87f + tmpPosY_Tree3));
                    tree3.transform.position = new Vector2(Mathf.Clamp(tree3.transform.position.x, tree3.transform.position.x - 2.63f, tree3.transform.position.x + 2.63f), mousePos.y - tmpPosY_Tree3);
                }

            }
            if (!myTaxi.isOnLaneTree3 && !treeNgang.isOnLaneTree3)
            {
                mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                mousePos = new Vector2(mousePos.x, Mathf.Clamp(mousePos.y, -6.87f + tmpPosY_Tree3, 6.87f + tmpPosY_Tree3));
                tree3.transform.position = new Vector2(Mathf.Clamp(tree3.transform.position.x, tree3.transform.position.x - 2.63f, tree3.transform.position.x + 2.63f), mousePos.y - tmpPosY_Tree3);
            }

            if (treeNgang.isOnLaneTree3 && !myTaxi.isOnLaneTree3)
            {
                if (tree3.transform.position.y < treeNgang.transform.position.y)
                {
                    mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    mousePos = new Vector2(mousePos.x, Mathf.Clamp(mousePos.y, -6.87f + tmpPosY_Tree3, treeNgang.transform.position.y - 1.67f - 3.7f + tmpPosY_Tree3));
                    tree3.transform.position = new Vector2(Mathf.Clamp(tree3.transform.position.x, tree3.transform.position.x - 2.63f, tree3.transform.position.x + 2.63f), mousePos.y - tmpPosY_Tree3);
                }
                if (tree3.transform.position.y > treeNgang.transform.position.y)
                {
                    mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    mousePos = new Vector2(mousePos.x, Mathf.Clamp(mousePos.y, treeNgang.transform.position.y + 1.67f + 3.7f + tmpPosY_Tree3, 6.87f + tmpPosY_Tree3));
                    tree3.transform.position = new Vector2(Mathf.Clamp(tree3.transform.position.x, tree3.transform.position.x - 2.63f, tree3.transform.position.x + 2.63f), mousePos.y - tmpPosY_Tree3);
                }

            }
            if (treeNgang.isOnLaneTree3 && myTaxi.isOnLaneTree3)
            {
                if (tree3.transform.position.y > myTaxi.transform.position.y)
                {
                    mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    mousePos = new Vector2(mousePos.x, Mathf.Clamp(mousePos.y, myTaxi.transform.position.y + 1.67f + 3.7f + tmpPosY_Tree3, 6.87f + tmpPosY_Tree3));
                    tree3.transform.position = new Vector2(Mathf.Clamp(tree3.transform.position.x, tree3.transform.position.x - 2.63f, tree3.transform.position.x + 2.63f), mousePos.y - tmpPosY_Tree3);
                }
                if (tree3.transform.position.y < treeNgang.transform.position.y)
                {
                    mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    mousePos = new Vector2(mousePos.x, Mathf.Clamp(mousePos.y, -6.87f + tmpPosY_Tree3, treeNgang.transform.position.y - 1.67f - 3.7f + tmpPosY_Tree3));
                    tree3.transform.position = new Vector2(Mathf.Clamp(tree3.transform.position.x, tree3.transform.position.x - 2.63f, tree3.transform.position.x + 2.63f), mousePos.y - tmpPosY_Tree3);
                }
            }
        }
        if (isTutorial2 && !tree1.isOnRoad && !tree2.isOnRoad && !tutorial1.activeSelf)
        {
            isTutorial2 = false;
            tutorial2.SetActive(true);
            ShowTutorial2();
        }
    }
}
