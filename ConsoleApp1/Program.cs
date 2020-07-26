using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDelegate
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***********Delegate as event enablers*********\n");
            Car car = new Car("SlugBug", 100, 10);
            car.RegisterWithCarEngine(new Car.CarEngineHandler(OnCarEngineEvent));

            Console.WriteLine("Speeding Up");
            for (int i = 0; i < 6; i++)
            {
                car.Accelerate(20);
                Console.ReadLine();
            }

        }

        private static void OnCarEngineEvent(string msgForCaller)
        {
            Console.WriteLine("\n **** Message From Car Object*****");
            Console.WriteLine("=>{0}",msgForCaller);
            Console.WriteLine("*************************************\n");
        }
    }

    public class Car
    {
        public int CurrentSpeed { get; set; }
        public int MaxSpeed { get; set; }
        public string PetName { get; set; }
        
        private bool carIsDead;

        public Car()
        {
            MaxSpeed = 240;
        }

        public Car(string name, int maxSp, int currSp)
        {
            CurrentSpeed = currSp;
            MaxSpeed = maxSp;
            PetName = name;
        }

        public delegate void CarEngineHandler (string msgForCaller );
        private CarEngineHandler listOfHandlers;
        public void RegisterWithCarEngine(CarEngineHandler methodToCall)
        {
            listOfHandlers = methodToCall;
        }

        public void Accelerate(int delta)
        {
            if(carIsDead)
            {
                if (listOfHandlers != null)
                    listOfHandlers("Sorry this car is dead");
            }
            else
            {
                CurrentSpeed += delta;
                if(10==(MaxSpeed-CurrentSpeed)&&listOfHandlers!=null)
                {
                    listOfHandlers("Careful buddy! Gonna blow!");
                }
                if (CurrentSpeed >= MaxSpeed)
                    carIsDead = true;
                else
                    Console.WriteLine("CurrentSpeed={0}",CurrentSpeed);
            }
        }


    }
}
