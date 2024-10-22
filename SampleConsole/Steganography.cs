using System;
using System.Text;



class Steganography{

    public String performSteganoGraphy(String replaceStr){
        byte[] bmpBytes = new byte[] {
0x42,0x4D,0x4C,0x00,0x00,0x00,0x00,0x00,
0x00,0x00,0x1A,0x00,0x00,0x00,0x0C,0x00,
0x00,0x00,0x04,0x00,0x04,0x00,0x01,0x00,
0x18,0x00,0x00,0x00,0xFF,0xFF,0xFF,0xFF,
0x00,0x00,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,
0xFF,0x00,0x00,0x00,0xFF,0xFF,0xFF,0x00,
0x00,0x00,0xFF,0x00,0x00,0xFF,0xFF,0xFF,
0xFF,0x00,0x00,0xFF,0xFF,0xFF,0xFF,0xFF,
0xFF,0x00,0x00,0x00,0xFF,0xFF,0xFF,0x00,
0x00,0x00
};


String[] inputStrArr = replaceStr.Split(" ");
byte[] replaceBytes = new byte[inputStrArr.Length];
Console.WriteLine(Convert.ToByte("B1", 16));
for( int i=0; i < inputStrArr.Length; i++){
    replaceBytes[i] = Convert.ToByte(inputStrArr[i],16);   
}

Console.WriteLine(BitConverter.ToString(replaceBytes).Replace("-"," "));
int pos = 0;
int insertPos = 26;

while (pos < replaceBytes.Length){
    int extractpos = 7;
    while(extractpos >= 0 && insertPos < bmpBytes.Length){
        int second = replaceBytes[pos] >> extractpos & 1;
        int first = replaceBytes[pos] >> (extractpos-1) & 1;
        int toadd = 0;
        toadd = toadd | second;
        toadd = toadd << 1;
        toadd = toadd | first;
        byte replacebyte = (byte) Convert.ToByte(toadd);
        Console.WriteLine(replacebyte);
        bmpBytes[insertPos] = (byte) ( bmpBytes[insertPos] ^ replacebyte);
        insertPos++;
        extractpos-=2;   
    }
    pos++;
}

Console.WriteLine(BitConverter.ToString(bmpBytes).Replace("-"," "));

String expected = @"42 4D 4C 00 00 00 00 00 00 00 1A 00 00 00 0C 00 00 00 04 00 04 00 01 00 18 00 02 03 FF FE FC FC 03 03 FC FC FC FC FC FF FC 00 02 01 FD FF FD 00 00 00 FF 00 02 FE FC FD FD 02 00 FF FE FF FE FF FD 00 01 03 FC FD FC 00 02 01";
    

String converted = BitConverter.ToString(bmpBytes).Replace("-"," ");

Console.WriteLine(expected.Equals(converted));

    return "Done";

    }
}