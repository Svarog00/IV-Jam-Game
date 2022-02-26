using System.Collections;
using UnityEngine;

namespace Assets.Code.Gameplay.Map
{
    public class AreaController : MonoBehaviour
    {
        private Area _model;
        private PathBuildingManager _pathManager;

        private bool _isRevealed;

        //[SerializeField] private GameObject _pathFlag;
        //Path flag is just a game object with flag sprite

        private void Start()
        {
            _pathManager = FindObjectOfType<PathBuildingManager>();
        }

        public void Initialize(Area areaModel)
        {
            _model = Instantiate(areaModel);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Player") && _isRevealed == false)
            {
                RevealArea();
            }
        }

        private void RevealArea()
        {
            _isRevealed = true;
            print($"Reavealed Area {_model}");
            //TODO: Turn off the mist, covering the area
        }

        private void OnMouseDown()
        {
            MarkArea();
        }

        private void MarkArea()
        {
            if (_isRevealed)
            {
                if (_model.IsMarked)
                {
                    _model.IsMarked = false;
                    _pathManager.DeletePoint(_model);
                    //TODO: Turn off the flag mark
                }
                else
                {
                    _model.IsMarked = true;
                    _pathManager.AddPoint(_model);
                    //TODO: Turn on the flag mark
                }
            }
        }
    }
}