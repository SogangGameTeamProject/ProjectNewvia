using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    [CreateAssetMenu(fileName = "New WavesInfo", menuName = "WavesInfo/Create WavesInfo", order = 0)]
    public class WavesInfo : ScriptableObject
    {
        public List<WaveLevel> wave;
    }
}

