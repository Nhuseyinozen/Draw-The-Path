using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    [SerializeField] private GameObject[] balls;
    [SerializeField] private GameObject[] points;
    public GameObject bucket;
    [SerializeField] private GameObject ballCenter;
    int activeBallIndex;
    int bucketRandomIndex;
    bool key;

    public static int throwBallCount; // Atýlan top sayýsý.
    public static int shotBallCount; // Top atýþ sayýsý.


    private void Start()
    {
        throwBallCount = 0;
        shotBallCount = 0;
    }

    public void GameStart()
    {
        StartCoroutine(BallThrowSystem());
    }
    IEnumerator BallThrowSystem()
    {
        while (true)
        {
            if (!key)
            {
                yield return new WaitForSeconds(.5f);

                if (shotBallCount != 0 && shotBallCount % 5 == 0)
                {

                    for (int i = 0; i < 2; i++)
                    {
                        BallSettings();
                    }
                    throwBallCount = 2;
                    shotBallCount++;
                }
                else
                {
                    BallSettings();
                    throwBallCount = 1;
                    shotBallCount++;
                }

                yield return new WaitForSeconds(.7f);

                bucketRandomIndex = Random.Range(0, balls.Length - 1);
                bucket.transform.position = points[bucketRandomIndex].transform.position;
                bucket.SetActive(true);
                key = true;

                Invoke("BallTimeControl", 5f); // Top 4 saniye sabit kalýrsa.
            }
            else
            {
                yield return null;
            }
        }
    }


    float Angle(float value1, float value2)
    {
        return Random.Range(value1, value2);
    }

    Vector3 Pos(float angle)
    {
        return Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
    }

    public void KeepGoing()
    {
        // Top girdiðinde yapýlacak iþlemler.

        if (throwBallCount == 1)
        {
            CancelInvoke(); // Top girme devam ediyorsa ýnvoke metotunu iptal et.
            bucket.SetActive(false);
            key = false;
            throwBallCount--;
        }
        else
        {
            throwBallCount--;
        }

    }

    public void BallTimeControl()
    {
        if (key)
            GetComponent<GameManager>().GameOver();
    }

    void BallSettings()
    {
        balls[activeBallIndex].transform.position = ballCenter.transform.position;
        balls[activeBallIndex].SetActive(true);
        balls[activeBallIndex].gameObject.GetComponent<Rigidbody2D>().AddForce(750 * Pos(Angle(70f, 110f)));

        if (activeBallIndex != balls.Length - 1)
        {
            activeBallIndex++;
        }
        else
        {
            activeBallIndex = 0;
        }
    }
}
