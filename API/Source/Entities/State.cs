﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Source.Entities
{
    public class State
    {
        public string name { get; set; }

        public State(){}

        public State(string sName)
        {
            name = sName;
        }
    }
}