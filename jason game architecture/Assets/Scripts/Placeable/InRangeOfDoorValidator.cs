using UnityEngine;

public class InRangeOfDoorValidator : MonoBehaviour, IValidatePlacement
{
    [SerializeField] float range = 10f;

    public bool IsValid()
    {
        return (Vector3.Distance(transform.position, FindObjectOfType<GameEventListener>().transform.position) < range);
    }
}