using System.Linq;
using UnityEngine;

public class InRangeOfDoorValidator : MonoBehaviour, IValidatePlacement
{
    [SerializeField] float range = 10f;
    GameObject[] results;

    public bool IsValid()
    {
        results = GameObject.FindGameObjectsWithTag("Door");

        return (results.Any(door => Vector3.Distance(transform.position, door.transform.position) < range));
    }
}