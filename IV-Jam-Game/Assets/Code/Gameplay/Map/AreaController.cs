using System.Collections;
using UnityEngine;

namespace Assets.Code.Gameplay.Map
{
    public class AreaController : MonoBehaviour
    {
        private Area _areaModel;
        private PathBuildingManager _pathManager;

        private bool _isRevealed;
        private bool _isMarked;

        private void Start()
        {
            _pathManager = FindObjectOfType<PathBuildingManager>();
        }

        public void Initialize(Area areaModel)
        {
            _areaModel = areaModel;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Player") && _isRevealed == false)
            {
                _isRevealed = true;
                print($"Reavealed Area {_areaModel}");
            }
        }

        private void OnMouseDown()
        {
            MarkArea();
        }

        private void MarkArea()
        {
            if (_isRevealed)
            {
                if (_isMarked)
                {
                    _isMarked = false;
                    _pathManager.DeletePoint(_areaModel);
                }
                else
                {

                    _isMarked = true;
                    _pathManager.AddPoint(_areaModel);
                }
            }
        }
    }
}