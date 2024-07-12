using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRender : MonoBehaviour
{

    private LineRenderer line;
    private UnityEngine.AI.NavMeshAgent agent;
    public Color[] colors = new Color[] {
    Color.red,
    Color.green,
    Color.blue,
    Color.yellow,
    Color.magenta,
    Color.cyan
};

    public Transform target;
    public float heightOffset = 0.5f;
    public Material lineMaterial;



    void Start(){
        line = GetComponent<LineRenderer>();
        line.material = lineMaterial;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        StartCoroutine(GetPath());
        }

    private IEnumerator GetPath(){
        Vector3 raisedStartPosition = new Vector3(transform.position.x, transform.position.y + heightOffset, transform.position.z);
        line.SetPosition(0, raisedStartPosition);
        agent.SetDestination(target.position);
        // Wait until the path is calculated
        while (agent.pathPending) {
            yield return new WaitForSeconds(.5f);
        }     
           
        DrawPath(agent.path);

        // agent.Stop(); add this if you don't want to move the agent
    }

    void DrawPath(UnityEngine.AI.NavMeshPath path){

        if(path.corners.Length < 2) {
            Debug.LogWarning("Path has no corners, perhaps incorrect calculation?");
            return;
        }
        line.positionCount = path.corners.Length; //set the array of positions to the amount of corners
        for(var i = 1; i < path.corners.Length; i++){
            // change colour of line renderer
            Color cornerColor = colors[i % colors.Length];
            line.startColor = cornerColor;
            line.endColor = cornerColor;
            Vector3 raisedPosition = new Vector3(path.corners[i].x, path.corners[i].y + heightOffset, path.corners[i].z);
            line.SetPosition(i, raisedPosition); // Set the raised position for each corner
        }
    }
    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, agent.transform.position);
        DrawPath(agent.path);
    }
}
