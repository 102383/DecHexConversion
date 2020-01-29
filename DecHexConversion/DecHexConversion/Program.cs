using System;

namespace DecHexConversion
{
    /// <summary>
    /// Author: Nathaniel Bosserd
    /// Phone:  269.359.1167
    /// Email:  ncbosserdfamily@gmail.com
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            const char YES = 'Y';
            const char NO = 'N';

            Console.WriteLine("***** Conversion Program *****");

            bool proceed = true;
            do
            {
                int choice = 0;
                do
                {
                    Console.WriteLine();
                    Console.WriteLine("Enter a encoding operation ");
                    Console.WriteLine("(1) Encode");
                    Console.WriteLine("(2) Decode");

                    int.TryParse(Console.ReadLine(), out choice);
                } while (choice != 1 && choice != 2);

                switch (choice) {
                    case 1:
                        performEncode();
                        break;
                    case 2:
                        performDecode();
                        break;
                }
                
                char proceedChoice = 'X';
                do
                {
                    Console.WriteLine();
                    Console.WriteLine("Continue with another operation (" + YES + "/" + NO + ")?");

                    if (char.TryParse(Console.ReadLine(), out proceedChoice))
                    {
                        proceedChoice = char.ToUpper(proceedChoice);

                        if (proceedChoice != YES && proceedChoice != NO)
                        {
                            Console.WriteLine();
                            Console.WriteLine(proceedChoice + " entered is not (" + YES + " or " + NO + ").");
                        }
                        else if (proceedChoice == NO)
                        {
                            Console.WriteLine("Exiting Program");
                            proceed = false;
                        }
                    }
                } while (proceedChoice != YES && proceedChoice != NO);
            } while (proceed == true);
        }

        private static void performDecode()
        {

            string hiByte = GetByte("high");
            string lowByte = GetByte("low");

            Console.WriteLine("High byte " + hiByte + " and low byte " + lowByte + " decodes to: " + decode(hiByte, lowByte));

        }

        private static string GetByte(string position)
        {
            string myByte;
            bool invalid = true;
            do
            {
                int? num = null;

                Console.WriteLine();
                Console.WriteLine("Enter " + position +" byte to encode between 0x00 and 0x7F ");
                myByte = Console.ReadLine();

                try
                {
                    num = int.Parse(myByte, System.Globalization.NumberStyles.HexNumber);
                }
                catch (Exception e) { 
                }

                if (num != null)
                {
                    if (num < 0x00 || 0x7F < num)
                    {
                        Console.WriteLine();
                        Console.WriteLine("The " + position + " byte '" + myByte + "' is out of range 0x00 to 0x7F");
                        Console.WriteLine();
                    }
                    else
                    {
                        invalid = false;
                    }
                }
                else

                {
                    Console.WriteLine();
                    Console.WriteLine("The " + position + " byte '" + myByte + "' is invalid.");
                    Console.WriteLine();
                }
            } while (invalid);

            return myByte;
        }

        private static void performEncode()
        {
            bool inValid = true;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Enter integer to encode between -8192 and 8192 ");

                int number;

                if (int.TryParse(Console.ReadLine(), out number))
                {
                    if (number < -8192 || 8192 < number)
                    {
                        Console.WriteLine();
                        Console.WriteLine("' " + number + "' is out of range -8192 to 8192.");
                        Console.WriteLine();
                    }
                    else
                    {
                        inValid = false;
                        Console.WriteLine("' " + number + "' encodes to: " + encode(number));
                    }
                }
            } while (inValid);
            
        }

        protected static string encode(int num)
        {
            num += 8192;

            var low = num & 0x007F; // 0000 0000 0111 1111
            var hi = num & 0x3F80; // 0011 1111 1000 0000

            return ((hi << 1) + low).ToString("X").PadLeft(4, '0');
        }

        protected static short decode(string hiByte, string lowByte)
        {
            byte hi = Convert.ToByte(hiByte, 16);
            byte low = Convert.ToByte(lowByte, 16);

            return (short)((short)((hi << 7) + low) - 8192);
        }
    }
}