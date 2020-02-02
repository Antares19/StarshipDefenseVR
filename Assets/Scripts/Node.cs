using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class DrawArrow
{
    public static void ForGizmo(Vector3 pos, Vector3 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
    {
        Gizmos.DrawRay(pos, direction);

        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
        Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
        Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
        Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
    }

    public static void ForGizmo(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
    {
        Gizmos.color = color;
        Gizmos.DrawRay(pos, direction);

        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
        Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
        Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
        Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
    }

    public static void ForDebug(Vector3 pos, Vector3 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
    {
        Debug.DrawRay(pos, direction);

        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
        Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
        Debug.DrawRay(pos + direction, right * arrowHeadLength);
        Debug.DrawRay(pos + direction, left * arrowHeadLength);
    }
    public static void ForDebug(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
    {
        Debug.DrawRay(pos, direction, color);

        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
        Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
        Debug.DrawRay(pos + direction, right * arrowHeadLength, color);
        Debug.DrawRay(pos + direction, left * arrowHeadLength, color);
    }
}


public class Node : MonoBehaviour
{



    [SerializeField] private List<GameObject> _nodesToHome;

    [SerializeField] private List<GameObject> _nodesToPlayer;
    [SerializeField] private List<GameObject> _buildings;
    [SerializeField] private bool isHome;
    //[SerializeField] private int _closestHouses;
    //[SerializeField] private 



    private Transform[] nodesToHomeTransform;
    private Transform[] nodesToPlayerTransform;
    private Transform[] buildingsTransform;



    public Node getRandomHomePath()
    {
        return _nodesToHome[UnityEngine.Random.Range(0, _nodesToHome.Count)].GetComponent<Node>();
    }

    public Node getRandomPlayerPath()
    {
        return _nodesToPlayer[UnityEngine.Random.Range(0, _nodesToPlayer.Count)].GetComponent<Node>();
    }

    public NodeBuilding getRandomBuildingPath()
    {
        return _buildings[UnityEngine.Random.Range(0, _buildings.Count)].GetComponent<NodeBuilding>();
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




        _buildings = new List<GameObject>();

        foreach (Transform child in GameObject.Find("Buildings_Group").transform)
            _buildings.Add(child.gameObject);

        _buildings.Sort(delegate (GameObject a, GameObject b)
        {
            return Vector2.Distance(this.transform.position, a.transform.position)
            .CompareTo(
              Vector2.Distance(this.transform.position, b.transform.position));
        });


        _buildings.RemoveRange(2, _buildings.Count - 2);

        /*
        nodesToPlayerTransform = new Transform[3];
        _nodesToPlayer = new List<GameObject>();

        



        List<Transform> others = new List<Transform>();

        foreach (Transform child in transform.parent.gameObject.transform)
            others.Add(child);

            others.Sort(delegate (Transform a, Transform b)
        {
            return Vector2.Distance(this.transform.position, a.position)
            .CompareTo(
              Vector2.Distance(this.transform.position, b.position));
        });


        others.RemoveAt(0);
        int j = 0;
        foreach (Transform child in others)
        {
            if (j<3)
                {

                nodesToPlayerTransform[j] = child.transform;
                _nodesToPlayer.Add(child.transform.gameObject);
                j++;
                }
        }
        */




    }

    // Update is called once per frame
    void Update()
    {
        foreach (var v in nodesToPlayerTransform)
            DrawArrow.ForDebug(transform.position+new Vector3(0,0.03f,0), v.position - transform.position + new Vector3(0, 0.03f, 0), Color.yellow,0.05f);


        foreach (var v in nodesToHomeTransform)
            DrawArrow.ForDebug(transform.position + new Vector3(0.01f, 0.04f, 0), v.position - transform.position + new Vector3(0.01f, 0.04f, 0), Color.blue, 0.05f);
    }
}
