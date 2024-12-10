using UnityEngine;

public class PaddleIA : MonoBehaviour
{
    [SerializeField] private float speed = 7f;

    //limite en y
    private float yBound = 3.75f;

    [SerializeField] private GameObject ball;

    void Update()
    {
        if (GameManager.Instance.IsballInArea1())
        {
            ChaseBall();
            //Debug.Log("en area 1");
        }
        else
        {
            //Debug.Log("en area 2");
            MoveToCenter();
        }
    }

    private void MoveToCenter()
    {
        float paddlePosCenterY = 0;
        MoveToY(paddlePosCenterY);
    }

    private void ChaseBall()
    {
        //    if (ball != null)
        //    {
        //        float ballPositionY = ball.transform.position.y;
        //        float paddlePositionY = transform.position.y;

        //        float distance = ballPositionY - paddlePositionY;

        //        // Normalizar la diferencia (movernos solo hacia la pelota sin superar el límite)
        //        if (Mathf.Abs(distance) > 0.1f)
        //        {
        //            float movement = Mathf.Sign(distance) * speed * Time.deltaTime;
        //            float newPaddlePositionY = paddlePositionY + movement;

        //            // Limitamos la posición de la pala para que no se salga de los bordes
        //            newPaddlePositionY = Mathf.Clamp(newPaddlePositionY, -yBound, yBound);

        //            // Asignamos la nueva posición a la pala
        //            transform.position = new Vector3(transform.position.x, newPaddlePositionY, transform.position.z);
        //        }
        //    }
        float ballPositionY = ball.transform.position.y;
        MoveToY(ballPositionY);
    }

    private void MoveToY(float posY)
    {
        if (ball != null)
        {
            float paddlePositionY = transform.position.y;

            float distance = posY - paddlePositionY;

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