using System;

namespace Ex03.ConsoleUI
{
    class IOUtils
    {
        internal static int getOptionInRange(int i_Min, int i_Max)
        {
            int option;
            string optionStr = readAndClear();

            if (!int.TryParse(optionStr, out option) || option < i_Min || option > i_Max)
            {
                throw new FormatException(String.Format("You should choice an option between {0} and {1}", i_Min, i_Max));
            }

            return option;
        }

        internal static string getLicenseNumber()
        {
            Console.WriteLine("Please enter your vehicle license number:");

            return readAndClear();
        }

        internal static int getEnumChoice(string i_Msg, string[] i_Choices)
        {
            bool validInput = false;
            int choiceNumber = 0;

            Console.WriteLine(i_Msg);
            while (!validInput)
            {
                try
                {
                    int index = 1;
                    foreach (string str in i_Choices)
                    {
                        Console.WriteLine("{0}: {1}", index++, str);
                    }
                    choiceNumber = getOptionInRange(1, index - 1);
                    validInput = true;
                }

                catch(FormatException fe)
                {
                    Console.WriteLine(fe.Message);
                }
            }

            return choiceNumber;
        }

        internal static float getPositiveFloat()
        {
            float posFloat;
            string posFloatStr = readAndClear();

            if (!float.TryParse(posFloatStr, out posFloat) || posFloat < 0)
            {
                throw new FormatException(String.Format("You should enter a positive float number, please try again"));
            }

            return posFloat;
        }

        internal static int getPositiveInt()
        {
            int posInt;
            string posIntStr = readAndClear();

            if (!int.TryParse(posIntStr, out posInt) || posInt < 0)
            {
                throw new FormatException(String.Format("You should enter a positive int number, please try again"));
            }

            return posInt;
        }

        internal static string getValidAlphaString()
        {
            string alphaString = readAndClear();

            foreach (char letter in alphaString)
            {
                if (!(char.IsLetter(letter)))
                {
                    throw new FormatException("The string is not an alpha based string");
                }
            }

            return alphaString;
        }

        internal static string getValidNumericString()
        {
            string NumericString = readAndClear();

            foreach (char digit in NumericString)
            {
                if (!(char.IsDigit(digit)))
                {
                    throw new FormatException("The string is not a numeric based string");
                }
            }

            return NumericString;
        }

        internal static string readAndClear()
        {
            string input = Console.ReadLine();
            Console.Clear();

            return input;
        }
    }
}