using UnityEngine;

public class PlaceableColliderValidator : MonoBehaviour, IValidatePlacement
{
    BoxCollider collider;
    Collider[] results = new Collider[100];

    public Collider CurrentlyBlockedBy;

    void Awake()
    {
        collider = GetComponent<BoxCollider>();    
    }

    public bool IsValid()
    {
        int hits = Physics.OverlapBoxNonAlloc(collider.transform.position + collider.center,
            collider.bounds.extents,
            results,
            collider.transform.rotation);

        CurrentlyBlockedBy = null;

        for (int i = 0; i < hits; i++)
        {
            if (results[i].transform.IsChildOf(transform))
            {
                continue;
            }

            CurrentlyBlockedBy = results[i];

            return false;
        }

        return true;
    }
}