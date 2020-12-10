using System;
using System.Linq;

namespace Case_1_Solutions
{
    /// <summary>
    /// Решение задачи Иосифа Флавия
    /// </summary>
    public static class JosephusProblem
    {

        /// <summary>
        /// Максимальное N, при котором рекурсия не переполнит стек
        /// Лучше - меньше
        /// </summary>
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once FieldCanBeMadeReadOnly.Global
        // ReSharper disable once ConvertToConstant.Global
        public static int MaxSimpleRecursionN = 10000;

        /// <summary>
        /// Максимальное N, при котором циклический алгоритм выполнится за приемлимое время
        /// (по умолчанию за 1 секунду)
        /// </summary>
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once FieldCanBeMadeReadOnly.Global
        // ReSharper disable once ConvertToConstant.Global
        public static int MaxCycledN = 190000000;

        /// <summary>
        /// Максимальное N, при котором алгоритм симуляции выполнится за приемлимое время
        /// (по умолчанию за 1 секунду)
        /// </summary>
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once FieldCanBeMadeReadOnly.Global
        // ReSharper disable once ConvertToConstant.Global
        public static int MaxSimulationN = 190000;

        /// <summary>
        /// Медленное рекурсивное решение, которое ограничено размером стека
        /// </summary>
        /// <param name="n">Количсетво человек в кругу</param>
        /// <param name="m">Количсетво человек, которых пропускает считающий</param>
        /// <returns>Последний выбранный</returns>
        /// <exception cref="T:System.StackOverflowException">Когда <paramref name="n"/> превышает максимальное N, указанное в коде, то выполнение алгоритма нецелесообразно.</exception>
        public static int SimpleRecursion(int n, int m) => n > MaxSimpleRecursionN
            ? throw new StackOverflowException("Possible Stack overflow exception.")
            : n > 1
                ? (SimpleRecursion(n - 1, m) + m - 1) % n + 1
                : 1;

        /// <summary>
        /// Циклическое решение, которое ограничено целессобразностью исполнения за время
        /// </summary>
        /// <param name="n">Количсетво человек в кругу</param>
        /// <param name="m">Количсетво человек, которых пропускает считающий</param>
        /// <returns>Последний выбранный</returns>
        /// <exception cref="T:System.TimeoutException">Когда <paramref name="n"/> превышает максимальное N, указанное в коде, то выполнение алгоритма нецелесообразно.</exception>
        public static int Cycled(int n, int m)
        {
            if (n > MaxCycledN)
                throw new TimeoutException("Too long calculation.");

            var ret = 0;
            for (int i = 1; i <= n; i++)
                ret = (ret + m) % i;

            return ret + 1;
        }

        /// <summary>
        /// Решение симуляцией, которое ограничено целессобразностью исполнения за время
        /// </summary>
        /// <param name="n">Количсетво человек в кругу</param>
        /// <param name="m">Количсетво человек, которых пропускает считающий</param>
        /// <returns>Последний выбранный</returns>
        /// <exception cref="T:System.TimeoutException">Когда <paramref name="n"/> превышает максимальное N, указанное в коде, то выполнение алгоритма нецелесообразно.</exception>
        public static int Simulation(int n, int m)
        {
            if (n > MaxSimulationN)
                throw new TimeoutException("Too long calculation.");

            var round = Enumerable.Range(1, n).ToList();

            var ind = --m;
            while (round.Count != 1)
            {
                if (ind >= round.Count)
                    ind -= round.Count * (ind / round.Count);
                round.RemoveAt(ind);
                ind += m;
            }

            return round[0];
        }

        /// <summary>
        /// Возвращает максимальные N к стандартным значениям: 10000 для рекурсии, 190000000 для циклического решения, 190000 для решения симуляцией.
        /// (Предполагается выполнение за секунду)
        /// </summary>
        public static void ResetMaxN()
        {
            MaxSimpleRecursionN = 10000;
            MaxCycledN = 190000000;
            MaxSimulationN = 190000;
        }

        /// <summary>
        /// Убирает ограничение по N.
        /// (Крайне не рекомендуется)
        /// </summary>
        public static void MaximizeMaxN() =>
            MaxSimpleRecursionN = MaxCycledN = MaxSimulationN = int.MaxValue;
    }
}
