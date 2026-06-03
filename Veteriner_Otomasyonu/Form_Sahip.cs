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
    public partial class Form_Sahip : Form
    {
        public Form_Sahip()
        {
            InitializeComponent();
        }
        VeterinerDbContext db = new VeterinerDbContext();

        private void btnListele_Click(object sender, EventArgs e)
        {
            try
            {
                var sahipler = db.Sahipler.ToList();

                dataGridView1.DataSource = sahipler;


                dataGridView1.Columns["AdSoyad"].Visible = false;

                dataGridView1.Columns["Sahip_Id"].HeaderText = "Sahip ID";
                dataGridView1.Columns["Sahip_Adi"].HeaderText = "Ad";
                dataGridView1.Columns["Sahip_Soyadi"].HeaderText = "Soyad";
                dataGridView1.Columns["Sahip_Telefon"].HeaderText = "Telefon";
                dataGridView1.Columns["Sahip_Email"].HeaderText = "E-Posta";

                dataGridView1.Columns["Sahip_Id"].Visible = false;
                dataGridView1.Columns["Hayvanlar"].Visible = false;

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
                if (txtSahipAdi.Text == "" || txtSahipSoyadi.Text == "" ||
                    txtSahipTelefon.Text == "" || txtSahipEmail.Text == "")
                {
                    MessageBox.Show("Lütfen tüm alanları eksiksiz doldurunuz!");
                    return;
                }

                if (txtSahipTelefon.Text.Length != 11 || !txtSahipTelefon.Text.All(char.IsDigit))
                {
                    MessageBox.Show("Telefon numarası 11 haneli ve sadece rakamlardan oluşmalıdır!");
                    return;
                }

                if (!txtSahipEmail.Text.Contains("@") || !txtSahipEmail.Text.Contains("."))
                {
                    MessageBox.Show("Geçerli bir e-posta adresi giriniz!");
                    return;
                }
                Sahip yeniSahip = new Sahip()
                {
                    Sahip_Adi = txtSahipAdi.Text,
                    Sahip_Soyadi = txtSahipSoyadi.Text,
                    Sahip_Telefon = txtSahipTelefon.Text,
                    Sahip_Email = txtSahipEmail.Text
                };

                db.Sahipler.Add(yeniSahip);
                db.SaveChanges();

                MessageBox.Show("Yeni Sahip Eklendi!");

                btnListele.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lütfen tüm alanları eksiksiz doldurunuz!");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtSahipAdi.Text = dataGridView1.Rows[e.RowIndex].Cells["Sahip_Adi"].Value.ToString();
                txtSahipSoyadi.Text = dataGridView1.Rows[e.RowIndex].Cells["Sahip_Soyadi"].Value.ToString();
                txtSahipTelefon.Text = dataGridView1.Rows[e.RowIndex].Cells["Sahip_Telefon"].Value.ToString();
                txtSahipEmail.Text = dataGridView1.Rows[e.RowIndex].Cells["Sahip_Email"].Value.ToString();
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
                    if (txtSahipAdi.Text == "" || txtSahipSoyadi.Text == "" ||
                        txtSahipTelefon.Text == "" || txtSahipEmail.Text == "")
                    {
                        MessageBox.Show("Lütfen tüm alanları eksiksiz doldurunuz!");
                        return;
                    }

                    if (txtSahipTelefon.Text.Length != 11 || !txtSahipTelefon.Text.All(char.IsDigit))
                    {
                        MessageBox.Show("Telefon numarası 11 haneli ve sadece rakamlardan oluşmalıdır!");
                        return;
                    }

                    if (!txtSahipEmail.Text.Contains("@") || !txtSahipEmail.Text.Contains("."))
                    {
                        MessageBox.Show("Geçerli bir e-posta adresi giriniz!");
                        return;
                    }
                    int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Sahip_Id"].Value);
                    Sahip sahip = db.Sahipler.Find(selectedId);

                    if (sahip != null)
                    {
                        sahip.Sahip_Adi = txtSahipAdi.Text;
                        sahip.Sahip_Soyadi = txtSahipSoyadi.Text;
                        sahip.Sahip_Telefon = txtSahipTelefon.Text;
                        sahip.Sahip_Email = txtSahipEmail.Text;

                        db.SaveChanges();

                        MessageBox.Show("Sahip Güncellendi!");

                        btnListele.PerformClick();
                    }
                }
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
                if (dataGridView1.Rows.Count == 0 || dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Cells["Sahip_Id"].Value == null)
                {
                    MessageBox.Show("Lütfen önce silmek istediğiniz kaydı seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Sahip_Id"].Value);
                        Sahip sahip = db.Sahipler.Find(selectedId);

                        if (sahip != null)
                        {
                            bool hayvanVarMi = db.Hayvanlar.Any(h => h.Sahip_Id == selectedId);
                            bool satisVarMi = db.Satislar.Any(s => s.Sahip_Id == selectedId);

                            if (hayvanVarMi)
                            {
                                MessageBox.Show("Bu sahibe ait hayvan kayıtları bulunmaktadır! Önce hayvan kayıtlarını siliniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            if (satisVarMi)
                            {
                                MessageBox.Show("Bu sahibe ait satış kayıtları bulunmaktadır! Önce satış kayıtlarını siliniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            db.Sahipler.Remove(sahip);
                            db.SaveChanges();
                            MessageBox.Show("Sahip Silindi!");
                            btnListele.PerformClick();
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Bu sahip silinemez! Önce bu sahibe ait hayvan kayıtlarını silmelisiniz.");
            }
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
