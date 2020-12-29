using System;
using System.Collections.Generic;
using System.Text;

namespace AttributeTest
{
    /// <summary>
    /// Function 클래스에 Attribute값을 가져오는 함수를 만들어 사용
    /// </summary>
    public class PLCBits : Function
    {
        /// <summary>
        /// PlcDeviceInfo라는 Attribute를 통해 값을 입력함
        /// </summary>
        private int _FirstBit;
        [PlcDeviceInfo(11)]
        public int FirstBit
        {
            get => GetOffset(nameof(FirstBit));
            set
            {
                _FirstBit = value;
            }

        }
        
        /// <summary>
        /// PlcDeviceInfo라는 Attribute를 통해 값을 입력
        /// </summary>
        private int _FirstLength;
        [PlcDeviceInfo(20,10)]
        public int FirstLength
        {
            get => GetLength(nameof(FirstLength));
            set
            {
                _FirstLength = value ;
            }
        }

    }
}
