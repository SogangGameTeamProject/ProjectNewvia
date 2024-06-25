using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    [CreateAssetMenu(fileName = "New Chacter", menuName = "Status/Create Character", order = 0)]
    public class CharacterStatus : ScriptableObject
    {
        public string characterName = "";
        public int hp = 1;
        public float moveSpeed = 10;
        public float power = 1;
    }
}