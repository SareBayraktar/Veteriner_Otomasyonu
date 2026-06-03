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
    public partial class Form_Hayvan : Form
    {
        VeterinerDbContext db = new VeterinerDbContext();
        public Form_Hayvan()
        {
            InitializeComponent();
        }

        private void Form_Hayvan_Load(object sender, EventArgs e)
        {
            try
            {
                comboBoxSahip.DataSource = db.Sahipler.ToList();
                comboBoxSahip.DisplayMember = "AdSoyad";
                comboBoxSahip.ValueMember = "Sahip_Id";

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata = {ex.Message}");
            }
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            try
            {
                var hayvanlar = db.Hayvanlar
                    .Select(h => new {
                        h.Hayvan_Id,
                        h.Hayvan_Adi,
                        h.Hayvan_Turu,
                        h.Hayvan_Cinsi,
                        h.Sahip_Id,
                        SahibiAdi = h.Sahip.Sahip_Adi + " " + h.Sahip.Sahip_Soyadi
                    }).ToList();

                dataGridView1.DataSource = hayvanlar;

                dataGridView1.Columns["Hayvan_Adi"].HeaderText = "Hayvan Adı";
                dataGridView1.Columns["Hayvan_Turu"].HeaderText = "Tür";
                dataGridView1.Columns["Hayvan_Cinsi"].HeaderText = "Cins";
                dataGridView1.Columns["SahibiAdi"].HeaderText = "Sahibi";

                dataGridView1.Columns["Hayvan_Id"].Visible = false;
                dataGridView1.Columns["Sahip_Id"].Visible = false;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata = {ex.Message}");
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                Hayvan yeniHayvan = new Hayvan()
                {
                    Hayvan_Adi = txtHayvanAdi.Text,
                    Hayvan_Turu = txtHayvanTuru.Text,
                    Hayvan_Cinsi = txtHayvanCinsi.Text,
                    Sahip_Id = Convert.ToInt32(comboBoxSahip.SelectedValue)
                };

                db.Hayvanlar.Add(yeniHayvan);
                db.SaveChanges();

                MessageBox.Show("Hayvan Eklendi!");

                btnListele.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lütfen tüm alanları eksiksiz doldurunuz!");
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count == 0 || dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Lütfen silmek istediğiniz kaydı seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DialogResult sonuc = MessageBox.Show(
                    "Silmek istediğinize emin misiniz?",
                    "Silme Onayı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (sonuc == DialogResult.Yes)
                {
                    if (dataGridView1.CurrentRow != null)
                    {
                        int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Hayvan_Id"].Value);
                        Hayvan hayvan = db.Hayvanlar.Find(selectedId);

                        if (hayvan != null)
                        {
                            bool muayeneVarMi = db.Muayeneler.Any(m => m.Hayvan_Id == selectedId);

                            if (muayeneVarMi)
                            {
                                MessageBox.Show("Bu hayvana ait muayene kayıtları bulunmaktadır! Önce muayene kayıtlarını siliniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            db.Hayvanlar.Remove(hayvan);
                            db.SaveChanges();
                            MessageBox.Show("Hayvan Silindi!");
                            btnListele.PerformClick();
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Bu hayvana ait muayene kayıtları bulunduğu için silme işlemi gerçekleştirilemedi.");
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count == 0 || dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Lütfen güncellemek istediğiniz kaydı seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                {
                    int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Hayvan_Id"].Value);
                    Hayvan hayvan = db.Hayvanlar.Find(selectedId);

                    if (hayvan != null)
                    {
                        if (txtHayvanAdi.Text == "" || txtHayvanTuru.Text == "" || txtHayvanCinsi.Text == "")
                        {
                            MessageBox.Show("Lütfen tüm alanları eksiksiz doldurunuz!");
                            return;
                        }

                        hayvan.Hayvan_Adi = txtHayvanAdi.Text;
                        hayvan.Hayvan_Turu = txtHayvanTuru.Text;
                        hayvan.Hayvan_Cinsi = txtHayvanCinsi.Text;
                        hayvan.Sahip_Id = Convert.ToInt32(comboBoxSahip.SelectedValue);

                        db.SaveChanges();

                        MessageBox.Show("Hayvan Bilgileri Güncellendi!");

                        btnListele.PerformClick();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata = {ex.Message}");
            }
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtHayvanAdi.Text = dataGridView1.Rows[e.RowIndex].Cells["Hayvan_Adi"].Value.ToString();
                txtHayvanTuru.Text = dataGridView1.Rows[e.RowIndex].Cells["Hayvan_Turu"].Value.ToString();
                txtHayvanCinsi.Text = dataGridView1.Rows[e.RowIndex].Cells["Hayvan_Cinsi"].Value.ToString();


                comboBoxSahip.SelectedValue = Convert.ToInt32(
                    dataGridView1.Rows[e.RowIndex].Cells["Sahip_Id"].Value
);
            }
        }

    }
}
