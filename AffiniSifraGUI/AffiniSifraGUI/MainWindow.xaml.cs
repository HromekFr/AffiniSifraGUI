using System;
using System.Windows;

namespace AffiniSifraGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int keyA_Counter = 0;
        private int keyB_Counter = 0;
        public MainWindow()
        {
            InitializeComponent();
        }
        private bool CheckKeys()
        {
            int keyAToInt;
            int keyBToInt;
            bool isIntA = int.TryParse(keyA_NumberView.Text, out keyAToInt);
            bool isIntB = int.TryParse(keyB_NumberView.Text, out keyBToInt);

            if (keyA_NumberView.Text == "" || keyB_NumberView.Text == "")
            {
                MessageBox.Show("Zadejte prosím klíče");
                return false;
            }
            else if (!isIntA || !isIntB)
            {
                MessageBox.Show("Zadejte prosím klíče ve správném formátu");
                return false;
            }
            else
            {
                if (Functions.Euklid(keyAToInt, Constants.alphabet.Length))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Hodnoty klíčů jsou soudělné, použijte nesoudělná čísla");
                    return false;
                }
            }
        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {

            if (CheckKeys())
            {
                if (textInputEncrypt.Text == "")
                {
                    MessageBox.Show("Vstup pro zašifrování je prázdný");
                }
                else
                {
                    int keyAToInt = Convert.ToInt32(keyA_NumberView.Text);
                    int keyBToInt = Convert.ToInt32(keyB_NumberView.Text);
                    var encryptedText = Functions.Encrypt(Functions.ChangeTextToCorrectFormat(textInputEncrypt.Text), keyAToInt, keyBToInt);
                    textOutput.Text = Functions.Fifths(encryptedText);
                }
                
            }
        }

        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {

            if (CheckKeys())
            {
                if (textInputDecrypt.Text == "")
                {
                    MessageBox.Show("Vstup pro dešifrování je prázdný");
                }
                else
                {
                    int keyAToInt = Convert.ToInt32(keyA_NumberView.Text);
                    int keyBToInt = Convert.ToInt32(keyB_NumberView.Text);
                    textOutput.Text = Functions.Decrypt(Functions.RemoveWhiteSpace(textInputDecrypt.Text), keyAToInt, keyBToInt);
                } 
            }

        }

        private void keyA_Minus_Click(object sender, RoutedEventArgs e)
        {

            keyA_Counter--;
            keyA_NumberView.Text = keyA_Counter.ToString();
        }

        private void keyA_Plus_Click(object sender, RoutedEventArgs e)
        {
            keyA_Counter++;
            keyA_NumberView.Text = keyA_Counter.ToString();
        }

        private void keyB_Minus_Click(object sender, RoutedEventArgs e)
        {

            keyB_Counter--;
            keyB_NumberView.Text = keyB_Counter.ToString();

        }

        private void keyB_Plus_Click(object sender, RoutedEventArgs e)
        {
            keyB_Counter++;
            keyB_NumberView.Text = keyB_Counter.ToString();
        }

    }
}

