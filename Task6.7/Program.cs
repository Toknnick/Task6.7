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
        private Train _train = new Train();

        public void CreateRoute()
        {
            Console.Clear();
            _waitingTrains.Add(new Train(ChooseStartWay(), ChooseEndWay()));
        }

        public void SendTrain()
        {
            ChooseTrain();
        }

        public void ShowInfoAboutWaitingTrains()
        {
            if (_waitingTrains.Count > 0)
            {
                for (int i = 0; i < _waitingTrains.Count; i++)
                {
                    _waitingTrains[i].ShowInfo((i + 1));
                }
            }
            else
            {
                Console.WriteLine("Поездов в депо нет!");
            }
        }

        public void ShowInfoAboutSentTrains()
        {
            if (_sentTrains.Count > 0)
            {
                for (int i = 0; i < _sentTrains.Count; i++)
                {
                    _sentTrains[i].ShowInfo((i + 1));
                }
            }
            else
            {
                Console.WriteLine("Поездов в пути нет!");
            }
        }

        private string ChooseStartWay()
        {
            Console.WriteLine("Выберите пункт отправления:");
            _train.BeginningOfWay = Console.ReadLine();
            return _train.BeginningOfWay;
        }

        private string ChooseEndWay()
        {
            Console.WriteLine("Выберите пункт прибытия:");
            _train.EndingOfWay = Console.ReadLine();
            return _train.EndingOfWay;
        }

        private void ChooseTrain()
        {
            Console.Clear();
            Console.WriteLine("Поезда ожидающие в депо:");
            ShowInfoAboutWaitingTrains();

            if (_waitingTrains.Count > 0)
            {
                TrainDeparture();
            }

            GetMessage();

        }

        private void TrainDeparture()
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

        public string BeginningOfWay { get; set; }
        public string EndingOfWay { get; set; }

        public Train(string beginningOfWay, string endingOfWay)
        {
            BeginningOfWay = beginningOfWay;
            EndingOfWay = endingOfWay;
            _countOfPassangers = GetRandomCountOfPassangers();
            _countOfCarriages = GetCountOfCarriages();
        }

        public Train() { }

        public void ShowInfo(int id)
        {
            Console.WriteLine($"{id}. {BeginningOfWay} - {EndingOfWay}. Число пассажиров: {_countOfPassangers}. Число вагонов: {_countOfCarriages}");
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
            int numberOfSeats = random.Next(minValue,maxValue);
            return numberOfSeats;
        }
    }
}
