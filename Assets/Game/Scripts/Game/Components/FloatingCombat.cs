using Game.Enemy;
using UnityEngine;

namespace Game.Components
{
    public class FloatingCombat : MonoBehaviour
    {
        [SerializeField]
        private bool isHazard;

        [SerializeField]
        private TextMesh combatText;

        private int displayValue;

        private void Start()
        {
            displayValue = isHazard == false
                ? GetComponent<EnemyBase>().PointsValue
                : GetComponent<HazardBase>().PointsValue;
        }

        public void ShowCombatText()
        {
            var instantiate = Instantiate(combatText);
            instantiate.color = Color.yellow;
            instantiate.text = displayValue.ToString();
            instantiate.transform.position = transform.position;
        }
    }
}