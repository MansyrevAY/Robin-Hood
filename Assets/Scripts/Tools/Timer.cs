using System;

namespace Tools
{
    public class Timer
    {
        private Action callback;
        private float _time;
        private float _currentTime;

        public bool IsActive { get; private set; }

        public Timer() => _currentTime = 0f;

        public void StartTimer(float time, Action action)
        {
            IsActive = true;
            callback = action;
            _time = time;
        }

        public void Update(float deltaTime)
        {
            _currentTime += deltaTime;

            if (_currentTime >= _time)
            {
                StopTimer();
                callback.Invoke();
            }
        }

        private void StopTimer()
        {
            IsActive = false;
            _currentTime = 0f;
        }
    }
}
