using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CitiConnect
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();

            // Only the button to Report Issues is enabled 
            btnReportIssues.Enabled = true;
            btnEvents.Enabled = true;

        }

       

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnReportIssues_Click(object sender, EventArgs e)
        {
            // Opens the Report Issues window
            ReportIssuesForm reportForm = new ReportIssuesForm();
            reportForm.Show();

        }

        private void btnEvents_Click(object sender, EventArgs e)
        {
            Events eventForm = new Events();
            eventForm.Show();
        }

        private void btnServiceStatus_Click(object sender, EventArgs e)
        {
            Service serviceForm = new Service();
            serviceForm.Show();
        }
    }
}
