// See https://aka.ms/new-console-template for more information
using System;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;

class Hello {

    static void Main(string[] args){

        // RSA rSA = new RSA();

        // BigInteger[] answer = rSA.performRSA(args);
        // Console.WriteLine(answer[0] + ","+ answer[1]);



        // DiffieHellman diffieHellman = new DiffieHellman();
        // Console.WriteLine(args.Length);
        
        
        // DiffieHellman diffieHellman1 = new DiffieHellman();
        // String[] output = diffieHellman.performDifieHellman(args);
        // Console.WriteLine(output[0]);
        // Console.WriteLine(output[1]);
        
        // Steganography steganography = new Steganography();

        // steganography.performSteganoGraphy(args[0]);
        // “Hello World” “RgdIKNgHn2Wg7jXwAykTlA==”
        string plaintext = args[0];
        string cipher = args[1];
        int usedseed = Cryptanalysis.findKey(plaintext, cipher);
        Console.WriteLine(usedseed);

        // Hash mD5BirthdayAttack = new Hash();

        
        // Byte[] alphabets = mD5BirthdayAttack.generateAlphabet();        
        // Console.WriteLine(Encoding.ASCII.GetString(alphabets));

        
        // Stopwatch stopwatch = Stopwatch.StartNew();
        // bool found = mD5BirthdayAttack.performBA("C5", alphabets,10, new byte[10], 0);
        // if (found){
        //     String[] matchedWords = mD5BirthdayAttack.matchedHashes;
        //     Console.WriteLine(matchedWords[0] + ","+ matchedWords[1]);
        //     String one = BitConverter.ToString(mD5BirthdayAttack.hashInputWithSalt(Encoding.ASCII.GetBytes(matchedWords[0]), "C5")).Replace("-","");
        //     String two = BitConverter.ToString(mD5BirthdayAttack.hashInputWithSalt(Encoding.ASCII.GetBytes(matchedWords[1]), "C5")).Replace("-","");
        
        //     Console.WriteLine("one " +one);
        //     Console.WriteLine("two" +two);

        // }
        // stopwatch.Stop();
        //  Console.WriteLine(stopwatch.ElapsedMilliseconds);
        

        // mD5BirthdayAttack.performIterAttack("C5");
        
        // String[] matchedWords = mD5BirthdayAttack.performIterAttack("C5");
        // Console.WriteLine(matchedWords[0] + ","+ matchedWords[1]);

        // String one = BitConverter.ToString(mD5BirthdayAttack.hashInputWithSalt(matchedWords[0], "C5")).Replace("-","");
        // String two = BitConverter.ToString(mD5BirthdayAttack.hashInputWithSalt(matchedWords[1], "C5")).Replace("-","");
        

        
        /*Console.WriteLine("Test new way");
        Console.WriteLine(new Hello().VigniereCiphere("maroonandgold","sparky", true));
        Console.WriteLine(new Hello().VigniereCiphere("eprfylscdxyjv","sparky", false));
        Console.WriteLine("Affine Cipher ");

        //Console.WriteLine(new Hello().AffineCipher("maroonandgold",9,5, true));
        Console.WriteLine(new Hello().AffineCipher("Uqp Lds Gpmza xbabcl By jfcbbs fsg hbag abbr hcpfu",9,5,29, false));
        Console.WriteLine(new Hello().AffineCipher("affinecipher",5,8, 21, true));
        Console.WriteLine(new Hello().AffineCipher("ihhwvcswfrcp",5,8, 21,false));
     
        Dictionary<char, char> keymap =   new Hello().buildkey();
        String plaintext = new Hello().subsitutioncipher("ZITYOKLZEGDHXZTKZTKDOFQSLLXEIQLZITZTSTZNHTVTKTZNHTVKOZTKLZIQZEGXSRHKGRXETQFRWTEGFZKGSSTRWNCQKOGXLEGDHXZTKEGRTLZITLTXLTRZITJVTKZNSQNGXZLQFRQRRTRATNLLXEIQLTLEQHTTLEVIOEIIQRLHTEOQSDTQFOFULZGEGDHXZTKLSQZTKATNWGQKRLQRRTRYXFEZOGFATNLQFRQKKGVATNLLOFETZITLZQFRQKROMQZOGFGYHEEGDHQZOWSTEGDHXZTKLQFRVOFRGVLDGLZYXSSLOMTREGDHXZTKATNWGQKRLIQCTYGSSGVTRZIOLLZQFRQKRZIOLSQNGXZIQLQLTHQKQZTFXDTKOEATNHQRYGKRQZQTFZKNQZZITKOUIZYXFEZOGFATNLEKGLLZITZGHQFRQEXKLGKLTEZOGFZGZITKOUIZQFRETFZTKVOZIATNLYGKOFLTKZRTSTZTIGDTTFRHQUTXHQFRHQUTRGVFVOZIEXKLGKQKKGVLOFQFOFCTKZTRZLIQHTTCTFZITATNWGQKRNGXQKTXLOFUZGRTEKNHZZIOLDTLLQUTLIGXSRWTQCQKOQFZGYJVTKZNXFSTLLNGXIQCTWXOSZNGXKGVFJVTKZNOLZITFTVQWER", keymap);
                
        Console.WriteLine(plaintext);

        Console.WriteLine(new Hello().caesarcipher("Jxu auo veh rhuqaydw oekh dunj syfxuh yi hywxj kdtuh oekh deiu", 16));
        */


    }


    public String VigniereCiphere(String cipher, String key, bool encrypt){
        
        String output = "";
        int l = key.Length;
        if (encrypt){
            for (int i =0; i < cipher.Length ; i++){
                int pos = (int) key[i%l] - 97;
                int iCode = (int) cipher[i]-97;
                int ccode = (iCode+pos)%26;
                output += (char) (97+ccode);
            }
        }else{
            for (int i=0; i < cipher.Length; i++){
                int pos = (int) key[i%l] - 97;
                int rCode = (int) cipher[i]-97;
                if (rCode-pos < 0){
                    output+= (char) (122-(Math.Abs(rCode-pos))+1);
                }else{
                    output+= (char) (97+(rCode-pos));
                }
                
            }
        }

        return output;

    }

    public String AffineCipher(String cipher, int a, int b, int modinv,  bool encrypt){
        
        String output = "";
   

        if (encrypt){
            for (int i =0; i < cipher.Length ; i++){
                int xcode = (int) cipher[i]-97;
                int ccode = (a*xcode+b)%26;
                output += (char) (97+ccode);
            }
        }else{

            for (int i=0; i < cipher.Length; i++){
                int cval = Char.ToLower(cipher[i]);
                if ( cval == 32 ){
                    output+= ' ';
                    continue;
                }
                int xcode = cval-97;
                int ccode = modinv*(xcode-b)%26;
                ccode = ccode < 0? ccode+26:ccode; 
                char ans = (char) (97+ccode); 
                if (Char.IsUpper(cipher[i])){
                    output += Char.ToUpper(ans);
                }else{
                     output += ans;
                }
                                
            }

        }

        return output;

    }


    public Dictionary<char,char> buildkey(){
         Dictionary<char, char> frequency = new Dictionary<char, char>();
         frequency['A'] = 'Q';
         frequency['B'] = 'W';
         frequency['C'] = 'E';
         frequency['D'] = 'R';
         frequency['E'] = 'T';
         frequency['F'] = 'Y';
         frequency['G'] = 'U';
         frequency['H'] = 'I';
         frequency['I'] = 'O';
         frequency['J'] = 'P';
         frequency['K'] = 'A';
         frequency['L'] = 'S';
         frequency['M'] = 'D';
         frequency['N'] = 'F';
         frequency['O'] = 'G';
         frequency['P'] = 'H';
         frequency['Q'] = 'J';
         frequency['R'] = 'K';
         frequency['S'] = 'L';
         frequency['T'] = 'Z';
         frequency['U'] = 'X';
         frequency['V'] = 'C';
         frequency['W'] = 'V';
         frequency['X'] = 'B';
         frequency['Y'] = 'N';
         frequency['Z'] = 'M';
         
        return frequency;

    }


    public String subsitutioncipher(String cipher, Dictionary<char,char> keymap){

        Dictionary<char, int> frequency = new Dictionary<char, int>();
        String  output = "";

        foreach(char ch in cipher){
            Char ans = keymap.FirstOrDefault(x => x.Value == ch).Key;
            output += ans;
            Console.WriteLine(ch+ " mapped to " + ans);
        }
                
        return output;

    }



    public Dictionary<char, int> findfrequencies(String cipher){

        Dictionary<char, int> frequency = new Dictionary<char, int>();

        foreach(char ch in cipher){
            if (frequency.ContainsKey(ch)){
                frequency[ch] = frequency[ch]+1;
            }else{
                frequency[ch] = 1;
            }
        }
        var ordered = frequency.OrderBy(x => x.Value);
        foreach(var entry in ordered){
            Console.WriteLine(entry.Key+ " "+ entry.Value);
        }
        
        return frequency;

    }



    public String caesarcipher(String cipher, int key){
        String output = "";

       
            for (int i =0; i < cipher.Length ; i++){
                if (Char.IsWhiteSpace(cipher[i])){
                    continue;
                }
                int xcode = (int) Char.ToLower(cipher[i])-97;
                
                int ccode = xcode-key ;
                Console.WriteLine(ccode);
                if (ccode< 0){
                    output += (char) (122-Math.Abs(ccode)+1);
                }else{
                    output += (char) (97+ccode);
                }
                
            }
        
        return output;

    } 


}