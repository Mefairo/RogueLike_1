using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Test1 : MonoBehaviour
{
    public Image qwd;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            qwd.color = qwd.color.WithAlpha(0);
            //_backgroundSprite.color = _backgroundSprite.color.WithAlpha(0);
        }
    }
}
