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
    public partial class Form_Satis : Form
    {
        VeterinerDbContext db = new VeterinerDbContext();

        public Form_Satis()
        {
            InitializeComponent();
        }

        private void Form_Satis_Load(object sender, EventArgs e)
        {
            try
            {
                comboBoxUrun.DataSource = db.Urunler.ToList();
                comboBoxUrun.DisplayMember = "Urun_Adi";
                comboBoxUrun.ValueMember = "Urun_Id";

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
                var liste = db.Satislar
                    .Select(s => new {
                        s.Satis_Id,
                        UrunAdi = s.Urun.Urun_Adi,
                        SahibiAdi = s.Sahip.Sahip_Adi + " " + s.Sahip.Sahip_Soyadi,
                        s.Miktar,
                        s.Toplam_Tutar,
                        s.Satis_Tarihi
                    }).ToList();

                dataGridView1.DataSource = liste;

                dataGridView1.Columns["UrunAdi"].HeaderText = "Ürün";
                dataGridView1.Columns["SahibiAdi"].HeaderText = "Sahip";
                dataGridView1.Columns["Miktar"].HeaderText = "Miktar";
                dataGridView1.Columns["Toplam_Tutar"].HeaderText = "Toplam Tutar";
                dataGridView1.Columns["Satis_Tarihi"].HeaderText = "Satış Tarihi";

                dataGridView1.Columns["Satis_Id"].Visible = false;
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
                if (string.IsNullOrWhiteSpace(txtMiktar.Text))
{
                MessageBox.Show("Lütfen miktar giriniz.");
                return;
}
                int urunId = Convert.ToInt32(comboBoxUrun.SelectedValue);
                Urun urun = db.Urunler.Find(urunId);

                int miktar;
                if (!int.TryParse(txtMiktar.Text, out miktar))
                {
                    MessageBox.Show("Lütfen miktar alanına sadece sayı giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (urun == null)
                {
                    MessageBox.Show("Ürün bulunamadı!");
                    return;
                }

                if (miktar <= 0)
                {
                    MessageBox.Show("Miktar 0'dan büyük olmalıdır!");
                    return;
                }

                if (urun.Stok_Miktari < miktar)
                {
                    MessageBox.Show($"Yetersiz stok! Mevcut stok: {urun.Stok_Miktari}");
                    return;
                }

                decimal toplamTutar = urun.Urun_Fiyati * miktar;

                Satis yeniSatis = new Satis()
                {
                    Satis_Tarihi = dateTimePicker1.Value,
                    Miktar = miktar,
                    Toplam_Tutar = toplamTutar,
                    Urun_Id = urunId,
                    Sahip_Id = Convert.ToInt32(comboBoxSahip.SelectedValue)
                };

                db.Satislar.Add(yeniSatis);

                urun.Stok_Miktari -= miktar;

                if (urun.Stok_Miktari <= 20)
                {
                    MessageBox.Show(
                        $"Dikkat!\n\n{urun.Urun_Adi} kritik stok seviyesine düştü.\nKalan stok: {urun.Stok_Miktari}",
                        "Kritik Stok Uyarısı",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }

                db.SaveChanges();

                MessageBox.Show($"Satış Eklendi! Toplam Tutar: {toplamTutar} TL");

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
                        int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Satis_Id"].Value);
                        Satis satis = db.Satislar.Find(selectedId);

                        if (satis != null)
                        {
                            Urun urun = db.Urunler.Find(satis.Urun_Id);

                            if (urun != null)
                            {
                                urun.Stok_Miktari += satis.Miktar;
                            }

                            db.Satislar.Remove(satis);
                            db.SaveChanges();

                            MessageBox.Show("Satış Silindi! Ürün stoğu geri eklendi.");

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
                    var sonuc = db.Satislar
                        .GroupBy(s => s.Urun.Urun_Adi)
                        .Select(g => new
                        {
                            UrunAdi = g.Key,
                            ToplamSatis = g.Sum(s => s.Miktar)
                        })
                        .OrderByDescending(x => x.ToplamSatis)
                        .FirstOrDefault();

                    if (sonuc != null)
                        lblSonuc.Text = $"En çok satan ürün: {sonuc.UrunAdi} ({sonuc.ToplamSatis} adet)";
                    else
                        lblSonuc.Text = "Kayıt bulunamadı.";
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
                    var sonuc = db.Satislar
                        .GroupBy(s => new { s.Sahip.Sahip_Adi, s.Sahip.Sahip_Soyadi })
                        .Select(g => new
                        {
                            SahibiAdi = g.Key.Sahip_Adi + " " + g.Key.Sahip_Soyadi,
                            ToplamHarcama = g.Sum(s => s.Toplam_Tutar)
                        })
                        .OrderByDescending(x => x.ToplamHarcama)
                        .ToList();

                    dataGridView1.DataSource = sonuc;
                    lblSonuc.Text = $"Toplam {sonuc.Count} sahip listelendi.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata = {ex.Message}");
            }
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (!dataGridView1.Columns.Contains("Miktar"))
            {
                return;
            }

            if (e.RowIndex >= 0)
            {
                var row = dataGridView1.CurrentRow;
                var item = (dynamic)row.DataBoundItem;

                txtMiktar.Text = item.Miktar.ToString();
                dateTimePicker1.Value = item.Satis_Tarihi;

                string urunAdi = item.UrunAdi;
                string sahipAdi = item.SahibiAdi;

                foreach (Urun urun in comboBoxUrun.Items)
                {
                    if (urun.Urun_Adi == urunAdi)
                    {
                        comboBoxUrun.SelectedItem = urun;
                        break;
                    }
                }

                foreach (Sahip sahip in comboBoxSahip.Items)
                {
                    string tamAd = sahip.Sahip_Adi + " " + sahip.Sahip_Soyadi;

                    if (tamAd == sahipAdi)
                    {
                        comboBoxSahip.SelectedItem = sahip;
                        break;
                    }
                }
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
                    if (string.IsNullOrWhiteSpace(txtMiktar.Text))
                    {
                        MessageBox.Show("Lütfen miktar giriniz.");
                        return;
                    }
                    int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Satis_Id"].Value);
                    Satis satis = db.Satislar.Find(selectedId);

                    if (satis != null)
                    {
                        Urun eskiUrun = db.Urunler.Find(satis.Urun_Id);
                        if (eskiUrun != null)
                        {
                            eskiUrun.Stok_Miktari += satis.Miktar;
                        }

                        int yeniUrunId = Convert.ToInt32(comboBoxUrun.SelectedValue);
                        Urun yeniUrun = db.Urunler.Find(yeniUrunId);
                        
                        int yeniMiktar;
                        if (!int.TryParse(txtMiktar.Text, out yeniMiktar))
                        {
                            MessageBox.Show("Lütfen miktar alanına sadece sayı giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (yeniUrun == null)
                        {
                            MessageBox.Show("Ürün bulunamadı!");
                            return;
                        }

                        if (yeniMiktar <= 0)
                        {
                            MessageBox.Show("Miktar 0'dan büyük olmalıdır!");
                            return;
                        }

                        if (yeniUrun.Stok_Miktari < yeniMiktar)
                        {
                            MessageBox.Show($"Yetersiz stok! Mevcut stok: {yeniUrun.Stok_Miktari}");
                            return;
                        }

                        decimal toplamTutar = yeniUrun.Urun_Fiyati * yeniMiktar;

                        
                        yeniUrun.Stok_Miktari -= yeniMiktar;

                        
                        satis.Urun_Id = yeniUrunId;
                        satis.Sahip_Id = Convert.ToInt32(comboBoxSahip.SelectedValue);
                        satis.Miktar = yeniMiktar;
                        satis.Toplam_Tutar = toplamTutar;
                        satis.Satis_Tarihi = dateTimePicker1.Value;

                        db.SaveChanges();

                        MessageBox.Show("Satış Güncellendi!");

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

