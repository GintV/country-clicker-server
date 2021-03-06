﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CountryClicker.Domain
{
    public class CustomGroup : Group, IParentableEntity<Guid>
    {
        // Table columns
        public Guid CreatedById { get; set; }

        // Navigation properties
        [ForeignKey(nameof(CreatedById))]
        public Player CreatedBy { get; set; }

        // Interface realization
        public Guid ParentId(string parentEntityName) => CreatedById;
    }
}
