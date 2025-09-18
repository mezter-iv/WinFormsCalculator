using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace SimpleCalculator
{
    public partial class Calculator : Form
    {
        List<string> numbers = new List<string>();
        List<string> operations = new List<string>();
        int memory = 0;
        public Calculator()
        {
            InitializeComponent();
            this.menuStrip1.BackColor = Color.FromArgb(50, 50, 50);
            this.menuStrip1.ForeColor = Color.White;
            this.fileToolStripMenuItem.BackColor = Color.FromArgb(50, 50, 50);
            this.menuStrip1.ForeColor = Color.White;
            listBox1.SelectionMode = SelectionMode.None;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void ClickNubmers(object sender, EventArgs e) {
            string text = (sender as Button).Text.ToString();
            if (label1.Text == "Error") label1.Text = "";
            if (text == "+") AlgorithmOfOperation(text);
            else if (text == "-") AlgorithmOfOperation(text);
            else if (text == "x") AlgorithmOfOperation(text);
            else if (text == "÷") AlgorithmOfOperation(text);
            else if (text == "%") AlgorithmOfOperation(text);
            else if (text == "=")
            {
                if (label1.Text != "" && label2.Text != "")
                {
                    numbers.Add(label1.Text);
                    label2.Text = "";
                    switch (operations[0])
                    {
                        case "+":
                            label1.Text = $"{Convert.ToDecimal(numbers[0]) + Convert.ToDecimal(numbers[1])}";
                            listBox1.Items.Add($"{numbers[0]} + {numbers[1]} = {label1.Text}");
                            numbers.Clear();
                            operations.Clear();
                            break;
                        case "-":
                            label1.Text = $"{Convert.ToDecimal(numbers[0]) - Convert.ToDecimal(numbers[1])}";
                            listBox1.Items.Add($"{numbers[0]} - {numbers[1]} = {label1.Text}");
                            numbers.Clear();
                            operations.Clear();
                            break;
                        case "x":
                            label1.Text = $"{Convert.ToDecimal(numbers[0]) * Convert.ToDecimal(numbers[1])}";
                            listBox1.Items.Add($"{numbers[0]} x {numbers[1]} = {label1.Text}");
                            numbers.Clear();
                            operations.Clear();
                            break;
                        case "÷":
                            if (numbers[1] == "0")
                            {
                                label1.Text = "Error";
                                break;
                            }
                            label1.Text = $"{Convert.ToDecimal(numbers[0]) / Convert.ToDecimal(numbers[1])}";
                            listBox1.Items.Add($"{numbers[0]} ÷ {numbers[1]} = {label1.Text}");
                            numbers.Clear();
                            operations.Clear();
                            break;
                        case "%":
                            label1.Text = $"{Convert.ToDecimal(numbers[0]) / 100 * Convert.ToDecimal(numbers[1])}";
                            listBox1.Items.Add($"{numbers[0]} % {numbers[1]} = {label1.Text}");
                            numbers.Clear();
                            operations.Clear();
                            break;
                    }
                }
                else if (label1.Text == "" && label2.Text != "")
                {
                    label1.Text = numbers[0];
                    numbers.Clear();
                    operations.Clear();
                    label2.Text = "";
                }
            }
            else if (text == "⌧")
            {
                if (label1.Text != "")
                {
                    label1.Text = label1.Text.Remove(label1.Text.Length - 1);
                }
            }
            else if (text == "+/-")
            {
                if (label1.Text != "0")
                {
                    char[] array = label1.Text.ToCharArray();
                    if (array[0] != '-')
                    {
                        label1.Text = $"-{label1.Text}";
                    }
                    else
                    {
                        label1.Text = "";
                        for (int i = 1; i < array.Length; i++)
                        {
                            label1.Text += array[i];
                        }
                    }
                }
            }
            else if (text == "x²")
            {
                if (label1.Text != "" && label1.Text != "0")
                {
                    label1.Text = $"{Convert.ToDecimal(label1.Text) * Convert.ToDecimal(label1.Text)}";
                }
            }
            else if (text == "√x")
            {
                if (label1.Text != "0" &&
                    label1.Text != "")
                {
                    label1.Text = $"{Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(label1.Text)))}";
                }
            }
            else if (text == "1/x")
            {
                if (label1.Text != "0" &&
                    label1.Text != "")
                {
                    label1.Text = $"{1 / Convert.ToDecimal(label1.Text)}";
                }
            }
            else if (text == "CE")
            {
                label1.Text = "";
            }
            else if (text == "C")
            {
                label1.Text = "";
                label2.Text = "";
                numbers.Clear();
                operations.Clear();
            }
            else
            {
                label1.Text += text;
            }
        }
        public void AlgorithmOfOperation(string text) {
            if (label1.Text != "")
            {
                if (numbers.Count == 1 && operations.Count == 1)
                {
                    numbers.Add(label1.Text);
                    SwitchCaseOperations(text);
                }
                else
                {
                    numbers.Add(label1.Text);
                    operations.Add(text);
                    label2.Text = $"{label1.Text}{text}";
                    label1.Text = "";
                }
            }
            else
            {
                if (numbers.Count != 0 && operations.Count != 0)
                {
                    operations[0] = text;
                    label2.Text = $"{numbers[0]}{operations[0]}";
                }
            }
        }
        public void SwitchCaseOperations(string text) {
            switch (operations[0])
            {
                case "+":
                    label2.Text = $"{Convert.ToDecimal(numbers[0]) + Convert.ToDecimal(numbers[1])}";
                    numbers.Clear();
                    numbers.Add(label2.Text);
                    operations.Clear();
                    operations.Add(text);
                    label2.Text += text;
                    label1.Text = "";
                    break;
                case "-":
                    label2.Text = $"{Convert.ToDecimal(numbers[0]) - Convert.ToDecimal(numbers[1])}";
                    numbers.Clear();
                    numbers.Add(label2.Text);
                    operations.Clear();
                    operations.Add(text);
                    label2.Text += text;
                    label1.Text = "";
                    break;
                case "x":
                    label2.Text = $"{Convert.ToDecimal(numbers[0]) * Convert.ToDecimal(numbers[1])}";
                    numbers.Clear();
                    numbers.Add(label2.Text);
                    operations.Clear();
                    operations.Add(text);
                    label2.Text += text;
                    label1.Text = "";
                    break;
                case "÷":
                    if (numbers[1] == "0") {
                        label1.Text = "Error";
                        break;
                    }
                    label2.Text = $"{Convert.ToDecimal(numbers[0]) / Convert.ToDecimal(numbers[1])}";
                    numbers.Clear();
                    numbers.Add(label2.Text);
                    operations.Clear();
                    operations.Add(text);
                    label2.Text += text;
                    label1.Text = "";
                    break;
                case "%":
                    label2.Text = $"{Convert.ToDecimal(numbers[0]) / 100 * Convert.ToDecimal(numbers[1])}";
                    numbers.Clear();
                    numbers.Add(label2.Text);
                    operations.Clear();
                    operations.Add(text);
                    label2.Text += text;
                    label1.Text = "";
                    break;
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Calculator calculator = new Calculator();
            calculator.Show();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.button1.BackColor = System.Drawing.Color.Gray;
            this.button2.BackColor = System.Drawing.Color.Gray;
            this.button3.BackColor = System.Drawing.Color.Gray;
            this.button4.BackColor = System.Drawing.Color.DarkGray;
            this.button5.BackColor = System.Drawing.Color.Gray;
            this.button6.BackColor = System.Drawing.Color.DarkGray;
            this.button7.BackColor = System.Drawing.Color.DarkGray;
            this.button8.BackColor = System.Drawing.Color.DarkGray;
            this.button9.BackColor = System.Drawing.Color.Gray;
            this.button10.BackColor = System.Drawing.Color.Gray;
            this.button11.BackColor = System.Drawing.Color.Gray;
            this.button12.BackColor = System.Drawing.Color.DarkGray;
            this.button13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button14.BackColor = System.Drawing.Color.DarkGray;
            this.button15.BackColor = System.Drawing.Color.DarkGray;
            this.button16.BackColor = System.Drawing.Color.DarkGray;
            this.button17.BackColor = System.Drawing.Color.Gray;
            this.button18.BackColor = System.Drawing.Color.Gray;
            this.button19.BackColor = System.Drawing.Color.Gray;
            this.button20.BackColor = System.Drawing.Color.DarkGray;
            this.button21.BackColor = System.Drawing.Color.DarkGray;
            this.button22.BackColor = System.Drawing.Color.DarkGray;
            this.button23.BackColor = System.Drawing.Color.DarkGray;
            this.button26.BackColor = System.Drawing.Color.Gray;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button4.ForeColor = System.Drawing.Color.Black;
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button6.ForeColor = System.Drawing.Color.Black;
            this.button7.ForeColor = System.Drawing.Color.Black;
            this.button8.ForeColor = System.Drawing.Color.Black;
            this.button9.ForeColor = System.Drawing.Color.White;
            this.button10.ForeColor = System.Drawing.Color.White;
            this.button11.ForeColor = System.Drawing.Color.White;
            this.button12.ForeColor = System.Drawing.Color.Black;
            this.button13.ForeColor = System.Drawing.Color.White;
            this.button14.ForeColor = System.Drawing.Color.Black;
            this.button15.ForeColor = System.Drawing.Color.Black;
            this.button16.ForeColor = System.Drawing.Color.Black;
            this.button17.ForeColor = System.Drawing.Color.White;
            this.button18.ForeColor = System.Drawing.Color.White;
            this.button19.ForeColor = System.Drawing.Color.White;
            this.button20.ForeColor = System.Drawing.Color.Black;
            this.button21.ForeColor = System.Drawing.Color.Black;
            this.button22.ForeColor = System.Drawing.Color.Black;
            this.button23.ForeColor = System.Drawing.Color.Black;
            this.button26.ForeColor = System.Drawing.Color.White;
            this.button24.ForeColor = System.Drawing.Color.Black;
            this.button25.ForeColor = System.Drawing.Color.Black;
            this.button27.ForeColor = System.Drawing.Color.Black;
            this.button28.ForeColor = System.Drawing.Color.Black;
            listBox1.BackColor = Color.FromArgb(200, 200, 200);
            listBox1.ForeColor = Color.Black;
            label1.ForeColor = System.Drawing.Color.Black;
            label2.ForeColor = System.Drawing.Color.Black;
            this.BackColor = Color.FromArgb(200, 200, 200);
            this.menuStrip1.BackColor = Color.Gray;
            this.menuStrip1.ForeColor = Color.White;
            this.fileToolStripMenuItem.BackColor = Color.Gray;
            this.menuStrip1.ForeColor = Color.White;


        }

        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.label1.ForeColor = System.Drawing.Color.White;
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.button10.ForeColor = System.Drawing.Color.White;
            this.button11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.button11.ForeColor = System.Drawing.Color.White;
            this.button18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.button18.ForeColor = System.Drawing.Color.White;
            this.button19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.button19.ForeColor = System.Drawing.Color.White;
            this.button26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.button26.ForeColor = System.Drawing.Color.White;
            this.button17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.button17.ForeColor = System.Drawing.Color.White;
            this.button9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.button9.ForeColor = System.Drawing.Color.White;

            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button7.ForeColor = System.Drawing.Color.White;
            this.button8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button8.ForeColor = System.Drawing.Color.White;
            this.button12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button12.ForeColor = System.Drawing.Color.White;
            this.button14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button14.ForeColor = System.Drawing.Color.White;
            this.button15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button15.ForeColor = System.Drawing.Color.White;
            this.button16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button16.ForeColor = System.Drawing.Color.White;
            this.button20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button20.ForeColor = System.Drawing.Color.White;
            this.button21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button21.ForeColor = System.Drawing.Color.White;
            this.button22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button22.ForeColor = System.Drawing.Color.White;
            this.button23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button23.ForeColor = System.Drawing.Color.White;
            this.button24.ForeColor = System.Drawing.Color.White;
            this.button25.ForeColor = System.Drawing.Color.White;
            this.button27.ForeColor = System.Drawing.Color.White;
            this.button28.ForeColor = System.Drawing.Color.White;

            this.button13.BackColor = System.Drawing.Color.Silver;
            this.button13.ForeColor = System.Drawing.Color.Black;

            this.BackColor = Color.FromArgb(30, 30, 30);

            listBox1.BackColor = Color.FromArgb(30, 30, 30);
            listBox1.ForeColor = Color.White;
            this.menuStrip1.BackColor = Color.FromArgb(50, 50, 50);
            this.menuStrip1.ForeColor = Color.White;
            this.fileToolStripMenuItem.BackColor = Color.FromArgb(50, 50, 50);
            this.menuStrip1.ForeColor = Color.White;
        }

        private void button27_Click(object sender, EventArgs e)
        {
            memory += Convert.ToInt32(label1.Text);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            memory -= Convert.ToInt32(label1.Text);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            memory = 0;
        }

        private void button25_Click(object sender, EventArgs e)
        {
            label1.Text = Convert.ToString(memory);
        }

        private void button29_Click(object sender, EventArgs e)
        {
            using (var sw = new StreamWriter("memorysave.txt", true)) {
                sw.WriteLine(memory);
            }
        }
    }
}
