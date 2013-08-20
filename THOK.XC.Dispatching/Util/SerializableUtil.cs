using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml;

namespace THOK.XC.Dispatching.Util
{
    class SerializableUtil
    {
        public static void Serialize(bool useBinary, string fileName, object o)
        {
            Stream file = File.Open(fileName, FileMode.Create);

            IFormatter formatter = useBinary ? (IFormatter)new BinaryFormatter() : (IFormatter)new SoapFormatter();

            formatter.Serialize(file, o);

            file.Close();
        }
        public static T Deserialize<T>(bool useBinary, string fileName) where T : new()
        {
            T o = new T();

            if (!File.Exists(fileName))
            {
                return new T();
            }

            Stream file = File.Open(fileName, FileMode.Open);

            IFormatter formatter = useBinary ? (IFormatter)new BinaryFormatter() : (IFormatter)new SoapFormatter();

            try
            {
                o = (T) formatter.Deserialize(file);
            }
            catch (Exception e)
            {
            }
            file.Close();
            return o;
        }
    }
}
