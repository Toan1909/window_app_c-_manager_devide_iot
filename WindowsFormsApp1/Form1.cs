using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string ReceiveData =  String.Empty;
        string TransmitData =  String.Empty;

        int indexImg_01 = 0;
        int indexImg_02 = 0;
        int indexImg_03 = 0;
        int secondsTL01_red;
        int secondsTL01_yellow;
        int secondsTL01_green;
        int secondsTL02_red;
        int secondsTL02_yellow;
        int secondsTL02_green;
        int secondsTL03_red;
        int secondsTL03_yellow;
        int secondsTL03_green;
        int secondsTL04_red;
        int secondsTL04_yellow;
        int secondsTL04_green;
        bool btnAutoTL01 = false;
        bool btnAutoTL02 = false;
        bool btnAutoTL03 = false;
        bool btnAutoTL04 = false;
        //btn auto all
        bool btnAutoAll_check = false;
        //State of 4 StrafficLight ["red","green","yellow"]
        String stateStrafficLight01 = "none";
        String stateStrafficLight02 = "none";
        String stateStrafficLight03 = "none";
        String stateStrafficLight04 = "none";

        string[] imgNotice = { "bienbao01.jpg", "bienbao02.jpg", "bienbao03.jpg" };
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                comboBox1.Items.Add(port);
            }
            var imgs = imageList1.Images;

            imgs.Add(Properties.Resources.bien1);
            imgs.Add(Properties.Resources.bien2);
            imgs.Add(Properties.Resources.bien3);
            imgs.Add(Properties.Resources.bien4);
            imgs.Add(Properties.Resources.bien5);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.PortName = comboBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Select COM Port!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    if (serialPort1.IsOpen)
                    {
                        MessageBox.Show("COM Port is connected and ready for user !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        serialPort1.Open();
                        textBox2.BackColor = Color.Lime;
                        textBox2.Text = "Connecting";
                        comboBox1.Enabled = false;
                    }
                }
                catch(Exception)
                {
                    MessageBox.Show("COM Port is not found !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Close();
                    textBox2.BackColor = Color.Red;
                    textBox2.Text = "Disconnect";
                    comboBox1.Enabled = true;
                }
                else
                {
                    MessageBox.Show("COM Port is disconnected !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("COM Port is not found !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    stateStrafficLight01 = "yellow";
                    lightOn(stateStrafficLight01,StrafficLight01,groupBox2,"c");
                }
                else
                {
                    MessageBox.Show("COM Port is disconnected !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("COM Port is not found !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    stateStrafficLight01 = "red";
                    lightOn(stateStrafficLight01,StrafficLight01,groupBox2,"a");

                }
                else
                {
                    MessageBox.Show("COM Port is disconnected !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("COM Port is not found !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void lightOn(String stateStrafficLight, GroupBox imgStrafficLight_big ,GroupBox imgStrafficLight_small,String dataSend)
        {
            if (stateStrafficLight.Equals("red"))
            {
                //send data
                TransmitData = dataSend;
                serialPort1.Write(TransmitData);
                // setup background
                imgStrafficLight_big.BackgroundImage = WindowsFormsApp1.Properties.Resources.dentinhieu_r;
                imgStrafficLight_small.BackgroundImage = WindowsFormsApp1.Properties.Resources.dentinhieu_r2;
            }
            else if(stateStrafficLight.Equals("green"))
            {
                TransmitData = dataSend;
                serialPort1.Write(TransmitData);
                // setup background
                imgStrafficLight_big.BackgroundImage = WindowsFormsApp1.Properties.Resources.dentinhieu_g;
                imgStrafficLight_small.BackgroundImage = WindowsFormsApp1.Properties.Resources.dentinhieu_g2;
            }
            else if (stateStrafficLight.Equals("yellow"))
            {
                TransmitData = dataSend;
                serialPort1.Write(TransmitData);
                // setup background
                imgStrafficLight_big.BackgroundImage = WindowsFormsApp1.Properties.Resources.dentinhieu_y;
                imgStrafficLight_small.BackgroundImage = WindowsFormsApp1.Properties.Resources.dentinhieu_y2;
            }
        }
        private void redLightState(String dataSend ,GroupBox imgStrafficLight_big,GroupBox imgStrafficLight_small,Timer timer)
        {
            TransmitData = dataSend;
            serialPort1.Write(TransmitData);
            // setup background
            imgStrafficLight_big.BackgroundImage = WindowsFormsApp1.Properties.Resources.dentinhieu_r;
            imgStrafficLight_small.BackgroundImage = WindowsFormsApp1.Properties.Resources.dentinhieu_r2;
            // counter seconds
            timer.Start();
        }
        private void greenLightState(String dataSend, GroupBox imgStrafficLight_big, GroupBox imgStrafficLight_small, Timer timer)
        {
            TransmitData = dataSend;
            serialPort1.Write(TransmitData);
            // setup background
            imgStrafficLight_big.BackgroundImage = WindowsFormsApp1.Properties.Resources.dentinhieu_g;
            imgStrafficLight_small.BackgroundImage = WindowsFormsApp1.Properties.Resources.dentinhieu_g2;
            // counter seconds
            timer.Start();
        }
        private void yellowLightState(String dataSend, GroupBox imgStrafficLight_big, GroupBox imgStrafficLight_small, Timer timer)
        {
            TransmitData = dataSend;
            serialPort1.Write(TransmitData);
            // setup background
            imgStrafficLight_big.BackgroundImage = WindowsFormsApp1.Properties.Resources.dentinhieu_y;
            imgStrafficLight_small.BackgroundImage = WindowsFormsApp1.Properties.Resources.dentinhieu_y2;

            // counter seconds
            
            timer.Start();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            ReceiveData = serialPort1.ReadExisting();
            this.Invoke(new EventHandler (doUpDate));
        }
        private void doUpDate(Object sender, EventArgs e)
        {
            if (ReceiveData == "r")
            {
                StrafficLight01.BackgroundImage = WindowsFormsApp1.Properties.Resources.dentinhieu_r;
            }
            else if (ReceiveData == "g")
            {
                StrafficLight01.BackgroundImage = WindowsFormsApp1.Properties.Resources.dentinhieu_g;
            }
            else if (ReceiveData == "y")
            {
                StrafficLight01.BackgroundImage = WindowsFormsApp1.Properties.Resources.dentinhieu_y;
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult answer = MessageBox.Show("Do you want exit the program ? ", "EXIT", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.No)
            {
                e.Cancel = true;
            }
            else if (serialPort1.IsOpen)
            {
                mapTraffic.Visible= false;
                serialPort1.Close(); 
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    stateStrafficLight01 = "green";
                    lightOn(stateStrafficLight01, StrafficLight01, groupBox2,"b");
                }
                else
                {
                    MessageBox.Show("COM Port is disconnected !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("COM Port is not found !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
        private void button6_Click_1(object sender, EventArgs e)
        {
            //this btn is playing auto for Trafficlight_01
            try
            {
                if (serialPort1.IsOpen)
                {

                    if (btnAutoTL01 == false)
                    {
                        button6.BackColor = Color.Pink;
                        secondsTL01_red = Convert.ToInt32(textBox1.Text);
                        secondsTL01_green = Convert.ToInt32(textBox3.Text);
                        secondsTL01_yellow = Convert.ToInt32(textBox4.Text);
                        timer1.Start();
                        textBox1.Enabled = false;
                        textBox3.Enabled = false;
                        textBox4.Enabled = false;
                        button4.Enabled = false;
                        button3.Enabled = false;
                        button5.Enabled = false;
                        btnAutoTL01 = true;
                    }
                    else if (btnAutoTL01 == true)
                    {
                        button6.BackColor = Color.White;
                        timer1.Stop();
                        textBox1.Enabled = true;
                        textBox3.Enabled = true;
                        textBox4.Enabled = true;
                        button4.Enabled = true;
                        button3.Enabled = true;
                        button5.Enabled = true;
                        btnAutoTL01 = false;
                    }
                }
                else
                {
                    MessageBox.Show("COM Port is disconnected !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("COM Port is not found !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (stateStrafficLight01.Equals("red"))
            {
                //display countdown number
                label3.Text = secondsTL01_red.ToString();
                label4.Text = secondsTL01_red.ToString();
                secondsTL01_red -= 1;
                if (secondsTL01_red < 0)
                {
                    timer1.Stop();
                    label3.Text = "--";
                    label4.Text = "--";
                    //change state Light
                    stateStrafficLight01 = "green";
                    //get time display
                    secondsTL01_green = Convert.ToInt32(textBox3.Text);
                    //display state
                    greenLightState("b", StrafficLight01, groupBox2, timer1);
                    
                }
            }
            else if (stateStrafficLight01.Equals("green"))
            {
                
                label3.Text = secondsTL01_green.ToString();
                label4.Text = secondsTL01_green.ToString();

                secondsTL01_green -= 1;
                if (secondsTL01_green < 0)
                {
                    timer1.Stop();
                    label3.Text = "--"; 
                    label4.Text = "--";
                    stateStrafficLight01 = "yellow";
                    secondsTL01_yellow = Convert.ToInt32(textBox4.Text);
                    yellowLightState("c", StrafficLight01, groupBox2, timer1);
                    
                }
                
            }
            else if (stateStrafficLight01.Equals("yellow"))
            {
                
                label3.Text = secondsTL01_yellow.ToString();
                label4.Text = secondsTL01_yellow.ToString();
                secondsTL01_yellow -= 1;
                if (secondsTL01_yellow < 0)
                {
                    timer1.Stop();
                    label3.Text = "--";
                    label4.Text = "--";
                    stateStrafficLight01 = "red";
                    secondsTL01_red = Convert.ToInt32(textBox1.Text);
                    redLightState("a", StrafficLight01, groupBox2, timer1);
                }
            }
            else if (stateStrafficLight01.Equals("none"))
            {
                stateStrafficLight01 = "red";
                TransmitData = "a";
                serialPort1.Write(TransmitData);
            }

        }
        

        private void groupBox3_Enter(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            textBox7.Text = textBox1.Text.Trim();
        }

        private void btnOpenMap_Click(object sender, EventArgs e)
        {
            mapTraffic.Visible = true;
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            mapTraffic.Visible=false;
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            StrafficLight01.Visible=true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            StrafficLight01.Visible=false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            StrafficLight02.Visible=true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            StrafficLight02.Visible = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //this btn is playing auto for Trafficlight_01
            try
            {
                if (serialPort1.IsOpen)
                {

                    if (btnAutoTL02 == false)
                    {
                        button12.BackColor = Color.Pink;
                        secondsTL02_red = Convert.ToInt32(textBox7.Text);
                        secondsTL02_green = Convert.ToInt32(textBox6.Text);
                        secondsTL02_yellow = Convert.ToInt32(textBox5.Text);
                        timer2.Start();
                        textBox7.Enabled = false;
                        textBox6.Enabled = false;
                        textBox5.Enabled = false;
                        button13.Enabled = false;
                        button14.Enabled = false;
                        button15.Enabled = false;
                        btnAutoTL02 = true;
                    }
                    else if (btnAutoTL02 == true)
                    {
                        button12.BackColor = Color.White;
                        timer2.Stop();
                        textBox7.Enabled = true;
                        textBox6.Enabled = true;
                        textBox5.Enabled = true;
                        button13.Enabled = true;
                        button14.Enabled = true;
                        button15.Enabled = true;
                        btnAutoTL02 = false;
                    }
                }
                else
                {
                    MessageBox.Show("COM Port is disconnected !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("COM Port is not found !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            stateStrafficLight02 = "red";
            lightOn(stateStrafficLight02, StrafficLight02, groupBox3,"d");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            stateStrafficLight02 = "yellow";
            lightOn(stateStrafficLight02, StrafficLight02, groupBox3,"f");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            stateStrafficLight02 = "green";
            lightOn(stateStrafficLight02, StrafficLight02, groupBox3,"e");
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (stateStrafficLight02.Equals("red"))
            {
                //display countdown number
                label11.Text = secondsTL02_red.ToString();
                label1.Text = secondsTL02_red.ToString();
                secondsTL02_red -= 1;
                if (secondsTL02_red < 0)
                {
                    timer2.Stop();
                    label11.Text = "--";
                    label1.Text = "--";
                    //change state Light
                    stateStrafficLight02 = "green";
                    //get time display
                    secondsTL02_green = Convert.ToInt32(textBox6.Text);
                    //display state
                    greenLightState("e", StrafficLight02, groupBox3, timer2);

                }
            }
            else if (stateStrafficLight02.Equals("green"))
            {

                label11.Text = secondsTL02_green.ToString();
                label1.Text = secondsTL02_green.ToString();

                secondsTL02_green -= 1;
                if (secondsTL02_green < 0)
                {
                    timer2.Stop();
                    label11.Text = "--";
                    label1.Text = "--";
                    stateStrafficLight02 = "yellow";
                    secondsTL02_yellow = Convert.ToInt32(textBox5.Text);
                    yellowLightState("f", StrafficLight02, groupBox3, timer2);

                }

            }
            else if (stateStrafficLight02.Equals("yellow"))
            {

                label11.Text = secondsTL02_yellow.ToString();
                label1.Text = secondsTL02_yellow.ToString();
                secondsTL02_yellow -= 1;
                if (secondsTL02_yellow < 0)
                {
                    timer2.Stop();
                    label11.Text = "--";
                    label1.Text = "--";
                    stateStrafficLight02 = "red";
                    secondsTL02_red = Convert.ToInt32(textBox7.Text);
                    redLightState("d", StrafficLight02, groupBox3, timer2);
                }
            }
            else if (stateStrafficLight02.Equals("none"))
            {
                stateStrafficLight02 = "red";
                TransmitData = "d";
                serialPort1.Write(TransmitData);
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            StrafficLight03.Visible = true;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            StrafficLight03.Visible = false;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            stateStrafficLight03 = "red";
            lightOn(stateStrafficLight03, StrafficLight03, groupBox5,"g");
        }

        private void button20_Click(object sender, EventArgs e)
        {
            stateStrafficLight03 = "yellow";
            lightOn(stateStrafficLight03, StrafficLight03, groupBox5,"i");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            stateStrafficLight03 = "green";
            lightOn(stateStrafficLight03, StrafficLight03, groupBox5,"h");
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (stateStrafficLight03.Equals("red"))
            {
                //display countdown number
                label16.Text = secondsTL03_red.ToString();
                label15.Text = secondsTL03_red.ToString();
                secondsTL03_red -= 1;
                if (secondsTL03_red < 0)
                {
                    timer3.Stop();
                    label16.Text = "--";
                    label15.Text = "--";
                    //change state Light
                    stateStrafficLight03 = "green";
                    //get time display
                    secondsTL03_green = Convert.ToInt32(textBox9.Text);
                    //display state
                    greenLightState("h", StrafficLight03, groupBox5, timer3);

                }
            }
            else if (stateStrafficLight03.Equals("green"))
            {

                label16.Text = secondsTL03_green.ToString();
                label15.Text = secondsTL03_green.ToString();

                secondsTL03_green -= 1;
                if (secondsTL03_green < 0)
                {
                    timer3.Stop();
                    label16.Text = "--";
                    label15.Text = "--";
                    stateStrafficLight03 = "yellow";
                    secondsTL03_yellow = Convert.ToInt32(textBox8.Text);
                    yellowLightState("i", StrafficLight03, groupBox5, timer3);

                }

            }
            else if (stateStrafficLight03.Equals("yellow"))
            {

                label16.Text = secondsTL03_yellow.ToString();
                label15.Text = secondsTL03_yellow.ToString();
                secondsTL03_yellow -= 1;
                if (secondsTL03_yellow < 0)
                {
                    timer3.Stop();
                    label16.Text = "--";
                    label15.Text = "--";
                    stateStrafficLight03 = "red";
                    secondsTL03_red = Convert.ToInt32(textBox10.Text);
                    redLightState("g", StrafficLight03, groupBox5, timer3);
                }
            }
            else if (stateStrafficLight03.Equals("none"))
            {
                stateStrafficLight03 = "red";
                TransmitData = "g";
                serialPort1.Write(TransmitData);
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            //this btn is playing auto for Trafficlight_01
            try
            {
                if (serialPort1.IsOpen)
                {

                    if (btnAutoTL03 == false)
                    {
                        button17.BackColor = Color.Pink;
                        secondsTL03_red = Convert.ToInt32(textBox10.Text);
                        secondsTL03_green = Convert.ToInt32(textBox9.Text);
                        secondsTL03_yellow = Convert.ToInt32(textBox8.Text);
                        timer3.Start();
                        textBox10.Enabled = false;
                        textBox9.Enabled = false;
                        textBox8.Enabled = false;
                        button18.Enabled = false;
                        button19.Enabled = false;
                        button20.Enabled = false;
                        btnAutoTL03 = true;
                    }
                    else if (btnAutoTL03 == true)
                    {
                        button17.BackColor = Color.White;
                        timer3.Stop();
                        textBox10.Enabled = true;
                        textBox9.Enabled = true;
                        textBox8.Enabled = true;
                        button18.Enabled = true;
                        button19.Enabled = true;
                        button20.Enabled = true;
                        btnAutoTL03 = false;
                    }
                }
                else
                {
                    MessageBox.Show("COM Port is disconnected !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("COM Port is not found !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            StrafficLight04.Visible = true;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            StrafficLight04.Visible = false;
        }

        private void button25_Click(object sender, EventArgs e)
        {
            stateStrafficLight04 = "red";
            lightOn(stateStrafficLight04, StrafficLight04, groupBox6,"j");
        }

        private void button26_Click(object sender, EventArgs e)
        {
            stateStrafficLight04 = "yellow";
            lightOn(stateStrafficLight04, StrafficLight04, groupBox6,"l");
        }

        private void button24_Click(object sender, EventArgs e)
        {
            stateStrafficLight04 = "green";
            lightOn(stateStrafficLight04, StrafficLight04, groupBox6,"k");
        }

        private void button23_Click(object sender, EventArgs e)
        {
            //this btn is playing auto for Trafficlight_04
            try
            {
                if (serialPort1.IsOpen)
                {

                    if (btnAutoTL04 == false)
                    {
                        button23.BackColor = Color.Pink;
                        secondsTL04_red = Convert.ToInt32(textBox13.Text);
                        secondsTL04_green = Convert.ToInt32(textBox12.Text);
                        secondsTL04_yellow = Convert.ToInt32(textBox11.Text);
                        timer4.Start();
                        textBox13.Enabled = false;
                        textBox12.Enabled = false;
                        textBox11.Enabled = false;
                        button25.Enabled = false;
                        button26.Enabled = false;
                        button24.Enabled = false;
                        btnAutoTL04 = true;
                    }
                    else if (btnAutoTL04 == true)
                    {
                        button23.BackColor = Color.White;
                        timer4.Stop();
                        textBox13.Enabled = true;
                        textBox12.Enabled = true;
                        textBox11.Enabled = true;
                        button24.Enabled = true;
                        button25.Enabled = true;
                        button26.Enabled = true;
                        btnAutoTL04 = false;
                    }
                }
                else
                {
                    MessageBox.Show("COM Port is disconnected !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("COM Port is not found !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            if (stateStrafficLight04.Equals("red"))
            {
                //display countdown number
                label20.Text = secondsTL04_red.ToString();
                label21.Text = secondsTL04_red.ToString();
                secondsTL04_red -= 1;
                if (secondsTL04_red < 0)
                {
                    timer4.Stop();
                    label20.Text = "--";
                    label21.Text = "--";
                    //change state Light
                    stateStrafficLight04 = "green";
                    //get time display
                    secondsTL04_green = Convert.ToInt32(textBox12.Text);
                    //display state
                    greenLightState("k", StrafficLight04, groupBox6, timer4);

                }
            }
            else if (stateStrafficLight04.Equals("green"))
            {

                label20.Text = secondsTL04_green.ToString();
                label21.Text = secondsTL04_green.ToString();
                secondsTL04_green -= 1;
                if (secondsTL04_green < 0)
                {
                    timer4.Stop();
                    label20.Text = "--";
                    label21.Text = "--";
                    stateStrafficLight04 = "yellow";
                    secondsTL04_yellow = Convert.ToInt32(textBox11.Text);
                    yellowLightState("l", StrafficLight04, groupBox6, timer4);

                }

            }
            else if (stateStrafficLight04.Equals("yellow"))
            {

                label20.Text = secondsTL04_yellow.ToString();
                label21.Text = secondsTL04_yellow.ToString();
                secondsTL04_yellow -= 1;
                if (secondsTL04_yellow < 0)
                {
                    timer4.Stop();
                    label20.Text = "--";
                    label21.Text = "--";
                    stateStrafficLight04 = "red";
                    secondsTL04_red = Convert.ToInt32(textBox13.Text);
                    redLightState("j", StrafficLight04, groupBox6, timer4);
                }
            }
            else if (stateStrafficLight04.Equals("none"))
            {
                stateStrafficLight04 = "red";
                TransmitData = "j";
                serialPort1.Write(TransmitData);
            }
        }

        private void btnAutoAll_Click(object sender, EventArgs e)
        {
            if (!btnAutoAll_check)
            {
                timer1.Start();
                timer2.Start();
                timer3.Start();
                timer4.Start();
                btnAutoAll.BackColor = Color.Pink;
                btnAutoAll.Text = "Auto OFF";
                btnAutoAll_check = true;
                if (btnAutoTL01 == false)
                {
                    button6_Click_1(sender, e);
                }
                if (btnAutoTL02 == false)
                {
                    button12_Click(sender, e);
                }
                if (btnAutoTL03 == false)
                {
                    button17_Click(sender, e);
                }
                if (btnAutoTL04 == false)
                {
                    button23_Click(sender, e);
                }
            }
            else
            {
                timer1.Stop();
                timer2.Stop();
                timer3.Stop();
                timer4.Stop();
                btnAutoAll.BackColor = Color.White;
                btnAutoAll.Text = "AutoAll";
                btnAutoAll_check = false;
                if (btnAutoTL01 == true)
                {
                    button6_Click_1(sender, e);
                }
                if (btnAutoTL02 == true)
                {
                    button12_Click(sender, e);
                }
                if (btnAutoTL03 == true)
                {
                    button17_Click(sender, e);
                }
                if (btnAutoTL04 == true)
                {
                    button23_Click(sender, e);
                }
            }
            
        }
        
        private void button30_Click(object sender, EventArgs e)
        {
            indexImg_01++;
            if (indexImg_01 > 4)
            {
                indexImg_01 = 0;

            }
            switch (indexImg_01)
            {
                case 0:
                    TransmitData = "6";
                    serialPort1.Write(TransmitData);
                    break;
                case 1:
                    TransmitData = "7";
                    serialPort1.Write(TransmitData);
                    break;
                case 2:
                    TransmitData = "8";
                    serialPort1.Write(TransmitData);
                    break;
                case 3:
                    TransmitData = "9";
                    serialPort1.Write(TransmitData);
                    break;
                case 4:
                    TransmitData = "0";
                    serialPort1.Write(TransmitData);
                    break;


            }
            pictureBox1.Image = imageList1.Images[indexImg_01]; 
            pictureBox2.Image = imageList1.Images[indexImg_01];
        }

        private void button31_Click(object sender, EventArgs e)
        {
            groupBox4.Visible = true;
        }

        private void button28_Click(object sender, EventArgs e)
        {
            groupBox4.Visible = false;
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {

        }

        private void button32_Click(object sender, EventArgs e)
        {
            indexImg_01--;
            if (indexImg_01 <0)
            {
                indexImg_01 = 4;

            }
            switch (indexImg_01)
            {
                case 0:
                    TransmitData = "6";
                    serialPort1.Write(TransmitData);
                    break;
                case 1:
                    TransmitData = "7";
                    serialPort1.Write(TransmitData);
                    break;
                case 2:
                    TransmitData = "8";
                    serialPort1.Write(TransmitData);
                    break;
                case 3:
                    TransmitData = "9";
                    serialPort1.Write(TransmitData);
                    break;
                case 4:
                    TransmitData = "0";
                    serialPort1.Write(TransmitData);
                    break;


            }
            pictureBox1.Image = imageList1.Images[indexImg_01];
            pictureBox2.Image = imageList1.Images[indexImg_01];
        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void button33_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void button34_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = textBox7.Text.Trim();
        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {
            textBox6.Text = textBox3.Text.Trim();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            textBox3.Text = textBox6.Text.Trim();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBox5.Text = textBox4.Text.Trim();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBox4.Text = textBox5.Text.Trim();
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            textBox13.Text = textBox10.Text.Trim();
        }
    }
}
