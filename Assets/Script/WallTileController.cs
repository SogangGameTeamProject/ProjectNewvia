using UnityEngine;
using UnityEngine.Tilemaps;

public class WallTileController : MonoBehaviour
{
    public Tilemap wallTilemap;

    void Start()
    {
        ApplyWallLogic();
    }

    void ApplyWallLogic()
    {
        foreach (var pos in wallTilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            if (wallTilemap.HasTile(localPlace))
            {
                Vector3 worldPosition = wallTilemap.CellToWorld(localPlace);
                ApplyYBasedLogic(worldPosition, localPlace);
            }
        }
    }

    void ApplyYBasedLogic(Vector3 worldPosition, Vector3Int cellPosition)
    {
        // Y축의 위치에 따라 다른 로직을 적용합니다.
        if (worldPosition.y > 0)
        {
            // 예: Y축이 0보다 큰 경우 다른 색을 적용
            var tile = wallTilemap.GetTile<Tile>(cellPosition);
            // 타일을 특정 색으로 변경
            tile.color = Color.red;
            wallTilemap.RefreshTile(cellPosition);
        }
        else if (worldPosition.y <= 0)
        {
            // Y축이 0보다 작은 경우 다른 로직 적용
            var tile = wallTilemap.GetTile<Tile>(cellPosition);
            // 타일을 다른 색으로 변경
            tile.color = Color.blue;
            wallTilemap.RefreshTile(cellPosition);
        }
    }
}