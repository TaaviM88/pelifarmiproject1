using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform player;
    float offsetX;
    float offsetY;
    public float _tmpCamera;
    // Use this for initialization
    void Start()
    {
        GameObject player_go = GameObject.FindGameObjectWithTag("Player");
        if (player_go == null)
        {
            return;
        }

        player = player_go.transform;
        offsetX = transform.position.x - player.position.x;
        offsetY = transform.position.y - player.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector3 _tmp = Camera.main.WorldToScreenPoint(new Vector3(player.position.x, player.position.y, 0));
            Vector3 pos = transform.position;

            //Mahdollisesti aiheuttaa sen että kamera jumittuu väärälle korkeudelle kuoleman jälkee, ei vielä testattu.
            if (player.position.y > Screen.height * 0.25f)
            {
                pos.y = player.position.y + offsetY;
            }
            pos.x = player.position.x + offsetX;
            transform.position = pos;
        }
        if (player == null)
        {
            return;
        }
    }
    void GoToPlayer()
    {
        Vector3 po = transform.position;
        po.x = player.position.x + offsetX;
        //po.y = player.position.y + offsetY;
        transform.position = po;
    }
}