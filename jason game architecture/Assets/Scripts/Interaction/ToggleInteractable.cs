using UnityEngine;

public class ToggleInteractable : Interactable
{
    [SerializeField] InteractionType toggledInteractionType;
    bool toggleState => data.interactionCount % 2 == 1 ? true : false;

    public override InteractionType InteractionType => toggleState ? toggledInteractionType : interactionType;

    /*
    protected override void AfterCompleteInteraction()
    {
        data.TimeInteracted = 0f;
    }
    */

    protected override void OnBound()
    {
        if(toggleState) 
        {
            RestoreInteractionText();
        }
    }
}
