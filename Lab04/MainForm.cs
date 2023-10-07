using System.Windows.Forms;

namespace Lab04
{
    public partial class MainForm : Form
    {
        private ThreadWorker[] threadWorkers;

        public MainForm()
        {
            Size = new Size(1218, 600);
            Text = "Головна форма";

            threadWorkers = new ThreadWorker[4];

            for (int i = 0; i < threadWorkers.Length; i++)
            {
                threadWorkers[i] = new ThreadWorker(i, this);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            foreach (ThreadWorker worker in threadWorkers)
            {
                worker.Stop();
            }
        }
    }
}