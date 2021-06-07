﻿using System;
using Desdiene.MonoBehaviourExtention;
using Desdiene.Types.Percent;
using UnityEngine;

namespace Desdiene.TimeControl.Scale
{
    //Взаимодействовать с UnityEngine.Time только внутри ЖЦ monoBehaviour
    public sealed class GlobalTimeScaler : MonoBehaviourExt, ITimeScaler
    {
        private IPercent timeScaleSaved;  // Сохраненное значение скорости времени
        private bool pause = false; // По умолчанию паузы нет

        public event Action<float> OnTimeScaleChanged;

        protected override void AwakeExt()
        {
            timeScaleSaved = new Percents(Time.timeScale);
        }

        public void SetPause(bool pause)
        {
            this.pause = pause;
            SetTimeScaleViaPause();
        }

        public void SetTimeScale(float timeScale) => timeScaleSaved.Set(timeScale);

        public float TimeScale => Time.timeScale;

        public bool IsPause => pause;

        private void SetTimeScaleViaPause()
        {
            if (pause) Time.timeScale = 0f;
            else Time.timeScale = timeScaleSaved.Get();
            OnTimeScaleChanged?.Invoke(Time.timeScale);
        }
    }
}
