using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sinema_salonu
{
    public partial class Form2 : Form
    {
        Salon salon = new Salon();
        
        ArrayList boxes = new ArrayList();
        Form1 f;
        public Form2()

        {
           
            InitializeComponent();

          

        }

        public void DrawForm(Salon s, Form1 f1)
        {
            
            
            this.Controls.Clear();
            

            this.InitializeComponent();
            this.Text = $"SALON {s.SalonNo}";
            f = f1;
            //this.MaximumSize = new Size(((s.KoltukSayi*50) + 50),((s.SiraSayi * 50) + 800) );
            salon = s;
            int ofset = 0;

            for (int k = 0; k < s.SiraSayi; k++)
            {
                Koltuk[] koltuks = ((Koltuk[])s.Koltuklar[k]);
                for (int i = 0; i < koltuks.Length; i++)
                {
                    CheckBox box;
                    box = new CheckBox();
                    box.Tag = i.ToString();
                    box.Text = $"{koltuks[i].KoltukSiraNo} / {koltuks[i].KoltukNo}";
                    box.AutoSize = true;

                    box.Location = new Point(i * 50, 10 + ofset);
                    if (koltuks[i].Durum == 0)
                    {
                        box.BackColor = System.Drawing.Color.Green;
                    }

                    if (koltuks[i].Durum == 1)
                    {
                        box.BackColor = System.Drawing.Color.Red;
                    }

                    if (koltuks[i].Durum == 2)
                    {
                        box.BackColor = System.Drawing.Color.Yellow;
                    }
                    boxes.Add(box);
                    this.Controls.Add(box);
                }
                ofset += 50;

            }

            PictureBox p = new PictureBox();
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "im.jpg");

            pictureBox1.Image = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "im.jpg"));

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Location = new Point(10, ((s.SiraSayi) * 50));
            pictureBox1.Size = new Size(s.KoltukSayi * 50, 150);


            checkBox1.Location = new Point(10, ((s.SiraSayi) * 50) + 200);
            checkBox1.Tag = "İndirimli";
            checkBox1.Text = "İndirimli";
            checkBox1.AutoSize = true;


            button1.Location = new Point(110, ((s.SiraSayi) * 50) + 200);
            button1.Text = "Bilet Sat";
            button1.AutoSize = true;

            button2.Location = new Point(210, ((s.SiraSayi) * 50) + 200);
            button2.Text = "Bilet Sil";
            button2.AutoSize = true;

            

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                for (int i = 0; i < boxes.Count; i++)
                {
                    if (((CheckBox)boxes[i]).Checked)
                    {
                        for (int j = 0; j < salon.SiraSayi; j++)
                        {
                            for (int k = 0; k < salon.KoltukSayi; k++)
                            {
                                var s = ((CheckBox)boxes[i]).Text.Split('/');

                                for(int x = 0; x <s.Length;x++)
                                {
                                    s[x] = s[x].Trim();
                                }
                                if (((Koltuk[])salon.Koltuklar[j])[k].KoltukSiraNo.ToString() == s[0])
                                {
                                    if (((Koltuk[])salon.Koltuklar[j])[k].KoltukNo.ToString() == s[1])
                                    {
                                        if (((Koltuk[])salon.Koltuklar[j])[k].Durum == 0)
                                        {
                                            f.label11.Text = $"{salon.SalonNo}. SALON, {((Koltuk[])salon.Koltuklar[j])[k].KoltukSiraNo}. Sıra, {((Koltuk[])salon.Koltuklar[j])[k].KoltukNo}. Koltuk İndirimli Satıldı. ( Balance +10)";
                                            ((Koltuk[])salon.Koltuklar[j])[k].Durum = 2;
                                            ((CheckBox)boxes[i]).BackColor = System.Drawing.Color.Yellow;

                                        }
                                        else
                                            MessageBox.Show("Dolu");
                                    }
                                }
                            }
                        }
                    }

                }
            }
            else
            {
                for (int i = 0; i < boxes.Count; i++)
                {
                    if (((CheckBox)boxes[i]).Checked)
                    {
                        for (int j = 0; j < salon.SiraSayi; j++)
                        {
                            for (int k = 0; k < salon.KoltukSayi; k++)
                            {
                                var s = ((CheckBox)boxes[i]).Text.Split('/');

                                for (int x = 0; x < s.Length; x++)
                                {
                                    s[x] = s[x].Trim();
                                }
                                if (((Koltuk[])salon.Koltuklar[j])[k].KoltukSiraNo.ToString() == s[0])
                                {
                                    if (((Koltuk[])salon.Koltuklar[j])[k].KoltukNo.ToString() == s[1])
                                    {
                                        if (((Koltuk[])salon.Koltuklar[j])[k].Durum == 0)
                                        {
                                            ((Koltuk[])salon.Koltuklar[j])[k].Durum = 1;
                                            f.label11.Text = $"{salon.SalonNo}. SALON, {((Koltuk[])salon.Koltuklar[j])[k].KoltukSiraNo}. Sıra, {((Koltuk[])salon.Koltuklar[j])[k].KoltukNo}. Koltuk Tam Satıldı. ( Balance +20)";
                                            ((CheckBox)boxes[i]).BackColor = System.Drawing.Color.Red;
                                        }
                                        else
                                            MessageBox.Show("Dolu");
                                            
                                    }
                                }
                            }
                        }
                    }

                }
            }
            f.Refresher();
            checkBoxClear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < boxes.Count; i++)
            {
                if (((CheckBox)boxes[i]).Checked)
                {
                    for (int j = 0; j < salon.SiraSayi; j++)
                    {
                        for (int k = 0; k < salon.KoltukSayi; k++)
                        {
                            var s = ((CheckBox)boxes[i]).Text.Split('/');

                            for (int x = 0; x < s.Length; x++)
                            {
                                s[x] = s[x].Trim();
                            }
                            if (((Koltuk[])salon.Koltuklar[j])[k].KoltukSiraNo.ToString() == s[0])
                            {
                                if (((Koltuk[])salon.Koltuklar[j])[k].KoltukNo.ToString() == s[1])
                                {
                                    if (((Koltuk[])salon.Koltuklar[j])[k].Durum != 0)
                                    {

                                        if (((Koltuk[])salon.Koltuklar[j])[k].Durum == 1)
                                        {
                                            f.label11.Text = $"{salon.SalonNo}. SALON, {((Koltuk[])salon.Koltuklar[j])[k].KoltukSiraNo}. Sıra, {((Koltuk[])salon.Koltuklar[j])[k].KoltukNo}. Koltuk Tam İptal Edildi. ( Balance -20)";
                                        }
                                        else
                                        {
                                            f.label11.Text = $"{salon.SalonNo}. SALON, {((Koltuk[])salon.Koltuklar[j])[k].KoltukSiraNo}. Sıra, {((Koltuk[])salon.Koltuklar[j])[k].KoltukNo}. Koltuk İndirimli İptal Edildi. ( Balance -10)";
                                        }

                                        ((Koltuk[])salon.Koltuklar[j])[k].Durum = 0;
                                        ((CheckBox)boxes[i]).BackColor = System.Drawing.Color.Green;
                                       
                                    }
                                    else
                                        MessageBox.Show("Boş koltuk iptal edilemez.");
                                }
                            }
                        }
                    }
                }

            }
            f.Refresher();
            checkBoxClear();
        }
        
        private void checkBoxClear()
        {
            foreach(var item in boxes)
            {
                ((CheckBox)item).Checked = false;
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {

            e.Cancel = true;
            Hide();    
                
            
        }
    }
}
