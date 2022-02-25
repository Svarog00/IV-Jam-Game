using System.Collections;

namespace Assets.Code.Gameplay.Map
{
    public class MapModel
    {
        public Area[,] Areas => _areas;

        private readonly Area[,] _areas;
        private readonly AreasContainer _areasContainer;

        public MapModel(int size, AreasContainer areasContainer)
        {
            _areas = new Area[size,size];
            _areasContainer = areasContainer;

            FillMap();
        }

        public MapModel(int width, int height, AreasContainer areasContainer)
        {
            _areas = new Area[width, height];
            _areasContainer = areasContainer;
            FillMap();
        }

        private void FillMap()
        {
            for(int i = 0; i < _areas.GetLength(0); i++)
            {
                for(int j = 0; j < _areas.GetLength(1); j++)
                {
                    _areas[i, j] = _areasContainer.GetRandomArea();
                }
            }

            int widthCenter = _areas.GetLength(0) / 2;
            int heightCenter = _areas.GetLength(1) / 2;
            _areas[widthCenter, heightCenter] = _areasContainer.GetTownArea();
        }
    }
}