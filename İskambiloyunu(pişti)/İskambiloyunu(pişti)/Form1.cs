using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace İskambiloyunu_pişti_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void metod()
        {
            if (yerkartisonhane == 's')
            {
                puan += 11;
            }
            else if (yerkartisonhane == 'x' || yerkartisonhane == 'z')
            {
                puan += 10;
            }
            else if (yerkartisonhane == 'r')
            {
                puan += 25;
            }
            else
            {
                puan += Convert.ToInt16(yerkartisonhane);
            }
        }
        OleDbConnection cnt_baglanti;
        List<string> kartlar = new List<string>();
        List<string> oyuncu1kartlar = new List<string>();
        List<string> oyuncu2kartlar = new List<string>();
        char yerkartisonhane;
        int puan;
        int puan1oyuncu=0;
        int puan2oyuncu=0;
        Random rnd = new Random();
        private void button1_Click(object sender, EventArgs e)
        {
            cnt_baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=kartlar.accdb");
            cnt_baglanti.Open();
            OleDbDataAdapter adp_dagit = new OleDbDataAdapter("select * from kartlar",cnt_baglanti);
            DataTable dtp_dagit = new DataTable();
            adp_dagit.Fill(dtp_dagit);
            do
            {
                int rastgele = rnd.Next(0,52);
                if (kartlar.IndexOf(dtp_dagit.Rows[rastgele]["kartadi"].ToString()) == -1)
                {
                    kartlar.Add(dtp_dagit.Rows[rastgele]["kartadi"].ToString());
                }
            } while (kartlar.Count!=52);
            for (int i = 0; i < 26; i++)
            {
                oyuncu1kartlar.Add(kartlar[i]);
            }
            for (int a = 26; a < 52; a++)
            {
                oyuncu2kartlar.Add(kartlar[a]);
            }
            button1.Visible = false;
            puan1.Visible = true;
            puan2.Visible = true;
            lbl_oyuncu1.Visible = true;
            lbl_oyuncu2.Visible = true;
            pct_yeroyuncu1.Visible = true;
            pct_yeroyuncu2.Visible = true;
            pct_yeroyuncu2.BackColor = Color.ForestGreen;
            pct_yeroyuncu1.BackColor = Color.ForestGreen;
            pct_oyuncu1.ImageLocation = "arka.jpg";
            pct_oyuncu2.ImageLocation = "arka.jpg";
            yerkarti.ImageLocation = "";
            yerkarti.BackColor = Color.ForestGreen;
            btn_oyuncu1.Visible = true;
            btn_oyuncu2.Visible = true;
            btn_oyuncu2.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            yerkarti.ImageLocation ="arka.jpg";
        }

        private void btn_oyuncu1_Click(object sender, EventArgs e)
        {
            if (yerkarti.ImageLocation=="")
            {
                puan = 0;
                yerkarti.ImageLocation = "iskambil_Ödev//" + oyuncu1kartlar[0] + ".png";
                yerkartisonhane = oyuncu1kartlar[0][oyuncu1kartlar[0].Length - 1];
                metod();
            }
            else if (yerkartisonhane == oyuncu1kartlar[0][oyuncu1kartlar[0].Length-1])
            {
                metod();
                pct_yeroyuncu2.ImageLocation = "iskambil_Ödev//" + oyuncu1kartlar[0] + ".png";
                yerkarti.ImageLocation = "";
                puan2oyuncu += puan;
            }
            else
            {
                yerkarti.ImageLocation = "iskambil_Ödev//" + oyuncu1kartlar[0] + ".png";
                yerkartisonhane = oyuncu1kartlar[0][oyuncu1kartlar[0].Length-1];
            }
            oyuncu1kartlar.RemoveAt(0);
            btn_oyuncu2.Enabled = true;
            btn_oyuncu1.Enabled = false;
            if (oyuncu1kartlar.Count==0)
            {
                pct_oyuncu1.ImageLocation = "";
                pct_oyuncu1.BackColor = Color.ForestGreen;
            }
        }

        private void btn_oyuncu2_Click(object sender, EventArgs e)
        {
            if (yerkarti.ImageLocation == "")
            {
                puan = 0;
                yerkarti.ImageLocation = "iskambil_Ödev//" + oyuncu2kartlar[0] + ".png";
                yerkartisonhane = oyuncu2kartlar[0][oyuncu2kartlar[0].Length - 1];
                metod();
            }
            else  if (yerkartisonhane == oyuncu2kartlar[0][oyuncu2kartlar[0].Length-1])
            {
                metod();
                pct_yeroyuncu1.ImageLocation = "iskambil_Ödev//" + oyuncu2kartlar[0] + ".png";
                yerkarti.ImageLocation = "";
                puan1oyuncu += puan;    
            }
            else
            {
                yerkarti.ImageLocation = "iskambil_Ödev//" + oyuncu2kartlar[0] + ".png";
                yerkartisonhane = oyuncu2kartlar[0][oyuncu2kartlar[0].Length-1];
            }
            oyuncu2kartlar.RemoveAt(0);
            btn_oyuncu2.Enabled = false;
            btn_oyuncu1.Enabled = true;
            if (oyuncu2kartlar.Count==0)
            {
                btn_oyuncu1.Enabled = false;
                btn_oyuncu2.Enabled = false;
                pct_oyuncu2.ImageLocation = "";
                pct_oyuncu2.BackColor = Color.ForestGreen;
                puan1.Text = puan1oyuncu.ToString();
                puan2.Text = puan2oyuncu.ToString();
                if (puan1oyuncu<puan2oyuncu)
                {
                    MessageBox.Show("1.OYUNCU KAZANDI TEBRİKLER");
                }
                else if (puan1oyuncu > puan2oyuncu)
                {
                    MessageBox.Show("2.OYUNCU KAZANDI TEBRİKLER");
                }
                else
                {
                    MessageBox.Show("DOSTLUK KAZANDI BERABERE :)");
                }
            }    
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
