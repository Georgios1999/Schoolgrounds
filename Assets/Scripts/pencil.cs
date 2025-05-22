using UnityEngine;
using Unity.Netcode;

public class pencil : NetworkBehaviour
{
    public GameObject obj;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(obj, transform.position, transform.rotation);
        }
    }
}
