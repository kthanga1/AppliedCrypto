using System;
using System.Numerics;
using System.Security.Cryptography;

class RSA {

    public BigInteger[] performRSA(String[] args){

        BigInteger[] answer = new BigInteger[2];

        int p_exp = int.Parse(args[0]);
        int p_const = int.Parse(args[1]);
        int q_exp = int.Parse(args[2]);
        int q_const = int.Parse(args[3]);
        BigInteger ciphertext = BigInteger.Parse(args[4]);
        BigInteger plaintext = BigInteger.Parse(args[5]);
       
        BigInteger encrypt = new BigInteger(65537);

        BigInteger p = BigInteger.Pow(2, p_exp) - BigInteger.Abs(p_const);
        BigInteger q = BigInteger.Pow(2, q_exp) - BigInteger.Abs(q_const);
        BigInteger calcN = BigInteger.Multiply(p,q);
       
        BigInteger phiN = BigInteger.Multiply(p-1, q-1);
        // Console.WriteLine(phiN);

        BigInteger decrypt = calcDusingExtendedEuclidean(phiN, encrypt);
        // Console.WriteLine(BigInteger.ModPow(BigInteger.Multiply(encrypt,decrypt), 1, phiN));
        // Console.WriteLine(decrypt);
        BigInteger decryptedVal = encryptdecryptUsingRSA(ciphertext, decrypt, calcN);
        BigInteger encryptedVal = encryptdecryptUsingRSA(plaintext, encrypt, calcN);

        // Console.WriteLine(encryptedVal);
        // Console.WriteLine(decryptedVal);

        // BigInteger decrypt = calcDusingExtendedEuclidean(new BigInteger(3120), new BigInteger(17));
        // Console.WriteLine(BigInteger.ModPow(BigInteger.Multiply(17,decrypt), 1, 3120));

        answer[0] = decryptedVal;
        answer[1] = encryptedVal;

        return answer;
    }


    public BigInteger calcDusingExtendedEuclidean(BigInteger phiN, BigInteger enc){
        BigInteger t1 = new BigInteger(0);
        BigInteger t2 = new BigInteger(1);
        // Console.WriteLine("before");
        // Console.WriteLine(phiN);
        // Console.WriteLine(enc);

        BigInteger N = phiN;
         BigInteger t3 = new BigInteger(1);
          BigInteger finalt = new BigInteger(1);
        while (enc > 1){
            BigInteger quotient = BigInteger.Divide(N, enc);
            BigInteger remainder =  BigInteger.ModPow(N, 1, enc);    
            t3 = BigInteger.Subtract(t1, BigInteger.Multiply(quotient, t2));
            t1 = t2;
            t2 = t3;
            N = enc;    
            enc = remainder;
        }
        // Console.WriteLine(t2);
        // Console.WriteLine(finalt);
        // Console.WriteLine(phiN);

        BigInteger calcD = new BigInteger();
        if (t2 < 0){
            calcD = BigInteger.Add(phiN, t2);
        }else{
            calcD = BigInteger.ModPow(t2, 1, phiN);
        }
        Console.WriteLine(calcD);
        return calcD;
    }


   

    public BigInteger encryptdecryptUsingRSA(BigInteger input, BigInteger key, BigInteger calcN){
        BigInteger encryptedVal = BigInteger.ModPow(input, key, calcN);
        return encryptedVal;

    }




}