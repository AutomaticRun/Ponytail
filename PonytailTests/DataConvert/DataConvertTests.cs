using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ponytail.DataConvert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponytail.DataConvert.Tests
{
    [TestClass()]
    public class DataConvertTests
    {
        [TestMethod()]
        public void GetBitFromByteTest()
        {
            byte b = 0x11;
            var result1 = DataConvert.GetBitFromByte(b, 0);
            var result2 = DataConvert.GetBitFromByte(b, 1);
            var result3 = DataConvert.GetBitFromByte(b, 4);
            Assert.IsTrue(result1);
            Assert.IsTrue(!result2);
            Assert.IsTrue(result3);
        }
    }
}