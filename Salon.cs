using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sinema_salonu
{
    public class Salon
    {
        private int salonNo;

        public int SalonNo
        {
            get { return salonNo; }
            set { salonNo = value; }
        }

        private int koltukSayi;

        public int KoltukSayi
        {
            get { return koltukSayi; }
            set { koltukSayi = value; }
        }

        private int siraSayi;

        public int SiraSayi
        {
            get { return siraSayi; }
            set { siraSayi = value; }
        }


        private int bosKoltuk;

        public int BosKoltuk
        {
            get { return bosKoltuk; }
            set { bosKoltuk = value; }
        }

        private int doluKoltuk;

        public int DoluKoltuk
        {
            get { return doluKoltuk; }
            set { doluKoltuk = value; }
        }

        private int indirimliKoltuk;

        public int IndirimliKoltuk
        {
            get { return indirimliKoltuk; }
            set { indirimliKoltuk = value; }
        }



        private ArrayList koltuklar = new ArrayList();

        public ArrayList Koltuklar { get { return koltuklar; }  }

        public void SalonOlustur(int no,int sira, int sayi)
        {
            this.SalonNo = no;
            this.SiraSayi = sira;
            this.KoltukSayi = sayi;
            //sira sayisi kadar koltuk arrayi oluşturur, ve bu sırayı temsil eden koltuk arraylerini bir arraylistin içine atar.
            for (int i = 0; i < siraSayi; i++)
            {
                Koltuk[] koltukSatir = new Koltuk[this.KoltukSayi];
                for(int j = 0; j < this.koltukSayi; j++)
                {
                    Koltuk koltuk = new Koltuk();
                    koltuk.KoltukSiraNo = i+1;
                    koltuk.KoltukNo = j+1;
                    koltukSatir[j] = koltuk;
                }
                koltuklar.Add(koltukSatir);
            }
            
            
        }

        private ArrayList bosKoltuklar;

        public ArrayList BosKoltuklar
        {
            get { return bosKoltuklar; }
            
        }

        private ArrayList tamKoltuklar;

        public ArrayList TamKoltuklar
        {
            get { return tamKoltuklar; }

        }

        private ArrayList indirimliKoltuklar;

        public ArrayList IndirimliKoltuklar
        {
            get { return indirimliKoltuklar; }

        }

            

        public int[] BilgiAl()
        {
            bosKoltuk = 0;
            doluKoltuk = 0;
            indirimliKoltuk = 0;

            ArrayList bilgi = new ArrayList();
            for(int i = 0; i < SiraSayi; i++)
            {
                for (int j = 0; j < KoltukSayi; j++)
                {
                    
                    if(((Koltuk[])Koltuklar[i])[j].Durum == 0)
                    {
                        BosKoltuk += 1;
                        
                    }

                    if (((Koltuk[])Koltuklar[i])[j].Durum == 1)
                    {
                        DoluKoltuk += 1;
                    }

                    if (((Koltuk[])Koltuklar[i])[j].Durum == 2)
                    {
                        IndirimliKoltuk += 1;
                    }

                }
            }
            return (new int[] { BosKoltuk, DoluKoltuk, IndirimliKoltuk });
        }

        private int balance = 0;

        public int Balance
        {
            get
            {
                balance = 0;
                for (int i = 0; i < Koltuklar.Count; i++)
                {
                    for (int j = 0; j < ((Koltuk[])Koltuklar[i]).Length; j++)
                    {
                        if (((Koltuk[])Koltuklar[i])[j].Durum == 1)
                        {
                            balance = balance + 20;
                        }
                        if (((Koltuk[])Koltuklar[i])[j].Durum == 2)
                        {
                            balance = balance + 10;
                        }
                    }
                }
                return balance;
            }

        }


        





    }
}
