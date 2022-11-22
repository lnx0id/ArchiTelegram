namespace BeholderArchi
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.Console = new System.Windows.Forms.TextBox();
            this.SeeLogsLable = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.UpdateListButton = new System.Windows.Forms.Button();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Console
            // 
            this.Console.Font = new System.Drawing.Font("White Rabbit", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Console.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.Console.Location = new System.Drawing.Point(12, 413);
            this.Console.Name = "Console";
            this.Console.ReadOnly = true;
            this.Console.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.Console.Size = new System.Drawing.Size(776, 21);
            this.Console.TabIndex = 0;
            this.Console.Click += new System.EventHandler(this.Console_Click);
            // 
            // SeeLogsLable
            // 
            this.SeeLogsLable.AutoSize = true;
            this.SeeLogsLable.Font = new System.Drawing.Font("White Rabbit", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SeeLogsLable.Location = new System.Drawing.Point(14, 387);
            this.SeeLogsLable.Name = "SeeLogsLable";
            this.SeeLogsLable.Size = new System.Drawing.Size(203, 11);
            this.SeeLogsLable.TabIndex = 1;
            this.SeeLogsLable.Text = "Click to see full log:";
            this.SeeLogsLable.Click += new System.EventHandler(this.SeeLogsLable_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(379, 366);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(409, 44);
            this.label1.TabIndex = 2;
            this.label1.Text = "В log.txt не появяться новые логи пока он открыт в блокноте. \r\nЗакройте его и отк" +
    "ройте сного:)";
            // 
            // UpdateListButton
            // 
            this.UpdateListButton.Location = new System.Drawing.Point(14, 45);
            this.UpdateListButton.Name = "UpdateListButton";
            this.UpdateListButton.Size = new System.Drawing.Size(164, 29);
            this.UpdateListButton.TabIndex = 4;
            this.UpdateListButton.Text = "Показать все чат id";
            this.UpdateListButton.UseVisualStyleBackColor = true;
            this.UpdateListButton.Click += new System.EventHandler(this.UpdateListButton_Click);
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(14, 12);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(264, 27);
            this.idTextBox.TabIndex = 5;
            this.idTextBox.Text = "Введите чат id";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(284, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 29);
            this.button1.TabIndex = 6;
            this.button1.Text = "Сообщение ->";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.idTextBox);
            this.Controls.Add(this.UpdateListButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SeeLogsLable);
            this.Controls.Add(this.Console);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "Beholder";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Leave += new System.EventHandler(this.FormMain_Leave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox Console;
        private Label SeeLogsLable;
        private Label label1;
        private Button UpdateListButton;
        private TextBox idTextBox;
        private Button button1;
    }
}