using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheater : MonoBehaviour
{
    private bool invincible = false;

    CollisionHandler ch;

    public bool IsInvincible()
    {
        return invincible;
    }

    private void Start()
    {
        ch = GetComponent<CollisionHandler>();
    }

    void Update()
    {
        ProcessCheats();
    }

    private void ProcessCheats()
    {
        // Next level cheat "L"
        if (Input.GetKey(KeyCode.L))
        {
            ch.NextLevel();
        }

        // Invincibility cheat "C"
        if (Input.GetKeyUp(KeyCode.C))
        {
            EnableInvincibility();
        }
    }

    private void EnableInvincibility()
    {
        invincible = !invincible;
    }
}
