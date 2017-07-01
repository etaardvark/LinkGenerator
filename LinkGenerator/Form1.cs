using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace LinkGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.CheckPathExists = true;
            fileDialog.Title = "Select Path to Generate URLS";
            fileDialog.InitialDirectory = txtSearchPath.Text;
            fileDialog.Multiselect = false;
            DialogResult res = fileDialog.ShowDialog();
            if(res == DialogResult.OK)
            {
                txtSearchPath.Text = System.IO.Path.GetDirectoryName(fileDialog.FileName);
            }

            fileDialog.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DirectoryInfo info = new DirectoryInfo(txtSearchPath.Text);
            FileInfo[] files = info.GetFiles("*.jpg", SearchOption.TopDirectoryOnly);
            List<FileUrl> infoFiles = new List<FileUrl>();
            foreach(FileInfo f in files)
            {
                FileUrl fu = new FileUrl();
                fu.FileName = f.FullName;
                fu.URL = f.FullName;

                fu.URL = Regex.Replace(fu.URL,@"\\\\PM-QNAP\\Media\\Models\\", @"",RegexOptions.IgnoreCase);
                fu.URL = @"http://shdgfx.duckdns.org/" + HttpUtility.HtmlEncode(fu.URL);
                

                infoFiles.Add(fu);

            }

            dataGridView1.DataSource = infoFiles;
    

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string rowData = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            Clipboard.SetText(rowData);
        }
    }
}
