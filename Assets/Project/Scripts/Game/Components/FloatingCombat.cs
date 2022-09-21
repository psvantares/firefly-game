// Copyright (C) 2022 Geronimo Games - All Rights Reserved.
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.

using UnityEngine;

namespace GeronimoGames.Firefly.Game
{
    public class FloatingCombat : MonoBehaviour
    {
        [SerializeField] private bool isHazard;
        [SerializeField] private TextMesh combatText;

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