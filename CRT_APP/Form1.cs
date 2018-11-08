using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRT_APP
{
    public partial class Form1 : Form
    {
        //List<Model> models = new List<Model>();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "打开串口" && (comboBox1.Text != "")) {

                //MessageBox.Show("内容", "标题");
                serialPort1.ReadTimeout = 1000;

                serialPort1.Open();//打开串口
                //serialPort1.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);///添加委托！！
                button1.Text = "关闭串口";
                comboBox1.Enabled = false;
            }
            else if (button1.Text == "关闭串口") {
                serialPort1.Close();//关闭串口
                button1.Text = "打开串口";
                comboBox1.Enabled = true;
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.PortName = comboBox1.Text;
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            
            string[] ports = SerialPort.GetPortNames();
            //comboBox1.DataSource = null;
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(ports);
            //comboBox1.SelectedItem = comboBox1.Items[0];
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try {

                Thread.Sleep(60);//用于接收完整信息  很重要的一个方法
                int count = serialPort1.BytesToRead;  //不一定等于帧长度
                byte[] ReceiveBuf = new byte[count];
                //String str = System.Text.Encoding.Default.GetString(ReceiveBuf);
                //MessageBox.Show(str, "tishi");

                serialPort1.Read(ReceiveBuf, 0, count);
                System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;

                if (ByteToHexStr(ReceiveBuf).Substring(0, 2) == "12")
                {
                    textBox1.Text += ByteToHexStr(ReceiveBuf);
                    textBox1.Text += "\r\n";
                    textBox2.Text += "12  \r\n";
                    List<Model> models = new List<Model>();
                    Model model = Convert(ByteToHexStr(ReceiveBuf));
                    models.Add(model);
                    dataGridView1.DataSource = models;
                }
                else if (ByteToHexStr(ReceiveBuf).Substring(0, 2) == "10")
                {
                    textBox1.Text += ByteToHexStr(ReceiveBuf);
                    textBox1.Text += "\r\n";
                    textBox2.Text += "控制器request \r\n";
                    List<Model> models = new List<Model>();
                    Model model = Convert(ByteToHexStr(ReceiveBuf));
                    models.Add(model);
                    dataGridView1.DataSource = models;
                }
                else if (ByteToHexStr(ReceiveBuf).Substring(0, 2) == "14")
                {
                    textBox1.Text += ByteToHexStr(ReceiveBuf);
                    textBox1.Text += "\r\n";
                    textBox2.Text += "控制器通知CRT未收到CRT确认  \r\n";
                    List<Model> models = new List<Model>();
                    Model model = Convert(ByteToHexStr(ReceiveBuf));
                    models.Add(model);
                    dataGridView1.DataSource = models; ;
                }
                else if (ByteToHexStr(ReceiveBuf).Substring(0, 2) == "15")
                {
                    textBox1.Text += ByteToHexStr(ReceiveBuf);
                    textBox1.Text += "\r\n";
                    textBox2.Text += "15  \r\n";
                    List<Model> models = new List<Model>();
                    Model model = Convert(ByteToHexStr(ReceiveBuf));
                    models.Add(model);
                    dataGridView1.DataSource = models;
                }
                else if (ByteToHexStr(ReceiveBuf).Substring(0, 2) == "16")
                {
                    textBox1.Text += ByteToHexStr(ReceiveBuf);
                    textBox1.Text += "\r\n";
                    textBox2.Text += "发起心跳连接\r\n";
                }
                else if (ByteToHexStr(ReceiveBuf).Substring(0, 2) == "18")
                {
                    textBox1.Text += ByteToHexStr(ReceiveBuf);
                    textBox1.Text += "\r\n";
                    textBox2.Text += "心跳\r\n";
                }
                else if (ByteToHexStr(ReceiveBuf).Substring(0, 2) == "26")
                {
                    textBox1.Text += ByteToHexStr(ReceiveBuf);
                    textBox1.Text += "\r\n";
                    textBox2.Text += "心跳回复\r\n";
                }
                else
                {
                    textBox1.Text += ByteToHexStr(ReceiveBuf);
                    textBox1.Text += "\r\n";
                    textBox2.Text += "未定义\r\n";
                }

                
                //dataGridView1.Refresh();


                ///鼠标始终最后
                this.textBox1.Focus();//获取焦点
                this.textBox1.Select(this.textBox1.TextLength, 0);//光标定位到文本最后
                this.textBox1.ScrollToCaret();//滚动到光标处

                this.textBox2.Focus();//获取焦点
                this.textBox2.Select(this.textBox2.TextLength, 0);//光标定位到文本最后
                this.textBox2.ScrollToCaret();//滚动到光标处
            }
            catch {

            }
            
        }

        /// <summary>
        /// 字节byte[]转为16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static string ByteToHexStr(byte[] bytes)
        {
            string returnstr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                    returnstr += bytes[i].ToString("X2");
            }
            return returnstr;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Byte[] BSendTemp = new Byte[1]; //建立临时字节数组对象
            //BSendTemp[0] = Byte.Parse(textBox1.Text);//由文本框读入想要发送的数据
            BSendTemp[0] = 0x26;//字节型常量就可以了！
            textBox1.Text += "0x26 \r\n";
            textBox2.Text += "心跳回复 \r\n";

            serialPort1.DtrEnable = true;
            serialPort1.Write(BSendTemp, 0, 1);//发送数据  
        }

        public void end_focus()
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            //models.Clear();
        }

        public Model Convert(String data)
        {
            Model model = new Model();
            if(data.Length == 68) {
                model.Demarcate = data.Substring(0, 2);
                model.Node = data.Substring(3, 1) + data.Substring(5, 1);
                model.Loop = data.Substring(7, 1) + data.Substring(9, 1);
                model.Unit = data.Substring(11, 1) + data.Substring(13, 1);
                model.UnitType = data.Substring(15, 1) + data.Substring(17, 1);
                model.Building = data.Substring(19, 1) + data.Substring(21, 1);
                model.Floor = data.Substring(23, 1) + data.Substring(25, 1);
                model.Area = data.Substring(27, 1) + data.Substring(29, 1);
                model.Year = data.Substring(31, 1) + data.Substring(33, 1);
                model.Month = data.Substring(35, 1) + data.Substring(37, 1);
                model.Day = data.Substring(39, 1) + data.Substring(41, 1);
                model.Hour = data.Substring(43, 1) + data.Substring(45, 1);
                model.Minute = data.Substring(47, 1) + data.Substring(49, 1);
                model.Second = data.Substring(51, 1) + data.Substring(53, 1);

                model.Time = model.Year + "/" + model.Month + "/" + model.Day + " " + model.Hour + ":" + model.Minute + ":" + model.Second;

                model.Events = data.Substring(55, 1) + data.Substring(57, 1);
                model.Unused = data.Substring(59, 1) + data.Substring(61, 1) + data.Substring(63, 1) + data.Substring(65, 1);
                model.Check = data.Substring(66, 2);
            }
            return model;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("内容","标题");
            //textBox1.Width = Convert.ToInt64(0.5 * panel1.Width);
            //textBox1.Width = (int)(panel1.Width * 0.5);
            //textBox1.Height = (int)(panel1.Height * 0.7);
            //dataGridView1.Width = (int)(panel1.Width);
            //dataGridView1.Height = (int)(panel1.Height * 0.26);
        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {

        }
    }
}
