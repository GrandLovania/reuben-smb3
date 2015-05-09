﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reuben.Model;
using Newtonsoft.Json;

namespace Reuben.Controllers
{
    public class ProjectController
    {
        public Project Project { get; private set; }

        public ProjectController()
        {
        }

        public void NewProject(string name)
        {
            Project = new Project();
            Project.Name = name;
        }

        public bool Load(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException();
            }

            Project = JsonConvert.DeserializeObject<Project>(File.ReadAllText(fileName));
            Project.GraphicsFile = Path.GetDirectoryName(fileName).Trim('\\') + @"\assets\graphics.chr";
            Project.PaletteFile = Path.GetDirectoryName(fileName).Trim('\\') + @"\assets\palettes.json";
            Project.LevelDataFile = Path.GetDirectoryName(fileName).Trim('\\') + @"\assets\levels.json";
            Project.WorldDataFile = Path.GetDirectoryName(fileName).Trim('\\') + @"\assets\worlds.json";
            Project.LevelsDirectory = Path.GetDirectoryName(fileName).Trim('\\') + @"\levels";
            Project.WorldsDirectory = Path.GetDirectoryName(fileName).Trim('\\') + @"\worlds";
            return Project != null;
        }

        public bool Save(string fileName)
        {
            try
            {
                Project.GraphicsFile = Path.GetDirectoryName(fileName).Trim('\\') + @"\assets\graphics.chr";
                Project.PaletteFile = Path.GetDirectoryName(fileName).Trim('\\') + @"\assets\palettes.json";
                Project.LevelDataFile = Path.GetDirectoryName(fileName).Trim('\\') + @"\assets\levels.json";
                Project.WorldDataFile = Path.GetDirectoryName(fileName).Trim('\\') + @"\assets\worlds.json";
                Project.LevelsDirectory = Path.GetDirectoryName(fileName).Trim('\\') + @"\levels";
                Project.WorldsDirectory = Path.GetDirectoryName(fileName).Trim('\\') + @"\worlds";
                File.WriteAllText(fileName, JsonConvert.SerializeObject(Project));
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}