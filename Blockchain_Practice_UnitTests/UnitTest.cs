public class EncryptionTests
{
    [Fact]
    public void TestSBlockEncodeAndDecode()
    {
        string input = "Hello World!";
        string encoded = Blockchain.SBlockEncode(input);
        string decoded = Blockchain.SBlockDecode(encoded);

        Assert.Equal(input, decoded);
    }

    [Fact]
    public void TestPBlockShuffleAndUnshuffle()
    {
        string input = "Hello World!";
        string shuffled = Blockchain.PBlockShuffle(input);
        string unshuffled = Blockchain.PBlockUnShuffle(shuffled);

        Assert.Equal(input, unshuffled);
    }
}