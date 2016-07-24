using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using AthameRPG.Objects.Castles;
using Microsoft.Xna.Framework;

namespace AthameRPG.GameEngine
{
    public static class FileLoader
    {
        private const int DefaultMapSquareSize = 50;

        public static List<string> PathsReader(string path)
        {
            List<string> pathList = new List<string>();

            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string readLine = reader.ReadLine();

                    while (readLine != null)
                    {
                        pathList.Add(readLine);

                        readLine = reader.ReadLine();
                    }
                }
            }
            else
            {
                throw new ArgumentNullException("Map list path", "Invalid path!");
            }
            

            return pathList;
        }

        public static List<List<int>> MapReader(string path)
        {
            List<List<int>> currentMap = new List<List<int>>();

            int listIndex = 0;

            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string readLine = reader.ReadLine();

                    while (readLine != null)
                    {
                        int[] currentObjectOnTheMap = readLine.Trim()
                        .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse).ToArray();

                        currentMap.Add(new List<int>());
                        foreach (var num in currentObjectOnTheMap)
                        {
                            currentMap[listIndex].Add(num);
                        }

                        listIndex++;

                        readLine = reader.ReadLine();

                    }
                }

            }
            else
            {
                throw new ArgumentNullException("Map path", "Invalid path!");
            }
            
            return currentMap;
        }

        public static void ReadEnemyAndBuildingPositions(string path)
        {
            int enemyID = 0;
            List<List<int>> currentEnemyMap = MapReader(path);
            
            for (int i = 0; i < currentEnemyMap.Count(); i++)
            {
                for (int j = 0; j < currentEnemyMap[i].Count(); j++)
                {
                    int currentEnemyPosition = currentEnemyMap[i][j];

                    switch (currentEnemyPosition)
                    {
                        case 9:
                            //CharacterManager.AddEnemies(new Vector2((float)(j * 50) + 25f, (float)(i * 50) + 25f));
                            KeyValuePair<int,Vector2> support = new KeyValuePair<int, Vector2>(enemyID, new Vector2((float)(j * DefaultMapSquareSize) + DefaultMapSquareSize / 2, (float)(i * DefaultMapSquareSize) + DefaultMapSquareSize/2));
                            CharacterManager.AddEnemies(support);
                            enemyID++;
                        break;
                        case 10:  // Create castle/s on map and include it's external sides for collision detection 
                            BuildingManager.AddCastleFromTxtMapToList(new StoneCastle(new Vector2((float)(j * DefaultMapSquareSize), (float)(i * DefaultMapSquareSize))));
                            MapManager.Instance.CurrentMap.AddObstacle(new Vector2((float)(j * DefaultMapSquareSize), (float)(i * DefaultMapSquareSize)));
                            break;

                        // only outside borders of Castles
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 19:
                        case 20:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                            MapManager.Instance.CurrentMap.AddObstacle(new Vector2((float)(j * DefaultMapSquareSize), (float)(i * DefaultMapSquareSize)));
                            break;

                        case 99:
                            // set player start position
                            CharacterManager.barbarian.SetStartPosition(new Vector2((float)(j* DefaultMapSquareSize) ,(float)(i * DefaultMapSquareSize)));
                            break;
                    }
                }
            }
            
        }
    }
}
