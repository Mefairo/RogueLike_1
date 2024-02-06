using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffList : MonoBehaviour
{
    [SerializeField] private List<Debuff_UI> _debuffs = new List<Debuff_UI>();

    public List<Debuff_UI> Debuffs => _debuffs;
}
