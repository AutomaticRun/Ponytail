# 工控软件WPF辅助类

### Get the bit in the byte:

```C#
            byte b = 0x11;
            var result1 = DataConvert.GetBitFromByte(b, 0);
            var result2 = DataConvert.GetBitFromByte(b, 1);
            var result3 = DataConvert.GetBitFromByte(b, 4);
            Assert.IsTrue(result1);
            Assert.IsTrue(!result2);
            Assert.IsTrue(result3);
```
