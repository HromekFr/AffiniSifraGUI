using System;
using System.Linq;

namespace AffiniSifraGUI
{
    public class Functions
    {

        public static bool Euklid(double keyA, double keyB)
        {
            keyA = Math.Abs(keyA);
            keyB = Math.Abs(keyB);

            if (keyA > keyB)
            {
                var temp = keyA;
                keyA = keyB;
                keyB = temp;
            }

            double remainder;

            while (keyB != 0)
            {
                remainder = keyA % keyB;
                keyA = keyB;
                keyB = remainder;
            }

            if (keyA == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public static string Encrypt(string message, double keyA, double keyB)
        {
            char[] messageToUpper = message.ToUpper().ToCharArray();
            int lengthMessage = messageToUpper.Length;
            string encryptedMessage = "";
            bool isEnd = false;
            double indexMessage;

            while (!isEnd)
            {
                for (int i = 0; i < message.Length; i++)
                {
                    bool isWhitespace = Char.IsWhiteSpace(messageToUpper[i]);

                    if (isWhitespace)
                    {
                        encryptedMessage += "GHF";
                        lengthMessage--;
                    }

                    if (!isWhitespace)
                    {
                        for (int j = 0; j < Constants.alphabet.Length; j++)
                        {
                            if (messageToUpper[i] == Constants.alphabet[j])
                            {
                                indexMessage = (keyA * j + keyB) % Constants.alphabet.Length;
                                encryptedMessage += Constants.alphabet[Convert.ToInt32(indexMessage)];
                                lengthMessage--;
                                break;
                            }
                        }
                    }

                    if (lengthMessage == 0)
                    {
                        isEnd = true;
                        break;
                    }
                }
            }

            return encryptedMessage;
        }

        private static double MMI(double keyA, int alphabetLength)
        {
            for (int i = 1; i < alphabetLength; i++)
            {
                double calc = (keyA * i) % alphabetLength;
                if (calc == 1)
                {
                    return i;
                }
            }

            return 0;
        }

        private static double correctMod(double subNumber, int alphabetLength)
        {
            double remainder = subNumber / alphabetLength;
            double correctIndex = subNumber - alphabetLength * Math.Floor(remainder);
            return correctIndex;
        }

        public static string Decrypt(string encryptedMessage, double keyA, double keyB)
        {
            char[] messageToUpper = encryptedMessage.ToUpper().ToCharArray();
            int lengthMessage = messageToUpper.Length;
            string decryptedMessage = "";
            bool isEnd = false;
            double mmiA = MMI(keyA, Constants.alphabet.Length);
            double indexMessage;

            while (!isEnd)
            {
                for (int i = 0; i < messageToUpper.Length; i++)
                {
                    if (i - messageToUpper.Length < -1)
                    {
                        bool isGHF = messageToUpper[i] == 'G' && messageToUpper[i + 1] == 'H' && messageToUpper[i + 2] == 'F';
                        if (isGHF)
                        {
                            decryptedMessage += ' ';
                            lengthMessage -= 3;
                            i += 2;

                            continue;
                        }
                    }
                    
                    for (int j = 0; j < Constants.alphabet.Length; j++)
                    {
                        if (messageToUpper[i] == Constants.alphabet[j])
                        {
                            indexMessage = ((j - keyB) * mmiA);
                            int correctModIndex = Convert.ToInt32(correctMod(indexMessage, Constants.alphabet.Length));
                            
                            decryptedMessage += Constants.alphabet[correctModIndex];
                            lengthMessage--;
                            break;
                        }
                    }

                    if (lengthMessage == 0)
                    {
                        isEnd = true;
                        break;
                    }
                }
            }

            return decryptedMessage;
        }

        private static string RemoveDiacritism(string Text)
        {
            string stringFormD = Text.Normalize(System.Text.NormalizationForm.FormD);
            System.Text.StringBuilder retVal = new System.Text.StringBuilder();
            for (int index = 0; index < stringFormD.Length; index++)
            {
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stringFormD[index]) != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    retVal.Append(stringFormD[index]);
                }
                    
            }
            return retVal.ToString().Normalize(System.Text.NormalizationForm.FormC);
        }

        private static string RemoveSpecialChars(string str)
        {
            string[] chars = new string[] { ",", ".", "/", "!", "@", "#", "$", "%", "^", "&", "*", "'", ";", "_", "(", ")", ":", "|", "[", "]", "?" };
            for (int i = 0; i < chars.Length; i++)
            {
                if (str.Contains(chars[i]))
                {
                    str = str.Replace(chars[i], "");
                }
            }
            return str;
        }

        public static string ChangeTextToCorrectFormat(string str)
        {
            str = RemoveDiacritism(RemoveSpecialChars(str));
            str = str.TrimEnd();
            return str;
        }

        public static string RemoveWhiteSpace(string str)
        {
            str = String.Concat(str.Where(c => !Char.IsWhiteSpace(c)));
            return str;
        }

        public static string Fifths(string str)
        {
            for (int i = 5; i <= str.Length; i += 5)
            {
                str = str.Insert(i, " ");
                i++;
            }
            return str;
        }
    }
}

