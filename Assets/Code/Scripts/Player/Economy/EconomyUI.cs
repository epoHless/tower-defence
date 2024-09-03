using TMPro;
using UnityEngine;

namespace epoHless
{
    public class EconomyUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text energyText;
        
        public void UpdateScore(float score)
        {
            scoreText.text = "score: " + score.ToString("000000");
        }

        public void UpdateEnergy(float energy)
        {
            energyText.text = "energy: " + energy.ToString("000000");
        }
    }
}