using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    private void Update()
    {
        RestartCode();
    }
    void RestartCode()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            player.transform.position = new Vector2(0, 2);
        }
    }
}
