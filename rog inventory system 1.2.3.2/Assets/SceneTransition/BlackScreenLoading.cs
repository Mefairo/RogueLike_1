using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavMeshPlus.Components;

public class BlackScreenLoading : MonoBehaviour
{
    [SerializeField] private NavMeshSurface navMeshSurface;

    private Animator _componentAnimator;

    private void Start()
    {
        _componentAnimator = GetComponent<Animator>();

        StartCoroutine(ScreenLoading());
    }

    private IEnumerator ScreenLoading()
    {
        _componentAnimator.SetTrigger("sceneClosing");

        yield return new WaitForSeconds(2f);

        navMeshSurface.BuildNavMesh();

        _componentAnimator.SetTrigger("sceneOpening");
    }

    public void OnAnimationOver()
    {

    }
}
