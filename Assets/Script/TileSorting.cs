using UnityEngine;
using UnityEngine.Rendering;

public class TileSorting : MonoBehaviour
{
    private void Start()
    {
        var sortingGroup = GetComponent<SortingGroup>();
        sortingGroup.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
    }
}