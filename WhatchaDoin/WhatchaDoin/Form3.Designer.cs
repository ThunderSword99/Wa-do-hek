namespace WhatchaDoin
{
    partial class frmDetails
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.chartMain = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button1 = new System.Windows.Forms.Button();
            this.radarChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radarChart)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(354, 319);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "History";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(6, 17);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(342, 295);
            this.textBox1.TabIndex = 2;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(1145, 12);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 2;
            // 
            // chartMain
            // 
            this.chartMain.BackColor = System.Drawing.Color.Gainsboro;
            chartArea1.Area3DStyle.Enable3D = true;
            chartArea1.Area3DStyle.PointDepth = 0;
            chartArea1.Name = "ChartArea1";
            this.chartMain.ChartAreas.Add(chartArea1);
            legend1.AutoFitMinFontSize = 6;
            legend1.ItemColumnSpacing = 1;
            legend1.MaximumAutoSize = 20F;
            legend1.Name = "Legend1";
            this.chartMain.Legends.Add(legend1);
            this.chartMain.Location = new System.Drawing.Point(372, 38);
            this.chartMain.Name = "chartMain";
            this.chartMain.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.Chartreuse;
            series1.CustomProperties = "ShowMarkerLines=True";
            series1.Legend = "Legend1";
            series1.MarkerBorderColor = System.Drawing.Color.Chartreuse;
            series1.MarkerBorderWidth = 2;
            series1.MarkerColor = System.Drawing.Color.White;
            series1.MarkerSize = 8;
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series1.Name = "Completing";
            dataPoint1.MarkerColor = System.Drawing.Color.Lime;
            dataPoint1.MarkerSize = 3;
            dataPoint1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Star5;
            series1.Points.Add(dataPoint1);
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.Red;
            series2.Legend = "Legend1";
            series2.MarkerBorderColor = System.Drawing.Color.Red;
            series2.MarkerBorderWidth = 2;
            series2.MarkerColor = System.Drawing.Color.White;
            series2.MarkerSize = 8;
            series2.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series2.Name = "Uncompleting";
            this.chartMain.Series.Add(series1);
            this.chartMain.Series.Add(series2);
            this.chartMain.Size = new System.Drawing.Size(982, 662);
            this.chartMain.TabIndex = 3;
            title1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            title1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.HorizontalCenter;
            title1.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Center;
            title1.BackImageTransparentColor = System.Drawing.Color.Snow;
            title1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            title1.Font = new System.Drawing.Font("Rosewood Std Regular", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            title1.Name = "Title1";
            title1.Text = "Details";
            this.chartMain.Titles.Add(title1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1046, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // radarChart
            // 
            this.radarChart.BackColor = System.Drawing.SystemColors.ControlLight;
            chartArea2.Area3DStyle.Enable3D = true;
            chartArea2.BackColor = System.Drawing.Color.Azure;
            chartArea2.BackImageTransparentColor = System.Drawing.Color.White;
            chartArea2.BorderColor = System.Drawing.Color.White;
            chartArea2.BorderWidth = 5;
            chartArea2.Name = "ChartArea1";
            this.radarChart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.radarChart.Legends.Add(legend2);
            this.radarChart.Location = new System.Drawing.Point(12, 346);
            this.radarChart.Name = "radarChart";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Radar;
            series3.Legend = "Legend1";
            series3.Name = "Test";
            this.radarChart.Series.Add(series3);
            this.radarChart.Size = new System.Drawing.Size(354, 354);
            this.radarChart.TabIndex = 5;
            this.radarChart.Click += new System.EventHandler(this.Chart1_Click);
            // 
            // frmDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.ClientSize = new System.Drawing.Size(1366, 717);
            this.Controls.Add(this.radarChart);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chartMain);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmDetails";
            this.Text = "Details";
            this.Load += new System.EventHandler(this.HistoryForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radarChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMain;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataVisualization.Charting.Chart radarChart;
    }
}