using UnityEngine;

[CreateAssetMenu(menuName = "Minigame/Flippy Box Settings", fileName = "FlippyBoxSettings-")]
public class FlippyBoxSettings : MinigameSettings
{
    public float MovingBlockSpeed = 5f;
    public Vector2 JumpVelocity = Vector2.up + Vector2.right;
    public float GrowTime = 20f;
    public Color PlayerColor = Color.green;
}
