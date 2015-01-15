using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{
    private bool isPause;
    public bool IsPause
    {
        get { return isPause; }
        set { isPause = value; }
    }
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }

    public GameObject[] listFruits;
    private List<FruitController> listFruitController = new List<FruitController>();
    private List<FruitController> listFruitControllerTemp = new List<FruitController>();
    public GameObject particle;

    private int countTouch = 0;
    private float time = 0;
    private float aTime = 1;

    private int indexRandom;
    public int IndexRandom
    {
        get { return indexRandom; }
    }
    private string tag;
    public string Tag
    {
        get { return tag; }
        set { tag = value; }
    }
    private int lifeCount = 3;
    public int LifeCount
    {
        get { return lifeCount; }
        set { lifeCount = value; }
    }
    private int score = 0;
    public int Score
    {
        get { return score; }
        set { score = value; }
    }
    private int miss = 0;
    public int Miss
    {
        get { return miss; }
        set { miss = value; }
    }
    private int wrong = 0;
    public int Wrong
    {
        get { return wrong; }
        set { wrong = value; }
    }

    private float speedGame = 0;
    public float SpeedGame
    {
        get { return speedGame; }
        set { speedGame = value; }
    }
    public AudioClip [] listAudio;

    //tam thoi xoa particle ntn
    private List<GameObject> listParticle = new List<GameObject>();

    void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start()
    {
        isPause = true;
        indexRandom = UnityEngine.Random.Range(0, listFruits.Length);
    }
    public void StartGame()
    {
        foreach(GameObject p in listParticle)
        {
            Destroy(p);
        }
        listParticle.Clear();
        countTouch = 0;
        speedGame = 0;
        aTime = 1;
        miss = 0;
        wrong = 0;
        score = 0;
        lifeCount = 3;
        UIManager.Instance.changeLabelScore(0);
        UIManager.Instance.changeLabelLife(3);
        UIManager.Instance.changeLabelMiss(0);
        UIManager.Instance.changeLabelWrong(0);
        indexRandom = UnityEngine.Random.Range(0, listFruits.Length);
        foreach (FruitController fc in listFruitControllerTemp)
        {
            destroy(fc);
        }
    }

    // Update is called once per frame
    void Update()
    {
        eventMouseClick();
        if (isPause) return;
        speedGame += 0.15f;
        time += 0.01f;
        if ((time * aTime) >= 1)
        {
            createFruit();
            time = 0;
            if (aTime < 10f)
            {
                aTime += 0.08f; 
            }
        }
        listFruitControllerTemp.Clear();
        foreach (FruitController fc in listFruitController)
        {
            listFruitControllerTemp.Add(fc);
        }
        foreach (FruitController fc in listFruitControllerTemp)
        {
            fc.Update();
        }
        if (lifeCount <= 0) finish();
    }
    void createFruit()
    {
        int sizelistFruit = listFruits.Length;
        int index = UnityEngine.Random.Range(0, sizelistFruit);
        GameObject go = (GameObject)GameObject.Instantiate(listFruits[index], new Vector3(UnityEngine.Random.Range(-7.5f, 7.5f), 6, 0), Quaternion.identity);
        FruitController fruitReal = new FruitController(go);
        listFruitController.Add(fruitReal);
    }

    public void destroy(FruitController fc)
    {
        Destroy(fc.Fruit);
        listFruitController.Remove(fc);
    }
    void eventMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (countTouch == 0)    //truong hop click de bat dau
            {
                UIManager.Instance.onStartClick();
                countTouch++;
                playSound(0);
            }
            else if (countTouch > 0)
            {
                if (isPause)    //truong hop click khi finish
                {
                    UIManager.Instance.onRestartClick();
                }
                else
                {
                    Vector3 v3 = Input.mousePosition;
                    v3.z = 10;
                    v3 = Camera.main.ScreenToWorldPoint(v3);
                    foreach (FruitController fc in listFruitControllerTemp)
                    {
                        if ((fc.Fruit != null) &&(Helper.Contain(fc.Fruit.collider2D.bounds, v3)))
                        {
                            if (fc.Fruit.tag.Equals(tag))
                            {
                                Score += 1;
                                //tao particle
                                GameObject p = (GameObject)GameObject.Instantiate(particle, fc.Fruit.transform.localPosition, Quaternion.Euler(0, 180, 0));
                                listParticle.Add(p);
                                UIManager.Instance.changeLabelScore(Score);
                                playSound(0);
                                destroy(fc);
                            }
                            else
                            {
                                lifeCount -= 1;
                                UIManager.Instance.changeLabelLife(LifeCount);
                                wrong++;
                                UIManager.Instance.changeLabelWrong(Wrong);
                                playSound(1);
                            }   //end else truetab
                            break; 
                        }   //end click in fruit
                    }
                }
            } //end else countTouch
        }
    } //end eventMouseclick
    private void finish()
    {
        isPause = true;
        UIManager.Instance.startFormFinish();
    }
    public void playSound(int index)
    {
        audio.clip = listAudio[index];
        audio.Play();
    }
}