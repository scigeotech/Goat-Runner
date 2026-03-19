using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float edgeX; //x position at which the obstacle will be destroyed
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        edgeX = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 2f; //set the x position at which the obstacle will be destroyed
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * GameManager.Instance.gameSpeed * Time.deltaTime; //move the obstacle left based on game speed

        if (transform.position.x < edgeX) //destroy the obstacle if it goes off screen
        {
            Destroy(gameObject); 
        }
    }
}
