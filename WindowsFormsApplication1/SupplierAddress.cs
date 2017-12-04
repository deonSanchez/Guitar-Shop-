﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarShop
{
    /// <summary>
    /// Data structure for Supplier Addresses
    /// </summary>
    public class SupplierAddress
    {
        public int SupAddressID;
        public int SupplierID;
        public string Line1;
        public string Line2;
        public string City;
        public string State;
        public int ZipCode;
    }
}
