using UnityEngine;
using Unity.Netcode;
using System.Collections;

public class pushPencil : NetworkBehaviour
{
    public float speed;

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        Destroy(this.gameObject, 20);
    }
}
