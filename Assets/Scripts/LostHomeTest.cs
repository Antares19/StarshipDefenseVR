using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostHomeTest : MonoBehaviour
{    
    public Transform home;




    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump"))
        {
            Vector3 pos = home.transform.localPosition;
            pos.z = 0.5f;
            home.transform.localPosition = pos;
            Messenger.Broadcast(HomeEvent.lostHome);

        }
    }
}


