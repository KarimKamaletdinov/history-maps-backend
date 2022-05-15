namespace HistoryMaps;

public class StlSerializer
{
    //Скопипасчено (и слегка изменено) из библиотеки IxMilia.Stl (добавлен цвет)
    public static byte[] StlToBytes(StlDocument stl)
    {
        var header = new byte[80]; // can be a garbage value
        var stream = new MemoryStream();
        var writer = new BinaryWriter(stream);
        writer.Write(header);

        // write vertex count
        writer.Write((uint)stl.Triangles.Count);

        // write triangles
        foreach (var triangle in stl.Triangles)
        {
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);

            writer.Write(triangle.A.X);
            writer.Write(triangle.A.Y);
            writer.Write(triangle.A.Z);

            writer.Write(triangle.B.X);
            writer.Write(triangle.B.Y);
            writer.Write(triangle.B.Z);
                
            writer.Write(triangle.C.X);
            writer.Write(triangle.C.Y);
            writer.Write(triangle.C.Z);
            
            var r = ConvertColorByte(triangle.Color.R);
            var g = ConvertColorByte(triangle.Color.G);
            var b = ConvertColorByte(triangle.Color.B);

            var byte1 = new bool[]
            {
                b[0],
                b[1],
                b[2],
                b[3],
                b[4],

                g[0],
                g[1],
                g[2]
            };

            var byte2 = new bool[]
            {
                g[3],
                g[4],

                r[0],
                r[1],
                r[2],
                r[3],
                r[4],

                true
            };
            
            writer.Write(ConvertBoolArrayToByte(byte1));
            writer.Write(ConvertBoolArrayToByte(byte2));
        }
        writer.Flush();
        return stream.ToArray();
    }

    //todo
    //public StlDocument BytesToStl(byte[] bytes)
    //{

    //}

    private static bool[] ConvertColorByte(byte b)
    {
        var val = (byte) ((b + 1) / 8 - 1);
        var arr = ConvertByteToBoolArray(val);
        return new []
        {
            arr[3],
            arr[4],
            arr[5],
            arr[6],
            arr[7]
        };
    }

    //Скопипасчено со stack overflow
    private static byte ConvertBoolArrayToByte(bool[] source)
    {
        byte result = 0;
        // This assumes the array never contains more than 8 elements!
        int index = 8 - source.Length;

        // Loop through the array
        foreach (bool b in source)
        {
            // if the element is 'true' set the bit at that position
            if (b)
                result |= (byte)(1 << (7 - index));

            index++;
        }

        return result;
    }

    //Скопипасчено со stack overflow
    private static bool[] ConvertByteToBoolArray(byte b)
    {
        // prepare the return result
        bool[] result = new bool[8];

        // check each bit in the byte. if 1 set to true, if 0 set to false
        for (int i = 0; i < 8; i++)
            result[i] = (b & (1 << i)) != 0;

        // reverse the array
        Array.Reverse(result);

        return result;
    }
}