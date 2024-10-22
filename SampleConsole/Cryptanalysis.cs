using System;
using System.Security.Cryptography;


class Cryptanalysis{

    
     public static int findKey(String plaintext, String cipher){
    
        DateTime start = new DateTime(2020, 7, 3, 11, 0,0);
        DateTime end= new DateTime(2020, 7, 4, 11, 0,0);
        int seed = 0;
        int counter =0;
        while(start <= end){
            start = start.AddSeconds(1);
            TimeSpan ts = start.Subtract(new DateTime(1970, 1, 1));
            seed = (int)ts.TotalMinutes;
            Random rng = new Random(seed);
            byte[] key = BitConverter.GetBytes(rng.NextDouble());
            String calccipher = performEncrypt(key, plaintext);
            if (calccipher.Equals(cipher)){
                break;
            }
            counter++;
        }
        Console.WriteLine(counter);
        return seed;
     }

    public static String performEncrypt(byte[] key, String plaintext){
        //string secretString="";
        DES des  = new DESCryptoServiceProvider();
        MemoryStream memoryStream = new MemoryStream();
        CryptoStream cs = new CryptoStream(memoryStream,
        des.CreateEncryptor(key, key), CryptoStreamMode.Write);
        StreamWriter sw = new StreamWriter(cs);
        sw.Write(plaintext);
        sw.Flush();
        cs.FlushFinalBlock();
        sw.Flush();
        return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
    }
}

