using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{



    [SerializeField] private List<GameObject> _nodesToHome;

    [SerializeField] private List<GameObject> _nodesToPlayer;
    [SerializeField] private bool isHome;
    //[SerializeField] private int _closestHouses;
    //[SerializeField] private 



    private Transform[] nodesToHomeTransform;
    private Transform[] nodesToPlayerTransform;
    


    public Node getRandomHomePath()
    {
        return _nodesToHome[UnityEngine.Random.Range(0, _nodesToHome.Count)].GetComponent<Node>();
    }

    public Node getRandomPlayerPath()
    {
        return _nodesToPlayer[UnityEngine.Random.Range(0, _nodesToPlayer.Count)].GetComponent<Node>();
    }
    // Start is called before the first frame update
    void Start()
    {
        nodesToHomeTransform = new Transform[_nodesToHome.Count];
        for (int i = 0; i < _nodesToHome.Count; i++)
            nodesToHomeTransform[i] = _nodesToHome[i].transform;


        nodesToPlayerTransform = new Transform[_nodesToPlayer.Count];
        for (int i = 0; i < _nodesToPlayer.Count; i++)
            nodesToPlayerTransform[i] = _nodesToPlayer[i].transform;




    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
