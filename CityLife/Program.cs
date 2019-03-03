using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CityLife
{
    public enum service { police, ambulance, fireEmergency }
    public enum happyEvent { cityDay, newYear, bridgeOpening }
    public enum badEvent { fire, crime, epidemy }

    public delegate void CityHandler(string msg);

    public class City
    {
        public string name { get; set; }
        public static Random rnd = new Random();
        public double moodLevel { get; set; }

        public City(string name)
        {
            this.name = name;
            this.moodLevel = 50;
        }

        public event CityHandler Fight;
        public event CityHandler Win;
        public event CityHandler Celebrate;

        public void HappyCityEvent()
        {            
            int choice = rnd.Next(0, 3);
    
            switch ((happyEvent)choice)
            {
                case happyEvent.cityDay:
                    if (Celebrate != null)
                    {
                        Celebrate("С Днем города!");
                        moodLevel += rnd.Next(20,41);
                        int option = rnd.Next(0, 2);
                        if(option==0)
                        Celebrate("Мэр поздравил горожан фирменным танцем!");
                        if (option == 1)
                            Celebrate("Сегодня вход в музей мелиораторов бесплатный!");
                    }
                    break;
                case happyEvent.newYear:
                    if (Celebrate != null)
                    {
                        Celebrate("С Новым годом!");
                        moodLevel += rnd.Next(30, 51);
                        int option = rnd.Next(0, 2);
                        if (option == 0)
                            Celebrate("Государственный фейрверк на ваши пенсионные");
                        if (option == 1)
                            Celebrate("Сегодня вход в музей мелиораторов бесплатный!");
                    }
                    break;
                case happyEvent.bridgeOpening:
                    if (Celebrate != null)
                    {
                        Celebrate("У нас новый мост!");
                        moodLevel += rnd.Next(10, 35);
                        int option = rnd.Next(0, 2);
                        if (option == 0)
                            Celebrate("На личных танках по мосту не ездить!");
                        if (option == 1)
                            Celebrate("Сегодня вход в музей мелиораторов бесплатный!");
                    }
                    break;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Уровень счастья горожан {0} процентов", moodLevel);
            Console.ForegroundColor = ConsoleColor.White;
            if (moodLevel >90)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Текут слезы и сопли радости");
                Console.ForegroundColor = ConsoleColor.White;
            }
            moodLevel = 50;
        }

        public void BadCityEvent()
        {
            int choice = rnd.Next(0, 3);
            switch ((badEvent)choice)
            {
                case badEvent.fire:
                    if (Fight != null)
                    {
                        double mood = 0;
                        Fight("Горит дом!");
                        moodLevel -= rnd.Next(10, 31);
                        Thread.Sleep(2000);
                        Console.Clear();
                        Console.WriteLine("Настроение горожан упало до {0} процентов", moodLevel);
                        Thread.Sleep(2000);
                        while (moodLevel < 70)
                        {
                            int option = rnd.Next(0, 3);
                            switch ((service)option)
                            {
                                case service.police:
                                    Console.Clear();
                                    Fight("На место прибыла полиция");
                                    mood= rnd.Next(10, 15);
                                    break;
                                case service.ambulance:
                                    Console.Clear();
                                    Fight("Скорая помощь приехала");
                                    mood= rnd.Next(10, 15);

                                    break;
                                case service.fireEmergency:
                                    Console.Clear();
                                    Fight("Приехали пожарники");
                                    mood= rnd.Next(20, 41);
                                    break;
                                default:
                                    break;
                            }
                            moodLevel += mood;
                        }
                        Console.Clear();
                        Win.Invoke("Пожар потушен! Жертв нет!");
                    }                   
                    break;
                case badEvent.crime:
                    if (Fight != null)
                    {
                        Fight("В городе появилась мафия!");
                        moodLevel -= rnd.Next(10, 31);
                        Thread.Sleep(2000);
                        Console.Clear();
                        Console.WriteLine("Настроение горожан упало до {0} процентов", moodLevel);
                        Thread.Sleep(2000);
                        while (moodLevel < 70)
                        {
                            double mood = 0;
                            int option = rnd.Next(0, 3);
                            switch ((service)option)
                            {
                                case service.police:
                                    Console.Clear();
                                    Fight("Полиция ловит преступников");
                                    mood= rnd.Next(20, 41);
                                    Thread.Sleep(2000);
                                    break;                           
                                                                        
                                case service.ambulance:
                                    Console.Clear();
                                    Fight("Скорая помощь раздает успокоительное");
                                    mood= rnd.Next(10, 15);
                                    Thread.Sleep(2000);
                                    break;
                                case service.fireEmergency:
                                    Console.Clear();
                                    Fight("Пожарники переживают за полицейских");
                                    mood= rnd.Next(5, 10);
                                    Thread.Sleep(2000);
                                    break;                            
                            }
                            moodLevel += mood;
                        }
                        Console.Clear();
                        Win.Invoke("Всех мафиози отловили! Город может спать спокойно");
                    }
                    break;
                case badEvent.epidemy:
                    if (Fight != null)
                    {
                        Fight("В городе эпидемия гриппа!");
                        moodLevel -= rnd.Next(10, 31);
                        Thread.Sleep(2000);
                        Console.Clear();
                        Console.WriteLine("Настроение горожан упало до {0} процентов", moodLevel);
                        Thread.Sleep(2000);

                        while (moodLevel < 70)
                        {
                            int option = rnd.Next(0, 3);
                            double mood = 0;
                            switch ((service)option)
                            {
                                case service.police:
                                    Console.Clear();
                                    Fight("Полиция следит за порядком в аптеках");
                                    mood= rnd.Next(5, 10);
                                    
                                    Thread.Sleep(2000);
                                    break;
                                case service.ambulance:
                                    Console.Clear();
                                    Fight("Скорая помощь ускоренно лечит");
                                    mood= rnd.Next(20, 41);
                                    
                                    Thread.Sleep(2000);
                                    break;
                                case service.fireEmergency:
                                    Console.Clear();
                                    Fight("Пожарники вяжут шарфы");
                                    mood= rnd.Next(5, 10);
                               
                                    Thread.Sleep(2000);
                                    break;
                                default:
                                    break;
                            }
                            moodLevel += mood;
                        }
                        Console.Clear();
                        Win.Invoke("И тебя вылечили, и меня вылечили!");
                    }
                    break;
                default:
                    break;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Настроение горожан - {0} процентов", moodLevel);
            Console.ForegroundColor = ConsoleColor.White;
            if (moodLevel > 90)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Текут слезы и сопли радости");
                Console.ForegroundColor = ConsoleColor.White;
            }
            moodLevel = 50;
        }

        class Program
        {

            private static void PrintMessage(string msg)
            {
                Console.WriteLine(msg);
            }

            static void Main(string[] args)
            {
                City city = new City("Бикини-Боттом");                
                city.Celebrate += PrintMessage;
                city.Fight += PrintMessage;
                city.Win += PrintMessage;
                Console.WriteLine("Доброе утро, жители {0}!",city.name);
                Thread.Sleep(3000);
                Console.Clear();
                city.BadCityEvent();                
                Thread.Sleep(3000);
                Console.Clear();
                Console.WriteLine("А теперь к хорошим новостям", city.name);
                Thread.Sleep(3000);
                Console.Clear();
                city.HappyCityEvent();            
                
            }
        }
    }
}
