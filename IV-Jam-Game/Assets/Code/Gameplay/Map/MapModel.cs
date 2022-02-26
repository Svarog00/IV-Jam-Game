using Assets.Code.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Gameplay.Map
{
    public class MapModel
    {
        public Area[,] Areas => _areas;

        private readonly Area[,] _areas;
        private readonly AreasContainer _areasContainer;

        private readonly int _width;
        private readonly int _height;

        private int _townX;
        private int _townY;

        public MapModel(int size, AreasContainer areasContainer)
        {
            _areas = new Area[size,size];
            _areasContainer = areasContainer;

            FillMap();
        }

        public MapModel(int width, int height, AreasContainer areasContainer)
        {
            _areas = new Area[width, height];
            _width = width;
            _height = height;
            _areasContainer = areasContainer;
            FillMap();
        }

        public Vector2 GetTownPosition()
        {
            return new Vector2(_townX, _townY);
        }

        private void FillMap()
        {
            for(int i = 0; i < _areas.GetLength(0); i++)
            {
                for(int j = 0; j < _areas.GetLength(1); j++)
                {
                    _areas[i, j] = GameObject.Instantiate(_areasContainer.GetRandomArea());
                    _areas[i, j].SetCoord(i, j);
                }
            }

            _townX = _areas.GetLength(0) / 2;
            _townY = _areas.GetLength(1) / 2;
            _areas[_townX, _townY] = GameObject.Instantiate(_areasContainer.GetTownArea());
            _areas[_townX, _townY].SetCoord(_townX, _townY);
        }

        public bool IsPathViable(List<Area> path)
        {
            foreach(var area in path)
            {
                foreach(var tmp in GetNeighbours(area))
                {
                    if(!path.Contains(tmp))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //Build path for task manager to make player build a proper path
        //Should return needed energy and time costs
        public void BuildEfficientPath(out int energyCost, out int timeCost, int x, int y)
        {
            int tmpEnergyCost = 0;
            int tmpTimeCost = 0;

            var start = _areas[_townX, _townY];
            var target = _areas[x, y];

            PriorityQueue<Area> frontier = new PriorityQueue<Area>();
            Dictionary<Area, Area> cameFrom = new Dictionary<Area, Area>(); //Ключ - узел; Value - узел, из которого пришел.                                                 
            //Сюда сохраняются узлы, которые мы будет использовать для реконструкции пути
            Dictionary<Area, double> costSoFar = new Dictionary<Area, double>(); //Для запоминания цен прохода от одного узла к тругому

            frontier.Enqueue(start, 0);
            cameFrom.Add(start, start);
            costSoFar.Add(start, 0);

            while (frontier.Count > 0)
            {
                Area current = frontier.Dequeue(); //Достаем узел с минимальной ценой прохода
                //Условие для раннего выхода

                if (current == target)
                {
                    break;
                }

                //Проверяем всех соседей текущего узла
                //Добавляем тот, проход к которому стоит меньше всего
                foreach (var next in GetNeighbours(current))
                {
                    double newCost = costSoFar[current] + next.GetEfficiency(); //Считаем общую цену прохода от текущего к соседу (цена до текущего + цена к соседу)
                    double oldCost = int.MaxValue;
                    costSoFar.TryGetValue(next, out oldCost);

                    bool isEstimated = costSoFar.ContainsKey(next);

                    if (!isEstimated || newCost < oldCost)
                    {
                        //Если этот узел ещё не оценивался ИЛИ новая цена меньше, чем старая то добавляем в список
                        if (!isEstimated)
                        {
                            costSoFar.Add(next, newCost);
                        }
                        costSoFar[next] = newCost;

                        //Сохраняем в список того соседа текущего узла, у которого цена прохода меньше
                        if (!cameFrom.ContainsKey(next))
                        {
                            cameFrom.Add(next, current);
                        }
                        cameFrom[next] = current;

                        frontier.Enqueue(next, newCost); //Добавляем этого соседа для дальнейшей проверки
                    }

                }
                //Путь будет найден с наименьшей ценой
            }

            foreach(var tmp in cameFrom)
            {
                tmpEnergyCost += tmp.Value.EnergyCost;
                tmpTimeCost += tmp.Value.TimeCost;
            }

            energyCost = tmpEnergyCost;
            timeCost = tmpTimeCost;
        }

        private List<Area> GetNeighbours(Area current)
        {
            List<Area> neighbourList = new List<Area>();

            if (current.X - 1 >= 0)
            {
                neighbourList.Add(GetArea(current.X - 1, current.Y));

                if (current.Y - 1 >= 0)
                    neighbourList.Add(GetArea(current.X - 1, current.Y - 1));

                if (current.Y + 1 < _height)
                    neighbourList.Add(GetArea(current.X - 1, current.Y + 1));
            }

            if (current.X + 1 < _width)
            {
                neighbourList.Add(GetArea(current.X + 1, current.Y));

                if (current.Y - 1 >= 0)
                    neighbourList.Add(GetArea(current.X + 1, current.Y - 1));

                if (current.Y + 1 < _height)
                    neighbourList.Add(GetArea(current.X + 1, current.Y + 1));
            }

            if (current.Y - 1 >= 0)
                neighbourList.Add(GetArea(current.X, current.Y - 1));

            if (current.Y + 1 < _height)
                neighbourList.Add(GetArea(current.X, current.Y + 1));

            return neighbourList;
        }

        private Area GetArea(int i, int j)
        {
            return _areas[i, j];
        }
    }
}