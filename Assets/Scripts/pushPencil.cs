using UnityEngine;

public class pushPencil : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
