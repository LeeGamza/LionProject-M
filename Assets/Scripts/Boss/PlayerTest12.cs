using UnityEngine;

public class PlayerTest12 : MonoBehaviour
{
    float speed;
    void Start()
    {
        speed = 5.0f;
        
    }

    void Update()
    {
        float px = Input.GetAxisRaw("Horizontal");
        float py = Input.GetAxisRaw("Vertical");

        transform.Translate(new Vector3(px, py, 0) * speed * Time.deltaTime);

    }
}
