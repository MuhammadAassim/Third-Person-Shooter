using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player; // Player Ka Transform Set Karna
    [SerializeField] private Vector3 offset; // Camera Ka Offset Set Karna

    void FixedUpdate()
    {
        // Agar Player Null Nahi Hai Toh Camera Ko Player Ki Position Par Set Karna
        if (player != null)
        {
            transform.position = player.position + offset; // Player Ki Position Mein Offset Add Karna Aur Camera Ko Update Karna
        }
    }
}
