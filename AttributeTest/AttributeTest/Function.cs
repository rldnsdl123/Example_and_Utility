using System;
using System.Collections.Generic;
using System.Text;

namespace AttributeTest
{
    /// <summary>
    /// Attribute의  offset과 length를 가져오는 함수
    /// </summary>
    public class Function
    {
       public int GetOffset(string propertyName="")
        {
            var findOffset = AttributeHelper.GetOffset(GetType(), propertyName);
            return findOffset;
        }

        public int GetLength(string propertyName)
        {
            var findLength = AttributeHelper.GetLength(GetType(), propertyName);
            return findLength;
        }
    }
}
