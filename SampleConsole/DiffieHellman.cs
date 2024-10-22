


using System.Numerics;
using System.Security.Cryptography;

class DiffieHellman{


    public String[] performDifieHellman(String[] args){

        String[] answer = new string[2];

        String initVectorStr = args[0];
        Console.WriteLine(initVectorStr);
        String[] initVecsarr = initVectorStr.Split(" ");
        byte[] initvecbarr = new byte[initVecsarr.Length];
        for (int i=0; i < initVecsarr.Length; i++){
            initvecbarr[i] = Convert.ToByte(initVecsarr[i], 16);
        }
        Console.WriteLine(BitConverter.ToString(initvecbarr).Replace("-"," "));

        int g_exp = int.Parse(args[1]);
        int g_const = int.Parse(args[2]);
        int n_exp = int.Parse(args[3]);
        int n_const = int.Parse(args[4]);
        int xval = int.Parse(args[5]);
        BigInteger gymodN = BigInteger.Parse(args[6]);
        String encryptedMsg =  args[7];

        Console.WriteLine(encryptedMsg);
        String[] encryptedSArr = encryptedMsg.Split(" ");
        byte[] encryptedBArr = new byte[encryptedSArr.Length];
        for (int i=0; i < encryptedSArr.Length; i++){
            encryptedBArr[i] = Convert.ToByte(encryptedSArr[i], 16);
        }
        Console.WriteLine(BitConverter.ToString(encryptedBArr).Replace("-"," "));

        String plaintext = args[8];

        BigInteger Gcalc = BigInteger.Subtract(BigInteger.Pow(2, g_exp), new BigInteger(g_const));
        BigInteger Ncalc = BigInteger.Subtract(BigInteger.Pow(2, n_exp), new BigInteger(n_const));
        
        BigInteger calcKey = BigInteger.ModPow(gymodN, xval, Ncalc);
        Byte[] encryptedString = encryptUsingAES(plaintext, calcKey, initvecbarr);
        answer[1] = BitConverter.ToString(encryptedString).Replace("-"," ");
        answer[0] =  decryptUsingAes(encryptedBArr, calcKey, initvecbarr);

        return answer;
    }



    public Byte[] encryptUsingAES(String input, BigInteger key, Byte[] initVector){

        Aes aes = Aes.Create();
        aes.IV = initVector;
        aes.Key = key.ToByteArray();
        Byte[] encryptedString;
        ICryptoTransform cryptoTransform = aes.CreateEncryptor(aes.Key, aes.IV);
        using(MemoryStream memoryStream = new MemoryStream()){
            using(CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write)){
                using(StreamWriter swrite = new StreamWriter(cryptoStream)){
                    swrite.Write(input);
                }
                encryptedString = memoryStream.ToArray();
            }
        }
        return encryptedString;

    }

    public String decryptUsingAes(Byte[] input, BigInteger key, Byte[] initVector){

        Aes aes = Aes.Create();
        aes.IV = initVector;
        aes.Key = key.ToByteArray();
        String plaintext ="";
        ICryptoTransform cryptoTransform = aes.CreateDecryptor(aes.Key, aes.IV);
        using(MemoryStream memoryStream = new MemoryStream(input)){
            using(CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Read)){
                using(StreamReader sread = new StreamReader(cryptoStream)){
                     plaintext = sread.ReadToEnd();
                }
               
            }
        }
        return plaintext;

    }
    



}