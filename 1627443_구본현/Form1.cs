using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1627443_구본현
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void num1_Click(object sender, EventArgs e)
        {
            Button press = (Button)sender;
            textBox1.Text += press.Text;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxBit.SelectedItem = "16";
            //comboBoxBit.SelectedText = "16";
            labelETC.Text = "입력받는 정수는 최대 9223372036854775807까지\n";
            labelETC.Text += "이용 가능합니다.(단 64bit 일 때)\n\n";
            labelETC.Text += "출력되는 답안은 4비트씩 끊어서 출력됩니다.\n\n";
            labelETC.Text += "0을 예외 처리하지 않았습니다.\n\n";
            labelETC.Text += "잘못된 범위의 정수가 입력되면 실행되지 않습니다.\n";
        }

        private int bitSize;
        private void buttonStart_Click(object sender, EventArgs e)
        {
            //비트연산자는 일부러 쓰지 않았습니다.
            //불리언으로 정의 할 수 있으나 편의상 캐릭터로 정의되었습니다.
            char[] case1 = new char[bitSize];//출력 1에 사용되는 배열
            char[] case2 = new char[bitSize];//출력 2, 3에 사용되는 배열

            string output1;
            string output2;
            string output3;
            string outputSplit1 = "";
            string outputSplit2 = "";
            string outputSplit3 = "";

            ulong bitOfMax = 1;//최대범위
            for (int i = 0; i < bitSize - 1; i++)
                bitOfMax *= 2;

            if (textBox1.Text == "")
                MessageBox.Show("입력받지 못했습니다.");
            else
            {
                //64비트 부호없는 정수
                ulong value = Convert.ToUInt64(textBox1.Text);
                
                if (value >= bitOfMax)//오류!  16 = 32767
                    MessageBox.Show("입력받은 정수가 최대범위를 벗어났습니다.");
                else
                {
                    //--
                    case1 = Enumerable.Repeat('0', bitSize).ToArray();//초기화
                    int index = 0;//while문에 사용되는 인덱스
                    case1[bitSize - 1] = '1';
                    while (value != 0)//2진수 변환
                    {
                        if (value % 2 == 1)
                            case1[index] = '1';
                        index++;
                        value /= 2;
                    }
                    Array.Reverse(case1);//역순
                    output1 = new string(case1);//답안1
                    //--
                    case1[0] = '0';
                    for (int i = 0; i < bitSize; i++)
                    {
                        if (case1[i] == '0')
                            case2[i] = '1';
                        else
                            case2[i] = '0';
                    }
                    output2 = new string(case2);//답안2
                    //-
                    index = bitSize - 1;//역순으로 내려감
                    while (index >= 0 && case2[index] != '0')//2진수에서 1을 더한다 = 0을 만날때 까지 1을 0으로 바꾼다.
                        case2[index--] = '0';
                    if (index >= 0)
                        case2[index] = '1';
                    output3 = new string(case2);//답안3

                    //쪼개는 작업
                    for (int i = 0; i < output1.Length; i++)//출력의 길이는 동일 
                    {
                        if (i % 4 == 0)
                        {
                            outputSplit1 += " ";
                            outputSplit2 += " ";
                            outputSplit3 += " ";
                        }
                        outputSplit1 += output1[i];
                        outputSplit2 += output2[i];
                        outputSplit3 += output3[i];
                    }

                    labelOUT1.Text = outputSplit1;
                    labelOUT2.Text = outputSplit2;
                    labelOUT3.Text = outputSplit3;
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            labelOUT1.Text = "NULL";
            labelOUT2.Text = "NULL";
            labelOUT3.Text = "NULL";
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            bitSize = Convert.ToInt32(cb.Items[cb.SelectedIndex].ToString());
        }

        //비트 콤보박스


    }
}
