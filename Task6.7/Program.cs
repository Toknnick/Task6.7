using System;
using System.Collections.Generic;

namespace Task6._7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            User user = new User();
            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine("Информация о поездах в депо:");
                user.ShowInfoAboutWaitingTrains();
                Console.WriteLine(" \nИнформация о поездах в пути:");
                user.ShowInfoAboutSentTrains();
                Console.WriteLine(" \n1.Создать маршрут поезда. \n2.Отправить поезд. \n3.Выход. \nВыберите вариант:");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        user.CreateRoute();
                        break;
                    case "2":
                        user.SendTrain();
                        break;
                    case "3":
                        isWork = false;
                        break;
                }

                Console.Clear();
            }
        }
    }

    class User
    {
        private List<Train> _waitingTrains = new List<Train>();
        private List<Train> _sentTrains = new List<Train>();

        public void CreateRoute()
        {
            Console.Clear();
            _waitingTrains.Add(new Train(ChooseStartWay(), ChooseEndWay()));
        }

        public void SendTrain()
        {
            Console.Clear();
            Console.WriteLine("Поезда ожидающие в депо:");
            ShowInfoAboutTrains(_waitingTrains, "депо");

            if (_waitingTrains.Count > 0)
            {
                bool isRepeating = true;
                Console.WriteLine("Выберите номер поезда:");

                while (isRepeating)
                {
                    string userInput = Console.ReadLine();

                    if (int.TryParse(userInput, out int number))
                    {
                        number -= 1;

                        if (number >= 0 && number < _waitingTrains.Count)
                        {
                            _sentTrains.Add(_waitingTrains[number]);
                            _waitingTrains.RemoveAt(number);
                            Console.WriteLine("Поезд отправлен успешно!");
                            GetMessage();
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Поезд не найден!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Данные некорректные");
                    }

                    Console.WriteLine("Попробуйте снова:");
                }
            }

            GetMessage();
        }

        public void ShowInfoAboutWaitingTrains()
        {
            ShowInfoAboutTrains(_waitingTrains, "депо");
        }

        public void ShowInfoAboutSentTrains()
        {
            ShowInfoAboutTrains(_sentTrains, "пути");
        }

        private void ShowInfoAboutTrains(List<Train> list, string word)
        {
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].ShowInfo((i + 1));
                }
            }
            else
            {
                Console.WriteLine($"Поездов в {word} нет!");
            }
        }

        private string ChooseStartWay()
        {
            Console.WriteLine("Выберите пункт отправления:");
           string beginningOfWay = Console.ReadLine();
            return beginningOfWay;
        }

        private string ChooseEndWay()
        {
            Console.WriteLine("Выберите пункт прибытия:");
            string endingOfWay = Console.ReadLine();
            return endingOfWay;
        }

        private void GetMessage()
        {
            Console.WriteLine("Для продолжения нажмите любую клавишу:");
            Console.ReadKey();
        }
    }

    class Train
    {
        private int _countOfPassangers;
        private int _countOfCarriages;

        private string _beginningOfWay;
        private string _endingOfWay;

        public Train(string beginningOfWay, string endingOfWay)
        {
            _beginningOfWay = beginningOfWay;
            _endingOfWay = endingOfWay;
            _countOfPassangers = GetRandomCountOfPassangers();
            _countOfCarriages = GetCountOfCarriages();
        }

        public void ShowInfo(int id)
        {
            Console.WriteLine($"{id}. {_beginningOfWay} - {_endingOfWay}. Число пассажиров: {_countOfPassangers}. Число вагонов: {_countOfCarriages}");
        }

        private int GetCountOfCarriages()
        {
            int numberOfPassangerInCarriages = GetNumberOfSeats();
            int countOFCarriages = _countOfPassangers / numberOfPassangerInCarriages;

            if (_countOfPassangers % numberOfPassangerInCarriages < numberOfPassangerInCarriages)
                countOFCarriages += 1;
            
            return countOFCarriages;
        }

        private int GetRandomCountOfPassangers()
        {
            Random random = new Random();
            int maxValue = 100;
            int countOfPassengers = random.Next(maxValue);
            return countOfPassengers;
        }

        private int GetNumberOfSeats()
        {
            Random random = new Random();
            int maxValue = 10;
            int minValue = 5;
            int numberOfSeats = random.Next(minValue, maxValue);
            return numberOfSeats;
        }
    }
}
