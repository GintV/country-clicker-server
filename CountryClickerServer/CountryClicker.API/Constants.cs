﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryClicker.API
{
    public static class Constants
    {
        public const string ApiBasePath = "Api";
        public const string ApiVerionString = "v1.0";
        public const string Id = "{id}";
        public const string PathSep = "/";
        public const string ParentId = "{parentId}";
        public static string SwaggerEndpoint = $"/swagger/{ApiVerionString}/swagger.json";
    }
}
