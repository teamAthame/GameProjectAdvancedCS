using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AthameRPG.GameEngine
{
    public static class FileLoader
    {
        
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
    }
}
