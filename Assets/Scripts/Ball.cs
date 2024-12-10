using UnityEngine;

public class Ball : MonoBehaviour
{
    public enum MovementType
    {
        Basic,
        Linear,
        Exponential
    }

    [SerializeField] private MovementType movementType = MovementType.Basic;
    [SerializeField] private float initialVelocity = 4f;
    [SerializeField] private float velocityMultiplier = 1.1f;

    private Rigidbody2D ballRb;

    // Start is called before the first frame update
    void Start()
    {
        ballRb = GetComponent<Rigidbody2D>();
        Launch();
    }

    private void Launch()
    {
        // Genera direcciones aleatorias para X y Y
        float xVelocity = Random.Range(0, 2) == 0 ? 1 : -1;
        float yVelocity = Random.Range(0, 2) == 0 ? 1 : -1;

        // Determina el área según la dirección generada
        bool isInArea1 = xVelocity > 0; // Si xVelocity > 0, irá hacia el área derecha (Área 1)
        GameManager.Instance.setIsballInArea1(isInArea1);

        Debug.Log($"Initial Direction: X Velocity: {xVelocity}, Y Velocity: {yVelocity}, In Area 1: {isInArea1}");

        // Aplica la velocidad inicial
        ballRb.velocity = new Vector2(xVelocity, yVelocity).normalized * initialVelocity;
    }

    private void Update()
    {
        // Update velocity based on movement type
        switch (movementType)
        {
            case MovementType.Basic:
                // Basic movement, no additional changes
                break;

            case MovementType.Linear:
                // Keep the velocity constant with multiplier growth
                ballRb.velocity = ballRb.velocity.normalized * initialVelocity;
                break;

            case MovementType.Exponential:
                // Apply exponential scaling to the velocity
                // Incremento de velocidad basado en el tiempo transcurrido
                float speed = ballRb.velocity.magnitude;
                float exponentialFactor = Mathf.Pow(1 + velocityMultiplier, Time.deltaTime);
                ballRb.velocity = ballRb.velocity.normalized * speed * exponentialFactor;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            switch (movementType)
            {
                case MovementType.Basic:
                    ballRb.velocity *= velocityMultiplier;
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Goal_1"))
        {
            GameManager.Instance.Paddle2Scored();
        }

        if (collision.gameObject.CompareTag("Goal_2"))
        {
            GameManager.Instance.Paddle1Scored();
        }

        //if (collision.gameObject.CompareTag("Middle"))
        //{
        //    GameManager.Instance.MiddleCrossed();
        //}
    }
}
