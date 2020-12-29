using System;
using System.Collections.Generic;
using System.Text;

namespace AttributeTest
{
    /// <summary>
    /// 내가 필요한 attribute
    /// 필요시 추가 삭제 가능
    /// </summary>
    public class PlcDeviceInfoAttribute : Attribute
    {
        public int length { get; set; }
        public int offset { get; set; }

        public PlcDeviceInfoAttribute()
        {

        }
        public PlcDeviceInfoAttribute(int origin)
        {
            offset = origin;
        }
        public PlcDeviceInfoAttribute(int len,int origin)
        {
            length = len;
            offset = origin;
        }
    }
}
