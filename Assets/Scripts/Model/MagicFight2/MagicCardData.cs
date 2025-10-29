using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardOnline.Card
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