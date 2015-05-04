﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reuben.UI.ProjectManagement
{

    public struct CompressionPoint
    {
        public int XPointer, YPointer, PagePointer;
        public bool EOD;

        public CompressionPoint(int x, int y, int p, bool e)
        {
            XPointer = x;
            YPointer = y;
            PagePointer = p;
            EOD = e;
        }
    }
}
