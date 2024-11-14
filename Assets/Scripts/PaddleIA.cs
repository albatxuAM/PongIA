using UnityEngine;

public class PaddleIA : MonoBehaviour
{
    [SerializeField] private float speed = 7f;

    //limite en y
    private float yBound = 3.75f;

    [SerializeField] private GameObject ball;

    void Update()
    {
        //float movement = Input.GetAxisRaw("Vertical");
        //transform.position += new Vector3(0, movement * speed * Time.deltaTime);

        //Vector2 paddlePosition = transform.position;

        //paddlePosition.y = Mathf.Clamp(paddlePosition.y + movement * speed * Time.deltaTime, -yBound, yBound);
        //transform.position = paddlePosition;

        ChaseBall();
    }

    private void ChaseBall()
    {
        if (ball != null)
        {
            float ballPositionY = ball.transform.position.y;
            float paddlePositionY = transform.position.y;

            float distance = ballPositionY - paddlePositionY;

            // Normalizar la diferencia (movernos solo hacia la pelota sin superar el límite)
            if (Mathf.Abs(distance) > 0.1f)
            {
                float movement = Mathf.Sign(distance) * speed * Time.deltaTime;
                float newPaddlePositionY = paddlePositionY + movement;

                // Limitamos la posición de la pala para que no se salga de los bordes
                newPaddlePositionY = Mathf.Clamp(newPaddlePositionY, -yBound, yBound);

                // Asignamos la nueva posición a la pala
                transform.position = new Vector3(transform.position.x, newPaddlePositionY, transform.position.z);
            }
        }
    }
}
