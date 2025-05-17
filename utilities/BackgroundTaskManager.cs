// Bu sınıf, evcil hayvanların istatistiklerini arka planda güncellemek için kullanılabilir.
// Şu an için boş bırakılmıştır. Geliştirme sırasında ihtiyaç duyulursa doldurulabilir.

using System;
using System.Threading;
using System.Threading.Tasks;
using Models;
using System.Collections.Generic;

namespace Utilities
{
    public class BackgroundTaskManager
    {
        private readonly List<Pets> _pets;
        private CancellationTokenSource _cancellationTokenSource;
        private readonly int _decayIntervalSeconds = 30; // Stats decay every 30 seconds
        private readonly int _decayAmount = 5; // Amount to decrease stats by

        public BackgroundTaskManager(List<Pets> pets)
        {
            _pets = pets;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public async Task StartStatDecayTask()
        {
            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                foreach (var pet in _pets)
                {
                    // Decrease all stats
                    pet.DecreaseStats(_decayAmount);
                }

                // Wait for the next interval
                await Task.Delay(TimeSpan.FromSeconds(_decayIntervalSeconds), _cancellationTokenSource.Token);
            }
        }

        public void StopTasks()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}
