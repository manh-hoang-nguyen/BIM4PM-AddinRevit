﻿using BIM4PM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.UI.Commun
{
   public class ModelVersionProvider
    {
        public static List<ModelVersion> ModelVersions { get; set; }
        public static ModelVersion CurrentModelVersion { get; set; }
    }
}
