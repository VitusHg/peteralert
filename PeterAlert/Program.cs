using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace PeterAlert
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new PeterAlertForm());
        }
    }

    public class PeterAlertForm : Form
    {
        public PeterAlertForm()
        {
            Text = "Peter Alert";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(300, 200);
            BackColor = SystemColors.Control;

            var pictureBox = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(100, 100),
                Location = new Point((ClientSize.Width - 100) / 2, 20),
                BackColor = Color.Transparent
            };

            // Load embedded peter.png if available, otherwise show placeholder
            var asm = Assembly.GetExecutingAssembly();
            var resourceName = asm.GetName().Name + ".peter.png";
            using var stream = asm.GetManifestResourceStream(resourceName);
            if (stream != null)
                pictureBox.Image = Image.FromStream(stream);

            var okButton = new Button
            {
                Text = "OK",
                Size = new Size(120, 30),
                Location = new Point((ClientSize.Width - 120) / 2, 145),
                DialogResult = DialogResult.OK
            };
            okButton.Click += (s, e) => Close();

            Controls.Add(pictureBox);
            Controls.Add(okButton);
            AcceptButton = okButton;
        }
    }
}
