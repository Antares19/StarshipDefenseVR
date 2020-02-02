using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    Node moveNode = null;
    Node oldNode = null;

    NodeBuilding moveBuilding = null; 


    
    public void setMoveNode(Node node)
    {
        moveNode = node;
        moveBuilding = null;
    }


    public void setMoveBuilding(NodeBuilding building)
    {
        oldNode = moveNode;
        moveNode = null;
        moveBuilding = building;

    }


    public void pickBuilding()
    {

    }

    double speed = 5*0.06;

    void Update()
    {

        
        if (moveNode!=null)
        {
            double step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, moveNode.transform.position, (float)step);
            if (Vector3.Distance(transform.position, moveNode.transform.position) < 0.01f)
            {

                if (UnityEngine.Random.Range(0,3)>1)
                {
                    NodeBuilding b = moveNode.getRandomBuildingPath();

                    if (b.OnBase())
                    {
                        setMoveBuilding(b);
                    }
                }
                else
                {
                    setMoveNode(moveNode.getRandomPlayerPath());
                }
                
            }
        }


        if (moveBuilding != null)
        {
            double step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, moveBuilding.transform.position, (float)step);

            if (Vector3.Distance(transform.position, moveBuilding.transform.position) < 0.05f)
            {
                pickBuilding();
            }

        }
    }
}
