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
    public partial class Form_Muayene : Form

    {
        VeterinerDbContext db = new VeterinerDbContext();
        public Form_Muayene()
        {
            InitializeComponent();
        }

        private void Form_Muayene_Load(object sender, EventArgs e)
        {
            try
            {
                comboBoxHayvan.DataSource = db.Hayvanlar.ToList();
                comboBoxHayvan.DisplayMember = "Hayvan_Adi";
                comboBoxHayvan.ValueMember = "Hayvan_Id";

                comboBoxVeteriner.DataSource = db.Veterinerler.ToList();
                comboBoxVeteriner.DisplayMember = "AdSoyad";
                comboBoxVeteriner.ValueMember = "Veteriner_Id";


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
              var liste = db.Muayeneler
                     .Select(m => new
                     {
                      m.Muayene_Id,
                      m.Hayvan_Id,
                      m.Veteriner_Id,

                      HayvanAdi = m.Hayvan.Hayvan_Adi,

                      VeterinerAdi =m.Veteriner.Veteriner_Adi + " " +
                      m.Veteriner.Veteriner_Soyadi,

                      m.Muayene_Tarihi,
                      m.Teshis,
                      m.Tedavi,
                      m.Muayene_Ucreti
                     })
                     .ToList();

                dataGridView1.DataSource = liste;

                dataGridView1.Columns["HayvanAdi"].HeaderText = "Hayvan";
                dataGridView1.Columns["VeterinerAdi"].HeaderText = "Veteriner";
                dataGridView1.Columns["Muayene_Tarihi"].HeaderText = "Muayene Tarihi";
                dataGridView1.Columns["Teshis"].HeaderText = "Teşhis";
                dataGridView1.Columns["Tedavi"].HeaderText = "Tedavi";
                dataGridView1.Columns["Muayene_Ucreti"].HeaderText = "Ücret";


                dataGridView1.Columns["Muayene_Id"].Visible = false;
                dataGridView1.Columns["Hayvan_Id"].Visible = false;
                dataGridView1.Columns["Veteriner_Id"].Visible = false;
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
                if (string.IsNullOrWhiteSpace(txtTeshis.Text) ||
                string.IsNullOrWhiteSpace(txtTedavi.Text) ||
                string.IsNullOrWhiteSpace(txtMuayeneUcreti.Text))
                {
                    MessageBox.Show("Lütfen tüm alanları doldurunuz.");
                    return;
                }
                decimal ucret;

                if (!decimal.TryParse(txtMuayeneUcreti.Text, out ucret))
                {
                    MessageBox.Show("Lütfen ücret alanına sadece sayı giriniz.");
                    return;
                }

                Muayene yeniMuayene = new Muayene()
                {
                    Muayene_Tarihi = dateTimePicker1.Value,
                    Teshis = txtTeshis.Text,
                    Tedavi = txtTedavi.Text,
                    Muayene_Ucreti = ucret,
                    Hayvan_Id = Convert.ToInt32(comboBoxHayvan.SelectedValue),
                    Veteriner_Id = Convert.ToInt32(comboBoxVeteriner.SelectedValue)
                };

                db.Muayeneler.Add(yeniMuayene);
                db.SaveChanges();

                MessageBox.Show("Muayene Eklendi!");

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
                        var row = dataGridView1.CurrentRow;
                        var item = (dynamic)row.DataBoundItem;
                        int selectedId = item.Muayene_Id;
                        Muayene muayene = db.Muayeneler.Find(selectedId);

                        if (muayene != null)
                        {
                            db.Muayeneler.Remove(muayene);
                            db.SaveChanges();

                            MessageBox.Show("Muayene Silindi!");

                            btnListele.PerformClick();
                        }
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

        private void rd_1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rd_1.Checked)
                {
                    var gruplar = db.Muayeneler
                        .GroupBy(m => m.Hayvan.Hayvan_Turu)
                        .Select(g => new
                        {
                            HayvanTuru = g.Key,
                            MuayeneSayisi = g.Count()
                        })
                        .ToList();

                    if (gruplar.Any())
                    {
                        int maxSayi = gruplar.Max(x => x.MuayeneSayisi);

                        var enCoklar = gruplar
                            .Where(x => x.MuayeneSayisi == maxSayi)
                            .Select(x => x.HayvanTuru)
                            .ToList();

                        lblsonuc.Text = $"En çok muayene edilen tür: {string.Join(", ", enCoklar)} ({maxSayi} muayene)";
                        var enCokTur = enCoklar.First();

                        dataGridView1.DataSource = db.Muayeneler
                            .Where(m => m.Hayvan.Hayvan_Turu == enCokTur)
                            .Select(m => new
                            {
                                HayvanAdi = m.Hayvan.Hayvan_Adi,
                                Teshis = m.Teshis,
                                Muayene_Tarihi = m.Muayene_Tarihi
                            })
                            .ToList();
                    }
                    else
                    {
                        lblsonuc.Text = "Kayıt bulunamadı.";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata = {ex.Message}");
            }
        }

        private void rd_2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rd_2.Checked)
                {
                    int buAy = DateTime.Now.Month;
                    int buYil = DateTime.Now.Year;

                    var sonuc = db.Muayeneler
                        .Where(m => m.Muayene_Tarihi.Month == buAy && m.Muayene_Tarihi.Year == buYil)
                        .Select(m => new
                        {
                            HayvanAdi = m.Hayvan.Hayvan_Adi,
                            m.Teshis,
                            m.Muayene_Tarihi
                        }).ToList();

                    dataGridView1.DataSource = sonuc;
                    lblsonuc.Text = $"Bu ay toplam {sonuc.Count} muayene yapılmış.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata = {ex.Message}");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Columns.Contains("Teshis") && dataGridView1.Columns.Contains("Tedavi"))
            {
                txtTeshis.Text = dataGridView1.Rows[e.RowIndex].Cells["Teshis"].Value.ToString();
                txtTedavi.Text = dataGridView1.Rows[e.RowIndex].Cells["Tedavi"].Value.ToString();
                txtMuayeneUcreti.Text = dataGridView1.Rows[e.RowIndex].Cells["Muayene_Ucreti"].Value.ToString();
                dateTimePicker1.Value =
                Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells["Muayene_Tarihi"].Value);
                comboBoxHayvan.SelectedValue =
                Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Hayvan_Id"].Value);

                comboBoxVeteriner.SelectedValue =
                    Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Veteriner_Id"].Value);
            }
        }

        private void rd_3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rd_3.Checked)
                {
                    decimal toplamGelir = db.Muayeneler
                        .Select(m => (decimal?)m.Muayene_Ucreti)
                        .Sum() ?? 0;

                    int buAy = DateTime.Now.Month;
                    int buYil = DateTime.Now.Year;

                    decimal aylikGelir = db.Muayeneler
                        .Where(m => m.Muayene_Tarihi.Month == buAy &&
                                    m.Muayene_Tarihi.Year == buYil)
                        .Select(m => (decimal?)m.Muayene_Ucreti)
                        .Sum() ?? 0;

                    lblsonuc.Text =
                        $"Toplam Muayene Geliri: {toplamGelir} TL\n" +
                        $"Bu Ayki Muayene Geliri: {aylikGelir} TL";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata = {ex.Message}");
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    if (string.IsNullOrWhiteSpace(txtTeshis.Text) ||
                        string.IsNullOrWhiteSpace(txtTedavi.Text) ||
                        string.IsNullOrWhiteSpace(txtMuayeneUcreti.Text))
                    {
                        MessageBox.Show("Lütfen tüm alanları doldurunuz.");
                        return;
                    }

                    int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Muayene_Id"].Value);
                    Muayene muayene = db.Muayeneler.Find(selectedId);

                    if (muayene != null)
                    {
                        muayene.Muayene_Tarihi = dateTimePicker1.Value;
                        muayene.Teshis = txtTeshis.Text;
                        muayene.Tedavi = txtTedavi.Text;

                        decimal ucret;

                        if (!decimal.TryParse(txtMuayeneUcreti.Text, out ucret))
                        {
                            MessageBox.Show("Lütfen ücret alanına sadece sayı giriniz.");
                            return;
                        }

                        muayene.Muayene_Ucreti = ucret;
                        muayene.Hayvan_Id = Convert.ToInt32(comboBoxHayvan.SelectedValue);
                        muayene.Veteriner_Id = Convert.ToInt32(comboBoxVeteriner.SelectedValue);

                        db.SaveChanges();

                        MessageBox.Show("Muayene Güncellendi!");

                        btnListele.PerformClick();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata = {ex.Message}");
            }
        }
    }
}