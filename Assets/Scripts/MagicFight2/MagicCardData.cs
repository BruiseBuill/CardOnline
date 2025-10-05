using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicFighting2
{
    [CreateAssetMenu(fileName = "MagicCardData", menuName = "Self/Magic2/MagicCardData")]
    public class MagicCardData : ScriptableObject
	{
        new public string name;
        public string power;
        public string coolDown;
        public string description;
    }
}