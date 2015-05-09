﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using Reuben.Model;
using Reuben.Controllers;

namespace Reuben.UI
{
    public partial class LevelEditor : Form
    {
        public LevelEditor()
        {
            InitializeComponent();
        }

        private Level level;
        private LevelInfo levelInfo;
        private LevelController levels;
        private GraphicsController graphics;

        public void LoadLevel(LevelInfo info, LevelController levelController, GraphicsController graphicsController)
        {
            levels = levelController;
            graphics = graphicsController;

            levelInfo = info;
            levelViewer.Level = level = levels.LoadLevel(levelInfo);
            levelViewer.LevelType = levels.LevelData.Types[level.LevelType];
            
            paletteList.ColorReference = levelViewer.ColorReference = graphics.GraphicsData.Colors;

            levelViewer.Palette = graphics.GraphicsData.Palettes.Where(p => p.ID == level.PaletteID).FirstOrDefault();
            levelViewer.Graphics = graphics.MakePatternTable(new List<int>() { level.GraphicsID, level.GraphicsID + 1, 0xD0, 0xD1 });
            levelViewer.UpdateArea(0, 0, 16 * 15, 0x1B);

            lvlHost.Width = level.NumberOfScreens * 16 * 16;

            paletteList.Palettes = graphics.GraphicsData.Palettes;
            paletteList.SelectedPalette = graphics.GraphicsData.Palettes.Where(p => p.ID == level.PaletteID).FirstOrDefault();

            UpdatePalette();
        }

        public void UpdatePalette()
        {
            levelViewer.Palette = graphics.GraphicsData.Palettes.Where(p => p.ID == level.PaletteID).FirstOrDefault();
            levelViewer.UpdateArea(0, 0, 16 * 15, 0x1B);
        }

        private void paletteList_SelectedIndexChanged(object sender, EventArgs e)
        {
            level.PaletteID = paletteList.SelectedPalette.ID;
            UpdatePalette();
        }
    }
}