using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace WaveFunctionCollapse
{
    public class InputReader : IInputReader<TileBase>
    {
        private Tilemap inputTilemap;

        public InputReader(Tilemap input)
        {
            inputTilemap = input;
        }

        public IValue<TileBase>[][] ReadInputToGrid()
        {
            var grid = ReadInputTileMap();

            TileBaseValue[][] gridOfValues = null;

            if (grid != null)
            {
                gridOfValues = Helpers.MyCollectionExtension.CreateJaggedArray<TileBaseValue[][]>(grid.Length, grid[0].Length);
                
                for(int row = 0; row < grid.Length; row++)
                {
                    for (int col = 0; col < grid.Length; col++)
                    {
                        gridOfValues[row][col] = new TileBaseValue(grid[row][col]);
                    }
                }
            }

            return gridOfValues;
        }

        private TileBase[][] ReadInputTileMap()
        {
            InputImageParametrs inputImageParametrs = new InputImageParametrs(inputTilemap);

            return CreateTileBaseGrid(inputImageParametrs);
        }

        private TileBase[][] CreateTileBaseGrid(InputImageParametrs inputImageParametrs)
        {
            throw new NotImplementedException();
        }
    }
}