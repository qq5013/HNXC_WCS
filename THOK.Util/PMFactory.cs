namespace THOK.Util
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    internal class PMFactory
    {
        private static Dictionary<string, int> countCollection = new Dictionary<string, int>();
        private static Dictionary<string, PersistentManager> pmCollection = new Dictionary<string, PersistentManager>();

        internal static void AddPM(PersistentManager pm)
        {
            string key = Thread.CurrentThread.ManagedThreadId.ToString();
            pmCollection.Add(key, pm);
            countCollection[key] = 0;
        }

        internal static PersistentManager GetPM()
        {
            string key = Thread.CurrentThread.ManagedThreadId.ToString();
            PersistentManager manager = null;
            Monitor.Enter(countCollection);
            if (pmCollection.ContainsKey(key))
            {
                manager = pmCollection[key];
                int num = countCollection[key] + 1;
                countCollection[key] = num;
            }
            Monitor.Exit(countCollection);
            return manager;
        }

        internal static void Remove()
        {
            string key = Thread.CurrentThread.ManagedThreadId.ToString();
            Monitor.Enter(countCollection);
            if (countCollection.ContainsKey(key))
            {
                Dictionary<string, int> dictionary;
                string str2;
                (dictionary = countCollection)[str2 = key] = dictionary[str2] - 1;
            }
            Monitor.Exit(countCollection);
        }

        internal static void Remove(int key)
        {
            string str = key.ToString();
            Monitor.Enter(countCollection);
            if (pmCollection.ContainsKey(str) && (countCollection[str] == 0))
            {
                pmCollection.Remove(str);
                countCollection.Remove(str);
            }
            Monitor.Exit(countCollection);
        }
    }
}

