using System;

namespace MersenneTwister
{
    public class Generator
    {
        private readonly MersenneGenerator generator;

        public Generator()
        {
            generator = new MersenneGenerator();
        }
        
        public void ShowMenu()
        {
            Console.WriteLine("1. Получить целое число");
            Console.WriteLine("2. Получить дробное число");
            Console.WriteLine("3. Получить последовательность целых чисел");
            Console.WriteLine("4. Получить последовательность дробных чисел");
            Console.Write("Выберите пункт меню: ");
            var number = int.Parse(Console.ReadLine());
            if (number < 1 || number > 4)
            {
                Console.Clear();
                Console.WriteLine("Было введено некорректное значение. Попробуйте еще раз.");
                ShowMenu();
            }
            ChooseAction(number);
        }

        private void ChooseAction(int number)
        {
            switch (number)
            {
                case 1:
                    IntegerNumberGenerating();
                    break;
                case 2:
                    DoubleNumberGenerating();
                    break;
                case 3:
                    IntegerNumbersGenerating();
                    break;
                case 4:
                    DoubleNumbersGenerating();
                    break;
            }
        }

        private void IntegerNumberGenerating()
        {
            Console.Write("Введите минимальное число: ");
            var minimum = int.Parse(Console.ReadLine());
            Console.Write("Введите максимальное число: ");
            var maximum = int.Parse(Console.ReadLine());
            Console.WriteLine($"Сгенерированное число: {generator.GetIntegerNumber(minimum, maximum)}");
        }

        private void DoubleNumberGenerating()
        {
            Console.WriteLine("Целая и дробная часть разделяются запятой");
            Console.Write("Введите минимальное число: ");
            var minimum = double.Parse(Console.ReadLine());
            Console.Write("Введите максимальное число: ");
            var maximum = double.Parse(Console.ReadLine());
            Console.WriteLine($"Сгенерированное число: {generator.GetRealNumber(minimum, maximum)}");
        }

        private void IntegerNumbersGenerating()
        {
            Console.Write("Введите минимальное число: ");
            var minimum = int.Parse(Console.ReadLine());
            Console.Write("Введите максимальное число: ");
            var maximum = int.Parse(Console.ReadLine());
            Console.Write("Введите количество чисел: ");
            var count = int.Parse(Console.ReadLine());
            var numbers = generator.GetIntegerNumbers(minimum, maximum, count);
            Console.WriteLine("Сгенерированные числа: ");
            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }
        }

        private void DoubleNumbersGenerating()
        {
            Console.WriteLine("Целая и дробная часть разделяются запятой");
            Console.Write("Введите минимальное число: ");
            var minimum = double.Parse(Console.ReadLine());
            Console.Write("Введите максимальное число: ");
            var maximum = double.Parse(Console.ReadLine());
            Console.Write("Введите количество чисел: ");
            var count = int.Parse(Console.ReadLine());
            var numbers = generator.GetRealNumbers(minimum, maximum, count);
            Console.WriteLine("Сгенерированные числа:");
            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }
        }
    }
}