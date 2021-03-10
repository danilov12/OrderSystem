using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystemTests.TestUtility
{
    public static class TestUtility
    {
        public static void RemoveAllRecords(string filePath)
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                streamWriter.Write("");
            }
        }
    }
}
