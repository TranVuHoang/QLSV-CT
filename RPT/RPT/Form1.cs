using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using adao_lib;

namespace RPT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            this.rp1.RefreshReport();
            rp1.Size = new Size(this.Size.Width, this.Size.Height);
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            this.rp1.ProcessingMode = ProcessingMode.Local;

            LocalReport localReport = this.rp1.LocalReport;

            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

            localReport.ReportPath = projectDirectory + "\\Report1\\Report1.rdl";


            StudentUpdate stu = new StudentUpdate();
            DataTable ds = stu.GetDataTable();


            Microsoft.Reporting.WinForms.ReportDataSource dataset = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", ds);
            this.rp1.LocalReport.DataSources.Clear();
            this.rp1.LocalReport.DataSources.Add(dataset);              
            this.rp1.LocalReport.Refresh();
            this.rp1.RefreshReport();
            this.rp1.SetDisplayMode(DisplayMode.PrintLayout);
            this.rp1.ZoomMode = ZoomMode.Percent;
            this.rp1.ZoomPercent = 100;
        }
    }
}
