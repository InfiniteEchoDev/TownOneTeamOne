using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowShovervfx : MonoBehaviour
{
    [SerializeField]
    GameObject vfx;

    public void destroySnow()
    {
        Instantiate(vfx, transform.position, Quaternion.identity);
    }
}
