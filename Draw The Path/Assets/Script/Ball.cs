using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] private GameManager _GameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BallEnter"))
        {
            _GameManager.BallEfect();
            gameObject.SetActive(false);
            _GameManager.KeepGoing();
        }
        else if (collision.gameObject.CompareTag("GameOver"))
        {
            gameObject.SetActive(false);
            _GameManager.GameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Edge"))
        {
            _GameManager.sounds[2].Play();
        }
    }
}
