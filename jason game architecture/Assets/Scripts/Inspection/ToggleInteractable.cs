using UnityEngine;

public class ToggleInteractable : Interactable
{
    [SerializeField] InteractionType toggledInteractionType;
    bool toggleState;

    public override InteractionType InteractionType => toggleState ? toggledInteractionType : interactionType;

    protected override void CompleteInteraction()
    {
        SendInteractionComplete();
        toggleState = !toggleState;
        data.TimeInteracted = 0f;
    }
}
