using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    Node moveNode = null;
    public void setMoveNode(Node node)
    {
        Debug.Log(node.ToString());
        moveNode = node;
    }


    float speed = 5;

    void Update()
    {

        
        if (moveNode!=null)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, moveNode.transform.position, step);
            if (Vector3.Distance(transform.position, moveNode.transform.position) < 0.01f)
            {
                setMoveNode(moveNode.getRandomPlayerPath());
            }
        }
            
    }
}
