﻿namespace Reuben.UI
{
    partial class BlockSelector
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
            this.blocks = new Reuben.UI.Controls.BlockListViewer();
            this.SuspendLayout();
            // 
            // blocks
            // 
            this.blocks.Location = new System.Drawing.Point(0, 0);
            this.blocks.Name = "blocks";
            this.blocks.ShowInteractionOverlays = false;
            this.blocks.ShowSolidityOverlays = false;
            this.blocks.Size = new System.Drawing.Size(256, 256);
            this.blocks.TabIndex = 0;
            this.blocks.Text = "blocksViewer1";
            this.blocks.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.blocks_MouseDoubleClick);
            this.blocks.MouseDown += new System.Windows.Forms.MouseEventHandler(this.blocks_MouseDown);
            // 
            // BlockSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.blocks);
            this.Name = "BlockSelector";
            this.Size = new System.Drawing.Size(256, 256);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.BlockListViewer blocks;
    }
}