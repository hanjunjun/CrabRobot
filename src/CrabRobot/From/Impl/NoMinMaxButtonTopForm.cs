﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrabRobot.From.Impl
{
    public class NoMinMaxButtonTopForm:TopForm
    {
        public NoMinMaxButtonTopForm()
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }
    }
}
