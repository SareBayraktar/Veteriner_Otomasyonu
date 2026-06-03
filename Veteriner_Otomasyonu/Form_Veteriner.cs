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
    public partial class Form_Veteriner : Form
    {
        VeterinerDbContext db = new VeterinerDbContext();
        public Form_Veteriner()
        {
            InitializeComponent();
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = db.Veterinerler.ToList();

                dataGridView1.Columns["Veteriner_Adi"].HeaderText = "Ad";
                dataGridView1.Columns["Veteriner_Soyadi"].HeaderText = "Soyad";
                dataGridView1.Columns["Veteriner_Uzmanlik"].HeaderText = "Uzmanlık";

                dataGridView1.Columns["Veteriner_Id"].Visible = false;
                dataGridView1.Columns["AdSoyad"].Visible = false;
                dataGridView1.Columns["Muayeneler"].Visible = false;
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
                if (txtVeterinerAdi.Text == "" || txtVeterinerSoyadi.Text == "" || txtVeterinerUzmanlik.Text == "")
                {
                    MessageBox.Show("Lütfen tüm alanları eksiksiz doldurunuz!");
                    return;
                }
                Veteriner yeniVeteriner = new Veteriner()
                {
                    Veteriner_Adi = txtVeterinerAdi.Text,
                    Veteriner_Soyadi = txtVeterinerSoyadi.Text,
                    Veteriner_Uzmanlik = txtVeterinerUzmanlik.Text
                };

                db.Veterinerler.Add(yeniVeteriner);
                db.SaveChanges();

                MessageBox.Show("Veteriner Eklendi!");

                btnListele.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata = {ex.Message}");
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult sonuc = MessageBox.Show(
                    "Silmek istediğinize emin misiniz?",
                    "Silme Onayı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (sonuc == DialogResult.Yes)
                {
                    if (dataGridView1.CurrentRow != null)
                    {
                        int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Veteriner_Id"].Value);
                        Veteriner veteriner = db.Veterinerler.Find(selectedId);

                        if (veteriner != null)
                        {
                            db.Veterinerler.Remove(veteriner);
                            db.SaveChanges();

                            MessageBox.Show("Veteriner Silindi!");

                            btnListele.PerformClick();
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Bu veterinere ait muayene kayıtları bulunduğu için silme işlemi gerçekleştirilemedi.");
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    if (txtVeterinerAdi.Text == "" || txtVeterinerSoyadi.Text == "" || txtVeterinerUzmanlik.Text == "")
                    {
                        MessageBox.Show("Lütfen tüm alanları eksiksiz doldurunuz!");
                        return;
                    }
                    int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Veteriner_Id"].Value);
                    Veteriner veteriner = db.Veterinerler.Find(selectedId);

                    if (veteriner != null)
                    {
                        veteriner.Veteriner_Adi = txtVeterinerAdi.Text;
                        veteriner.Veteriner_Soyadi = txtVeterinerSoyadi.Text;
                        veteriner.Veteriner_Uzmanlik = txtVeterinerUzmanlik.Text;

                        db.SaveChanges();

                        MessageBox.Show("Veteriner Güncellendi!");

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
                txtVeterinerAdi.Text = dataGridView1.Rows[e.RowIndex].Cells["Veteriner_Adi"].Value.ToString();
                txtVeterinerSoyadi.Text = dataGridView1.Rows[e.RowIndex].Cells["Veteriner_Soyadi"].Value.ToString();
                txtVeterinerUzmanlik.Text = dataGridView1.Rows[e.RowIndex].Cells["Veteriner_Uzmanlik"].Value.ToString();
            }
        }

    }
}
