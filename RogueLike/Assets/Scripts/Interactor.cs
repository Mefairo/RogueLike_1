using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Transform InteractionPoint;
    public LayerMask InteractionLayer;
    public float InteractionPointRadius = 1f;
    public bool IsInteracting { get; private set; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            CheckIntecractObjectAround();
    }

    private void StartInteraction(IInteractable interactable)
    {
        interactable.Interact(this, out bool interactSuccessful);
        IsInteracting = true;
    }

    private void EndInteracting()
    {
        IsInteracting = false;
    }

    private void CheckIntecractObjectAround()
    {
        var colliders = Physics2D.OverlapCircleAll(InteractionPoint.position, InteractionPointRadius, InteractionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            var interactable = colliders[i].GetComponent<IInteractable>();

            if (interactable != null)
                StartInteraction(interactable);
        }
    }

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    Debug.Log("trigger");
    //    // Проверяем, вышел ли игрок из триггера объекта CraftKeeper
    //    if (other.CompareTag("NPC"))
    //    {
    //        Debug.Log("inter");
    //        // Закрываем панель крафта, если игрок отошел от объекта CraftKeeper
    //        EndInteracting();
    //    }
    //}

}
