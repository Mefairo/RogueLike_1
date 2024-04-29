using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPrefabData : MonoBehaviour
{
    [SerializeField] private ItemPickUp _itemPickUp;
    [SerializeField] private SpriteRenderer _backGround;
    [SerializeField] private EquipSlot _equipSlot;
    [SerializeField] private List<EquipSlotStatsList> _statsList;

    public EquipSlot EquipSlot => _equipSlot;

    private void Awake()
    {
        _equipSlot.ItemData = _itemPickUp.ItemData;
        SetColorBackGround();
        //_equipSlot._itemTier = _itemPickUp.ItemData;
    }

    private void Update()
    {
        //if (Input.GetKeyUp(KeyCode.Y))
        //{
        //    SetColorBackGround();
        //}

        //if (Input.GetKeyUp(KeyCode.U))
        //{
        //    Debug.Log(_equipSlot);
        //}
    }

    private void SetColorBackGround()
    {
        switch (_equipSlot.ItemTier)
        {
            case 0:
                _backGround.color = Color.white;
                break;

            case 1:
                _backGround.color = Color.green;
                break;

            case 2:
                _backGround.color = Color.blue;
                break;

            case 3:
                _backGround.color = Color.yellow;
                break;

            case 4:
                _backGround.color = Color.red;
                break;
        }
    }
}
