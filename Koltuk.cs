using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sinema_salonu
{
    class Koltuk
    {
        private int koltukSiraNo;

        public int KoltukSiraNo
        {
            get { return koltukSiraNo; }
            set { koltukSiraNo = value; }
        }

        private int koltukNo;

        public int KoltukNo
        {
            get { return koltukNo; }
            set { koltukNo = value; }
        }

        private int durum = 0;

        public int Durum
        {
            get { return durum ; }
            set { durum  = value; }
        }



    }
}
