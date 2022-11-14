using Game.Managers;
using UnityEngine;

namespace Game.UI
{
    public class OptionMenuUI : MonoBehaviour
    {
        [SerializeField] private MixerManager _mixerManager;
        [SerializeField] GameplayConfigManager _gameplayManager;

        public void ConfirmOptions()
        {
            _mixerManager.ConfirmChange();
            _gameplayManager.ConfirmChange();
            gameObject.SetActive(false);
        }

        public void CancelOptions()
        {
            _mixerManager.CancelChange();
            _gameplayManager.CancelChange();
            gameObject.SetActive(false);
        }
    }
}
