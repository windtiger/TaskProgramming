using System.Net;

namespace Part44
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int CalculateValue(int x, int y)
        {
            Thread.Sleep(5000);
            return x + y;
        }

        private Task<int> CalculateValueByTask(int x, int y)
        {
            return Task.Factory.StartNew(() =>
            {
                Thread.Sleep(5000);
                return x + y;
            });

        }

        private async Task<int> CalculateValueByAsync(int x, int y)
        {
            await Task.Delay(5000);
            return x + y;
        }



        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            var result = CalculateValue(1, 2);
            richTextBox1.Text = result.ToString();
            progressBar1.Visible = false;
        }

        private void BtnCalculateByTask_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            var calculation = CalculateValueByTask(3, 4);
            calculation.ContinueWith(t =>
            {
                var result = t.Result;
                richTextBox1.Text = result.ToString();
                progressBar1.Visible = false;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private async void BtnCalculateByAsync_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            var result = await CalculateValueByAsync(5, 6);
            richTextBox1.Text = result.ToString();
            progressBar1.Visible = false;
        }
    }
}