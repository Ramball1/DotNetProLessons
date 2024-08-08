using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;

namespace Homework_1
{
    enum MenuItem
    {
        TaskAddMileage = '!',
        TaskAddMileageStopOrSkip = '$',
    }

    enum MenuItemAge
    {
        TaskCarAge = '!',
        TaskCarAgeSkip = '$'
    }

    enum MenuItemAverageSpeed
    {
        TaskCarAverageSpeed = '!',
        TaskCarAverageSpeedSkip = '$'
    }

    enum MenuItemElectricCar
    {
        TaskChargeBattery = '!',
        TaskChargeBatterySkip = '$'
    }

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

    public class Programm
    {
        public static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            Car newCar = new Car("Koenigsegg", "Jesko Absolut", 2020, 0d);
            newCar.ShowCarInfo();

            while (true)
            {
                try
                {
                    Console.WriteLine($"\nIf you want to Increase Mileage Task: Press: {(char)MenuItem.TaskAddMileage}" +
                        $" \nIf you want to skip Increase Mileage Task: Press: {(char)MenuItem.TaskAddMileageStopOrSkip}");
                    var menuSelect = (char)Console.Read();
                    Console.ReadLine();
                    var currentSelect = (MenuItem)menuSelect;

                    if (menuSelect != ((char)MenuItem.TaskAddMileage) && menuSelect != ((char)MenuItem.TaskAddMileageStopOrSkip))
                    {
                        throw new Exception("Ошибка! Введен не правильный символ или Вы вообще не ввели символ! Попробуйте еще раз: ");
                    }

                    switch (currentSelect)
                    {
                        case MenuItem.TaskAddMileage:
                            {
                                while (menuSelect == ((char)MenuItem.TaskAddMileage))
                                {
                                    try
                                    {
                                        Console.WriteLine("\nEnter how much you want to increase the mileage of the car in km: ");
                                        double distance = Convert.ToDouble(Console.ReadLine());
                                        double mileage = newCar.Drive(distance);
                                        Console.WriteLine($"Increased mileage of the car: {mileage} km \n");
                                        newCar.ShowCarInfo();

                                        const double maxIncreasedMileage = 999999d;

                                        if (maxIncreasedMileage < distance)
                                        {
                                            Console.WriteLine($"\nYou have reached the limit of increasing the vehicle's mileage! " +
                                                $"Your Total Mileage now is: {maxIncreasedMileage} km");
                                            break;
                                        }

                                        while (menuSelect == ((char)MenuItem.TaskAddMileage))
                                        {
                                            try
                                            {
                                                Console.WriteLine($"\nIf you want to continue Increase Mileage Task: Press: {(char)MenuItem.TaskAddMileage}" +
                                                    $" \nIf you want to stop and skip Increase Mileage Task: Press: {(char)MenuItem.TaskAddMileageStopOrSkip}");
                                                var menuSelectAction = (char)Console.Read();
                                                Console.ReadLine();
                                                var currentSelectAction = (MenuItem)menuSelectAction;

                                                if (menuSelectAction != ((char)MenuItem.TaskAddMileage) && menuSelectAction != ((char)MenuItem.TaskAddMileageStopOrSkip))
                                                {
                                                    throw new Exception("Ошибка! Введен не правильный символ или Вы вообще не ввели символ! Попробуйте еще раз: ");
                                                }

                                                if (menuSelectAction == ((char)MenuItem.TaskAddMileage))
                                                {
                                                    Console.WriteLine("\nEnter how much you want to increase the mileage of the car in km: ");
                                                    double newDistance = Convert.ToDouble(Console.ReadLine());
                                                    double newMileage = newCar.Drive(newDistance);
                                                    Console.WriteLine($"Total Increased mileage of the car: {newMileage} km \n");
                                                    newCar.ShowCarInfo();

                                                    double newMaxIncreasedMileage = 999999d;

                                                    if (newMaxIncreasedMileage < newDistance)
                                                    {
                                                        Console.WriteLine($"\nYou have reached the limit of increasing the vehicle's mileage! " +
                                                            $"Your Total Mileage now is: {newMaxIncreasedMileage} km");
                                                        break;
                                                    }
                                                }

                                                if (menuSelectAction == ((char)MenuItem.TaskAddMileageStopOrSkip))
                                                {
                                                    while (true)
                                                    {
                                                        try
                                                        {
                                                            Console.WriteLine($"\nIf you want to see the age of your Car: Press: {(char)MenuItemAge.TaskCarAge}" +
                                                                $" \nIf you want to skip the age of your Car Task: Press: {(char)MenuItemAge.TaskCarAgeSkip}");
                                                            var menuSelectAge = (char)Console.Read();
                                                            Console.ReadLine();
                                                            var currentSelectAge = (MenuItemAge)menuSelectAge;

                                                            if (menuSelectAge != ((char)MenuItemAge.TaskCarAge) && menuSelectAge != ((char)MenuItemAge.TaskCarAgeSkip))
                                                            {
                                                                throw new Exception("Ошибка! Введен не правильный символ или Вы вообще не ввели символ! Попробуйте еще раз: ");
                                                            }

                                                            if (menuSelectAge == (char)MenuItemAge.TaskCarAge)
                                                            {
                                                                Console.WriteLine($"\nYour car is {newCar.Age} years old");

                                                                while (true)
                                                                {
                                                                    try
                                                                    {
                                                                        Console.WriteLine($"\nIf you want to know the Average Speed of your Car: Press: {(char)MenuItemAverageSpeed.TaskCarAverageSpeed}" +
                                                                            $" \nIf you want to skip the Average Speed of your Car Task: Press: {(char)MenuItemAverageSpeed.TaskCarAverageSpeedSkip}");
                                                                        var menuSelectAverageSpeed = (char)Console.Read();
                                                                        Console.ReadLine();
                                                                        var currentSelectAverageSpeed = (MenuItemAverageSpeed)menuSelectAverageSpeed;

                                                                        if (menuSelectAverageSpeed != ((char)MenuItemAverageSpeed.TaskCarAverageSpeed) && menuSelectAverageSpeed != ((char)MenuItemAverageSpeed.TaskCarAverageSpeedSkip))
                                                                        {
                                                                            throw new Exception("Ошибка! Введен не правильный символ или Вы вообще не ввели символ! Попробуйте еще раз: ");
                                                                        }

                                                                        if (menuSelectAverageSpeed == (char)MenuItemAverageSpeed.TaskCarAverageSpeed)
                                                                        {
                                                                            Console.WriteLine("\nEnter the distance you want to travel in km: ");
                                                                            double newDistance = Convert.ToDouble(Console.ReadLine());
                                                                            Console.WriteLine("\nEnter the time in hours in which you want to cover this distance: ");
                                                                            double time = Convert.ToDouble(Console.ReadLine());

                                                                            double averageSpeed = newCar.Drive(newDistance, time);
                                                                            Console.WriteLine($"\nAverage speed of the car should be: {averageSpeed} km/h \n");
                                                                        }

                                                                        if (menuSelectAverageSpeed == (char)MenuItemAverageSpeed.TaskCarAverageSpeedSkip)
                                                                        {
                                                                            break;
                                                                        }
                                                                        break;
                                                                    }
                                                                    catch (Exception ex)
                                                                    {
                                                                        Console.WriteLine(ex.Message);
                                                                    }
                                                                }
                                                            }

                                                            if (menuSelectAge == (char)MenuItemAge.TaskCarAgeSkip)
                                                            {
                                                                while (true)
                                                                {
                                                                    try
                                                                    {
                                                                        Console.WriteLine($"\nIf you want to know the Average Speed of your Car: Press: {(char)MenuItemAverageSpeed.TaskCarAverageSpeed}" +
                                                                            $" \nIf you want to skip the Average Speed of your Car Task: Press: {(char)MenuItemAverageSpeed.TaskCarAverageSpeedSkip}");
                                                                        var menuSelectAverageSpeed = (char)Console.Read();
                                                                        Console.ReadLine();
                                                                        var currentSelectAverageSpeed = (MenuItemAverageSpeed)menuSelectAverageSpeed;

                                                                        if (menuSelectAverageSpeed != ((char)MenuItemAverageSpeed.TaskCarAverageSpeed) && menuSelectAverageSpeed != ((char)MenuItemAverageSpeed.TaskCarAverageSpeedSkip))
                                                                        {
                                                                            throw new Exception("Ошибка! Введен не правильный символ или Вы вообще не ввели символ! Попробуйте еще раз: ");
                                                                        }

                                                                        if (menuSelectAverageSpeed == (char)MenuItemAverageSpeed.TaskCarAverageSpeed)
                                                                        {
                                                                            Console.WriteLine("\nEnter the distance you want to travel in km: ");
                                                                            double newDistance = Convert.ToDouble(Console.ReadLine());
                                                                            Console.WriteLine("\nEnter the time in hours in which you want to cover this distance: ");
                                                                            double time = Convert.ToDouble(Console.ReadLine());

                                                                            double averageSpeed = newCar.Drive(newDistance, time);
                                                                            Console.WriteLine($"\nAverage speed of the car should be: {averageSpeed} km/h \n");
                                                                        }

                                                                        if (menuSelectAverageSpeed == (char)MenuItemAverageSpeed.TaskCarAverageSpeedSkip)
                                                                        {
                                                                            break;
                                                                        }
                                                                        break;
                                                                    }
                                                                    catch (Exception ex)
                                                                    {
                                                                        Console.WriteLine(ex.Message);
                                                                    }
                                                                }
                                                                break;
                                                            }
                                                            break;
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            Console.WriteLine(ex.Message);
                                                        }
                                                    }
                                                    break;
                                                }
                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine("Не вводите символы только цифры!");
                                            }

                                            catch (Exception ex)
                                            {
                                                Console.WriteLine(ex.Message);
                                            }
                                        }
                                        break;
                                    }
                                    catch (FormatException)
                                    {
                                        Console.WriteLine("Не вводите символы только цифры!");
                                    }
                                }
                                break;
                            }
                        case MenuItem.TaskAddMileageStopOrSkip:
                            {
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine($"\nIf you want to see the age of your Car: Press: {(char)MenuItemAge.TaskCarAge}" +
                                            $" \nIf you want to skip the age of your Car Task: Press: {(char)MenuItemAge.TaskCarAgeSkip}");
                                        var menuSelectAge = (char)Console.Read();
                                        Console.ReadLine();
                                        var currentSelectAge = (MenuItemAge)menuSelectAge;

                                        if (menuSelectAge != ((char)MenuItemAge.TaskCarAge) && menuSelectAge != ((char)MenuItemAge.TaskCarAgeSkip))
                                        {
                                            throw new Exception("Ошибка! Введен не правильный символ или Вы вообще не ввели символ! Попробуйте еще раз: ");
                                        }

                                        if (menuSelectAge == (char)MenuItemAge.TaskCarAge)
                                        {
                                            Console.WriteLine($"\nYour car is {newCar.Age} years old");

                                            while (true)
                                            {
                                                try
                                                {
                                                    Console.WriteLine($"\nIf you want to know the Average Speed of your Car: Press: {(char)MenuItemAverageSpeed.TaskCarAverageSpeed}" +
                                                        $" \nIf you want to skip the Average Speed of your Car Task: Press: {(char)MenuItemAverageSpeed.TaskCarAverageSpeedSkip}");
                                                    var menuSelectAverageSpeed = (char)Console.Read();
                                                    Console.ReadLine();
                                                    var currentSelectAverageSpeed = (MenuItemAverageSpeed)menuSelectAverageSpeed;

                                                    if (menuSelectAverageSpeed != ((char)MenuItemAverageSpeed.TaskCarAverageSpeed) && menuSelectAverageSpeed != ((char)MenuItemAverageSpeed.TaskCarAverageSpeedSkip))
                                                    {
                                                        throw new Exception("Ошибка! Введен не правильный символ или Вы вообще не ввели символ! Попробуйте еще раз: ");
                                                    }

                                                    if (menuSelectAverageSpeed == (char)MenuItemAverageSpeed.TaskCarAverageSpeed)
                                                    {
                                                        Console.WriteLine("\nEnter the distance you want to travel in km: ");
                                                        double distance = Convert.ToDouble(Console.ReadLine());
                                                        Console.WriteLine("\nEnter the time in hours in which you want to cover this distance: ");
                                                        double time = Convert.ToDouble(Console.ReadLine());

                                                        double averageSpeed = newCar.Drive(distance, time);
                                                        Console.WriteLine($"\nAverage speed of the car should be: {averageSpeed} km/h \n");
                                                    }

                                                    if (menuSelectAverageSpeed == (char)MenuItemAverageSpeed.TaskCarAverageSpeedSkip)
                                                    {
                                                        break;
                                                    }
                                                    break;
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine(ex.Message);
                                                }
                                            }
                                        }

                                        if (menuSelectAge == (char)MenuItemAge.TaskCarAgeSkip)
                                        {
                                            while (true)
                                            {
                                                try
                                                {
                                                    Console.WriteLine($"\nIf you want to know the Average Speed of your Car: Press: {(char)MenuItemAverageSpeed.TaskCarAverageSpeed}" +
                                                        $" \nIf you want to skip the Average Speed of your Car Task: Press: {(char)MenuItemAverageSpeed.TaskCarAverageSpeedSkip}");
                                                    var menuSelectAverageSpeed = (char)Console.Read();
                                                    Console.ReadLine();
                                                    var currentSelectAverageSpeed = (MenuItemAverageSpeed)menuSelectAverageSpeed;

                                                    if (menuSelectAverageSpeed != ((char)MenuItemAverageSpeed.TaskCarAverageSpeed) && menuSelectAverageSpeed != ((char)MenuItemAverageSpeed.TaskCarAverageSpeedSkip))
                                                    {
                                                        throw new Exception("Ошибка! Введен не правильный символ или Вы вообще не ввели символ! Попробуйте еще раз: ");
                                                    }

                                                    if (menuSelectAverageSpeed == (char)MenuItemAverageSpeed.TaskCarAverageSpeed)
                                                    {
                                                        Console.WriteLine("\nEnter the distance you want to travel in km: ");
                                                        double distance = Convert.ToDouble(Console.ReadLine());
                                                        Console.WriteLine("\nEnter the time in hours in which you want to cover this distance: ");
                                                        double time = Convert.ToDouble(Console.ReadLine());

                                                        double averageSpeed = newCar.Drive(distance, time);
                                                        Console.WriteLine($"\nAverage speed of the car should be: {averageSpeed} km/h \n");
                                                    }

                                                    if (menuSelectAverageSpeed == (char)MenuItemAverageSpeed.TaskCarAverageSpeedSkip)
                                                    {
                                                        break;
                                                    }
                                                    break;
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine(ex.Message);
                                                }
                                            }
                                            break;
                                        }
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                }
                                break;
                            }
                    }
                    if (menuSelect == ((char)MenuItem.TaskAddMileage) || menuSelect == ((char)MenuItem.TaskAddMileageStopOrSkip))
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine("\nElectric Car Task: \n");

            ElectricCar newElectricCar = new ElectricCar("Porshe", "Taycan", 2019, 0d, 0d);
            newElectricCar.ShowElectricCarInfo();

            while (true)
            {
                try
                {
                    Console.WriteLine($"\nIf you want to Charge Battery: Press: {(char)MenuItem.TaskAddMileage}" +
                        $" \nIf you want to skip Charge Battery Task: Press: {(char)MenuItem.TaskAddMileageStopOrSkip}");
                    var menuSelectElectricCar = (char)Console.Read();
                    Console.ReadLine();
                    var currentSelectElectricCar = (MenuItemElectricCar)menuSelectElectricCar;

                    if (menuSelectElectricCar != ((char)MenuItemElectricCar.TaskChargeBattery) && menuSelectElectricCar != ((char)MenuItemElectricCar.TaskChargeBatterySkip))
                    {
                        throw new Exception("Ошибка! Введен не правильный символ или Вы вообще не ввели символ! Попробуйте еще раз: ");
                    }

                    switch (currentSelectElectricCar)
                    {
                        case MenuItemElectricCar.TaskChargeBattery:
                            {
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("\nEnter how much you want to charge the car in %: ");
                                        double amount = Convert.ToDouble(Console.ReadLine());
                                        double amountCharged = newElectricCar.Charge(amount);
                                        Console.WriteLine($"\nYour battery charged up to {amountCharged} % \n");
                                        newElectricCar.ShowElectricCarInfo();

                                        const double maxBatteryCapacity = 100;

                                        if (newElectricCar._batteryCapacity == maxBatteryCapacity)
                                        {
                                            Console.WriteLine("\nYour Battery is fully Charged!");

                                            while (true)
                                            {
                                                try
                                                {
                                                    Console.WriteLine("\nEnter the initial Battery Capacity in %: ");
                                                    double initialBatteryCapacity = Convert.ToDouble(Console.ReadLine());
                                                    Console.WriteLine("\nEnter the distance traveled in km: ");
                                                    double distance = Convert.ToDouble(Console.ReadLine());
                                                    Console.WriteLine("\nEnter the energy consumption per km in %: ");
                                                    double consumptionPerKm = Convert.ToDouble(Console.ReadLine());

                                                    double remainingBattery = newElectricCar.Charge(initialBatteryCapacity, distance, consumptionPerKm);

                                                    if (remainingBattery >= 0)
                                                    {
                                                        Console.WriteLine($"Battery level after the trip: {remainingBattery} %");
                                                    }
                                                    break;
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Не вводите символы только цифры!");
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine(ex.Message);
                                                }
                                            }
                                            break;
                                        }

                                        while (true)
                                        {
                                            try
                                            {
                                                Console.WriteLine($"\nIf you want to Charge Battery Again: Press: {(char)MenuItem.TaskAddMileage}" +
                                                    $" \nIf you want to skip Charge Battery Again: Press: {(char)MenuItem.TaskAddMileageStopOrSkip}");
                                                var menuSelectElectricCarAgain = (char)Console.Read();
                                                Console.ReadLine();
                                                var currentSelectElectricCarAgain = (MenuItemElectricCar)menuSelectElectricCar;

                                                if (menuSelectElectricCarAgain != ((char)MenuItemElectricCar.TaskChargeBattery) && menuSelectElectricCarAgain != ((char)MenuItemElectricCar.TaskChargeBatterySkip))
                                                {
                                                    throw new Exception("Ошибка! Введен не правильный символ или Вы вообще не ввели символ! Попробуйте еще раз: ");
                                                }

                                                if (menuSelectElectricCarAgain == (char)MenuItemElectricCar.TaskChargeBattery)
                                                {
                                                    Console.WriteLine("\nEnter how much you want to charge the car in %: ");
                                                    double newAmount = Convert.ToDouble(Console.ReadLine());
                                                    double newAmountCharged = newElectricCar.Charge(newAmount);
                                                    Console.WriteLine($"Your battery charged up to {newAmountCharged} % \n");
                                                    newElectricCar.ShowElectricCarInfo();

                                                    if (newElectricCar._batteryCapacity == maxBatteryCapacity)
                                                    {
                                                        Console.WriteLine("\nYour Battery is fully Charged!");

                                                        while (true)
                                                        {
                                                            try
                                                            {
                                                                Console.WriteLine("\nEnter the initial Battery Capacity in %: ");
                                                                double initialBatteryCapacity = Convert.ToDouble(Console.ReadLine());
                                                                Console.WriteLine("\nEnter the distance traveled in km: ");
                                                                double distance = Convert.ToDouble(Console.ReadLine());
                                                                Console.WriteLine("\nEnter the energy consumption per km in %: ");
                                                                double consumptionPerKm = Convert.ToDouble(Console.ReadLine());

                                                                double remainingBattery = newElectricCar.Charge(initialBatteryCapacity, distance, consumptionPerKm);

                                                                if (remainingBattery >= 0)
                                                                {
                                                                    Console.WriteLine($"Battery level after the trip: {remainingBattery} %");
                                                                }
                                                                break;
                                                            }
                                                            catch (FormatException)
                                                            {
                                                                Console.WriteLine("Не вводите символы только цифры!");
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                Console.WriteLine(ex.Message);
                                                            }
                                                        }
                                                        break;
                                                    }
                                                }

                                                if (menuSelectElectricCarAgain == (char)MenuItemElectricCar.TaskChargeBatterySkip)
                                                {
                                                    while (true)
                                                    {
                                                        try
                                                        {
                                                            Console.WriteLine("\nEnter the initial Battery Capacity in %: ");
                                                            double initialBatteryCapacity = Convert.ToDouble(Console.ReadLine());
                                                            Console.WriteLine("\nEnter the distance traveled in km: ");
                                                            double distance = Convert.ToDouble(Console.ReadLine());
                                                            Console.WriteLine("\nEnter the energy consumption per km in %: ");
                                                            double consumptionPerKm = Convert.ToDouble(Console.ReadLine());

                                                            double remainingBattery = newElectricCar.Charge(initialBatteryCapacity, distance, consumptionPerKm);

                                                            if (remainingBattery >= 0)
                                                            {
                                                                Console.WriteLine($"Battery level after the trip: {remainingBattery} %");
                                                            }
                                                            break;
                                                        }
                                                        catch (FormatException)
                                                        {
                                                            Console.WriteLine("Не вводите символы только цифры!");
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            Console.WriteLine(ex.Message);
                                                        }
                                                    }
                                                    break;
                                                }

                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine(ex.Message);
                                            }
                                        }
                                        break;
                                    }
                                    catch (FormatException)
                                    {
                                        Console.WriteLine("Не вводите символы только цифры!");
                                    }
                                }
                                break;
                            }
                        case MenuItemElectricCar.TaskChargeBatterySkip:
                            {
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("\nEnter the initial Battery Capacity in %: ");
                                        double initialBatteryCapacity = Convert.ToDouble(Console.ReadLine());
                                        Console.WriteLine("\nEnter the distance traveled in km: ");
                                        double distance = Convert.ToDouble(Console.ReadLine());
                                        Console.WriteLine("\nEnter the energy consumption per km in %: ");
                                        double consumptionPerKm = Convert.ToDouble(Console.ReadLine());

                                        double remainingBattery = newElectricCar.Charge(initialBatteryCapacity, distance, consumptionPerKm);

                                        if (remainingBattery >= 0)
                                        {
                                            Console.WriteLine($"Battery level after the trip: {remainingBattery} %");
                                        }
                                        break;
                                    }
                                    catch (FormatException)
                                    {
                                        Console.WriteLine("Не вводите символы только цифры!");
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                }
                                break;
                            }
                    }
                    if (menuSelectElectricCar == ((char)MenuItemElectricCar.TaskChargeBattery) || menuSelectElectricCar == ((char)MenuItemElectricCar.TaskChargeBatterySkip))
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}