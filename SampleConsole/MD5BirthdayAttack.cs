using System;
using System.Diagnostics.Tracing;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;

class MD5BirthdayAttack {

    Dictionary<String, String> computedHash = new Dictionary<string, String>();
    public String[] matchedHashes = new String[2]{"None","None"};
    
    HashSet<String> wordset = new HashSet<String>();

    public byte[] hashInputWithSalt(Byte[] input, string salt){
        MD5 md5 = MD5.Create();
        byte[] inputBytes = input;
        byte[] saltBytes = new byte[] {Convert.ToByte(salt, 16)};
        byte[] combinedBytes = new byte[inputBytes.Length+ saltBytes.Length];
        int j=0;
        for(int i = 0; i< combinedBytes.Length; i++){
            if (i < inputBytes.Length){
                combinedBytes[i] = inputBytes[i];
            }else{
                combinedBytes[i] = saltBytes[j];
                j+=1;
            }
        }
        byte[] hashBytes = md5.ComputeHash(combinedBytes);
        return hashBytes;
    }


    // public byte[] hashInputWithSalt(String input, string salt){
    //     MD5 md5 = MD5.Create();
    //     byte[] inputBytes = Encoding.ASCII.GetBytes(input);
    //     byte[] saltBytes = new byte[] {Convert.ToByte(salt, 16)};
    //     byte[] combinedBytes = new byte[inputBytes.Length+ saltBytes.Length];
    //     int j=0;
    //     for(int i = 0; i< combinedBytes.Length; i++){
    //         if (i < inputBytes.Length){
    //             combinedBytes[i] = inputBytes[i];
    //         }else{
    //             combinedBytes[i] = saltBytes[j];
    //             j+=1;
    //         }
    //     }
    //     byte[] hashBytes = md5.ComputeHash(combinedBytes);
    //     return hashBytes;
    // }


    // public String[] performBirthdayAttack(string salt){
    //     Dictionary<String, String> calculatedHashes = new Dictionary<string, string>();
    //     Char[] alphabets = generateAlphabet();
    //     String[] matchedHashes = new String[]{"None", "None"};
    //     Console.WriteLine(new String(alphabets));
    //     int cnt =0;
    //     Stopwatch stopwatch = Stopwatch.StartNew();
    //     while(cnt < Int32.MaxValue){
    //         int size =0;
    //         String nextString ="";
    //         while (size<10){
    //             Random random = new Random();
    //             random.Next(0,61);
    //             nextString+= alphabets[random.Next(0,61)];
    //             size+=1;
    //         }
    //         byte[] hashCalc = hashInputWithSalt(nextString,salt);
    //         var outputHash = BitConverter.ToString(hashCalc).Replace("-","");
    //         string firstfive = BitConverter.ToString(hashCalc[0..6]).Replace("-","");
    //         if (computedHash.ContainsKey(firstfive) && !computedHash[firstfive].Equals(nextString)){
    //             matchedHashes[0] = computedHash[firstfive];
    //             matchedHashes[1] = nextString;
    //             break;
    //         }else{
    //             computedHash[firstfive] = nextString;
    //         }
    //         cnt+=1;
    //     }
    //     stopwatch.Stop();
    //     Console.WriteLine(stopwatch.ElapsedMilliseconds);
    //     return matchedHashes;
    // }

    public Char[] generateAlphabet(char ch){
        
        Char[]  strings = new Char[18];
        int cnt=48;
        int pos=0;
        
        cnt=65;
        while(cnt <= 70){
            strings[pos] = (char) cnt;
            cnt+=1;
            pos+=1;
        }
        cnt=97;
        while(cnt <=102){
            strings[pos] = (char) cnt;
            cnt+=1;
            pos+=1;
        }

        cnt=48;
        while(cnt <= 53){
            strings[pos] = (char) cnt;
            cnt+=1;
            pos+=1;
        }
        

        return strings;
    }



    public Byte[] generateAlphabet(){
        
        Byte[]  strings = new Byte[62];
        int cnt=48;
        int pos=0;
        
        cnt=65;
        while(cnt <= 90){
            strings[pos] = (Byte) cnt;
            cnt+=1;
            pos+=1;
        }
        cnt=97;
        while(cnt <=122){
            strings[pos] = (Byte) cnt;
            cnt+=1;
            pos+=1;
        }

        cnt=48;
        while(cnt <= 57){
            strings[pos] = (Byte) cnt;
            cnt+=1;
            pos+=1;
        }
        return strings;
    }


    public bool performBA(string salt, Byte[] alphabets, int size, Byte[] word, int pos){ 
            if (word[size-1] != 0 ){
                byte[] hashCalc = hashInputWithSalt(word,salt);
                string firstfive = BitConverter.ToString(hashCalc[0..7]).Replace("-","");
                if ( computedHash.ContainsKey(firstfive) 
                    && !computedHash[firstfive].Equals(Encoding.ASCII.GetString(word))){
                    matchedHashes[0] = computedHash[firstfive];
                    matchedHashes[1] = Encoding.ASCII.GetString(word);
                    //Console.WriteLine(matchedHashes[0] + " - "+ matchedHashes[1]);
                    return true;
                }else{
                    computedHash[firstfive] = Encoding.ASCII.GetString(word);
                }
                return false;
            }

            for (int i=0; i < alphabets.Length; i++){
                word[pos] = alphabets[i];
                bool found = performBA(salt, alphabets, size, word, pos+1);
                if (found){
                    return true;
                }
                word[pos] = 0;
            }

            return false;
        }



    
    

}