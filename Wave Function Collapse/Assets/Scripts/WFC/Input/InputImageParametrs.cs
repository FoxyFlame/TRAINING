using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace WaveFunctionCollapse
{
    public class InputImageParametrs
    {
        Vector2Int? bottorRightTileCords = null;
        Vector2Int? topLeftTileCords = null;
        BoundsInt inputTileMapBounds;
        TileBase[] inputTileMapTilesArray;
        Queue<TileContainer> stackOfTiles = new Queue<TileContainer>();
        private int width = 0, height = 0;
        private Tilemap inputTilemap;

        public Queue<TileContainer> StackOfTiles { get => stackOfTiles; set => stackOfTiles = value; }
        public int Width { get => width;}
        public int Height { get => height;}

        public InputImageParametrs(Tilemap inputTilemap)
        {
            this.inputTilemap = inputTilemap;
            this.inputTileMapBounds = this.inputTilemap.cellBounds;
            this.inputTileMapTilesArray = this.inputTilemap.GetTilesBlock(this.inputTileMapBounds);
            ExtractNonEmptyTiles();
            VeryfyInputTiles();
        }

        private void VeryfyInputTiles()
        {
            if(topLeftTileCords != null || bottorRightTileCords != null)
            {
                throw new System.Exception("WFC: Input tilemap is empty !!!");
            }

            int minX = bottorRightTileCords.Value.x;
            int maxX = topLeftTileCords.Value.x;
            int minY = bottorRightTileCords.Value.y;
            int maxY = topLeftTileCords.Value.y;

            width = Math.Abs(maxX - minX) + 1;
            height = Math.Abs(maxY - minY) + 1;

            int tileCount = width * height;

            if(stackOfTiles.Count != tileCount)
            {
                throw new System.Exception("WFC: Tilemap has empty fields !!!");
            }

            if(stackOfTiles.Any(tile => tile.X > maxX || tile.X < minX || tile.Y > maxY || tile.Y < minY))
            {
                throw new System.Exception("WFC: Tilemap should be filled rectangle !!!");
            }
        }

        private void ExtractNonEmptyTiles()
        {
            for (int row = 0; row < inputTileMapBounds.size.y; row++)
            {
                for (int col = 0; col < inputTileMapBounds.size.x; col++)
                {
                    int index = col + (row * inputTileMapBounds.size.x);

                    TileBase tile = inputTileMapTilesArray[index];

                    if(bottorRightTileCords == null && tile != null)
                    {
                        bottorRightTileCords = new Vector2Int(col, row);
                    }

                    if(tile != null)
                    {
                        stackOfTiles.Enqueue(new TileContainer(tile, col, row));
                        topLeftTileCords = new Vector2Int(col, row);
                    }
                }
            }
        }
    }
}