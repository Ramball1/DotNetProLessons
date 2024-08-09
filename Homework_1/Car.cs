using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_1
{
    public class Car
    {
        public string _brand = string.Empty;
        public string _model = string.Empty;
        public int _year = 0;
        public double _mileage = 0d;

        public Car(string brand, string model, int year, double mileage)
        {
            _brand = brand;
            _model = model;
            _year = year;
            _mileage = mileage;
        }

        public void ShowCarInfo()
        {
            Console.WriteLine($"Car Brand Name: {_brand}" +
                $"\nCar Model Name: {_model}" +
                $"\nCar Release Year: {_year}" +
                $"\nCar Mileage: {_mileage}");
        }

        public double Drive(double distance)
        {
            const double minDistance = 0d;
            const double maxDistance = 999999d;

            if (distance < minDistance)
            {
                throw new ArgumentException("\nОшибка! Дистанция не может быть отрицательной! Попробуйте еще раз: ");
            }

            if (distance > maxDistance)
            {
                distance = maxDistance;
                _mileage = distance;
                return _mileage;
            }

            _mileage += distance;
            return _mileage;
        }

        // Оба варианта работают первый вариант имеет свойство с логикой а второй только для чтения

        //public int Age
        //{
        //    get
        //    {
        //        int currentYear = DateTime.Now.Year;
        //        return currentYear - _year;
        //    }
        //}

        public int Age => DateTime.Now.Year - _year;

        public double Mileage
        {
            get { return _mileage; }
            private set { _mileage = value; }
        }

        public double _averageSpeed = 0d;

        public double Drive(double distance, double time)
        {
            const double minDistance = 0d;
            const double minTime = 0d;

            if (distance < minDistance || time < minTime)
            {
                throw new ArgumentException("\nОшибка! Дистанция или время не могут быть отрицательными! Попробуйте еще раз: ");
            }

            _averageSpeed = distance / time;
            return _averageSpeed;
        }

        ~Car()
        {
            Console.WriteLine($"Машина уничтожена!");
        }
    }
}