using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sinema_salonu
{
    public partial class Form1 : Form
    {
        ArrayList salons = new ArrayList();
        ArrayList forms = new ArrayList();
        

        public Form1()
        {
            InitializeComponent();
        }

        //Salonu oluşturur.
        private void button1_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(textBox1.Text)&& !String.IsNullOrEmpty(textBox2.Text) && !String.IsNullOrEmpty(textBox2.Text))
            {
                Salon salon = new Salon();
                salon.SalonOlustur(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text));
                salons.Add(salon);
                Form2 f = new Form2();
                f.DrawForm(salon, this);
                forms.Add(f);
            }

            //Bilet Satış bölmesinin ilk comboboxına salonları ekler
            comboBox1.Items.Clear();
            for (int i = 0; i < salons.Count; i++)
            {
                comboBox1.Items.Add($"SALON {((Salon)salons[i]).SalonNo}");
            }

            //Bilet İptal Bölmesinin ilk comboboxına salonları ekler
            comboBox4.Items.Clear();
            for (int i = 0; i < salons.Count; i++)
            {
                comboBox4.Items.Add($"SALON {((Salon)salons[i]).SalonNo}");
            }

            treeGuncelle(); //treView i günceller
            TotalBalanceHesapla(); // toplam balance ı günceller
            label11.Text = $"SALON {((Salon)salons[salons.Count-1]).SalonNo} Oluşturuldu."; //Aksiyon Logu
            

        }

        //treeViewi resetler, kurguladığım dizayna göre elemanlarını yerleştirir.
        private void treeGuncelle()
        {
            treeView1.Nodes.Clear();
            for (int i = 0; i < salons.Count; i++)
            {
                treeView1.Nodes.Add($"SALON {((Salon)salons[i]).SalonNo}");
                treeView1.Nodes[i].Nodes.Add($"Kapasite : {(((Salon)salons[i]).KoltukSayi* ((Salon)salons[i]).SiraSayi)}");
                treeView1.Nodes[i].Nodes.Add($"Balance : {((Salon)salons[i]).Balance}");
                treeView1.Nodes[i].Nodes[0].Nodes.Add($"Boş Koltuklar : {((Salon)salons[i]).BilgiAl()[0]} Tam Koltuklar : {((Salon)salons[i]).BilgiAl()[1]} İndirimli Koltuklar : {((Salon)salons[i]).BilgiAl()[2]}");
                for (int j = 0; j < ((Salon)salons[i]).SiraSayi; j++)
                {
                    Koltuk[] koltuklar = (Koltuk[])((ArrayList)((Salon)salons[i]).Koltuklar)[j];
                    treeView1.Nodes[i].Nodes[0].Nodes[0].Nodes.Add($"{j+1}. Sıra");

                    for (int k = 0; k < koltuklar.Length; k++)
                    {
                        if (koltuklar[k].Durum == 0)
                        {
                            treeView1.Nodes[i].Nodes[0].Nodes[0].Nodes[j].Nodes.Add($"{koltuklar[k].KoltukNo.ToString()} Boş");
                        }
                        if (koltuklar[k].Durum == 1)
                        {
                            treeView1.Nodes[i].Nodes[0].Nodes[0].Nodes[j].Nodes.Add($"{koltuklar[k].KoltukNo.ToString()} Tam");
                        }
                        if (koltuklar[k].Durum == 2)
                        {
                            treeView1.Nodes[i].Nodes[0].Nodes[0].Nodes[j].Nodes.Add($"{koltuklar[k].KoltukNo.ToString()} İndirimli");
                        }

                    }
                }
                
            }   
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            
        }
        //Bilet Satın alınacak salon seçildiğinde sıraları gösteren bir alttaki comboboxı doldurur.
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            for (int i = 0; i < ((Salon)salons[comboBox1.SelectedIndex]).SiraSayi; i++)
            {
                comboBox2.Items.Add($"{i+1}. Sıra");
            }
        }
        //Bilet satın alınacak salon ve sıra seçildiğinde o sıradaki boş koltukları comboboxın içine yerleştirir.
        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            Koltuk[] koltuklar = (Koltuk[])((ArrayList)((Salon)salons[comboBox1.SelectedIndex]).Koltuklar)[comboBox2.SelectedIndex];


            for (int i = 0; i < koltuklar.Length; i++)
            {
                if (koltuklar[i].Durum == 0)
                {
                    comboBox3.Items.Add($"{koltuklar[i].KoltukNo}. Koltuk");
                }
                
            }
        }

        //Bilet satın alma buttonu basıldığında çalışır.
        

        private void button2_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedItem!=null&& comboBox2.SelectedItem != null && comboBox3.SelectedItem != null) //error vermemesi için condition
            {
                Koltuk[] koltukDizi = ((Koltuk[])((ArrayList)((Salon)salons[comboBox1.SelectedIndex]).Koltuklar)[comboBox2.SelectedIndex]);//o sıradaki koltukların arrayini verir.
               
                if (checkBox1.Checked) //indirim seçiliyse
                {
                    for (int i = 0; i < koltukDizi.Length; i++)
                    {
                        if (Convert.ToInt32((comboBox3.Text).Substring(0, 1)) == koltukDizi[i].KoltukNo) //seçili koltuğu tespit eder
                        {
                            koltukDizi[i].Durum = 2; //indirimli satar
                            label11.Text = $"{comboBox1.SelectedItem.ToString()}, {comboBox2.SelectedItem.ToString()}, {comboBox3.SelectedItem.ToString()} İndirimli Satıldı. ( Balance +10)"; //log
                        }
                    }
                }
                else //tam satış aynı işlem
                {
                    for (int i = 0; i < koltukDizi.Length; i++)
                    {
                        if (Convert.ToInt32((comboBox3.Text).Substring(0,1)) == koltukDizi[i].KoltukNo)
                        {
                            koltukDizi[i].Durum = 1;
                            label11.Text = $"{comboBox1.SelectedItem.ToString()}, {comboBox2.SelectedItem.ToString()}, {comboBox3.SelectedItem.ToString()} Tam Satıldı. ( Balance +20)";
                        }
                    }
                }
                TotalBalanceHesapla(); //total balance hesaplar
            }

            //koltuk satın alındıktan sonra resetleme işlemini yapan kodlar
            comboBox3.Text = ""; 
            combo3Guncelle(); //satılan koltuğu comboboxtan çıkardı
            combo6Guncelle(); //satılan koltuğu iptal comboboxına ekledi
            treeGuncelle(); //treeviev güncelledi

            for(int i = 0; i<forms.Count;i++)
            {
                ((Form2)forms[i]).DrawForm((Salon)salons[i], this);
            }
            

        }

        //satın alma comboboxını günceller
        private void combo3Guncelle()
        {

            if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null)
            {
                comboBox3.Items.Clear();
                Koltuk[] koltuklar = (Koltuk[])((ArrayList)((Salon)salons[comboBox1.SelectedIndex]).Koltuklar)[comboBox2.SelectedIndex];


                for (int i = 0; i < koltuklar.Length; i++)
                {
                    if (koltuklar[i].Durum == 0)
                    {
                        comboBox3.Items.Add($"{koltuklar[i].KoltukNo}. Koltuk");
                    }

                }
            }
            
        }


        //iptal etme için combobox düzenlemesi
        private void comboBox4_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBox5.Items.Clear();
            for (int i = 0; i < ((Salon)salons[comboBox4.SelectedIndex]).SiraSayi; i++)
            {
                comboBox5.Items.Add($"{i + 1}. Sıra");
            }
        }
        //iptal etme için combobox düzenlemesi
        private void comboBox5_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBox6.Items.Clear();
            Koltuk[] koltuklar = (Koltuk[])((ArrayList)((Salon)salons[comboBox4.SelectedIndex]).Koltuklar)[comboBox5.SelectedIndex];


            for (int i = 0; i < koltuklar.Length; i++)
            {
                if (koltuklar[i].Durum == 1 || koltuklar[i].Durum == 2)
                {
                    comboBox6.Items.Add($"{koltuklar[i].KoltukNo}. Koltuk");
                }

            }
        }


        //iptal etme butonu
        private void button3_Click(object sender, EventArgs e)
        {

            if (comboBox4.SelectedItem != null && comboBox5.SelectedItem != null && comboBox6.SelectedItem != null) //hatayı engellemek için
            {
                Koltuk[] koltukDizi = ((Koltuk[])((ArrayList)((Salon)salons[comboBox4.SelectedIndex]).Koltuklar)[comboBox5.SelectedIndex]); //seçili koltuk arrayi
                for (int i = 0; i < koltukDizi.Length; i++)
                {
                    if(Convert.ToInt32((comboBox6.Text).Substring(0, 1)) == koltukDizi[i].KoltukNo) //seçili koltuğu arrayde bulur
                    {
                        if(koltukDizi[i].Durum == 1) //eğer tam biletse ona göre log basar
                        {
                            label11.Text = $"{comboBox4.SelectedItem.ToString()}, {comboBox5.SelectedItem.ToString()}, {comboBox6.SelectedItem.ToString()} Tam Koltuk İptal Edildi ( Balance -20)";
                        }
                        else //indirimli biletse ona göre log basar.
                        {
                            label11.Text = $"{comboBox4.SelectedItem.ToString()}, {comboBox5.SelectedItem.ToString()}, {comboBox6.SelectedItem.ToString()}  İndirimli Koltuk İptal Edildi ( Balance -10)";
                        }
                        koltukDizi[i].Durum = 0; //koltuğu boş yapar.
                        
                    }
                }

                TotalBalanceHesapla(); //total balance ı tekrar hesaplar.
            }
           //combobox ve treeview resetleme
            comboBox6.Text = "";
            combo6Guncelle();
            combo3Guncelle();
            treeGuncelle();
            for (int i = 0; i < forms.Count; i++)
            {
                ((Form2)forms[i]).DrawForm((Salon)salons[i], this);
            }

        }
        private void combo6Guncelle()
        {

            if(comboBox4.SelectedItem!=null&& comboBox5.SelectedItem != null)
            {
                comboBox6.Items.Clear();
                Koltuk[] koltuklar = (Koltuk[])((ArrayList)((Salon)salons[comboBox4.SelectedIndex]).Koltuklar)[comboBox5.SelectedIndex];


                for (int i = 0; i < koltuklar.Length; i++)
                {
                    if (koltuklar[i].Durum == 1 || koltuklar[i].Durum == 2)
                    {
                        comboBox6.Items.Add($"{koltuklar[i].KoltukNo}. Koltuk");
                    }

                }
            }
            

        }


        private void TotalBalanceHesapla()
        {
            int balance = 0;
            for (int i = 0; i < salons.Count; i++)
            {
                balance += ((Salon)salons[i]).Balance;
            }
            label10.Text = $"Toplam Balance : {balance} TL";

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            ((Form2)forms[comboBox1.SelectedIndex]).Show();

            
        }

        public void Refresher()
        {
            TotalBalanceHesapla();
            combo3Guncelle();
            combo6Guncelle();
            treeGuncelle();
            comboBox3.Text = "";
            comboBox6.Text = "";
        }
       
    }

}
