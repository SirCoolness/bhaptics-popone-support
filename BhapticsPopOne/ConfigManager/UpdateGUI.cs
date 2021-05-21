using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if PLATFORM_DESKTOP
using System.Windows.Forms;
#endif

namespace BhapticsPopOne.ConfigManager
{
    class UpdateGUI
    {
        public struct ConfigUpdateChoice
        {
            public bool Remember;
            public bool Backup;
        }

#if PLATFORM_DESKTOP
        public static ConfigUpdateChoice ShowConfigPrompt()
        {
            var form = new System.Windows.Forms.Form
            {
                Width = 240,
                Height = 200,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "bHaptics mod",
                StartPosition = FormStartPosition.CenterScreen
            };

            Label label = new Label
            {
                Text = "Config needs to be regenerated.\nWould you like to back it up first?",
                Top = 20,
                Left = 30,
                Width = 200,
                Height = 30
            };

            CheckBox checkBox = new CheckBox
            {
                Text = "remember my choice",
                Top = 50,
                Left = 30,
                Width = 160
            };

            Button confirmation = new Button
            {
                Text = "Overwrite",
                DialogResult = DialogResult.OK,
                Top = 80,
                Left = 30
            };
            confirmation.Click += (sender, e) => { form.Close(); };

            Button backup = new Button
            {
                Text = "Backup",
                DialogResult = DialogResult.Yes,
                Top = 110,
                Left = 30
            };
            confirmation.Click += (sender, e) => { form.Close(); };

            form.Controls.Add(label);
            form.Controls.Add(checkBox);
            form.Controls.Add(backup);
            form.Controls.Add(confirmation);

            form.AcceptButton = confirmation;
            form.CancelButton = backup;

            var res = form.ShowDialog();

            return new ConfigUpdateChoice
            {
                Backup = res == DialogResult.Yes,
                Remember = checkBox.Checked
            };
        }
#elif PLATFORM_ANDROID
        public static ConfigUpdateChoice ShowConfigPrompt()
        {
            return new ConfigUpdateChoice
            {
                Backup = true,
                Remember = true
            };
        }
#endif
    }
}
