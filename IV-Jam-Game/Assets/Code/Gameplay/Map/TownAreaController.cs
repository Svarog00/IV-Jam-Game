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
			if (_map.IsPathViable(PathManager.Path))
            {
				bool isCompleted = _taskManager.CompleteTask(PathManager.Path);
				if (isCompleted)
				{
					PathManager.ClearPath();
				}
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
		{
			if(collision.CompareTag("Player"))
			{
				_isPlayerIn = true;
                NotifyInterface();
            }
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
            if (collision.CompareTag("Player"))
            {
                _isPlayerIn = false;
                NotifyInterface();
            }
        }
	}
}