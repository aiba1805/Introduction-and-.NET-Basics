using System;
using System.Threading;
using System.Threading.Tasks;

namespace MPAO
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Start(null);
        }

        private static async Task Start(CancellationTokenSource prevCts)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            Console.WriteLine("Введите число:");
            int.TryParse(Console.ReadLine(), out var num);
            if (num == 0) return;
            prevCts?.Cancel();
            Calculate(num, token);
            Start(cts);
        }

        private static async Task Calculate(int num, CancellationToken token)
        {
            var sum = 0;
            for (var i = 1; i <= num; i++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine($"Операция прервана досчитала до {sum}");
                    return;
                }

                await Task.Delay(1000);
                sum += i;
            }

            Console.WriteLine($"Сумма до числа {num} = {sum}");
        }
    }
}