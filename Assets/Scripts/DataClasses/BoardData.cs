using System.Collections.Generic;
using UnityEngine;

namespace DataClasses
{
    public class BoardData
    {
        public readonly int BoardSpriteID;
        public readonly int[,] NormalItemIds;
        public readonly Vector3 BoardPosition;
        public readonly Dictionary<Vector2Int, int> UnderlayItemIds;
        public readonly Dictionary<Vector2Int, int> OverlayItemIds;

        public BoardData(int boardSpriteID, int[,] normalItemIds, Vector3 boardPosition, Dictionary<Vector2Int, int> underlayItemIds, Dictionary<Vector2Int, int> overlayItemIds)
        {
            BoardSpriteID = boardSpriteID;
            NormalItemIds = normalItemIds;
            BoardPosition = boardPosition;
            UnderlayItemIds = underlayItemIds;
            OverlayItemIds = overlayItemIds;
        }
    }
}