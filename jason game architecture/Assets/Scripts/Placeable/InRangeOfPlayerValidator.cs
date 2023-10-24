﻿using UnityEngine;

public class InRangeOfPlayerValidator : MonoBehaviour, IValidatePlacement
{
    [SerializeField] float range = 10f;

    public bool IsValid()
    {
        return (Vector3.Distance(transform.position, FindObjectOfType<ThirdPersonMover>().transform.position) < range);
    }
}
