using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOverTime : MonoBehaviour
{
    [SerializeField] Vector3 magnitude = Vector3.down;

    Vector3 startingPosition;
    Vector3 endingPosition;

    float elapsed;

    void Awake() => startingPosition = transform.position;

    void OnEnable()
    {
        elapsed = 0f;
        endingPosition = startingPosition + magnitude;
    }

    void OnDisable()
    {
        transform.position = startingPosition;    
    }

    void Update()
    {
        elapsed += Time.deltaTime;    
        transform.position = Vector3.Lerp(startingPosition, endingPosition, elapsed);   
    }
}
