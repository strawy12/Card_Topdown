using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UtilDefine;

public class WorldScrolling : MonoBehaviour
{
    [SerializeField] float tileSize = 20f;
    [SerializeField] Vector2Int currentTilePos = new Vector2Int(0, 0); // 현재 선택된 타일의 위치
    [SerializeField] Vector2Int onTileGridPlayerPos;
    [SerializeField] Vector2Int playerTilePos;
    [field:SerializeField] GameObject[,] terrainTiles;  // 타일들


    [SerializeField] int terrainTileHorizontalCount; // 가로 갯수
    [SerializeField] int terrainTileVerticalCount;   // 세로 갯수

    [SerializeField] int fieldOfVisionHeight = 3;
    [SerializeField] int fieldOfVisionWidth = 3;

    private void Awake()
    {
        terrainTiles = new GameObject[terrainTileHorizontalCount, terrainTileVerticalCount];
    }

    private void Update()
    {
        playerTilePos.x = (int)(PlayerRef.transform.position.x / tileSize);
        playerTilePos.y = (int)(PlayerRef.transform.position.y / tileSize);

        playerTilePos.x -= PlayerRef.transform.position.x < 0 ? 1 : 0;
        playerTilePos.y -= PlayerRef.transform.position.y < 0 ? 1 : 0;

        if (currentTilePos != playerTilePos)
        {
            currentTilePos = playerTilePos;

            onTileGridPlayerPos.x = CalculatePositionOnAxis(onTileGridPlayerPos.x, true);
            onTileGridPlayerPos.y = CalculatePositionOnAxis(onTileGridPlayerPos.y, false);
            UpdateTileOnScreen();
        }
    }

    private void UpdateTileOnScreen()
    {
        for (int pov_x = -(fieldOfVisionWidth/2); pov_x < fieldOfVisionWidth; pov_x++)
        {
            for (int pov_y = -(fieldOfVisionHeight/2); pov_y < fieldOfVisionHeight; pov_y++)
            {
                int tileToUpdate_x = CalculatePositionOnAxis(playerTilePos.x + pov_x, true);
                int tileToUpdate_y = CalculatePositionOnAxis(playerTilePos.y + pov_y, false);

                GameObject tile = terrainTiles[tileToUpdate_x, tileToUpdate_y];
                tile.transform.position = CalculateTilePosition(
                    playerTilePos.x + pov_x,
                    playerTilePos.y + pov_y);
            }
        }
    }

    private Vector3 CalculateTilePosition(int x, int y)
    {
        return new Vector3(x * tileSize, y * tileSize, 0f);
    }

    private int CalculatePositionOnAxis(float currentValue, bool horizontal)
    {
        if (horizontal)
        {
            if (currentValue >= 0)
            {
                currentValue = currentValue % terrainTileHorizontalCount;
            }
            else
            {
                currentValue += 1;
                currentValue = terrainTileHorizontalCount -1  + currentValue % terrainTileHorizontalCount;
            }
        }
        else
        {
            if (currentValue >= 0)
            {
                currentValue = currentValue % terrainTileVerticalCount;
            }
            else
            {
                currentValue += 1;
                currentValue = terrainTileVerticalCount -1 + currentValue % terrainTileVerticalCount;
            }
        }

        return (int)currentValue;
    }

    public void Add(GameObject tileGameObject, Vector2Int tilePosition)
    {
        terrainTiles[tilePosition.x, tilePosition.y] = tileGameObject;
    }
}
