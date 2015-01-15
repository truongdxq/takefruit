using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get { return instance; }
    }
    public GameObject ObStart, ObPlay, ObFinish;
    public GameObject apple, banana, cherry, eggplant, lemon, orange, pear, pineapple, strawberry, watermelon;
    public GUIText labelScore, labelLife, labelMiss, labelWrong, labelYourscore;

    private GameManager mng;
    void Awake()
    {
        instance = this;
        mng = GameManager.Instance;
    }

    void Start()
    {
        mng = GameManager.Instance;
        displayRandomFruit();
    }
    void displayRandomFruit()
    {
        switch (mng.IndexRandom)
        {
            case 0: apple.SetActive(true);
                mng.Tag = "apple";
                break;
            case 1: banana.SetActive(true);
                mng.Tag = "banana";
                break;
            case 2: cherry.SetActive(true);
                mng.Tag = "cherry";
                break;
            case 3: eggplant.SetActive(true);
                mng.Tag = "eggplant";
                break;
            case 4: lemon.SetActive(true);
                mng.Tag = "lemon";
                break;
            case 5: orange.SetActive(true);
                mng.Tag = "orange";
                break;
            case 6: pear.SetActive(true);
                mng.Tag = "pear";
                break;
            case 7: pineapple.SetActive(true);
                mng.Tag = "pineapple";
                break;
            case 8: strawberry.SetActive(true);
                mng.Tag = "strawberry";
                break;
            case 9: watermelon.SetActive(true);
                mng.Tag = "watermelon";
                break;
            default:
                break;
        }
    } //end displayRandomFruit()
    // Update is called once per frame
    void Update()
    {

    }
    public void onStartClick()
    {
        mng.IsPause = false;
        ObStart.animation.Play("aobjectstart");
        ObPlay.animation.Play("aobjectplayout");

    }
    public void changeLabelScore(int score)
    {
        labelScore.text = "Score: " + score.ToString();
    }
    public void changeLabelLife(int life)
    {
        labelLife.text = "Life: " + life.ToString();
    }
    public void changeLabelMiss(int miss)
    {
        labelMiss.text = "Miss: " + miss;
    }
    public void changeLabelWrong(int wrong)
    {
        labelWrong.text = "Wrong: " + wrong;
    }

    public void startFormFinish()
    {
        labelYourscore.text = mng.Score.ToString();
        ObFinish.SetActive(true);
        ObPlay.animation.Play("aobjectplayin");

        apple.SetActive(false);
        banana.SetActive(false);
        cherry.SetActive(false);
        eggplant.SetActive(false);
        lemon.SetActive(false);
        orange.SetActive(false);
        pear.SetActive(false);
        pineapple.SetActive(false);
        strawberry.SetActive(false);
        watermelon.SetActive(false);
    }
    public void onRestartClick()
    {
        ObFinish.SetActive(false);
        mng.StartGame();
        ObStart.transform.localScale = new Vector3(1, 1, 1);
        ObStart.transform.rotation = Quaternion.Euler(0, 0, 0);
        displayRandomFruit();
    }
}
