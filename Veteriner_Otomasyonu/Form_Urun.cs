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
    public partial class Form_Urun : Form
    {
        VeterinerDbContext db = new VeterinerDbContext();

        public Form_Urun()
        {
            InitializeComponent();
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = db.Urunler.ToList();

                dataGridView1.Columns["Urun_Adi"].HeaderText = "Ürün Adı";
                dataGridView1.Columns["Urun_Turu"].HeaderText = "Ürün Türü";
                dataGridView1.Columns["Urun_Fiyati"].HeaderText = "Fiyat";
                dataGridView1.Columns["Stok_Miktari"].HeaderText = "Stok";

                dataGridView1.Columns["Urun_Id"].Visible = false;
                dataGridView1.Columns["Satislar"].Visible = false;
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
                if (string.IsNullOrWhiteSpace(txtUrunAdi.Text) ||
                    string.IsNullOrWhiteSpace(txtUrunTuru.Text) ||
                    string.IsNullOrWhiteSpace(txtUrunFiyati.Text) ||
                    string.IsNullOrWhiteSpace(txtStokMiktari.Text))
                {
                    MessageBox.Show("Lütfen tüm alanları doldurunuz.");
                    return;
                }
                decimal fiyat;
                int stok;

                if (!decimal.TryParse(txtUrunFiyati.Text, out fiyat))
                {
                    MessageBox.Show("Lütfen fiyat alanına sadece sayı giriniz.");
                    return;
                }

                if (!int.TryParse(txtStokMiktari.Text, out stok))
                {
                    MessageBox.Show("Lütfen stok alanına sadece sayı giriniz.");
                    return;
                }

                Urun yeniUrun = new Urun()
                {
                    Urun_Adi = txtUrunAdi.Text,
                    Urun_Turu = txtUrunTuru.Text,
                    Urun_Fiyati = fiyat,
                    Stok_Miktari = stok
                };

                db.Urunler.Add(yeniUrun);
                db.SaveChanges();

                MessageBox.Show("Ürün Eklendi!");

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
                        int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Urun_Id"].Value);
                        Urun urun = db.Urunler.Find(selectedId);

                        if (urun != null)
                        {
                            bool satisVarMi = db.Satislar.Any(s => s.Urun_Id == selectedId);

                            if (satisVarMi)
                            {
                                MessageBox.Show("Bu ürüne ait satış kayıtları bulunmaktadır! Önce satış kayıtlarını siliniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            db.Urunler.Remove(urun);
                            db.SaveChanges();
                            MessageBox.Show("Ürün Silindi!");
                            btnListele.PerformClick();
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Bu ürüne ait satış kayıtları bulunduğu için silme işlemi gerçekleştirilemedi.");
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow == null || dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("Lütfen güncellemek istediğiniz kaydı seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtUrunAdi.Text) ||
                    string.IsNullOrWhiteSpace(txtUrunTuru.Text) ||
                    string.IsNullOrWhiteSpace(txtUrunFiyati.Text) ||
                    string.IsNullOrWhiteSpace(txtStokMiktari.Text))
                {
                    MessageBox.Show("Lütfen tüm alanları doldurunuz.");
                    return;
                }

                if (dataGridView1.CurrentRow != null)
                {
                    int selectedId = Convert.ToInt32(
                        dataGridView1.CurrentRow.Cells["Urun_Id"].Value);

                    Urun urun = db.Urunler.Find(selectedId);

                    if (urun != null)
                    {
                        decimal fiyat;
                        int stok;

                        if (!decimal.TryParse(txtUrunFiyati.Text, out fiyat))
                        {
                            MessageBox.Show("Lütfen fiyat alanına sadece sayı giriniz. ");
                            return;
                        }

                        if (!int.TryParse(txtStokMiktari.Text, out stok))
                        {
                            MessageBox.Show("Lütfen stok alanına sadece sayı giriniz.");
                            return;
                        }

                        urun.Urun_Adi = txtUrunAdi.Text;
                        urun.Urun_Turu = txtUrunTuru.Text;
                        urun.Urun_Fiyati = fiyat;
                        urun.Stok_Miktari = stok;

                        db.SaveChanges();

                        MessageBox.Show("Ürün Güncellendi!");

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
                txtUrunAdi.Text = dataGridView1.Rows[e.RowIndex].Cells["Urun_Adi"].Value.ToString();
                txtUrunTuru.Text = dataGridView1.Rows[e.RowIndex].Cells["Urun_Turu"].Value.ToString();
                txtUrunFiyati.Text = dataGridView1.Rows[e.RowIndex].Cells["Urun_Fiyati"].Value.ToString();
                txtStokMiktari.Text = dataGridView1.Rows[e.RowIndex].Cells["Stok_Miktari"].Value.ToString();
            }
        }

        
    }
}
