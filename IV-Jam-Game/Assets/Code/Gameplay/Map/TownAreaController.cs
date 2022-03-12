using Assets.Code.GUI;
using Assets.Code.Infrastructure;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Gameplay.Map
{
	public class TownAreaController : AreaController
	{
        private TaskUI _ui;
		private IInputService _inputSerivce;

		private MapView _map;
		private TaskManager _taskManager;

		private bool _isPlayerIn = false;

		private void Start()
		{
			Interface = FindObjectOfType<ResourcesUI>();
			PathManager = FindObjectOfType<PathBuildingManager>();
			
			_map = GetComponentInParent<MapView>();
			_inputSerivce = ServiceLocator.Container.Single<IInputService>();

			_taskManager = new TaskManager(_map);
			_taskManager.GetNewTask();

			_ui = FindObjectOfType<TaskUI>();
			_ui.SetValues(_taskManager.Time, _taskManager.Energy);
		}

		private void Update()
		{
			if(_isPlayerIn && _inputSerivce.IsPassPathButtonDown())
            {
                PassPath();
            }
        }

        private void PassPath()
        {
			int tmpTime = 0;
			int tmpEnergy = 0;
			foreach (var area in PathManager.Path)
			{
				tmpEnergy += area.EnergyCost;
				tmpTime += area.TimeCost;
			}

			if (_map.IsPathViable(PathManager.Path))
            {
                _taskManager.CompleteTask(PathManager.Path);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
		{
			if(collision.CompareTag("Player"))
			{
				Interface.SetValues(Model.TimeCost, Model.EnergyCost);
				_isPlayerIn = true;
			}
		}
	}
}