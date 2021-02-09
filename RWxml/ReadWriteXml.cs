using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RWxml
{
    public static class ReadWriteXml
    {
        static readonly XmlSerializer _XmlSerializer = new XmlSerializer(typeof(RecoveryText));
        public static XmlSerializer GetSerializer()
        {
            return _XmlSerializer;
        }
        public static RecoveryText LoadXml(string path)
        {
            try
            {

                FileInfo info = new FileInfo(path);
                if (!info.Exists)
                    return null;

                lock (_XmlSerializer)
                {
                    RecoveryText readparam;
                    XmlSerializer deserial = GetSerializer();
                    using (TextReader txtReader = new StreamReader(string.Format(@"{0}", path)))
                        readparam = (RecoveryText)deserial.Deserialize(txtReader);

                    return readparam;
                }
            }
            catch
            {
                return null;
            }
        }
        public static bool SaveXml(RecoveryText instance, string path)
        {
            try
            {
                lock (_XmlSerializer)
                {
                    XmlSerializer serializer = GetSerializer();
                    using (TextWriter txtWriter = new StreamWriter(string.Format(@"{0}", path)))
                        serializer.Serialize(txtWriter, instance);
                }
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
