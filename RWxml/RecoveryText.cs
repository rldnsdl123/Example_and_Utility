using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RWxml
{
    [XmlType("cAlarmRecoverySolution")]
    public class RecoveryText
    {
        private string _Solution;

        [XmlElement("Solution")]
        public string Solution
        {
            get => _Solution;
            set
            {
                if (_Solution == value)
                    return;
                _Solution = value;
            }
        }
        public RecoveryText()
        {

        }
    }
}
