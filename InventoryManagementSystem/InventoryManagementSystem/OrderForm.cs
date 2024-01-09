using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace InventoryManagementSystem
{
    public partial class OrderForm : Form
    {
        // Veritabanı bağlantısı için SqlConnection tanımı.
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kalka\OneDrive\Documents\dbLMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;

        // Formun yapıcı metodu, form başlatıldığında çağrılır ve siparişleri yükler.
        public OrderForm()
        {
            InitializeComponent();
            LoadOrder(); // Sipariş listesini yükler.
            dgvOrder.DataError += dgvOrder_DataError; // Hata işleme için olay bağlantısı.
        }

        // Siparişleri veritabanından yükleyip DataGridView'a ekler.
        public void LoadOrder()
        {
            try
            {
                int i = 0;
                dgvOrder.Rows.Clear();
                cmd = new SqlCommand("SELECT orderid, odate, O.pid, P.pname, O.cid, C.cname, qty, price, total FROM tbOrder AS O JOIN tbCustomer AS C ON O.cid = C.cid JOIN JOIN tbProduct AS P ON O.pid = P.pid WHERE CONCAT (orderid, odate, O.pid, P.pname, O.cid, C.cname, qty, price) LIKE '%" + txtSearch.Text + "%'", con);
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgvOrder.Rows.Add(i, dr[0].ToString(), Convert.ToDateTime(dr[1].ToString()).ToString("dd/MM/yyyy"), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString());
                }
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }
        }

        // DataGridView'da veri hatası olduğunda çağrılır.
        private void dgvOrder_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Hata mesajını gösterir.
            MessageBox.Show("Bir hata oluştu: " + e.Exception.Message);
            e.ThrowException = false;
        }

        // DataGridView'da bir hücreye tıklandığında çağrılır.
        private void dgvOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvOrder.Columns[e.ColumnIndex].Name;

            if (colName == "Delete")
            {
                // Siparişi silme işlemini onaylar ve gerçekleştirir.
                if (MessageBox.Show("Bu siparişi silmek istediğinizden emin misiniz?", "Kaydı Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("DELETE FROM tbOrder WHERE orderid LIKE '" + dgvOrder.Rows[e.RowIndex].Cells[1].Value.ToString() + "' ", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Kayıt başarıyla silindi!");

                    // İlgili ürünün stok miktarını günceller.
                    cmd = new SqlCommand("UPDATE tbProduct SET pqty=(pqty+@pqty) WHERE pid LIKE '" + dgvOrder.Rows[e.RowIndex].Cells[3].Value.ToString() + "' ", con);
                    cmd.Parameters.AddWithValue("@pqty", Convert.ToInt16(dgvOrder.Rows[e.RowIndex].Cells[5].Value.ToString()));
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                LoadOrder(); // Sipariş listesini yeniden yükler.
            }
        }

        // "Ekle" butonuna tıklanınca OrderModuleForm'u açar.
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OrderModuleForm moduleForm = new OrderModuleForm();
            moduleForm.ShowDialog();
            LoadOrder(); // Sipariş listesini günceller.
        }

        // Label ve TextBox olayları - Şu anda işlevsiz.
        private void label2_Click(object sender, EventArgs e) { }
        private void txtSearch_TextChanged(object sender, EventArgs e) { }
    }
}
