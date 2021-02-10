using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{

    public enum MovementType
    {
        MoveTowards,
        LerpTowards
    }


    public MovementType Type = MovementType.MoveTowards;
    public MovementPath MyPath;
    public float Speed = 1;
    public float MaxDistanceToGoal = .1f;
   


    private IEnumerator<Transform> pointInPath;


    // Start is called before the first frame update
    void Start()
    {
        if (MyPath == null)
        {
            Debug.LogError("Movement Path cannot be null", gameObject);
            return;
        }

        pointInPath = MyPath.GetNextPathPoint();
        pointInPath.MoveNext();

        if (pointInPath.Current == null)
        {
            Debug.LogError("Path needs points", gameObject);
            return;
        }

        transform.position = pointInPath.Current.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(pointInPath == null || pointInPath == null)
        {
            return;
        }

        if (Type == MovementType.MoveTowards)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointInPath.Current.position, Time.deltaTime * Speed);
        }
        if (Type == MovementType.LerpTowards)
        {
            transform.position = Vector3.Lerp(transform.position, pointInPath.Current.position, Time.deltaTime * Speed);
        }


        var distanceSquared = (transform.position - pointInPath.Current.position).sqrMagnitude;
        if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
        {
            pointInPath.MoveNext();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(null);
        }
    }
}
