using UnityEngine;

public class GameFlagTriggerAreaForIntFlags : MonoBehaviour
{
    [SerializeField] int amount;
    [SerializeField] IntGameFlag intGameFlag;

    private void OnTriggerEnter(Collider other)
    {
        intGameFlag.Modify(amount);
    }
}
