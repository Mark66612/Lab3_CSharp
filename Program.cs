using System;

class Program
{
    static void Main() // метод
    {
        bool isMenuActive = true; // флаг работы меню. Пока он true - меню повторяется

        // Главный цикл программы — меню
        while (isMenuActive)
        {
            // Выводим пункты меню
            Console.WriteLine("\n===== МЕНЮ =====");
            Console.WriteLine("1. Отгадай ответ");
            Console.WriteLine("2. Об авторе");
            Console.WriteLine("3. Сортировка массива");
            Console.WriteLine("4. Выход");
            Console.WriteLine("================");
            Console.Write("Выберите пункт меню: ");

            int menuChoice = GetIntFromUser(); // универсальный ввод числа

            switch (menuChoice) // Обработка выбранного пункта меню
            {
                case 1:
                    PlayGuessGame(); // запуск угадайки
                    break;

                case 2:
                    ShowAuthor(); // вывод информации об авторе
                    break;

                case 3:
                    SortArrayMenu(); // меню сортировки массива
                    break;

                case 4:
                    isMenuActive = ExitProgram(); // выход из программы // метод возвращает true/false
                    break;

                default:
                    Console.WriteLine("Введите число от 1 до 4!");
                    break;
            }
        }

        Console.WriteLine("Программа завершена.");
    }


    // универсальный ввод числа
    static int GetIntFromUser()
    {
        int number = 0;
        bool isCorrect = false;

        while (!isCorrect)
        {
            if (int.TryParse(Console.ReadLine(), out number))
            {
                isCorrect = true;
            }
            else
            {
                Console.WriteLine("Вы ввели не число! Введите заново:");
            }
        }

        return number;
    }


    // ввод длины массива
    static int GetArrayLength()
    {
        Console.Write("Введите длину массива: ");
        uint length = 0;
        bool isCorrect = false;

        while (!isCorrect)
        {
            if (uint.TryParse(Console.ReadLine(), out length))
            {
                isCorrect = true;
                
            }
            else
            {
                Console.WriteLine("Вы ввели не число!");
                Console.Write("Введите длину массива: ");
            }
        }

        return (int)length;
    }


    // создание массива
    static int[] CreateArray(int length)
    {
        int[] array = new int[length];
        Random rnd = new Random();

        for (int i = 0; i < length; i++)
        {
            array[i] = rnd.Next(-100, 100);
        }

        return array;
    }


    // клонирование массива
    static int[] CloneArray(int[] original)
    {
        int[] clone = new int[original.Length];

        for (int i = 0; i < original.Length; i++)
        {
            clone[i] = original[i];
        }

        return clone;
    }


    // вывод массива
    static void PrintArray(int[] array)
    {
        if (array.Length > 10)
        {
            Console.WriteLine("Массив не может быть выведен на экран, так как длина массива больше 10.");
            return;
        }

        Console.Write("Массив: ");
        for (int i = 0; i < array.Length; i++)
        {
            Console.Write(array[i] + " ");
        }
        Console.WriteLine();
    }


    // гномья сортировка
    static int[] GnomeSort(int[] array)
    {
        int index = 0; // начинаем с 0(первый элемент)

        bool isContinue = true; // флаг цикла. пока он true, сортировка продолжается
        while (isContinue)
        {
            if (index == array.Length - 1)
            {
                isContinue = false;
            }
            else
            {
                if (array[index] > array[index + 1]) //сравниваем текущий элем. со следующим. Если текущий больше след. нужен обмен
                {
                    int temp = array[index]; //обмен через временную переменную temp
                    array[index] = array[index + 1];
                    array[index + 1] = temp;

                    if (index > 0) //шаг назад. Если индекс 0 остаемся на месте
                    {
                        index--;
                    }
                }
                else
                {
                    index++; //если порядок правильный, двигаемся вперед
                }
            }
        }

        return array; //возвращается тот же массив
    }


    // сортировка вставками
    static int[] InsertionSort(int[] array)
    {
        int i = 1; /*индекс первого неотсортированного элемента. 
        Начинаем с 1, потому что один элемент (array[0]) уже сам по себе считается отсортированным «куском». 
        Всё, что левее i, - отсортированная часть*/

        bool isContinue = true; // флаг цикла. пока он true, сортировка продолжается
        while (isContinue)
        {
            if (i >= array.Length) // условие выхода(сортировка завершена)
            {
                isContinue = false;
            }
            else
            {
                int key = array[i]; // текущий элемент, который мы собираемся вставить в отсортированную часть
                int j = i - 1; // j-индекс, с которого начинаем просмотр отсортированной части справа налево

                bool innerContinue = true;
                while (innerContinue)
                {
                    if (j >= 0 && array[j] > key) /* j>=0-не вышли за левую границу массива. 
                    array[j]>key-элем слева больше вставляемого, значит он должен оказаться правее кей*/
                    {
                        array[j + 1] = array[j]; /*освобождаем место j для элемента, который меньше кей
                        (раздвигаем отсортированную часть)*/
                        j--; /*уходим левее проверять следующий элемент*/
                    }
                    else
                    {
                        innerContinue = false; /*выходим из внутреннего цикла(если встретили элемент <= кей)*/
                    }
                }

                array[j + 1] = key; // вставляем кей на позицию j+1
                i++; // переходим к след неотсортир элементу
            }
        }

        return array;
    }


    // пункт меню сортировка массива
    static void SortArrayMenu()
    {
        Console.WriteLine("\n=== Сортировка массива ===");

        int length = GetArrayLength(); // ввод длины массива
        int[] array = CreateArray(length); // создание массива

        Console.WriteLine("Исходный массив:");
        PrintArray(array);

        int[] arrayGnome = CloneArray(array);
        int[] arrayInsertion = CloneArray(array);

        // Засекаем время
        var startGnome = DateTime.Now;
        GnomeSort(arrayGnome);
        var endGnome = DateTime.Now;

        var startInsertion = DateTime.Now;
        InsertionSort(arrayInsertion);
        var endInsertion = DateTime.Now;

        Console.WriteLine("\nРезультат гномьей сортировки:");
        PrintArray(arrayGnome);
        Console.WriteLine("Время: " + (endGnome - startGnome).TotalMilliseconds + " мс");

        Console.WriteLine("\nРезультат сортировки вставками:");
        PrintArray(arrayInsertion);
        Console.WriteLine("Время: " + (endInsertion - startInsertion).TotalMilliseconds + " мс");
    }








    // игра
    static void PlayGuessGame()
    {
        Console.WriteLine("\n=== Игра: Отгадай ответ ===");

        double a = GetDoubleWithCondition("Введите значение переменной a:", x => Math.Cos(2 * x) > 0,
            "Ограничение: cos(2a) должен быть > 0.");

        double b = GetDoubleWithCondition("Введите значение переменной b:", x => x > 0,
            "Ограничение: b должно быть > 0.");

        double correct = CalculateFunction(a, b);

        Console.WriteLine("\nТеперь попробуйте отгадать результат функции.");
        Console.WriteLine("У вас 3 попытки.");

        GuessGame(correct);
    }


    // ввод double с условием
    static double GetDoubleWithCondition(string message, Func<double, bool> condition, string errorMessage)
    {
        Console.WriteLine(message);

        double value = 0.0;
        bool isCorrect = false;

        while (!isCorrect)
        {
            if (double.TryParse(Console.ReadLine(), out value))
            {
                if (condition(value))
                {
                    isCorrect = true;
                }
                else
                {
                    Console.WriteLine(errorMessage);
                }
            }
            else
            {
                Console.WriteLine("Вы ввели не число! Введите заново.");
            }
        }

        return value;
    }


// вычисление функции
    static double CalculateFunction(double a, double b)
    {
        double log5b = Math.Log(b, 5);
        double denominator = Math.Sqrt(Math.Cos(2 * a));
        double functionValue = Math.Sin(log5b / denominator);
        return Math.Round(Math.Pow(functionValue, 2), 2);
    }

    // угадайка
    static void GuessGame(double correct)
    {
        int attempts = 3;
        bool isContinue = true;

        while (isContinue)
        {
            Console.Write($"Введите ваш ответ (осталось попыток: {attempts}): ");

            double guess = 0.0;

            if (double.TryParse(Console.ReadLine(), out guess))
            {
                double rounded = Math.Round(guess, 2);

                if (Math.Abs(rounded - correct) < 0.001)
                {
                    Console.WriteLine("Верно! Вы выиграли!");
                    isContinue = false;
                }
                else
                {
                    attempts--;
                    Console.WriteLine("Неверно.");

                    if (attempts == 0)
                    {
                        Console.WriteLine($"Вы проиграли. Правильный ответ: {correct}");
                        isContinue = false;
                    }
                }
            }
            else
            {
                Console.WriteLine("Вы ввели не число!");
                attempts--;

                if (attempts == 0)
                {
                    Console.WriteLine($"Вы проиграли. Правильный ответ: {correct}");
                    isContinue = false;
                }
            }
        }
    }

    // автор
    static void ShowAuthor()
    {
        Console.WriteLine("\nАвтор: Алина");
        Console.WriteLine("Группа: 6105-090301D");
    }

    // выход
    static bool ExitProgram()
    {
        Console.Write("Вы действительно хотите выйти? (д - да, н - нет): ");

        bool isCorrect = false;
        bool result = true;

        while (!isCorrect)
        {
            string answer = Console.ReadLine().Trim().ToLower();

            if (answer == "д")
            {
                result = false; // закрываем программу
                isCorrect = true;
            }
            else if (answer == "н")
            {
                result = true; // остаёмся
                isCorrect = true;
            }
            else
            {
                Console.WriteLine("Некорректный ввод! Введите 'д' или 'н'.");
            }
        }

        return result;
    }
}
