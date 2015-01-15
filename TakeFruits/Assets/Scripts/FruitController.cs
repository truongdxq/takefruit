using UnityEngine;
using System.Collections;

public class FruitController
{
    private GameObject gameObject;
    public GameObject Fruit
    {
        get { return gameObject; }
    }

    private float speed;
    private GameManager mng;
    public FruitController(GameObject _gameObject)
    {
        gameObject = _gameObject;
        mng = GameManager.Instance;
        speed = UnityEngine.Random.Range(3.4f, 3.5f);
        if (mng.SpeedGame > 350f)
        {
            speed += 6.2f;
        }
        else if(mng.SpeedGame >= 200f && mng.SpeedGame <= 350f )
        {
            speed += 4.5f;
        }
        else
        {
              speed += 0.02f * mng.SpeedGame;
        }
    }
    public void Update()
    {
        gameObject.transform.Translate(0, -speed * Time.deltaTime, 0, Space.Self);
        if (gameObject.transform.localPosition.y < -5.5f)
        {
            if (gameObject.tag.Equals(mng.Tag))
            {
                mng.LifeCount--;
                UIManager.Instance.changeLabelLife(mng.LifeCount);
                mng.Miss++;
                UIManager.Instance.changeLabelMiss(mng.Miss);
                mng.playSound(1);
            }
         mng.destroy(this);
        }
    }
}
