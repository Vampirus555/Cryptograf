using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace Cryptograf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
       
        char[] characters = new char[] { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и',
                                                'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с',
                                                'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь',
                                                'э', 'ю', 'я' };
        private int N;
        public MainWindow()
        {
            InitializeComponent();
            N = characters.Length;
        }


        // шифрование
        public string Encode(string input, string keyword)
        {
            

            string result = "";

            int keyword_index = 0;

            foreach (char symbol in input)
            {

                if ((keyword_index) == keyword.Length)
                    keyword_index = 0;

                if (characters.Contains(symbol))
                {
                    int c = (Array.IndexOf(characters, symbol) +
                    Array.IndexOf(characters, keyword[keyword_index])) % N;

                    result += characters[c];

                    keyword_index++;
                }
                else
                    result += symbol;

               
            }

            return result;
        }

        // дешифрование
        public string Decode(string input, string keyword)
        {
            

            string result = "";

            int keyword_index = 0;

            foreach (char symbol in input)
            {
                if ((keyword_index) == keyword.Length)
                    keyword_index = 0;

                if (characters.Contains(symbol))
                {
                    int p = (Array.IndexOf(characters, symbol) + N -
                    Array.IndexOf(characters, keyword[keyword_index])) % N;
                    
                    result += characters[p];

                    keyword_index++;
                }
                else
                    result += symbol;
            }

            return result;
        }


        // Нажать: Расшифровать файл
        private OpenFileDialog openFileDialog1;
        private void ButtonDecipher_Click(object sender, EventArgs e)
        {
            
            string s;
            openFileDialog1 = new OpenFileDialog();
            try
            {
                openFileDialog1.ShowDialog();
                StreamReader sr = new StreamReader(openFileDialog1.FileName, Encoding.GetEncoding(1251));

                StreamWriter sw;

                SaveFileDialog SFD = new SaveFileDialog();

                SFD.FileName = "Out";
                SFD.Filter = "TXT (*.txt)|*.txt";

                try
                {
                    SFD.ShowDialog();
                    sw = new StreamWriter(SFD.FileName, false, Encoding.GetEncoding(1251));

                    while (!sr.EndOfStream)
                    {
                        s = sr.ReadLine();
                        sw.WriteLine(Decode(s, textBox1.Text));
                        textBox2.Text = Decode(s, textBox1.Text);
                    }

                    sr.Close();
                    sw.Close();
                }
                catch (Exception ex) { }
            }
            catch (Exception ex) { }
 
        }







        //Нажать: Зашифровать файл
        private void ButtonEncoder_Click(object sender, EventArgs e)
        {

            string s;
            openFileDialog1 = new OpenFileDialog();
            try
            {
                openFileDialog1.ShowDialog();
                StreamReader sr = new StreamReader(openFileDialog1.FileName);

                StreamWriter sw;

                SaveFileDialog SFD = new SaveFileDialog();

                SFD.FileName = "Out";
                SFD.Filter = "TXT (*.txt)|*.txt";

                try
                {
                    SFD.ShowDialog();
                    sw = new StreamWriter(SFD.FileName);

                    while (!sr.EndOfStream)
                    {
                        s = sr.ReadLine();
                        sw.WriteLine(Encode(s, textBox1.Text));
                    }

                    sr.Close();
                    sw.Close();
                }
                catch (Exception ex) { }
            }
            catch (Exception ex) { }

        }
    }
}
