using Game.Managers;
using UnityEngine;

namespace Game.UI
{
    public class OptionMenuUI : MonoBehaviour
    {
        [SerializeField] MixerUI mixerUI;
        [SerializeField] GameplayConfigManager _gameplayManager;

        public void ConfirmOptions()
        {
            mixerUI.ConfirmChange();
            _gameplayManager.ConfirmChange();
            gameObject.SetActive(false);
        }

        public void CancelOptions()
        {
            mixerUI.CancelChange();
            _gameplayManager.CancelChange();
            gameObject.SetActive(false);
        }
    }
}
