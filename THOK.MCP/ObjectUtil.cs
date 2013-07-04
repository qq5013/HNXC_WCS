using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.MCP
{
    public class ObjectUtil
    {
        /// <summary>
        /// 从Array中获取第一个元素
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static object GetObject(object o)
        {
            object result = null;
            if (o is Array)
            {
                Array a = (Array)o;
                result = a.GetValue(0);
            }
            return result;
        }

        /// <summary>
        /// 如果o为Array，则转换成数组
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static object[] GetObjects(object o)
        {
            object[] result = null;
            if (o is Array)
            {
                Array a = (Array)o;
                result = new object[a.Length];
                for (int i = 0; i < a.Length; i++)
                    result[i] = a.GetValue(i);
            }
            return result;
        }
    }
}
