using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarRotater : MonoBehaviour
{
    void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }
}
