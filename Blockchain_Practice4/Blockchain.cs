using System.Text;

public class Blockchain
{
    // Визначаємо S-блок і P-блок, це постійні значення, а також обернені, 
    // які будуть розраховані пізніше 
    static byte[] sBox = { 5, 8, 1, 13, 6, 4, 11, 3, 7, 2, 12, 15, 14, 9, 10, 0 };
    static byte[] inverseSBox = new byte[16];

    static byte[] pBox = { 7, 6, 5, 4, 3, 2, 1, 0 };
    static byte[] inversePBox = new byte[8];

    static void Main()
    {
        // Запускаємо дослідження алгоритмів S-блоку та P-блоку
        Console.WriteLine("S-block: ");
        string input1 = Console.ReadLine();
        string encoded = SBlockEncode(input1);
        Console.WriteLine($"Encoded: {encoded}");

        string decoded = SBlockDecode(encoded);
        Console.WriteLine($"Decoded: {decoded}");

        Console.WriteLine("P-block: ");
        string input2 = Console.ReadLine();
        string shuffled = PBlockShuffle(input2);
        Console.WriteLine($"Shuffled: {shuffled}");

        string unshuffled = PBlockUnShuffle(shuffled);
        Console.WriteLine($"Unshuffled: {unshuffled}");
    }

    // Шифрує вхідні дані за допомогою S-блоку
    public static string SBlockEncode(string input)
    {
        byte[] bytes = System.Text.Encoding.ASCII.GetBytes(input);
        for (int i = 0; i < bytes.Length; i++)
        {
            byte upperFour = (byte)((bytes[i] >> 4) & 0xF);
            byte lowerFour = (byte)(bytes[i] & 0xF);
            byte newByte = (byte)((sBox[upperFour] << 4) | sBox[lowerFour]);
            bytes[i] = newByte;
        }
        return Convert.ToBase64String(bytes);
    }

    // Дешифрує вхідні дані за допомогою S-блоку
    public static string SBlockDecode(string input)
    {
        byte[] bytes = Convert.FromBase64String(input);
        InverseSBlock();
        for (int i = 0; i < bytes.Length; i++)
        {
            byte upperFour = (byte)((bytes[i] >> 4) & 0xF);
            byte lowerFour = (byte)(bytes[i] & 0xF);
            byte newByte = (byte)((inverseSBox[upperFour] << 4) | inverseSBox[lowerFour]);
            bytes[i] = newByte;
        }
        return Encoding.ASCII.GetString(bytes);
    }

    // Обчислює зворотній S-блок
    private static void InverseSBlock()
    {
        for (int i = 0; i < sBox.Length; i++)
            inverseSBox[sBox[i]] = (byte)i;
    }

    // Перемішує вхідні дані за допомогою P-блоку
    public static string PBlockShuffle(string input)
    {
        byte[] bytes = Encoding.ASCII.GetBytes(input);
        for (int i = 0; i < bytes.Length; i++)
        {
            byte newByte = 0;
            for (int j = 0; j < 8; j++)
            {
                byte bit = (byte)((bytes[i] >> j) & 0x1);
                newByte |= (byte)(bit << pBox[j]);
            }
            bytes[i] = newByte;
        }
        return Convert.ToBase64String(bytes);
    }

    // Розшифровує вхідні дані, що були перемішані за допомогою P-блоку
    public static string PBlockUnShuffle(string input)
    {
        byte[] bytes = Convert.FromBase64String(input);
        InversePBlock();
        for (int i = 0; i < bytes.Length; i++)
        {
            byte newByte = 0;
            for (int j = 0; j < 8; j++)
            {
                byte bit = (byte)((bytes[i] >> j) & 0x1);
                newByte |= (byte)(bit << inversePBox[j]);
            }
            bytes[i] = newByte;
        }
        return Encoding.ASCII.GetString(bytes);
    }

    // Обчислює зворотній P-блок
    private static void InversePBlock()
    {
        for (int i = 0; i < pBox.Length; i++)
            inversePBox[pBox[i]] = (byte)i;
    }
}