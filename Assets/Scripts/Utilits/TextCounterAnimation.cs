using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Utilits
{
    public static class TextCounterAnimation
    {
        public static IEnumerator CountText(int oldValue, int newValue, float countFPS, float duration, TextMeshProUGUI text, string numberFormat = "N0")
        {
            WaitForSeconds Wait = new WaitForSeconds(1f / countFPS);
            int previousValue = oldValue;
            int stepAmount;

            if (newValue - previousValue < 0)
                stepAmount = Mathf.FloorToInt((newValue - previousValue) / (countFPS * duration));
            else
                stepAmount = Mathf.CeilToInt((newValue - previousValue) / (countFPS * duration));

            if (previousValue < newValue)
            {
                while (previousValue < newValue)
                {
                    previousValue += stepAmount;
                    if (previousValue > newValue)
                        previousValue = newValue;

                    text.SetText(previousValue.ToString(numberFormat));

                    yield return Wait;
                }
            }
            else
            {
                while (previousValue > newValue)
                {
                    previousValue += stepAmount;
                    if (previousValue < newValue)
                        previousValue = newValue;

                    text.SetText(previousValue.ToString(numberFormat));

                    yield return Wait;
                }
            }
        }

        public static IEnumerator CountTextWithAnimationCurve(int oldValue, int newValue, float duration, TextMeshProUGUI text)
        {
            float stepTime = 1f / duration;
            int step = oldValue;

            int absoluteNewValue = Math.Abs(newValue);
            int absoluteStep = Math.Abs(step);
            while (absoluteStep != absoluteNewValue)
            {
                step = MathInt.Lerp(step, newValue, stepTime);
                absoluteStep = Math.Abs(step);

                text.SetText(step.ToString());
                yield return new WaitForSeconds(stepTime * Time.deltaTime);
            }
        }
    }
}
