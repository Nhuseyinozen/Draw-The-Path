using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ThrowBall _throwBall;
    [SerializeField] private DrawLine _drawLine;
    public AudioSource[] sounds;
    public ParticleSystem[] ballEfect;
    int activeEfectCount;
    [SerializeField] private TextMeshProUGUI[] score;
    [SerializeField] private GameObject[] panel;
    int enterBallCount;

    void Start()
    {
        Time.timeScale = 0;

        if (PlayerPrefs.HasKey("BestScore"))
        {
            score[0].text = PlayerPrefs.GetInt("BestScore").ToString();
            score[1].text = PlayerPrefs.GetInt("BestScore").ToString();
        }
        else
        {
            PlayerPrefs.SetInt("BestScore", 0);
            score[0].text = "0";
            score[1].text = "0";
        }
    }


    void Update()
    {

    }

    public void BallEfect()
    {
        ballEfect[activeEfectCount].transform.position = _throwBall.bucket.transform.position;
        ballEfect[activeEfectCount].gameObject.SetActive(true);

        if (activeEfectCount != ballEfect.Length - 1)
        {
            activeEfectCount++;
        }
        else
        {
            activeEfectCount = 0;
        }
    }


    public void KeepGoing()
    {
        enterBallCount++;
        sounds[0].Play();
        _throwBall.KeepGoing();
        _drawLine.KeepGoing();
    }

    public void GameOver()
    {
        sounds[1].Play();
        panel[1].SetActive(true); ;
        panel[2].SetActive(false); ;

        score[2].text = enterBallCount.ToString();

        if (enterBallCount > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", enterBallCount);
            score[1].text = PlayerPrefs.GetInt("BestScore").ToString();
        }
        Time.timeScale = 0;
    }

    public void GameStart()
    {

        _throwBall.GameStart();
        panel[0].SetActive(false);
        Time.timeScale = 1;
        panel[2].SetActive(true);
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
