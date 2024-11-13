using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float speed = 7f;

    //limite en y
    private float yBound = 3.75f;

    void Update()
    {
        float movement = Input.GetAxisRaw("Vertical");
        //transform.position += new Vector3(0, movement * speed * Time.deltaTime);

        Vector2 paddlePosition = transform.position;

        paddlePosition.y = Mathf.Clamp(paddlePosition.y + movement * speed * Time.deltaTime, -yBound, yBound);
        transform.position = paddlePosition;
    }
}
