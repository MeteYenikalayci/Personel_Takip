using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PersonelTakip
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        SqlConnection baglantim = new SqlConnection("Data Source=LAPTOP-5L1LUNIR;Initial Catalog=personel;Integrated Security=True");

        private void personelleri_goster()
        {
            try
            {
                baglantim.Open();
                SqlDataAdapter personelleri_listele = new SqlDataAdapter("select tcno AS[TC KİMLİK NO], ad AS[ADI],soyad AS[SOYADI], cinsiyet as[CİNSİYETİ], mezuniyet as[MEZUNİYETİ], dogumtarihi as[DOĞUM TARİHİ],gorevi as[GÖREVİ],gorevyeri as[GÖREV YERİ],maasi as[MAAŞI] from personeller Order By ad ASC",baglantim);
                DataSet dshafizza = new DataSet();
                personelleri_listele.Fill(dshafizza);
                dataGridView1.DataSource = dshafizza.Tables[0];
                baglantim.Close();

            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message,"Personel Takip Programı",MessageBoxButtons.OK,MessageBoxIcon.Error);
                baglantim.Close();
            }
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            personelleri_goster();
            this.Text = "KULLANICI İŞLEMLERİ";
            lblaktfklnc.Text = Form1.adi + " " + Form1.soyadi;
            pictureBox1.Height = 150;pictureBox1.Width = 150;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            pictureBox2.Height = 150; pictureBox2.Width = 150;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.BorderStyle = BorderStyle.Fixed3D;
            try
            {
                pictureBox2.Image = Image.FromFile(Application.StartupPath+"\\kullaniciresimler\\"+Form1.tcno+".jpg");

            }
            catch (Exception)
            {
                pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\resimyok.jpg");


            }
            maskedTextBox1.Mask = "00000000000";
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            bool kayit_arama_durumu = false;
            if(maskedTextBox1.Text.Length == 11)
            {
                baglantim.Open();
                SqlCommand selectsorgu = new SqlCommand("select*from personeller where tcno='"+maskedTextBox1.Text+"'",baglantim);
                SqlDataReader kayitokuma = selectsorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    kayit_arama_durumu = true;
                    try
                    {
                        pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\personelresimler\\" + kayitokuma.GetValue(0) + ".jpg");
                    }
                    catch
                    {
                        pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\personelresimler\\resimyok.jpg");
                        lbl10.Text = kayitokuma.GetValue(1).ToString();
                        lbl11.Text = kayitokuma.GetValue(2).ToString();
                        if (kayitokuma.GetValue(3).ToString()=="Bay")
                        {
                            lbl12.Text = "Bay";
                        }
                        else
                        {
                            lbl12.Text = "Bayan";

                        }
                        lbl13.Text = kayitokuma.GetValue(4).ToString();
                        lbl14.Text = kayitokuma.GetValue(5).ToString();
                        lbl15.Text = kayitokuma.GetValue(6).ToString();
                        lbl16.Text = kayitokuma.GetValue(7).ToString();
                        lbl17.Text = kayitokuma.GetValue(8).ToString();
                        break;
                    }
                    if (kayit_arama_durumu==false)
                    {
                        MessageBox.Show("Aranan kayıt bulunamadı!");
                    }
                    baglantim.Close();
                }

            }
            else
            {
                MessageBox.Show("11 Haneli bir tc kimlik no giriniz!");
            }
        }
    }
}
