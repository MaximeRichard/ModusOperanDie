using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour
{
    private enum PickUpType
    {
        Weapon,
        Signature,
        Victim
    };
    private PickUpType Type;
    private string Name;
    public float horizontalSpeed;
    public float verticalSpeed;
    public float amplitude;
    private Vector3 tempPosition;
    // Use this for initialization
    void Start()
    {
        tempPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tempPosition.x += horizontalSpeed;
        tempPosition.y = Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed) * amplitude;
        transform.position = tempPosition;
    }

    void OnMouseDown()
    {
        Destroy(gameObject);
    }

}
