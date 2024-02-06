using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class NextLevelTransfer : MonoBehaviour, IInteractable
{
    public UnityAction OnCountRoundComplete { get; set; }
    public UnityAction<IInteractable> OnInteractionComplete { get; set; }

    private void Start()
    {
        this.gameObject.SetActive(false);
    }


    public void Interact(Interactor interactor, out bool interactSuccessful)
    {
        OnCountRoundComplete?.Invoke();
        interactSuccessful = true;
    }


    public void EndInteraction()
    {
        throw new System.NotImplementedException();
    }
}
