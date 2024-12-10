using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text paddle1ScoreText;
    [SerializeField] private TMP_Text paddle2ScoreText;

    [SerializeField] private Transform paddle1Transform;
    [SerializeField] private Transform paddle2Transform;
    [SerializeField] private Transform ballTransform;

    private int paddle1Score;
    private int paddle2Score;

    private bool isballInArea1 = false;

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    public void Paddle1Scored()
    {
        paddle1Score++;
        paddle1ScoreText.text = paddle1Score.ToString();
        Restart();
    }
    public void Paddle2Scored()
    {
        paddle2Score++;
        paddle2ScoreText.text = paddle2Score.ToString();
        Restart();
    }

    public void Restart()
    {
        // Reinicia las posiciones de los paddles y la bola
        paddle1Transform.position = new Vector2(paddle1Transform.position.x, 0);
        paddle2Transform.position = new Vector2(paddle2Transform.position.x, 0);
        ballTransform.position = new Vector2(0, 0);

        // Detiene la bola
        Rigidbody2D ballRb = ballTransform.GetComponent<Rigidbody2D>();
        if (ballRb != null)
        {
            ballRb.velocity = Vector2.zero;
        }

        // Llama a Launch después de un retraso de 1 segundo
        Invoke(nameof(LaunchBall), 1f);
    }

    private void LaunchBall()
    {
        Ball ballScript = ballTransform.GetComponent<Ball>();
        if (ballScript != null)
        {
            ballScript.Launch();
        }
    }

    internal bool IsballInArea1()
    {
        MiddleCrossed();
        return isballInArea1;
    }

    internal void setIsballInArea1(bool hasCrossedMiddle)
    {
        isballInArea1 = hasCrossedMiddle;
    }

    internal void MiddleCrossed()
    {
        //setIsballInArea1(!isballInArea1);
        // Comprueba en qué área está la bola según su posición en el eje X
        isballInArea1 = ballTransform.position.x > 0;
    }
}
