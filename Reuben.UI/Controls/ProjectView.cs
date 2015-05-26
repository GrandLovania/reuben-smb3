﻿using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Reuben.Model;
using Reuben.Controllers;

namespace Reuben.UI
{
    public partial class ProjectView : UserControl
    {
        static ProjectController projectController;
        static GraphicsController graphicsController;
        static LevelController levelController;
        static StringController stringController;
        static SpriteController spriteController;

        public ProjectView()
        {
            InitializeComponent();
        }

        public void SetProjectController(ProjectController controller)
        {
            projectController = controller;

            graphicsController = new GraphicsController();
            graphicsController.LoadGraphics(controller.Project.GraphicsFile);
            graphicsController.LoadExtraGraphics(controller.Project.ExtraGraphicsFile);
            graphicsController.LoadPalettes(controller.Project.PaletteFile);

            levelController = new LevelController();
            levelController.Load(controller.Project.LevelDataFile);

            stringController = new StringController();
            stringController.Load(controller.Project.StringDataFile);

            spriteController = new SpriteController();
            spriteController.Load(controller.Project.SpriteDataFile);

            savebutton.Enabled =
            palettesButton.Enabled =
            blocksButton.Enabled =
            spritesButton.Enabled =
            asmButton.Enabled = 
            textButton.Enabled = 
            defaultsButton.Enabled =
            projectName.Enabled = true;

            projectName.Text = controller.Project.Name;
        }

        public void RefreshTreeView()
        {
            projectTree.BeginUpdate();
            projectTree.Nodes.Clear();
            foreach (var n in projectController.Project.Structure.Nodes)
            {
                AddNode(projectTree.Nodes, n);
            }
            projectTree.EndUpdate();
        }

        public void AddNode(TreeNodeCollection nodes, ProjectNode currentNode)
        {
            var newNode = new TreeNode() { Text = currentNode.Name, Tag = currentNode };
            nodes.Add(newNode);
            foreach (var n in currentNode.Nodes)
            {
                AddNode(newNode.Nodes, n);
            }
        }

        ProjectController mainProject = new ProjectController();
        private void OpenFile()
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "JSON File (*.json)|*json|All Files|*";
            if (openDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!mainProject.Load(openDialog.FileName))
                {
                    MessageBox.Show("Unable to load " + openDialog.FileName + ". Please verify the file is a proper Reuben project file.");
                }
                else
                {
                    SetProjectController(mainProject);
                    RefreshTreeView();
                }
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PaletteManager mgr = new PaletteManager();
            mgr.Initialize(graphicsController, levelController);
            mgr.ShowDialog();
        }

        private void projectTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(projectTree.SelectedNode != null && ((ProjectNode)projectTree.SelectedNode.Tag).Type == NodeType.Level)
            {
                projectTree.ContextMenuStrip = levelContext;
            }
            else
            {
                projectTree.ContextMenuStrip = worldContext;
            }
        }

        private void openLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LevelInfo levelInfo = levelController.LevelData.Levels.Where(l => l.ID == ((ProjectNode) projectTree.SelectedNode.Tag).ID).FirstOrDefault();
            if (levelInfo != null)
            {
                levelInfo.File = projectController.Project.LevelsDirectory + "\\" + levelInfo.Name + ".json";
                LevelEditor editor = new LevelEditor();
                editor.Show();
                editor.LoadLevel(levelInfo, levelController, graphicsController, stringController, spriteController);
            }
        }

        private void textButton_Click(object sender, EventArgs e)
        {
            StringManager mgr = new StringManager();
            mgr.Show();
            mgr.SetResources(stringController);
        }

        private void blocksButton_Click(object sender, EventArgs e)
        {
            ProjectView.ShowBlockEditor();
        }

        private static BlockEditor BlockEditor;
        public static void ShowBlockEditor()
        {
            ShowBlockEditor(-1, -1);
        }

        public static void ShowBlockEditor(int levelType, int blockSelected)
        {
            if (ProjectView.BlockEditor == null)
            {
                ProjectView.BlockEditor = new BlockEditor();
                ProjectView.BlockEditor.Initialize(projectController, levelController, graphicsController, stringController);
                ProjectView.BlockEditor.Show();
                ProjectView.BlockEditor.FormClosing += BlockEditor_FormClosing;
            }
            else
            {
                ProjectView.BlockEditor.Focus();
            }

            if(levelType >= 0 && blockSelected >= 0)
            {
                ProjectView.BlockEditor.SelectBlock(levelType, blockSelected);
            }
        }

        static void BlockEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ProjectView.BlockEditor.DialogResult == DialogResult.OK)
            {
                levelController.LevelData.Types = ProjectView.BlockEditor.LocalLevelTypes;
                levelController.LevelData.Overlays = ProjectView.BlockEditor.Overlays;
                levelController.Save();
            }
            ProjectView.BlockEditor.FormClosing -= BlockEditor_FormClosing;
        }

        private void asmButton_Click(object sender, EventArgs e)
        {
            if (Main.ASMEditor == null)
            {
                Main.ASMEditor = new ASMEditor();
                Main.ASMEditor.Initialize(projectController);
                Main.ASMEditor.Show();
            }
            else
            {
                Main.ASMEditor.Focus();
            }
        }

        private void spritesButton_Click(object sender, EventArgs e)
        {
            ShowSpriteEditor();
        }

        private static SpriteEditor SpriteEditor;        
        public void ShowSpriteEditor(int spriteId = -1)
        {
            if (ProjectView.SpriteEditor == null)
            {
                ProjectView.SpriteEditor = new SpriteEditor();
                ProjectView.SpriteEditor.Initialize(graphicsController, spriteController, levelController);
                ProjectView.SpriteEditor.Show();
            }
            else
            {
                ProjectView.SpriteEditor.Focus();
            }
        }
    }
}
