using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PersonelTakip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-5L1LUNIR;Initial Catalog=personel;Integrated Security=True");
        
        public static string tcno, adi, soyadi, yetki ,kullaniciadi, parola;

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        int hak = 3; bool durum = false;

        private void button1_Click(object sender, EventArgs e)
        {
            if (hak!=0)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select * from kullanicilar ",baglanti);
                SqlDataReader kayitokuma = komut.ExecuteReader();
                while (kayitokuma.Read())
                {
                    if (radioButton1.Checked==true)
                    {
                        if (kayitokuma["kullaniciadi"].ToString()==textBox1.Text && kayitokuma["parola"].ToString()==textBox2.Text && kayitokuma["yetki"].ToString()=="Yönetici")
                        {
                            durum = true;
                            tcno = kayitokuma.GetValue(0).ToString();
                            adi = kayitokuma.GetValue(1).ToString();
                            soyadi = kayitokuma.GetValue(2).ToString();
                            yetki = kayitokuma.GetValue(3).ToString();
                            //kullaniciadi = kayitokuma.GetValue(4).ToString();
                            //parola = kayitokuma.GetValue(5).ToString();
                            this.Hide();
                            Form2 form2 = new Form2();
                            form2.Show();
                            break;
                        }
                    }

                    if (radioButton2.Checked == true)
                    {
                        if (kayitokuma["kullaniciadi"].ToString() == textBox1.Text && kayitokuma["parola"].ToString() == textBox2.Text && kayitokuma["yetki"].ToString() == "Kullanıcı")
                        {
                            durum = true;
                            tcno = kayitokuma.GetValue(0).ToString();
                            adi = kayitokuma.GetValue(1).ToString();
                            soyadi = kayitokuma.GetValue(2).ToString();
                            yetki = kayitokuma.GetValue(3).ToString();
                            //kullaniciadi = kayitokuma.GetValue(4).ToString();
                            //parola = kayitokuma.GetValue(5).ToString();
                            this.Hide();
                            Form3 form3 = new Form3();
                            form3.Show();
                            break;
                        }
                    }
                }
                if (durum == false)
                {
                    hak--;
                }
                baglanti.Close();
            }
            label5.Text = Convert.ToString(hak);
            if (hak==0)
            {
                button1.Enabled = false;
                MessageBox.Show("Giriş Hakkı Kalmadı","Personel Takip Programı",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Kullanıcı Girişi...";
            this.AcceptButton = button1;
            this.CancelButton = button2;
            label5.Text = Convert.ToString(hak);
            radioButton1.Checked = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;

        }
    }
}
