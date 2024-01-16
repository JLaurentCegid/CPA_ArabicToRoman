using ArabicToRomanLibrary;

Converter converter = new ();
for (int arabic = 0; arabic < 4000; arabic++)
{
   Console.WriteLine($"{arabic} = {converter.ToRoman(arabic)}");
}