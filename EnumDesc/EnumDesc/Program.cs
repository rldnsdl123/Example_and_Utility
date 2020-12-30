using System;

namespace EnumDesc
{
    class Program
    {
        /// <summary>
        /// Enumhelper를 통해 enum의 Description의 값을 가져올때 
        /// Description을 지정해주지 않으면 Enum의 이름을 그래도 반환한다.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine(EnumHelper.GetDescription(Define.First));
            Console.WriteLine(EnumHelper.GetDescription(Define.Second));
            
        }
    }
}
