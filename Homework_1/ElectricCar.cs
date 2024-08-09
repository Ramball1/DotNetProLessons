using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_1
{
    public class ElectricCar : Car
    {
        public double _batteryCapacity = 0;

        public ElectricCar(string brand, string model, int year, double mileage, double batteryCapacity) : base(brand, model, year, mileage)
        {
            _batteryCapacity = batteryCapacity;
        }

        public void ShowElectricCarInfo()
        {
            Console.WriteLine($"Car Brand Name: {_brand}" +
                $"\nCar Model Name: {_model}" +
                $"\nCar Release Year: {_year}" +
                $"\nCar Mileage: {_mileage}" +
                $"\nCar Battery Capacity: {_batteryCapacity}");
        }

        public double Charge(double amount)
        {
            const double minBatteryCapacity = 0;
            const double maxBatteryCapacity = 100;

            if (amount < minBatteryCapacity)
            {
                throw new ArgumentException("\nОшибка! Количество для заряда не может быть отрицательным! Попробуйте еще раз: ");
            }

            _batteryCapacity += amount;

            if (_batteryCapacity >= maxBatteryCapacity)
            {
                _batteryCapacity = maxBatteryCapacity;
            }

            return _batteryCapacity;
        }

        public double Charge(double initialBatteryCapacity, double distance, double consumptionPerKm)
        {
            double batteryLevel = initialBatteryCapacity;
            double usedCharge = distance * consumptionPerKm;
            double distanceAtFullCharge = batteryLevel / consumptionPerKm;

            if (initialBatteryCapacity > 100 || initialBatteryCapacity < 0)
            {
                throw new ArgumentException("\nОшибка! Начальное количество заряда не может быть больше 100 % или меньше 0%! Попробуйте еще раз: ");
            }

            if (distance > distanceAtFullCharge)
            {
                double remainingDistance = distance - distanceAtFullCharge;

                Console.WriteLine($"\nBattery depleted at {distanceAtFullCharge} km, Additional distance {remainingDistance} km");
                return 0;
            }

            else
            {
                batteryLevel -= usedCharge;
                return batteryLevel;
            }
        }
    }
}