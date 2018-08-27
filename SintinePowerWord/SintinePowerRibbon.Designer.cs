namespace SintinePowerWord
{
    partial class SintinePowerRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public SintinePowerRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tab1 = this.Factory.CreateRibbonTab();
            this.gbSintine = this.Factory.CreateRibbonGroup();
            this.btnStartSintinePower = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.gbSintine.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.gbSintine);
            this.tab1.Label = "TabAddIns";
            this.tab1.Name = "tab1";
            // 
            // gbSintine
            // 
            this.gbSintine.Items.Add(this.btnStartSintinePower);
            this.gbSintine.Label = "Sintine Power";
            this.gbSintine.Name = "gbSintine";
            // 
            // btnStartSintinePower
            // 
            this.btnStartSintinePower.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnStartSintinePower.Image = global::SintinePowerWord.Properties.Resources.Other_office_icon;
            this.btnStartSintinePower.Label = "Start SintinePower";
            this.btnStartSintinePower.Name = "btnStartSintinePower";
            this.btnStartSintinePower.ShowImage = true;
            this.btnStartSintinePower.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnStartSintinePower_Click);
            // 
            // SintinePowerRibbon
            // 
            this.Name = "SintinePowerRibbon";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.SintinePowerRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.gbSintine.ResumeLayout(false);
            this.gbSintine.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup gbSintine;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnStartSintinePower;
    }

    partial class ThisRibbonCollection
    {
        internal SintinePowerRibbon SintinePowerRibbon
        {
            get { return this.GetRibbon<SintinePowerRibbon>(); }
        }
    }
}
