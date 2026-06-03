using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Veteriner_Otomasyonu
{
    public partial class AnaMenu : Form
    {
        public AnaMenu()
        {
            InitializeComponent();
        }

        private void btnSahipler_Click(object sender, EventArgs e)
        {
            Form_Sahip formSahip = new Form_Sahip();
            this.Hide();
            formSahip.ShowDialog();
            this.Show();
        }


        private void btnHayvanlar_Click(object sender, EventArgs e)
        {
            Form_Hayvan formHayvan = new Form_Hayvan();
            this.Hide();
            formHayvan.ShowDialog();
            this.Show();
        }

        private void btnVeterinerler_Click(object sender, EventArgs e)
        {
            Form_Veteriner formVeteriner = new Form_Veteriner();
            this.Hide();
            formVeteriner.ShowDialog();
            this.Show();
        }

        private void btnMuayeneler_Click(object sender, EventArgs e)
        {
            Form_Muayene formMuayene = new Form_Muayene();
            this.Hide();
            formMuayene.ShowDialog();
            this.Show();
        }

        private void btnUrunler_Click(object sender, EventArgs e)
        {
            Form_Urun formUrun = new Form_Urun();
            this.Hide();
            formUrun.ShowDialog();
            this.Show();
        }

        private void btnSatis_Click(object sender, EventArgs e)
        {
            Form_Satis formSatis = new Form_Satis();
            this.Hide();
            formSatis.ShowDialog();
            this.Show();
        }
    }
}
