using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject player;
    private bool error = false;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) 
        {
            Debug.LogError("ERROR: No Player Found!");
            error = true;
        }
    }

    private void Update()
    {
        if (!error)
        {
            transform.position = player.transform.position;
        }
    }
}
