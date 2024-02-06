using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockSprite : MonoBehaviour
{
    [SerializeField] private Sprite[] _wallSprites;
    private SpriteRenderer _sprite;
    private int qqq;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (_wallSprites != null)
            _sprite.sprite = _wallSprites[Random.Range(0, _wallSprites.Length)];

    }
}
