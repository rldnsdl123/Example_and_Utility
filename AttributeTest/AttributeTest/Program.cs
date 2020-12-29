using System;

namespace AttributeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            PLCBits Bits = new PLCBits();

            // 실제 외부에서 아무값도 입력하지 않았지만 
            // 내부 프로퍼티에서 이미 Attribute를 통해 값을 지정해 주었기 때문에 
            // 지정해준 값이 출력됨
            System.Diagnostics.Debug.WriteLine(Bits.FirstBit);
            System.Diagnostics.Debug.WriteLine(Bits.FirstLength);
        }
    }
}
