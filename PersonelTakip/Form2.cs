using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.IO; 

namespace PersonelTakip
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-5L1LUNIR;Initial Catalog=personel;Integrated Security=True");

        private void kullanicilari_goster()
        {
            //MessageBox.Show("kullanıcıları göster fonksiyonu çalıştı");
            try
            {
                baglanti.Open();
                SqlDataAdapter kullanicilari_listele = new SqlDataAdapter
                ("select tcno AS[TC KİMLİK NO],ad AS[ADI],soyad AS[SOYADI],yetki AS[YETKİ],kullaniciadi AS[KULLANICI ADI],parola AS[PAROLA] from kullanicilar Order By ad ASC", baglanti);
                DataSet dshafiza = new DataSet();
                kullanicilari_listele.Fill(dshafiza);
                dataGridView1.DataSource = dshafiza.Tables[0];
                baglanti.Close();
            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();

            }
        }

        private void personelleri_goster()
        {
      
            try
            {
                baglanti.Open();
                SqlDataAdapter personelleri_listele = new SqlDataAdapter
                ("select tcno AS[TC KİMLİK NO],ad AS[ADI],soyad AS[SOYADI],cinsiyet AS[CİNSİYETİ],mezuniyet AS[MEZUNİYETİ],dogumtarih AS[DOĞUM TARİHİ],gorevi AS[GÖREVİ],gorevyeri AS[GÖREV YERİ],maasi AS[MAAŞI] from personeller Order By ad ASC", baglanti);
                DataSet dshafiza = new DataSet();
                personelleri_listele.Fill(dshafiza);
                dataGridView2.DataSource = dshafiza.Tables[0];
                baglanti.Close();
            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();

            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            pictureBox1.Height = 150;
            pictureBox1.Width = 150;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            try
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath+"\\kullaniciresimler\\"+Form1.tcno+".jpg");
            }
            catch 
            {

                pictureBox1.Image = Image.FromFile(Application.StartupPath+"\\kullaniciresimler\\resimyok.jpg");

            }
            //kullanıcı işlemleri ayarları
            this.Text = "YÖNETİCİ İŞLEMLERİ";
            lbl11.ForeColor = Color.DarkRed;
            lbl11.Text = Form1.adi + " " + Form1.soyadi;
            txt1.MaxLength = 11;
            txt4.MaxLength = 8;
            radioButton1.Checked = true;

            txt2.CharacterCasing = CharacterCasing.Upper;
            txt3.CharacterCasing = CharacterCasing.Upper;
            txt5.MaxLength = 10;
            txt6.MaxLength = 10;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;
            kullanicilari_goster();
            toolTip1.SetToolTip(this.txt1, "TC Kimlik no 11 karakter olmalı");
            radioButton1.Checked = true;
            txt2.CharacterCasing = CharacterCasing.Upper;
            txt3.CharacterCasing = CharacterCasing.Upper;
            txt5.MaxLength = 10;
            txt6.MaxLength = 10;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;
            //personel işlemleri sekmesi
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Width = 100;pictureBox2.Height = 100;
            pictureBox2.BorderStyle = BorderStyle.Fixed3D;
            DateTime zaman = DateTime.Now;
            int yil = int.Parse(zaman.ToString("yyyy"));
            int ay = int.Parse(zaman.ToString("MM"));
            int gun = int.Parse(zaman.ToString("dd"));

            dateTimePicker1.MinDate = new DateTime(1968, 1, 1);
            dateTimePicker1.MaxDate = new DateTime(yil - 18, ay, gun);
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            /*snrdekld*/personelleri_goster();
            radioButton3.Checked = true;
        }

 

        private void txt1_TextChanged(object sender, EventArgs e)
        {
            if (txt1.Text.Length<11)
            {
                errorProvider1.SetError(txt1, "TC Kimlik no 11 karakter olmalı!");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void txt1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar>=48 && (int)e.KeyChar<=57) || (int)e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txt2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true) 
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txt3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txt4_TextChanged(object sender, EventArgs e)
        {
            if (txt4.Text.Length != 8)
            {
                errorProvider1.SetError(txt4, "Kullanıcı adı 8 karakter olmalı!");

            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void txt4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar)==true||char.IsDigit(e.KeyChar)==true)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        int parola_skoru = 0;
        private void txt5_TextChanged(object sender, EventArgs e)
        {
            string parola_seviyesi = "";
            int kucuk_harf_skoru = 0, buyuk_harf_skoru = 0, rakam_skoru = 0, sembol_skoru = 0;
            string sifre = txt5.Text;
            string duzeltilmis_sifre = "";
            duzeltilmis_sifre = sifre;
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('İ','I');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ı', 'i');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ç', 'C');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ç', 'c');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ş', 'S');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ş', 's');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ğ', 'G');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ğ', 'g');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ü', 'U');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ü', 'u');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ö', 'O');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ö', 'o');
            if (sifre != duzeltilmis_sifre)
            {
                sifre = duzeltilmis_sifre;
                txt5.Text = sifre;
                MessageBox.Show("Paroladaki türkçe karakterler ingilizce karakterlere dönüştürülmüştür.","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);

            }
            int az_karakter_sayisi = sifre.Length-Regex.Replace(sifre,"[a-z]","").Length;
            kucuk_harf_skoru = Math.Min(2,az_karakter_sayisi)*10;

            int AZ_karakter_sayisi = sifre.Length - Regex.Replace(sifre, "[A-Z]", "").Length;
            buyuk_harf_skoru = Math.Min(2, AZ_karakter_sayisi) * 10;

            int rakam_sayisi = sifre.Length - Regex.Replace(sifre, "[a-z]", "").Length;
            rakam_skoru = Math.Min(2, rakam_sayisi) * 10;

            int sembol_sayisi = sifre.Length - az_karakter_sayisi-AZ_karakter_sayisi-rakam_sayisi;
            sembol_skoru = Math.Min(2,sembol_sayisi)*10;

            parola_skoru = kucuk_harf_skoru + buyuk_harf_skoru + rakam_skoru + sembol_skoru;

            if (sifre.Length==9)
            {
                parola_skoru++;
            }
            else if (sifre.Length==10)
            {
                parola_skoru += 20;
            }
            if (kucuk_harf_skoru==0 ||  buyuk_harf_skoru==0||rakam_skoru==0||sembol_skoru==0)
            {
                lbl22.Text = "Büyük harf, küçük harf, rakam ve sembol mutlaka kullanmalısın!";

            }
            if (kucuk_harf_skoru!=0&&buyuk_harf_skoru!=0&&rakam_skoru!=0&&sembol_skoru!=0)
            {
                lbl22.Text = "";
            }
            if (parola_skoru<70)
            {
                parola_seviyesi = "Kabul edilemez";
            }
            else if (parola_skoru==70||parola_skoru==80)
            {
                parola_seviyesi = "Güçlü";
            }
            else if (parola_skoru==90||parola_skoru==100)
            {
                parola_seviyesi = "Çok güçlü";
            }

            lbl9.Text = "%"+Convert.ToString(parola_skoru);
            lbl10.Text = parola_seviyesi;
         progressBar1.Value = parola_skoru;
        }

        private void txt6_TextChanged(object sender, EventArgs e)
        {
            if (txt6.Text!=txt5.Text)
            {
                errorProvider1.SetError(txt6, "Parola tekrarı eşleşmiyor!");

            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void txt5_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void topPage1_temizle()
        {
            txt1.Clear();
            txt2.Clear();
            txt3.Clear();
            txt4.Clear();
            txt5.Clear();
            txt6.Clear();
        }
        private void topPage2_temizle()
        {
            pictureBox2.Image = null;
            txtTC.Clear();
            txtAd.Clear();
            txtSoyad.Clear();
            txtMaas.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            string yetki = "";
            Boolean kayitkontrol = false;

            baglanti.Open();
            SqlCommand selectsorgu = new SqlCommand("select * from kullanicilar where tcno='"+txt1.Text+"'",baglanti);
            SqlDataReader kayitokuma = selectsorgu.ExecuteReader();
            while (kayitokuma.Read())
            {
                kayitkontrol = true;
                break;
                
            }
            baglanti.Close();

            if (kayitkontrol==false)
            {
                //TC kimlik No kontrolü.
                if (txt1.Text.Length<11||txt1.Text=="")
                
                    lbl1.ForeColor= Color.Red;
                
                else
                
                    lbl1.ForeColor = Color.Black;
                //Adı veri kontrolü
                if (txt2.Text.Length < 2 || txt2.Text == "")

                    lbl2.ForeColor = Color.Red;

                else

                    lbl2.ForeColor = Color.Black;
                //Soyadı veri kontrolü
                if (txt3.Text.Length < 2 || txt3.Text == "")

                    lbl3.ForeColor = Color.Red;

                else

                    lbl3.ForeColor = Color.Black;
                //Kullanıcı Adı veri kontrolü
                if (txt4.Text.Length != 8 || txt4.Text == "")

                    lbl5.ForeColor = Color.Red;

                else

                    lbl5.ForeColor = Color.Black;
                //Parola veri kontrolü
                if (parola_skoru < 70 || txt5.Text == "")

                    lbl6.ForeColor = Color.Red;

                else

                    lbl6.ForeColor = Color.Black;
                //Parola tekrar veri kontrolü
                if (txt5.Text!=txt6.Text || txt6.Text == "")

                    lbl7.ForeColor = Color.Red;

                else

                    lbl7.ForeColor = Color.Black;

                if(txt1.Text.Length==11 && txt1.Text != "" && txt2.Text != "" && txt2.Text.Length>1 && txt3.Text != "" && txt3.Text.Length>1 && txt4.Text != "" && txt5.Text != "" && txt6.Text != "" && txt5.Text==txt6.Text && parola_skoru >= 70)
                {
                    if (radioButton1.Checked == true)
                    {
                        yetki = "Yönetici";
                    }
                    else if (radioButton2.Checked == true)
                    {
                        yetki = "Kullanıcı";
                    }
                    try
                    {
                        baglanti.Open();
                        SqlCommand eklekomutu = new SqlCommand("insert into kullanicilar values ('"+txt1.Text+"','"+txt2.Text+"','"+txt3.Text+"','"+yetki+ "','"+txt4.Text+"','"+txt5.Text+"','"+txt6.Text+"')",baglanti);
                    //System.Data.SqlClient.SqlException: 'Column name or number of supplied values does not match table definition.'
                    eklekomutu.ExecuteNonQuery();
                        baglanti.Close();
                        MessageBox.Show("Yeni kullanıcı kaydı oluşturuldu!","Personel Takip Programı",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                        topPage1_temizle();
                    }
                    catch (Exception hatamsj)
                    {
                        MessageBox.Show(hatamsj.Message);
                        baglanti.Close();

                    }
                }
                else
                {
                    MessageBox.Show("Yazı rengi kırmızı olan alanları yeniden gözden geçiriniz","Personel Takip Programı",MessageBoxButtons.OK,MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("Girilen TC Kimlik numarası daha önceden kayıtlıdır!","Personel Takip Programı",MessageBoxButtons.OK,MessageBoxIcon.Error);

            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            Boolean kayit_arama_durumu = false;
            if (txt1.Text.Length==11)
            {
                baglanti.Open();
                SqlCommand selectsoru = new SqlCommand("select * from kullanicilar where tcno='"+txt1.Text+"'",baglanti);
                SqlDataReader kayitokuma = selectsoru.ExecuteReader();
                while (kayitokuma.Read())
                {
                    kayit_arama_durumu = true;
                    txt2.Text = kayitokuma.GetValue(1).ToString();
                    txt3.Text = kayitokuma.GetValue(2).ToString();
                    if (kayitokuma.GetValue(3).ToString()=="Yönetici")
                    {
                        radioButton1.Checked = true;
                    }
                    else
                    {
                        radioButton2.Checked = true;
                    }
                    txt4.Text = kayitokuma.GetValue(4).ToString();
                    txt5.Text = kayitokuma.GetValue(5).ToString();
                    txt6.Text = kayitokuma.GetValue(5).ToString();
                    break;
                }
                if (kayit_arama_durumu==false)
                {
                    MessageBox.Show("Aranan kayıt bulunamadı!","Personel Takip Programı",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }
                baglanti.Close();
            }
            else
            {
                MessageBox.Show("Lütfen 11 haneli bir TC Kimlik No giriniz!", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                topPage1_temizle();
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            string yetki = "";
 
            
                //TC kimlik No kontrolü.
                if (txt1.Text.Length < 11 || txt1.Text == "")

                    lbl1.ForeColor = Color.Red;

                else

                    lbl1.ForeColor = Color.Black;
                //Adı veri kontrolü
                if (txt2.Text.Length < 2 || txt2.Text == "")

                    lbl2.ForeColor = Color.Red;

                else

                    lbl2.ForeColor = Color.Black;
                //Soyadı veri kontrolü
                if (txt3.Text.Length < 2 || txt3.Text == "")

                    lbl3.ForeColor = Color.Red;

                else

                    lbl3.ForeColor = Color.Black;
                //Kullanıcı Adı veri kontrolü
                if (txt4.Text.Length != 8 || txt4.Text == "")

                    lbl5.ForeColor = Color.Red;

                else

                    lbl5.ForeColor = Color.Black;
                //Parola veri kontrolü
                if (parola_skoru < 70 || txt5.Text == "")

                    lbl6.ForeColor = Color.Red;

                else

                    lbl6.ForeColor = Color.Black;
                //Parola tekrar veri kontrolü
                if (txt5.Text != txt6.Text || txt6.Text == "")

                    lbl7.ForeColor = Color.Red;

                else

                    lbl7.ForeColor = Color.Black;

                if (txt1.Text.Length == 11 && txt1.Text != "" && txt2.Text != "" && txt2.Text.Length > 1 && txt3.Text != "" && txt3.Text.Length > 1 && txt4.Text != "" && txt5.Text != "" && txt6.Text != "" && txt5.Text == txt6.Text && parola_skoru >= 70)
                {
                    if (radioButton1.Checked == true)
                    {
                        yetki = "Yönetici";
                    }
                    else if (radioButton2.Checked == true)
                    {
                        yetki = "Kullanıcı";
                    }
                    try
                    {
                        baglanti.Open();
                        SqlCommand guncellekomutu=new SqlCommand("update kullanicilar set ad='"
                        +txt2.Text+"',soyad='"+txt3.Text+"',yetki='"+yetki+
                        "',kullaniciadi='"+txt4.Text+"',parola='"+txt5.Text+"'where tcno='"+txt1.Text+"'",
                        baglanti);
                                                                      
                        guncellekomutu.ExecuteNonQuery();
                        baglanti.Close();
                        MessageBox.Show("Kullanıcı bilgileri güncellendi!", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        kullanicilari_goster();
                    }
                    catch (Exception hatamsj)
                    {
                        MessageBox.Show(hatamsj.Message);
                        baglanti.Close();

                    }
                }
                else
                {
                    MessageBox.Show("Yazı rengi kırmızı olan alanları yeniden gözden geçiriniz", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            
        
        }

        private void btndel_Click(object sender, EventArgs e)
        {
            if (txt1.Text.Length == 11)
            {
                bool kayit_arama_durumu = false;
                baglanti.Open();
                SqlCommand selectsorgu = new SqlCommand("select * from kullanicilar where tcno='"+txt1.Text+"'",baglanti);
                SqlDataReader kayitokuma = selectsorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    kayit_arama_durumu = true;
                    SqlCommand deletesorgu = new SqlCommand("delete from kullanicilar where tcno=¹" + txt1.Text + "", baglanti);
                    //System.InvalidOperationException: 'Bu Command ile ilişkili, öncelikle kapatılması gereken açık bir DataReader zaten var.'
                    deletesorgu.ExecuteNonQuery();
                    MessageBox.Show("Kullanıcı kaydı silindi!","Personel Takip Programı",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    baglanti.Close();
                    kullanicilari_goster();
                    topPage1_temizle();
                    break;
                }
                if (kayit_arama_durumu==false)
                {
                    MessageBox.Show("Silinecek kayıt bulunamadı!", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglanti.Close();
                    topPage1_temizle();
                }
            }
            else
            {
                MessageBox.Show("Lütfen 11 karakterden oluşan bir TC Kimlik NO giriniz!", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void btnfrmclean_Click(object sender, EventArgs e)
        {
            topPage1_temizle();
        }

        private void btnGozat_Click(object sender, EventArgs e)
        {
            OpenFileDialog resimsec = new OpenFileDialog();
            resimsec.Title = "Personel resmi seçiniz.";
            resimsec.Filter = "JPG Dosyalar(*.jpg)|*.jpg";
            if (resimsec.ShowDialog() == DialogResult.OK)
            {
                this.pictureBox2.Image = new Bitmap(resimsec.OpenFile());
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string cinsiyet = "";
            Boolean kayitkontrol = false;

            baglanti.Open();
            SqlCommand selectsorgu = new SqlCommand("select*from personeller where tcno='"+txtTC.Text+"'",baglanti);
            SqlDataReader kayitokuma = selectsorgu.ExecuteReader();
            while (kayitokuma.Read())
            {
                kayitkontrol = true;
                break;
            }
            baglanti.Close();
            if (kayitkontrol==false)
            {
               
                if (pictureBox2.Image == null)
                    btnGozat.ForeColor = Color.Red;
                else
                    btnGozat.ForeColor = Color.Black;
                if (comboBox1.Text == "")
                    lblmezuniyet.ForeColor = Color.Red;
                else
                    label17.ForeColor = Color.Black;
                if (comboBox2.Text == "")
                    lblgorev.ForeColor = Color.Red;
                else
                    lblgorev.ForeColor = Color.Black;
                if (comboBox3.Text == "")
                    lblgorevyer.ForeColor = Color.Red;
                else
                    lblgorevyer.ForeColor = Color.Black;
                if (int.Parse(txtMaas.Text) < 1000)
                    lblmaas.ForeColor = Color.Red;
                else
                    lblmaas.ForeColor = Color.Black;

                if (pictureBox2.Image != null && comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "")
                {
                        if (radioButton3.Checked == true)
                            cinsiyet = "Bay";
                        else if (radioButton4.Checked == true)
                            cinsiyet = "Bayan";
                    try
                    {
                        baglanti.Open();
                            SqlCommand eklekomutu = new SqlCommand("insert into personeller values('"+txtTC.Text+ "','"+txtAd.Text+ "','"+txtSoyad.Text+ "','"+cinsiyet+ "','"+comboBox1.Text+ "','"+dateTimePicker1.Text+"','"+comboBox2.Text+"','"+comboBox3.Text+"','"+txtMaas.Text+"') ",baglanti);
                        //gdi+ içinde genel bir hata oluştu.
                        eklekomutu.ExecuteNonQuery();
                            baglanti.Close();
                            if ( !Directory.Exists(Application.StartupPath + "\\personelresimler") )
                            {
                                Directory.CreateDirectory(Application.StartupPath+"\\personelresimler");

                            }
                            
                            
                            pictureBox2.Image.Save(Application.StartupPath+ "\\ personelresimler \\"+txtTC.Text+".jpg");
                            
                            MessageBox.Show("Yeni personel kaydı oluşturuldu","Personel Takip Programı",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                            personelleri_goster();
                            topPage2_temizle();
                            txtMaas.Text = "0";
                    }
                    catch (Exception hatamsj)
                    {
                        MessageBox.Show(hatamsj.Message, "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        baglanti.Close();


                    }
                }
                    else
                    {
                        MessageBox.Show("Yazı rengi kırmızı olan alanları yeniden gözden geçiriniz", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                
            }
            else
            {
                MessageBox.Show("Girilen TC kimlik numarası daha önceden kayıtlıdır", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            Boolean kayit_arama_durumu = false;
            if (txtTC.Text.Length == 11)
            {
                baglanti.Open();
                SqlCommand selectsorgu = new SqlCommand("select * from personeller where tcno='"+txtTC.Text+"'",baglanti);
                SqlDataReader kayitokuma = selectsorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    kayit_arama_durumu = true;
                    try
                    {
                        pictureBox2.Image = Image.FromFile(Application.StartupPath+"\\personelresimler\\"+kayitokuma.GetValue(0).ToString()+".jpg");
                    }
                    catch (Exception)
                    {
                        pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\personelresimler\\resimyok.jpg");


                    }
                    txtAd.Text = kayitokuma.GetValue(1).ToString();
                    txtSoyad.Text = kayitokuma.GetValue(2).ToString();
                    if (kayitokuma.GetValue(3)=="Bay")
                    {
                        radioButton3.Checked = true;
                    }
                    else
                    {
                        radioButton4.Checked = true;
                    }
                    comboBox1.Text = kayitokuma.GetValue(4).ToString();
                    dateTimePicker1.Text = kayitokuma.GetValue(5).ToString();
                    comboBox2.Text = kayitokuma.GetValue(6).ToString();
                    comboBox3.Text = kayitokuma.GetValue(7).ToString();
                    txtMaas.Text = kayitokuma.GetValue(8).ToString();
                    break;

                }
                if (kayit_arama_durumu==false)
                {
                    MessageBox.Show("Aranan kayıt bulunamadı!","Personel Takip Programı",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }
                baglanti.Close();
            }
            else
            {
                MessageBox.Show("11 haneli TC no giriniz!!", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            string cinsiyet = "";
            if (pictureBox2.Image == null)
                btnGozat.ForeColor = Color.Red;
            else
                btnGozat.ForeColor = Color.Black;
            if (comboBox1.Text == "")
                lblmezuniyet.ForeColor = Color.Red;
            else
                label17.ForeColor = Color.Black;
            if (comboBox2.Text == "")
                lblgorev.ForeColor = Color.Red;
            else
                lblgorev.ForeColor = Color.Black;
            if (comboBox3.Text == "")
                lblgorevyer.ForeColor = Color.Red;
            else
                lblgorevyer.ForeColor = Color.Black;
            if (int.Parse(txtMaas.Text) < 1000)
                lblmaas.ForeColor = Color.Red;
            else
                lblmaas.ForeColor = Color.Black;

            if (pictureBox2.Image != null && comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "")
            {
                if (radioButton3.Checked == true)
                    cinsiyet = "Bay";
                else if (radioButton4.Checked == true)
                    cinsiyet = "Bayan";
                try
                {
                    baglanti.Open();
                    SqlCommand guncellekomutu = new SqlCommand("update personeller set ad='" + txtAd.Text + "',soyad='" + txtSoyad.Text + "',cinsiyet='" + cinsiyet + "',mezuniyet='" + comboBox1.Text + "',dogumtarihi='" + dateTimePicker1.Text + "',gorevi='" + comboBox2.Text + "',gorevyeri='" + comboBox3.Text + "',maasi='" + txtMaas.Text + "'where tcno='"+txtTC.Text+"' ", baglanti);
                    guncellekomutu.ExecuteNonQuery();
                    baglanti.Close();
                    personelleri_goster();
                    topPage2_temizle();
                    txtMaas.Text = "0";
                    if (!Directory.Exists(Application.StartupPath + "\\personelresimler"))
                    {
                        Directory.CreateDirectory(Application.StartupPath + "\\personelresimler");

                    }
                    else
                    {
                        pictureBox2.Image.Save(Application.StartupPath + "\\personelresimler\\" + txtTC.Text + ".jpg");
                    }
                    MessageBox.Show("Yeni personel kaydı oluşturuldu", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    personelleri_goster();
                    topPage2_temizle();

                }
                catch (Exception hatamsj)
                {
                    MessageBox.Show(hatamsj.Message, "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglanti.Close();


                }
            }
            
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            Boolean kayit_arama_durumu = false;
            baglanti.Open();
            SqlCommand arama_sorgusu = new SqlCommand("select*from personeller where tcno='"+txtTC.Text+"'",baglanti);
            SqlDataReader kayitokuma = arama_sorgusu.ExecuteReader();
            while (kayitokuma.Read())
            {
                kayit_arama_durumu = true;
                SqlCommand deletesorgu = new SqlCommand("delete from personeller where tcno='"+txtTC.Text+"'",baglanti);
                //System.InvalidOperationException: 'Bu Command ile ilişkili, öncelikle kapatılması gereken açık bir DataReader zaten var.'
                deletesorgu.ExecuteNonQuery();
                break;

            }
            if (kayit_arama_durumu==false)
            {
                MessageBox.Show("Silinecek Kayıt Bulunamadı","Personel Takip Programı",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            baglanti.Close();
            personelleri_goster();
            topPage2_temizle();
            txtMaas.Text = "0";

        }
    }
}
