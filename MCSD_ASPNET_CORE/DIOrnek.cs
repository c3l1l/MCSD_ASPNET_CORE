using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCSD_ASPNET_CORE
{
    public class DIOrnek
    {
        public DIOrnek()
        {
            Console.WriteLine("DI Örneği");

            Araba1 a1 = new Araba1();

            Araba2 a2 = new Araba2(new BenzinMotor());

            Araba2 a3 = new Araba2(new ElektrikMotor());

            Araba2 a4 = new Araba2(new DizelMotor());

        }
    }

    public class BenzinliMotor
    {
        public void Calistir()
        {
            Console.WriteLine("Benzinli motor çalıştı.");
        }
    }

    public class Araba1
    {
        public Araba1()
        {
            //Araba sınıfı, BenzinliMoto sınıfına sıkı bir şekilde bağlıdır. 
            /*BenzinliMotor bm = new BenzinliMotor();
            bm.Calistir();*/

            ElektrikliMotor em = new ElektrikliMotor();
            em.Calistir();
        }
    }

    public class ElektrikliAraba { }

    //Bundan böyle arabalar elektrikli motor kullansın dediğimiz anda Araba sınıfına dokunmak onu değiştirmek zorundayız. 
    public class ElektrikliMotor
    {
        public void Calistir()
        {
            Console.WriteLine("Elektrikli motor çalıştı.");
        }
    }

    //Burada Üst Seviye olan Araba sınıfı Alt Seviye olan BenzinliMotor veya ElektrikliMotor sınıflarına bağımlı olduğu SOLID'ın DIP (Dependency Inversion Principle) ilkesini ihlal etmekte. DIP, somut sınıflara değil soyutlamara bağlı olmalıyız.

    public interface IMotor
    {
        void Calistir();
    }


    public class Araba2
    {
        private IMotor _motor;

        //Artık Araba sınıfımız, motorun belirli bir uygulama değil yalnızca IMotor arayüzüne bağlıdır. Yani soyut olarak bağlı.
        public Araba2(IMotor motor)
        {
            _motor = motor;
        }

        public void Yurut()
        {
            _motor.Calistir();
        }
    }

    //GEVŞEK BAĞLANTI : Araba sınıfını değiştirmeden kolayca yeni motor türleri ekleyebiliriz.

    public class BenzinMotor : IMotor
    {
        public void Calistir()
        {
            Console.WriteLine("Benzinli motor çalıştı.");
        }
    }

    public class ElektrikMotor : IMotor
    {
        public void Calistir()
        {
            Console.WriteLine("Elektrikli motor çalıştı.");
        }
    }

    public class DizelMotor : IMotor
    {
        public void Calistir()
        {
            Console.WriteLine("Dizel motor çalıştı.");
        }
    }

}

