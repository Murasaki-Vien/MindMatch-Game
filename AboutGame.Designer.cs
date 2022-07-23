
namespace Second_Project_attempt
{
    partial class AboutGame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxAboutGame = new System.Windows.Forms.PictureBox();
            this.btnOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAboutGame)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxAboutGame
            // 
            this.pictureBoxAboutGame.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxAboutGame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBoxAboutGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxAboutGame.Image = global::Second_Project_attempt.Properties.Resources.About_game_details__1_;
            this.pictureBoxAboutGame.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxAboutGame.Name = "pictureBoxAboutGame";
            this.pictureBoxAboutGame.Size = new System.Drawing.Size(370, 551);
            this.pictureBoxAboutGame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxAboutGame.TabIndex = 0;
            this.pictureBoxAboutGame.TabStop = false;
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.Transparent;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Location = new System.Drawing.Point(272, 516);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(86, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "I understand";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // AboutGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ClientSize = new System.Drawing.Size(370, 551);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.pictureBoxAboutGame);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AboutGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AboutGame";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAboutGame)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxAboutGame;
        private System.Windows.Forms.Button btnOk;
    }
}