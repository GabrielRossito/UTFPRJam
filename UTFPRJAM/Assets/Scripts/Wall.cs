            using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Wall colision");
    }

}
