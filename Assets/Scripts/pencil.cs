using UnityEngine;
using Unity.Netcode;

public class pencil : NetworkBehaviour
{
    public GameObject obj;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(obj, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
        }
    }
}
