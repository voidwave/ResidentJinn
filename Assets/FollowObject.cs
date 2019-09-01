using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{

    public Transform objectToFollow;
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = objectToFollow.localPosition;
        pos.y = 8f;
        transform.localPosition = pos;
    }
}
