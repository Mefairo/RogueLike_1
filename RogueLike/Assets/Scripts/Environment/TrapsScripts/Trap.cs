using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Environment.TrapsScripts
{
    internal class Trap: MonoBehaviour
    {
        [SerializeField] private EntityStats _trapStats;

        public EntityStats TrapStats => _trapStats;
    }
}
