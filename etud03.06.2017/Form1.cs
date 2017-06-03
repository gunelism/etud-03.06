using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace etud03._06._2017
{
    public partial class Form1 : Form
    {
        public int ID;
        public string NamePerson;
        public string SurName;
        public string Phone;

        database db = new database();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("name", txtName.Text);
            data.Add("surname", txtSurname.Text);
            data.Add("phone", txtPhone.Text);


            db.insert("guest", data);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'hotel2DataSet.guest' table. You can move, or remove it, as needed.
            this.guestTableAdapter.Fill(this.hotel2DataSet.guest);

        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            NamePerson = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            SurName = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            Phone = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("id", ID.ToString());
            db.delete("guest", data);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            txtName.Text = NamePerson;
            txtSurname.Text = SurName;
            txtPhone.Text = Phone;       


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("name", txtName.Text);
            data.Add("surname", txtSurname.Text);
            data.Add("phone", txtPhone.Text);
            Dictionary<string, string> where = new Dictionary<string, string>();
            where.Add("id", ID.ToString());
            db.update("guest", data, where);
            Form1_Load(this, null);
        }
    }
}
