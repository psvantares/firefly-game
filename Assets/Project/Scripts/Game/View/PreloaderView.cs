// Copyright (C) 2022 Geronimo Games - All Rights Reserved.
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.

using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace GeronimoGames.Firefly.Game
{
    public class PreloaderView : View
    {
        [Header("TEXTS")] [SerializeField] private TMP_Text titleText;

        public void Render(Action callback)
        {
            SetActive(true);
            StartCoroutine(TextAsync(callback));
        }

        private IEnumerator TextAsync(Action callback)
        {
            var input = titleText.text;

            for (var i = 0; i <= input.Length; i++)
            {
                titleText.text = input[..i];
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(0.75f);

            callback?.Invoke();

            yield return null;
        }
    }
}