using TMPro;
using UnityEngine;

namespace _Scripts.AI.Entities.Pawn.UI
{
    public class PawnHUD : MonoBehaviour
    {
        [SerializeField] private TMP_Text healthText;
        [SerializeField] private TMP_Text hungerText;

        private PawnModel _model;
        private bool _isInitialized;

        public void Init(PawnModel model)
        {
            _model = model;
        }

        private void OnEnable()
        {
            if (_model == null) return;

            _model.OnHealthChanged += UpdateHealth;
            _model.OnHungerChanged += UpdateHunger;
        }

        private void OnDisable()
        {
            if (_model == null) return;

            _model.OnHealthChanged -= UpdateHealth;
            _model.OnHungerChanged -= UpdateHunger;
        }

        private void UpdateHealth(float health)
        {
            healthText.text = Mathf.FloorToInt(health).ToString();
        }

        private void UpdateHunger(float hunger)
        {
            hungerText.text = Mathf.FloorToInt(hunger).ToString();
        }
    }
}