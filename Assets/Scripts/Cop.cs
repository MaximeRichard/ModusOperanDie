using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cop : MonoBehaviour {
    public List<Vector2> points;
    public float Speed;
    Vector3 pointA;
    IEnumerator Start()
    {
        pointA = transform.position;
        while (true)
        {
            points.Reverse();
            foreach (Vector2 point in points)
            {
                yield return StartCoroutine(MoveObject(transform, pointA, point, Speed));
                Debug.Log(point);
                
            }
            points.Reverse();
            foreach (Vector2 point in points)
            {
                yield return StartCoroutine(MoveObject(transform, pointA, point, Speed));
                Debug.Log(point);
            }
            
        }
    }

    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector2 endPos, float time)
    {
        
        float i = 0.0f;
        float rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
        pointA = endPos;
    }

}
