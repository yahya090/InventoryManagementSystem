﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace InventoryManagementSystem
{
    public partial class UserForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kalka\OneDrive\Documents\dbLMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public UserForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            LoadUser();
        }

        public void LoadUser()
        {
            int i = 0;
            dgvUser.Rows.Clear();
            cmd = new SqlCommand("SELECT * FROM tbUser", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvUser.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
            }
            dr.Close();
            con.Close();

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            UserModuleForm userModule = new UserModuleForm();
            userModule.btnSave.Enabled = true;
            userModule.btnClear.Enabled = false;
            userModule.ShowDialog();
            LoadUser();

        }

        private void dgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvUser.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                UserModuleForm userModule = new UserModuleForm();
                userModule.StartPosition = FormStartPosition.CenterScreen;


                userModule.txtUserName.Text = dgvUser.Rows[e.RowIndex].Cells[1].Value.ToString();
                userModule.txtFullName.Text = dgvUser.Rows[e.RowIndex].Cells[2].Value.ToString();
                userModule.txtPassword.Text = dgvUser.Rows[e.RowIndex].Cells[3].Value.ToString();
                userModule.txtPhone.Text = dgvUser.Rows[e.RowIndex].Cells[4].Value.ToString();

                userModule.btnSave.Enabled = false;
                userModule.btnUpdate.Enabled = true;
                userModule.txtUserName.Enabled = false;
                userModule.ShowDialog();


            }

            else if (colName == "Delete")
            {
                if (MessageBox.Show("Bu kullanıcıyı silmek istediğinizden emin misiniz?", "Kaydı Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    
                    {
                        con.Open();
                        cmd = new SqlCommand("DELETE FROM tbUser WHERE username LIKE '" + dgvUser.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Kayıt başarıyla silindi!");

                      
                    }
                }
                LoadUser();
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
