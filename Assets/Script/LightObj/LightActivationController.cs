using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class LightActivationController : MonoBehaviour
    {
        private void OnTriggerExit2D(Collider2D other)
        {
            other.GetComponent<ILight>().OutLight();
        }
    }
}

