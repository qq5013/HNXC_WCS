using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;

namespace THOK.CRANE
{
    public static class LogFile
    {
        //private static Dictionary<string, string> attributes = null;
        public static void DeleteFile()
        {
            try
            {
                int monthTime = 2;
                string[] yearList = Directory.GetDirectories("»’÷æ");
                for (int i = 0; i < yearList.Length; i++)
                {
                    string[] monthList = Directory.GetDirectories(yearList[i]);

                    for (int j = 0; j < monthList.Length; j++)
                    {
                        DateTime t = Directory.GetCreationTime(monthList[j]);

                        TimeSpan timeSpan = DateTime.Now - Directory.GetCreationTime(monthList[j]);
                        if (timeSpan.Ticks < 0)
                            break;
                        DateTime dTime = new DateTime(timeSpan.Ticks);
                        int month = (dTime.Month - 1) + (dTime.Year - 1) * 12;
                        if (month > monthTime || month < 0)
                            Directory.Delete(monthList[j], true);
                    }
                    if (Directory.GetDirectories(yearList[i]).Length == 0)
                        Directory.Delete(yearList[i], true);
                }
            }
            catch (Exception e)
            {
                
            }

        }
    }
}
