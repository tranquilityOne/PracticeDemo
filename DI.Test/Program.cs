using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //没有加入依赖对象声明,修改Tire类的属性会导致全部类进行修改
            Car car = new Car();
            car.Run();

            //依赖对象声明调用
            Tire tire = new Tire(10);
            Bottom bottom = new Bottom(tire);
            Framework framework = new Framework(bottom);

            Car newCar = new Car(framework);
            newCar.Run();
        }
    }
}
